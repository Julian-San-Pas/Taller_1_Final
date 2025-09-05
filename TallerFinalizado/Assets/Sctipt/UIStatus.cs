using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;        
using System.Linq;
using System.Reflection;

public class UIStatus : MonoBehaviour
{
    [Header("Referencias de Sistema")]
    public MonoBehaviour bancoSimulacion;   
    public Cajero[] cajeros;                 

    [Header("UI Cola")]
    public TMP_Text TxtCola;

    [Header("UI Cajeros")]
    public Image ImgEstado1; public TMP_Text TxtAtendio1; public TMP_Text TxtTiempo1;
    public Image ImgEstado2; public TMP_Text TxtAtendio2; public TMP_Text TxtTiempo2;
    public Image ImgEstado3; public TMP_Text TxtAtendio3; public TMP_Text TxtTiempo3;
    public Image ImgEstado4; public TMP_Text TxtAtendio4; public TMP_Text TxtTiempo4;

    [Header("Sprites de Estado")]
    public Sprite spriteLibre;
    public Sprite spriteOcupado;

    [Header("Métricas Globales")]
    public TMP_Text TxtClientesAtendidosTotal;
    public TMP_Text TxtTiempoTotalGlobal;

    
    FieldInfo fiCola;
    FieldInfo fiCajeros;
    FieldInfo fiOcupado; 

    void Awake()
    {
        if (bancoSimulacion != null)
        {
            var t = bancoSimulacion.GetType();
            fiCola    = t.GetField("colaClientes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            fiCajeros = t.GetField("cajeros",      BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        
        fiOcupado = typeof(Cajero).GetField("ocupado", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }

    void Update()
    {
        
        if (fiCola != null && bancoSimulacion != null && TxtCola != null)
        {
            var colaObj = fiCola.GetValue(bancoSimulacion) as ICollection;
            int count = colaObj != null ? colaObj.Count : 0;
            TxtCola.text = $"En cola: {count}";
        }

        
        PintarCajero(0, ImgEstado1, TxtAtendio1, TxtTiempo1);
        PintarCajero(1, ImgEstado2, TxtAtendio2, TxtTiempo2);
        PintarCajero(2, ImgEstado3, TxtAtendio3, TxtTiempo3);
        PintarCajero(3, ImgEstado4, TxtAtendio4, TxtTiempo4);

        // aqui le metemos lo de las metricas
        if (TxtClientesAtendidosTotal != null || TxtTiempoTotalGlobal != null)
        {
            int totalAtendidos = 0;
            float totalTiempo  = 0f;
            if (cajeros != null)
            {
                foreach (var c in cajeros)
                {
                    if (c == null) continue;
                    totalAtendidos += c.clientesAtendidos;
                    totalTiempo    += c.tiempoTotal;
                }
            }
            if (TxtClientesAtendidosTotal != null) TxtClientesAtendidosTotal.text = $"Atendidos (total): {totalAtendidos}";
            if (TxtTiempoTotalGlobal != null)      TxtTiempoTotalGlobal.text      = $"Tiempo total: {totalTiempo:0.0}s";
        }
    }

    void PintarCajero(int idx, Image img, TMP_Text tAt, TMP_Text tTi)
    {
        if (cajeros == null || idx >= cajeros.Length) return;
        var c = cajeros[idx];
        if (c == null) return;

        
        bool ocupado = false;
        if (fiOcupado != null)
        {
            object val = fiOcupado.GetValue(c);
            if (val is bool b) ocupado = b;
        }

        if (img != null)
        {
            img.color = Color.white; 
            if (spriteLibre != null && spriteOcupado != null)
                img.sprite = ocupado ? spriteOcupado : spriteLibre;
        }

        if (tAt != null) tAt.text = $"Atendidos: {c.clientesAtendidos}";
        if (tTi != null) tTi.text = $"Tiempo: {c.tiempoTotal:0.0}s";
    }
}
