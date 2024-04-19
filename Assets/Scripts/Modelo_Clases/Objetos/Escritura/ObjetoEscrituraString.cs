using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjetoEscrituraString : ObjetoEscritura
{
    public string valor;
    public override void EscribirValor()
    {
        Task.Run( () =>
        {
            try
            {
                this.getEscena().EscribirVariable(this.nombreVariable, valor);
                Debug.Log("Escritura task run realizada");
            }
            catch (Exception ex)
            {
                Debug.Log("Escritura task run realizada con errores: " + ex.Message);
            }
        }); 
    }

}
