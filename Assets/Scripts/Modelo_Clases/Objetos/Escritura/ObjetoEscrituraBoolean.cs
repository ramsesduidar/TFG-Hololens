using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjetoEscrituraBoolean : ObjetoEscritura
{
    // Valor a escribir
    public bool valor;

    // If true, escribir valor y luego negarlo
    public bool conmutar;

    public override void EscribirValor()
    {
        if (conmutar)
        {
            this.getEscena().EscribirVariable(this.nombreVariable, valor.ToString());
            valor = !valor;
        }
        else
        {
            Task.Run(() => {
                try
                {
                    this.getEscena().EscribirVariable(this.nombreVariable, valor.ToString());
                    this.getEscena().EscribirVariable(this.nombreVariable, (!valor).ToString());
                    Debug.Log("Escritura task run realizada");
                }
                catch (Exception ex)
                {
                    Debug.Log("Escritura task run realizada con errores: " + ex.Message);
                }
                
            });
            
        }
    }

}
