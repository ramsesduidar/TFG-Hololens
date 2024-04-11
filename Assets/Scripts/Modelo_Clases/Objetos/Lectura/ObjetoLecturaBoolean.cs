using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoLecturaBoolean : ObjetoLectura
{
    public string v_true;
    public string v_false;
    
    protected override string GenerarCadena(string entrada)
    {
        bool valor = Convert.ToBoolean(entrada);
        if (valor)
        {
            return v_true;
        }
        else { 
            return v_false;
        }
        

    }
}
