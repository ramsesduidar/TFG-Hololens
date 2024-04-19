using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using S7.Net;

using System;
using System.Linq;
using S7.Net.Types;
using Unity.Collections.LowLevel.Unsafe;
using System.Threading.Tasks;


public class Clase_PLC
{

    public string DireccionIP;

    public CpuType TipoCPU;

    public short RackCPU;

    public short SlotCPU;

    public Plc My_plc;

    //var requiredRequestSize = 19 + dataItems.Count * 12;
    //si el limite es 240(en la libreria de S7NetPlus), podemos enviar 18 dataitems,
    public const int READ_LIMIT = 18;

    public Dictionary<string, Atributo> Atributos { get; private set; }


    public Clase_PLC(string direccionIP, CpuType modelo, short rackCPU, short slotCPU)
    {
        this.DireccionIP = direccionIP; 
        this.RackCPU = rackCPU;
        this.SlotCPU = slotCPU;
        this.TipoCPU = modelo;

        this.My_plc = new Plc(this.TipoCPU, this.DireccionIP, this.RackCPU, this.SlotCPU);
        

        Atributos = new Dictionary<string, Atributo>();

    }

    public void StartConnection()
    {
        if (this.My_plc == null)
        {
            throw new System.Exception("No hay plc asignado");
        }

        this.My_plc.Open();
        if (!this.My_plc.IsConnected)
        {
            throw new System.Exception("conectado con exito!!");
           
        }
    }

    public void EndConnection()
    {
        if (this.My_plc == null)
        {
            throw new System.Exception("No hay plc asignado");
        }

        this.My_plc.Close();
        if (this.My_plc.IsConnected)
        {
            Debug.Log("no se ha podido cerrar la conexion");
           
        }
    }

    private List<DataItem> ObtenerDataItems()
    {
        return Atributos.Values.Where(a => a.getLeerVariable()).Select(a => a.dataItem).ToList();
    }

    public void LeerVariables()
    {

        List<DataItem> dataitems = ObtenerDataItems();

        int num_items = dataitems.Count;

        double num_pasos = num_items*1.0 / READ_LIMIT;
        num_pasos = Math.Ceiling(num_pasos);
        //Debug.Log("Haremos pasos: " + num_pasos);

        StartConnection();

        // Recorrer la lista en pasos
        for (int i = 0; i < num_pasos; i++)
        {
            // Calcular el índice inicial y final para cada paso
            int indiceInicio = i * READ_LIMIT;
            int indiceFinal = Math.Min((i + 1) * READ_LIMIT, dataitems.Count);

            // Acceder a los elementos en el rango actual
            List<DataItem> elementosDelPaso = dataitems.GetRange(indiceInicio, indiceFinal - indiceInicio);

            // Imprimir los elementos del paso actual
            //Debug.Log($"Paso {i + 1}: desde " + indiceInicio + " hasta " + indiceFinal);
            // read the list of variables
            My_plc.ReadMultipleVars(elementosDelPaso);

            //Debug.Log("");
        }
        // close the connection
        EndConnection();
    }

    public void PedirVariable(string nombre)
    {
        this.Atributos[nombre].setLeerVariable(true);
    }

    public object getVariable(string nombre)
    {
        return this.Atributos[nombre].getValor();
    }

    public void EscribirVariable(string variable, string valor)
    {
        DataItem item = this.Atributos[variable].dataItem;
        item.Value = Conversor.ToS7Type(valor, item.VarType);

        StartConnection();
        Debug.Log("Voy a escribir " +  valor);
        this.My_plc.Write(item);
 
        // close the connection
        EndConnection();
    }


    public void AgregarAtributo(string nombreAtributo, DataType tipo, VarType variable, int db, int start, byte bitadr = 0, int count = 1)
    {
        if(this.Atributos.ContainsKey(nombreAtributo))
        {
            throw new Exception("El plc ya contiene un atributo llamado " + nombreAtributo);
        }

        Atributos.Add(nombreAtributo, new Atributo(nombreAtributo, tipo, variable, db, start, bitadr, count));
    }

    public static Clase_PLC FromDTO(PLC_DTO dto)
    {
        Clase_PLC plc = new Clase_PLC(dto.DireccionIP, dto.TipoCPU, dto.RackCPU, dto.SlotCPU);

        dto.lista_atributos.ForEach(a =>
        {
            plc.AgregarAtributo(a.Nombre, a.Tipo_Dato, a.Tipo_Variable, a.Direccion_Tipo_Dato, a.StartByteAdr, a.BitAdr, a.Cantidad);
        });

        return plc;
    }

}
