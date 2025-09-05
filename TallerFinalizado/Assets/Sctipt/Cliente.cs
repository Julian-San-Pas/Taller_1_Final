using UnityEngine;
using System;



    [Serializable]
    public class Cliente : persona
    {
        public string idCliente;
        public string tramite;
        public float tiempoAtencion;
        public Cliente(string nombrep, string correo, string direccion, string idCliente, string TipoTramite, float tiempoAtencion) : base(nombrep, correo, direccion)
        {
            this.idCliente = idCliente;
            this.tramite = TipoTramite;
            this.tiempoAtencion = tiempoAtencion;
        }

        public string IdCliente { get => idCliente; set => idCliente = value; }
        public string TipoTramite1 { get => tramite; set => tramite = value; }
        public float TiempoAtencion { get => tiempoAtencion; set => tiempoAtencion = value; }

    }
