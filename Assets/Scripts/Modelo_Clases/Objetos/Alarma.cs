using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alarma : ObjetoLectura
{

    public string alarma = "Alarma!!!";
    public string comparar_con;
    
    override protected string GenerarCadena(string entrada)
    {
        string salida = "";

        if(entrada.ToLower() == comparar_con.ToLower())
        {
            salida = alarma;
            //this.texto.renderer.enabled = true;
        }
        else
        {
            //this.texto.renderer.enabled = false;
        }

        return salida;

    }
}
