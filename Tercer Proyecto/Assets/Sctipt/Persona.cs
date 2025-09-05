using UnityEngine;
using System;



    [Serializable]
    public class persona
    {
        public string nombrep;
        public string correo;
        public string direccion;



        public persona(string nombrep, string correo, string direccion)
        {
            this.nombrep = nombrep;
            this.correo = correo;
            this.direccion = direccion;
        }
        public string nombreP { get => nombrep; set => nombrep = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Direccion { get => direccion; set => direccion = value; }





    }