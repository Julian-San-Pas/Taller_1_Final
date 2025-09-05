using UnityEngine;

public class UISimulationButtons : MonoBehaviour
{

    public BancoSimulacion bancoSimulacion;

    public void Iniciar()
    {
        if (!bancoSimulacion.enabled) bancoSimulacion.enabled = true; 
        bancoSimulacion.Iniciar();                                     
    }

    public void Detener()
    {
        bancoSimulacion.Detener();                                     
        
        
    }
}
