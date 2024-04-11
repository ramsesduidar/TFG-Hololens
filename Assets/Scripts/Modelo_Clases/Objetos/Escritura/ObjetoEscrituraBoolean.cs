using System.Collections;
using System.Collections.Generic;
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
            this.getEscena().EscribirVariable(this.nombreVariable, valor.ToString());
            this.getEscena().EscribirVariable(this.nombreVariable, (!valor).ToString());
        }
    }
}