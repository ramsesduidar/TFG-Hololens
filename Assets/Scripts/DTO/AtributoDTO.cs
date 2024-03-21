using S7.Net.Types;
using S7.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AtributoDTO
{
    public string Nombre;

    public DataType Tipo_Dato;

    public VarType Tipo_Variable;

    public int Direccion_Tipo_Dato;

    public byte BitAdr;

    public int Cantidad;

    public int StartByteAdr;


    public AtributoDTO(Atributo atributo)
    {
        this.Nombre = atributo.Nombre;
        this.Tipo_Dato = atributo.Tipo_Dato;
        this.Tipo_Variable = atributo.Tipo_Variable;
        this.Direccion_Tipo_Dato = atributo.Direccion_Tipo_Dato;
        this.StartByteAdr = atributo.StartByteAdr;
        this.BitAdr = atributo.BitAdr;
        this.Cantidad = atributo.Cantidad;

    }

    public static string ToJson(Atributo atr)
    {
        AtributoDTO dto = new AtributoDTO(atr);

        string json = JsonUtility.ToJson(dto);

        return json;
    }

    public static AtributoDTO FromJson(string json)
    {
        AtributoDTO nuevo = JsonUtility.FromJson<AtributoDTO>(json);

        return nuevo;
    }
}
