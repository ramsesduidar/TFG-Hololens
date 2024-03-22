using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aviso : ObjetoLectura
{
    public string aviso = "Aviso!!!";
    public string comparar_con;

    override protected string GenerarCadena(string entrada)
    {
        string salida = "";

        if (entrada.ToLower() == comparar_con.ToLower())
        {
            salida = aviso;
        }

        return salida;

    }
}
