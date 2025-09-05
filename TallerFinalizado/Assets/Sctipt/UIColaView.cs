using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UIColaView : MonoBehaviour
{
    [Header("Sistema")]
    public BancoSimulacion bancoSimulacion;   
    [Header("UI")]
    public Transform contentCola;             
    public GameObject prefabClienteItem;      

    
    private readonly List<GameObject> pool = new();
    private int ultimoConteo = -1;

    void OnEnable()  { InvokeRepeating(nameof(Sync), 0f, 0.25f); }
    void OnDisable() { CancelInvoke(nameof(Sync)); }

    void Sync()
    {
        if (bancoSimulacion == null || bancoSimulacion.colaClientes == null || contentCola == null || prefabClienteItem == null)
            return;

        var q = bancoSimulacion.colaClientes;
        int n = q.Count;
        if (n == ultimoConteo) return; 

        ultimoConteo = n;

        
        while (pool.Count < n)
            pool.Add(Instantiate(prefabClienteItem, contentCola));
        for (int i = 0; i < pool.Count; i++)
            pool[i].SetActive(i < n);

        
        int idx = 0;
        foreach (var cli in q.ToArray()) 
        {
            var go = pool[idx++];
            var item = go.GetComponent<UIClienteItem>();

           
            string id = SafeGet<string>(cli, "idCliente");
            string tramite = SafeGet<string>(cli, "tramite");
            float tiempo = SafeGet<float>(cli, "tiempoAtencion");

            
            string nombre = SafeGet<string>(cli, "nombre"); 

            if (item != null) item.Set(id, nombre, tramite, tiempo);
        }
    }

    
    T SafeGet<T>(object obj, string fieldName)
    {
        if (obj == null) return default;
        var f = obj.GetType().GetField(fieldName);
        if (f != null && f.FieldType == typeof(T)) return (T)f.GetValue(obj);

        
        var p = obj.GetType().GetProperty(fieldName);
        if (p != null && p.PropertyType == typeof(T)) return (T)p.GetValue(obj);

        return default;
    }
}
