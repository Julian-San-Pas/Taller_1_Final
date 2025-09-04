
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using packageCliente;
using System;
using System.IO;

public class QueueManager : MonoBehaviour
{
    public Queue<Cliente> colaClientes = new Queue<Cliente>(); // Shared queue of clients

    public Button botonIniciar; // Assign in Inspector
    public Button botonDetener; // Assign in Inspector
    public Transform contenedorCola; // Panel with VerticalLayoutGroup for queue visual
    public GameObject prefabClienteUI; // Prefab: A TextMeshProUGUI object for each client in UI

    private bool generandoClientes = false;
    private List<Cajero> cajeros = new List<Cajero>(); // List of 4 cashiers
    public int contadorConsignaciones = 0;

    // Your lists for random generation
    private string[] listaNombres = { "Juan", "María", "Pedro", "Ana", "Luis", "Pepe", "Mani", "Rosa", "Betty", "Jose" };
    private string[] listaCorreos = { "juan@gmail.com", "mari@uao.edu.co", "pe@hotmail.com", "an@yahoo.com", "lui@gmail.com.co", "pep@gmail.com", "mani@gmail.com", "Ros@gmail.com", "bet@gmail.com", "jos@gmail.com" };
    private string[] listaDirecciones = { "Calle 1", "Carrera 2", "Avenida 3", "Calle 4", "Carrera 5" };
    private string[] listaTramites = { "Retirar", "Consignar" }; // Fixed capitalization

    void Start()
    {
        // Find the 4 Cajero scripts (make sure they are in the scene)
        Cajero[] cajerosArray = FindObjectsOfType<Cajero>();
        cajeros.AddRange(cajerosArray);

        // Setup buttons
        if (botonIniciar != null) botonIniciar.onClick.AddListener(IniciarGeneracion);
        if (botonDetener != null) botonDetener.onClick.AddListener(DetenerGeneracion);

        // Disable Detener at start
        if (botonDetener != null) botonDetener.interactable = false;
    }

    private void IniciarGeneracion()
    {
        if (!generandoClientes)
        {
            generandoClientes = true;
            InvokeRepeating("GenerarClientesAleatorios", 0f, 1f); // Every second
            if (botonIniciar != null) botonIniciar.interactable = false;
            if (botonDetener != null) botonDetener.interactable = true;
        }
    }

    private void DetenerGeneracion()
    {
        if (generandoClientes)
        {
            generandoClientes = false;
            CancelInvoke("GenerarClientesAleatorios");
            if (botonIniciar != null) botonIniciar.interactable = true;
            if (botonDetener != null) botonDetener.interactable = false;
            GenerarJSON(); // Generate JSON on stop
        }
    }

    private void GenerarClientesAleatorios()
    {
        int numClientes = UnityEngine.Random.Range(1, 4); // 1 to 3
        for (int i = 0; i < numClientes; i++)
        {
            int indexNomCor = UnityEngine.Random.Range(0, listaNombres.Length);
            string nombre = listaNombres[indexNomCor];
            string correo = listaCorreos[indexNomCor];
            string direccion = listaDirecciones[UnityEngine.Random.Range(0, listaDirecciones.Length)];
            string tramite = listaTramites[UnityEngine.Random.Range(0, listaTramites.Length)];
            string idCliente = UnityEngine.Random.Range(1000, 9999).ToString();
            float segundos = UnityEngine.Random.Range(2f, 5f); // As per recommendations: 2-5 seconds

            Cliente elCliente = new Cliente(idCliente, tramite, segundos, nombre, correo, direccion);
            colaClientes.Enqueue(elCliente);

            // Update visual
            ActualizarVisualCola();
        }
    }

    public Cliente DesencolarCliente()
    {
        if (colaClientes.Count > 0)
        {
            return colaClientes.Dequeue();
        }
        return null;
    }

    public void ActualizarVisualCola()
    {
        // Clear previous visual
        foreach (Transform child in contenedorCola)
        {
            Destroy(child.gameObject);
        }

        // Show each client in queue
        foreach (Cliente cliente in colaClientes)
        {
            GameObject item = Instantiate(prefabClienteUI, contenedorCola);
            TextMeshProUGUI texto = item.GetComponent<TextMeshProUGUI>();
            if (texto != null)
            {
                texto.text = $"{cliente.IdCliente} - {cliente.Tramite} ({cliente.AtencionT}s)";
            }
        }
    }

    private void GenerarJSON()
    {
        // Collect data
        var jsonData = new
        {
            clientesSinAtender = colaClientes.Count,
            cajerosData = cajeros.ConvertAll(c => new
            {
                clientesAtendidos = c.clientesAtendidos,
                tiempoTotal = c.tiempoTotal
            }),
            transaccionesConsignacion = contadorConsignaciones
        };

        // To JSON
        string json = JsonUtility.ToJson(jsonData, true);

        // Save to file
        string path = Application.persistentDataPath + "/estadisticas.json";
        File.WriteAllText(path, json);
        Debug.Log("JSON generado en: " + path);
    }
}