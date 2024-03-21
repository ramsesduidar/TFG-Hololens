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
        this.getEscena().EscribirVariable(this.nombreVariable, valor.ToString());
        Debug.Log("Escribo: " + valor);
        if (conmutar) valor = !valor;
    }
}
