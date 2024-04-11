using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Alarma
{

    public string alarma = "Alarma!!!";
    public string descripcion;
    public string comparar_con;

    private string antes = "false";

    public string nombreVariable;
    public Escena_PLC escena;

    public event EventHandler<AlarmaEventArgs> AlarmaEncendida;
    public event EventHandler<AlarmaEventArgs> AlarmaApagada;

    public void Update()
    {
        // Actualizar el texto con el valor leído en cada frame
        //escena.PedirVariable(nombreVariable);
        string valor = this.LeerVariable(nombreVariable).ToString();

        if(antes != valor)
        {
            antes = valor;
            this.ComprobarAlarma(valor);
        }
        //Debug.Log("hemos leido: " + valor + " en: " + nombreVariable);
        

    }

    private object LeerVariable(string name)
    {
        escena.PedirVariable(nombreVariable);
        string valor = escena.LeerVariable(nombreVariable).ToString();

        return valor;
    }

    private void ComprobarAlarma(string entrada)
    {

        if (entrada.ToLower() == comparar_con.ToLower())
        {
            Debug.Log("ha saltado la alarma ");
            AlarmaEncendida?.Invoke(this, new AlarmaEventArgs(alarma, descripcion));
        }
        else
        {
            Debug.Log("se ha apagado la alarma ");
            AlarmaApagada?.Invoke(this, new AlarmaEventArgs(alarma, descripcion));
        }
    }

}

    