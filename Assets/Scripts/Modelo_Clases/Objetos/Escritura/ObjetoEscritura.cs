using Microsoft.MixedReality;
using MixedReality.Toolkit.UX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

// ObjetoEscritura.cs

public abstract class ObjetoEscritura : MonoBehaviour
{
    public string nombreVariable;
    public Escena_PLC escena;

    void Start()
    {
        if (escena == null)
            escena = GetComponentInParent<Escena_PLC>();

        PressableButton boton = GetComponent<PressableButton>();
        if (boton.OnClicked == null)
        {
            throw new Exception("Onclick no asignado para el boton " + boton.name);
            
        }
    }

    public Escena_PLC getEscena()
    {
        return this.escena;
    }

    public abstract void EscribirValor();

}
