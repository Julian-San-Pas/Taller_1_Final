using System;
using UnityEngine;

namespace PackagePersona
{
    public class Persona
    {
        private string nombre;
        private string correo;
        private string direccion;

        public Persona() { }
        public Persona(string nombre, string correo, string direccion)
        {
            this.Nombre = nombre;
            this.Correo = correo;
            this.Direccion = direccion;
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Direccion { get => direccion; set => direccion = value; }
    }

}


//Lista

