using S7.Net;
using S7.Net.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Clase_PLC_Stub : Clase_PLC
{
    

    public Clase_PLC_Stub(string direccionIP, CpuType modelo, short rackCPU, short slotCPU) : base(direccionIP, modelo, rackCPU, slotCPU)
    {
       

    }

    public new void StartConnection()
    {
        
    }

    public new void EndConnection()
    {
       
    }

    public new void LeerVariables()
    {

       
    }

    public new void PedirVariable(string nombre)
    {
        this.Atributos[nombre].setLeerVariable(true);
    }

    public new object getVariable(string nombre)
    {
        if (this.Atributos[nombre].Tipo_Variable == VarType.Int) return "10";
        return "false";
    }

    public new void EscribirVariable(string variable, string valor)
    {
        
    }
}
