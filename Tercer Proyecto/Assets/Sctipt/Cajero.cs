using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using packageCliente;

public class Cajero : MonoBehaviour
{
    public TMP_Text estadoText; 
    public bool estaLibre = true;
    public int clientesAtendidos = 0;
    public List<float> tiemposIndividuales = new List<float>();
    public float tiempoTotal = 0f;

    private QueueManager queueManager; 

    void Start()
    {
        queueManager = FindObjectOfType<QueueManager>();
        ActualizarEstado();
    }

    void Update()
    {
        if (estaLibre && queueManager.colaClientes.Count > 0)
        {
            AtenderCliente();
        }
    }

    private void AtenderCliente()
    {
        if (queueManager.colaClientes.Count == 0) return;

        Cliente cliente = queueManager.DesencolarCliente();
        if (cliente != null)
        {
            estaLibre = false;
            ActualizarEstado();
            StartCoroutine(ProcesarAtencion(cliente));
        }
    }

    private IEnumerator ProcesarAtencion(Cliente cliente)
    {
        yield return new WaitForSeconds(cliente.AtencionT);

        clientesAtendidos++;
        tiemposIndividuales.Add(cliente.AtencionT);
        tiempoTotal += cliente.AtencionT;

        if (cliente.Tramite == "Consignar")
        {
            queueManager.contadorConsignaciones++;
        }

        estaLibre = true;
        ActualizarEstado();

        queueManager.ActualizarVisualCola();
    }

    private void ActualizarEstado()
    {
        if (estadoText != null)
        {
            estadoText.text = estaLibre ? "Libre" : "Ocupado";
            estadoText.color = estaLibre ? Color.green : Color.red;
        }
    }

    
    public void Equeue() { } 

    public void Dequeue()
    {
        // Adaptado, pero usamos el del manager
        if (queueManager.colaClientes.Count > 0)
        {
            // Llama a AtenderCliente en lugar
            AtenderCliente();
        }
        else if (texto != null)
        {
            texto.text = "No hay clientes en la fila.";
        }
    }

    public void MostrarClientes()
    {
        // Muestra la cola del manager
        queueManager.ActualizarVisualCola();
    }

    public void ClearQueue()
    {
        queueManager.colaClientes.Clear();
        if (texto != null)
        {
            texto.text = "La fila quedo limpia.";
        }
    }

    public void AddClient()
    {
        
        queueManager.GenerarClientesAleatorios(1); // Genera uno manual 
    }

    public void RemoveClient()
    {
        
    }

   
    public TMP_InputField txtCorreo; 
    public TextMeshProUGUI texto; 
    int delayCoroutine;

    public List<Cliente> clientes = new List<Cliente>(); // Mantengo, pero usamos cola principal

    
}