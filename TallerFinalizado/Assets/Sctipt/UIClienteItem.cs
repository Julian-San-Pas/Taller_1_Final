using UnityEngine;
using TMPro;

public class UIClienteItem : MonoBehaviour
{
    public TMP_Text TxtNombre;
    public TMP_Text TxtId;
    public TMP_Text TxtTramite;
    public TMP_Text TxtTiempo;

   
    public void Set(string id, string nombre, string tramite, float tiempo)
    {
        if (TxtNombre)  TxtNombre.text  = string.IsNullOrEmpty(nombre) ? "—" : nombre;
        if (TxtId)      TxtId.text      = $"ID: {id}";
        if (TxtTramite) TxtTramite.text = tramite;
        if (TxtTiempo)  TxtTiempo.text  = $"t: {tiempo:0.0}s";

        
        if (TxtTramite)
        {
            var c = tramite == "Consignar" ? new Color(0.20f,0.75f,0.25f) : new Color(0.95f,0.55f,0.15f);
            TxtTramite.color = c;
        }
    }
}
