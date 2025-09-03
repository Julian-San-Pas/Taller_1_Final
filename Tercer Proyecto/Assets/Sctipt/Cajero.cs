using packageCliente;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cajero : MonoBehaviour
{
    Queue<Cliente> clientes = new Queue<Cliente>();

    

    public TextMeshProUGUI texto;
    public bool atendiendo = false;



    public void Equeue(Cliente elCliente)
    {
        clientes.Enqueue(elCliente);
    }
    public Cliente Dequeue()
    {
        if (clientes.Count > 0)
        {
            Cliente elCliente = clientes.Dequeue();
            texto.text = "Atendiendo al cliente con ID: " + elCliente.IdCliente;
            return elCliente;
        }
        else
        {
            texto.text = "No hay clientes en la fila.";
            return null;
        }
    }
    public void MostrarClientes()
    {
        if (clientes.Count > 0)
        {
            texto.text = "Clientes en la fila: " + string.Join(", ", clientes);
        }
        else
        {
            texto.text = "No hay clientes en la fila.";
        }
    }
    public void ClearQueue()
    {
        clientes.Clear();
        texto.text = "La fila quedo limpia.";
    }
    public void ActivarCajero()
    {
        for (int i = 0; i < clientes.Count; i++)
        {
            StartCoroutine(Activar());
            texto.text = "Clientes en la fila: " + string.Join(", ", clientes);
        }   
        
    }
    public void DeactivarCajero()
    {
        StopCoroutine(Activar());
        atendiendo = false;
        texto.text = "Cajero desactivado.";
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Activar()
    {
        Cliente elCliente = Dequeue();
        if (elCliente != null)
        {
            atendiendo = true;
            yield return new WaitForSeconds(elCliente.AtencionT);
            atendiendo = false;
            texto.text = "El cliente con ID: " + elCliente.IdCliente + " ha sido atendido.";
        }

        else
        {
            texto.text = "No hay clientes para atender.";
        }
    }
}
