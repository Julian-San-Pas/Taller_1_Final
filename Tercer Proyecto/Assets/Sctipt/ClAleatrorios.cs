using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using packageCliente;
using PackagePersona;
using System.Collections;
using System;
public class ClAleatorios : MonoBehaviour {


    int delayCoroutine;
    [SerializeField] List<Cajero>  Cajeros = new List<Cajero>();

    public Cliente clienteAleatorio()
    {
        
        string[] listaNombres = { "Juan", "María", "Pedro", "Ana", "Luis", "Pepe", "Mani", "Rosa", "Betty", "Jose" };
        string[] listaCorreos = { "juan@gmail.com", "mari@uao.edu.co", "pe@hotmail.com", "an@yahoo.com", "lui@gmail.com.co", "pep@gmail.com", "mani@gmail.com", "Ros@gmail.com", "bet@gmail.com", "jos@gmail.com" };
        string[] listaDirecciones = { "Calle 1", "Carrera 2", "Avenida 3", "Calle 4", "Carrera 5" };
        string[] listaTramites = { "retirar", "consignar" };
        int idCliente = UnityEngine.Random.Range(1000, 9999);
        int segundos = UnityEngine.Random.Range(2, 6);

        int indexNomCor = UnityEngine.Random.Range(0, listaNombres.Length);

        string nombre = listaNombres[indexNomCor];
        string correo = listaCorreos[indexNomCor];
        string direccion = listaDirecciones[UnityEngine.Random.Range(0, listaDirecciones.Length)];
        string tramite = listaTramites[UnityEngine.Random.Range(0, listaTramites.Length)];
        Cliente elCliente = new Cliente(idCliente.ToString(), tramite, segundos, nombre, correo, direccion);
        return elCliente;
    }

    //==START==//
    void Start()
    {
        
        StartCoroutine(Generador());
    }
    void Activar()
    {
        StartCoroutine(Generador());
        for (int i = 0; i < Cajeros.Count; i++)
        {
            Cajeros[i].ActivarCajero();
        }
    }
    void Desactivar()
    {
        StopCoroutine(Generador());
        for (int i = 0; i < Cajeros.Count; i++)
        {
            Cajeros[i].DeactivarCajero();
        }
    }

    //==UPDATE==//
    void Update()
    {
        //Public clienteAleatorio();
    }
    IEnumerator Generador()
    {
        delayCoroutine = UnityEngine.Random.Range(1, 4);
        for (int i = 0; i < delayCoroutine; i++)
        {
            int indexCajero = UnityEngine.Random.Range(0, Cajeros.Count);
            Cajeros[indexCajero].Equeue(clienteAleatorio());
        }
        yield return new WaitForSeconds(1);
    }
    
}
