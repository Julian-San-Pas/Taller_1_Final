using System;
using UnityEngine;
using PackagePersona;

namespace packageCliente
{
    [Serializable]
    public class Cliente : Persona
    {
        private string idCliente;
        private string tramite;
        private float tiempoA; // Cambiado a float para segundos con decimales

        public Cliente() { }

        public Cliente(string idCliente, string tramite, float tiempoA, string nombre, string correo, string direccion)
            : base(nombre, correo, direccion)
        {
            this.idCliente = idCliente;
            this.tramite = tramite;
            this.tiempoA = tiempoA;
        }

        public string IdCliente { get => idCliente; set => idCliente = value; }
        public string Tramite { get => tramite; set => tramite = value; }
        public float AtencionT { get => tiempoA; set => tiempoA = value; }
    }
}
        /*
        public int generarID()
        {
            int numero = Random.Range(1000, 9999);
            idCliente = "C" + numero.ToString();
            Debug.Log("El ID del cliente es: " + idCliente);
            return numero;
        }
        public void tramiteCliente()
        {
            string[] modos = { "retirar", "consignar" };
            tramite = modos[Random.Range(0, modos.Length)];
            Debug.Log("El trámite elegido es: " + tramite);
        }
        public void tiempoAtencion()
        {
            if (tramite == "retirar")
            {
                tiempoA = Random.Range(1, 4);
            }
            else if (tramite == "consignar")
            {
                tiempoA = Random.Range(2, 6);
            }
            Debug.Log("El tiempo de atención es: " + tiempoA + " minutos.");
        }*/
        



    }
}
