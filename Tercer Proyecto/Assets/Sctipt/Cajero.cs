using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using packageCliente;
using PackagePersona;
using System.Collections;
using System;
public class Cajero : MonoBehaviour {

    Queue<int> cajero = new Queue<int>();

    public TMP_InputField txtCorreo;
    public TextMeshProUGUI texto;
    int delayCoroutine;

    public List<Cliente> clientes = new List<Cliente>();

    public void Equeue()
    {

    }
    public void Dequeue()
    {
        if (cajero.Count > 0)
        {
            int atendido = cajero.Dequeue();
            texto.text = "Atendiendo al cliente con ID: " + atendido;
        }
        else
        {
            texto.text = "No hay clientes en la fila.";
        }
    }
    public void MostrarClientes()
    {
        if (cajero.Count > 0)
        {
            texto.text = "Clientes en la fila: " + string.Join(", ", cajero);
        }
        else
        {
            texto.text = "No hay clientes en la fila.";
        }
    }
    public void ClearQueue()
    {
        cajero.Clear();
        texto.text = "La fila quedo limpia.";
    }
    public void AddClient()
    {
        /*if (int.TryParse(txtCorreo.text, out int clientId))
        {
            cajero.Enqueue(clientId);
            texto.text = "Cliente con ID: " + clientId + " añadido a la fila.";
            txtCorreo.text = "";
        }*/

        if (!int.IsNullOrEmpty(txtCorreo.text))
        {

            int idCliente = Generador().ToString();
            cajero.Enqueue(idCliente);
            texto.text = "Cliente con ID: " + idCliente + " añadido a la fila.";
            //txtCorreo.text = "";
        }
        else
        {
            texto.text = "Por favor, ingrese un ID de cliente válido.";
        }
    }
    public void RemoveClient() 
    { 
    }

    public void clienteAleatorio()
    {
        
        string[] listaNombres = { "Juan", "María", "Pedro", "Ana", "Luis", "Pepe", "Mani", "Rosa", "Betty", "Jose" };
        string[] listaCorreos = { "juan@gmail.com", "mari@uao.edu.co", "pe@hotmail.com", "an@yahoo.com", "lui@gmail.com.co", "pep@gmail.com", "mani@gmail.com", "Ros@gmail.com", "bet@gmail.com", "jos@gmail.com" };
        string[] listaDirecciones = { "Calle 1", "Carrera 2", "Avenida 3", "Calle 4", "Carrera 5" };
        string[] listaTramites = { "retirar", "consignar" };
        int idCliente = UnityEngine.Random.Range(1000, 9999);
        int segundos = UnityEngine.Random.Range(1, 6);

        int indexNomCor = UnityEngine.Random.Range(0, listaNombres.Length);

        string nombre = listaNombres[indexNomCor];
        string correo = listaCorreos[indexNomCor];
        string direccion = listaDirecciones[UnityEngine.Random.Range(0, listaDirecciones.Length)];
        string tramite = listaTramites[UnityEngine.Random.Range(0, listaTramites.Length)];
        Cliente elCliente = new Cliente(idCliente.ToString(), tramite, segundos, nombre, correo, direccion);
        clientes.Add(elCliente);
    }

    //==START==//
    void Start()
    {
        StartCoroutine(Generador());
    }

    //==UPDATE==//
    void Update()
    {
        //Public clienteAleatorio();
    }
    IEnumerator Generador()
    {
        delayCoroutine = UnityEngine.Random.Range(1, 4);
        clienteAleatorio();
        yield return new WaitForSeconds(delayCoroutine);
    }
}
