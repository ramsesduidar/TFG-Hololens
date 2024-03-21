using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoEscrituraString : ObjetoEscritura
{
    public string valor;
    public override void EscribirValor()
    {
        this.getEscena().EscribirVariable(this.nombreVariable, valor);
    }

}
