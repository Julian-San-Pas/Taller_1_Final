using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cajero : MonoBehaviour
{
    public int idCajero;

    public int clientesAtendidos = 0;
    public float tiempoTotal = 0f;
    public List<float> tiemposAtencion = new();

    private Queue<Cliente> colaCompartida;

    [Header("UI")]
    public Image estadoImagen;   // la imagen estandar del cajero
    public Sprite spriteLibre;   // cambiar la imagen a libre
    public Sprite spriteOcupado; // cambiar la imagen a ocupado

    private bool ocupado = false;

    public void Inicializar(int id, Queue<Cliente> cola)
    {
        idCajero = id;
        colaCompartida = cola;
        ActualizarEstadoVisual();
    }

    void Update()
    {
        if (!ocupado && colaCompartida.Count > 0)
        {
            Cliente cliente = colaCompartida.Dequeue();
            StartCoroutine(Atender(cliente));
        }
    }

    IEnumerator Atender(Cliente cliente)
    {
        ocupado = true;
        ActualizarEstadoVisual();

        clientesAtendidos++;
        tiemposAtencion.Add(cliente.tiempoAtencion);
        tiempoTotal += cliente.tiempoAtencion;

        Debug.Log($"Cajero {idCajero} atendiendo a {cliente.idCliente} ({cliente.tramite})");

        yield return new WaitForSecondsRealtime(cliente.tiempoAtencion);

        Debug.Log($"Cajero {idCajero} terminó con {cliente.idCliente}");

        ocupado = false;
        ActualizarEstadoVisual();
    }

    void ActualizarEstadoVisual()
    {
        if (estadoImagen != null)
        {
            estadoImagen.sprite = ocupado ? spriteOcupado : spriteLibre;
        }
    }
}