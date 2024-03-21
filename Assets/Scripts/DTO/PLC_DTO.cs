using S7.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PLC_DTO 
{
  
    public string DireccionIP;

    public CpuType TipoCPU;

    public short RackCPU;

    public short SlotCPU;

    public List<AtributoDTO> lista_atributos = new List<AtributoDTO>();

    public PLC_DTO(string direccionIP, CpuType modelo, short rackCPU, short slotCPU, List<Atributo> lista_)
    {
        this.DireccionIP = direccionIP;
        this.RackCPU = rackCPU;
        this.SlotCPU = slotCPU;
        this.TipoCPU = modelo;

        lista_.ForEach(atributo => this.lista_atributos.Add( new AtributoDTO(atributo)));

    }

    public static string ToJson(Clase_PLC plc)
    {
        PLC_DTO dto = new PLC_DTO(plc.DireccionIP, plc.TipoCPU, plc.RackCPU, plc.SlotCPU, plc.Atributos.Values.ToList());
        
        string json = JsonUtility.ToJson(dto);

        return json;
    }

    public static PLC_DTO FromJson(string json)
    {
        PLC_DTO nuevo = JsonUtility.FromJson<PLC_DTO>(json);

        return nuevo;
    }
}
