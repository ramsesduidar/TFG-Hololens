using S7.Net.Types;
using S7.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class Atributo
{
    public string Nombre;

    public DataType Tipo_Dato;

    public VarType Tipo_Variable;

    public int Direccion_Tipo_Dato;

    public byte BitAdr;

    public int Cantidad;

    public int StartByteAdr;

    // En TIA, un atributo en el offset 0.1 sería StartByteAdr = 0, BitAdr = 1.

    private bool leerVariable = false;

    public DataItem dataItem;

    public Atributo(string nombre, DataType tipo, VarType variable, int db, int start, byte bitadr = 0, int count = 1)
    {
        this.Nombre = nombre;

        this.Tipo_Dato = tipo;
        this.Tipo_Variable = variable;
        this.Direccion_Tipo_Dato = db;
        this.StartByteAdr = start;
        this.BitAdr = bitadr;
        this.Cantidad = count;

        

        this.dataItem = new DataItem()
        {
            DataType = this.Tipo_Dato,
            VarType = this.Tipo_Variable,
            DB = this.Direccion_Tipo_Dato,
            BitAdr = this.BitAdr,
            Count = this.Cantidad,
            StartByteAdr = this.StartByteAdr,
            Value = new object()
    };
    }

    public object getValor()
    {
        return this.dataItem.Value;
    }

    
    public static Atributo FromDTO(AtributoDTO dto)
    {
        Atributo atributo = new Atributo(dto.Nombre, dto.Tipo_Dato, dto.Tipo_Variable, dto.Direccion_Tipo_Dato, dto.StartByteAdr, dto.BitAdr, dto.Cantidad);

        return atributo;
    }

    public void setLeerVariable(bool v)
    {
        this.leerVariable = v;
    }

    public bool getLeerVariable()
    {
        return this.leerVariable; ;
    }
}
