using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BancoSimulacion : MonoBehaviour
{
    public Queue<Cliente> colaClientes = new Queue<Cliente>();
    public Cajero[] cajeros;

    private Coroutine generadorCoroutine;
    private System.Random rnd = new System.Random();

    private int transaccionesConsignacion = 0;

    void Start()
    {
        // esto es para iniciar los cajeros 
        for (int i = 0; i < cajeros.Length; i++)
        {
            cajeros[i].Inicializar(i + 1, colaClientes);
        }
    }

    public void Iniciar()
    {
        if (generadorCoroutine == null)
        {
            generadorCoroutine = StartCoroutine(GenerarClientes());
            Debug.Log("▶ Simulación iniciada...");
        }
    }

    public void Detener()
    {
        if (generadorCoroutine != null)
        {
            StopCoroutine(generadorCoroutine);
            generadorCoroutine = null;
            Debug.Log("⏹ Simulación detenida.");
            GuardarResultadosJSON();
        }
    }

IEnumerator GenerarClientes()
{
    while (true)
    {
        int cantidad = rnd.Next(1, 4); 
        for (int i = 0; i < cantidad; i++)
        {
            string id = System.Guid.NewGuid().ToString().Substring(0, 5);
            string tramite = rnd.Next(2) == 0 ? "Retirar" : "Consignar";
            float tiempo = Random.Range(2f, 5f);

            Cliente cliente = new Cliente(
                "Cliente" + id,
                "cliente" + id + "@mail.com",
                "Calle Falsa 123",
                id,
                tramite,
                tiempo
            );

            colaClientes.Enqueue(cliente);

            if (tramite == "Consignar") transaccionesConsignacion++;

            Debug.Log($"+ Encolado {cliente.idCliente} ({cliente.tramite})");
        }

        
        yield return new WaitForSeconds(1f);

        
        yield return new WaitForSeconds(1f);
    }
}

    void GuardarResultadosJSON()
    {
        Resultados datos = new Resultados();
        datos.clientesEnCola = colaClientes.Count;
        datos.transaccionesConsignacion = transaccionesConsignacion;
        datos.cajeros = new List<CajeroData>();

        foreach (var cajero in cajeros)
        {
            CajeroData cd = new CajeroData();
            cd.idCajero = cajero.idCajero;
            cd.clientesAtendidos = cajero.clientesAtendidos;
            cd.tiempoTotal = cajero.tiempoTotal;
            datos.cajeros.Add(cd);
        }

        string json = JsonUtility.ToJson(datos, true);
        string path = Application.dataPath + "/resultados.json";
        File.WriteAllText(path, json);

        Debug.Log("📄 JSON guardado en: " + path);
    }
}

[System.Serializable]
public class Resultados
{
    public int clientesEnCola;
    public int transaccionesConsignacion;
    public List<CajeroData> cajeros;
}

[System.Serializable]
public class CajeroData
{
    public int idCajero;
    public int clientesAtendidos;
    public float tiempoTotal;
}