using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opc.UaFx.Client;
using Opc.UaFx;
using System;
using S7.Net;
using S7.Net.Types;
using UnityEditor.PackageManager;

public class Lector_OPC_UA : ILectorPLC
{
    public string DireccionIP;

    public OpcClient ClienteOPC;

    //var requiredRequestSize = 19 + dataItems.Count * 12;
    //si el limite es 240(en la libreria de S7NetPlus), podemos enviar 18 dataitems,
    public const int READ_LIMIT = 18;

    public Dictionary<string, AtributoOPC> Atributos { get; private set; }

    private Dictionary<string, AtributoOPC> direcciones;

    public class AtributoOPC
    {
        public string Direccion;
        public object valor;
        public S7.Net.VarType TipoVariable;
        public bool LeerVariable = false;

        public AtributoOPC(string direccion, S7.Net.VarType tipo)
        {
            this.Direccion = direccion;
            this.TipoVariable = tipo;
            this.valor = new object();
        }
    }


    public Lector_OPC_UA(string direccionIP)
    {
        this.DireccionIP = direccionIP;

        Atributos = new Dictionary<string, AtributoOPC>();
        direcciones = new Dictionary<string, AtributoOPC>();

        this.ClienteOPC = new OpcClient(direccionIP);

    }

    private List<OpcReadNode> ObtenerCommands()
    {
        List<OpcReadNode> commands = new List<OpcReadNode>();

        foreach (AtributoOPC atributo in Atributos.Values)
        {
            if (atributo.LeerVariable)
                commands.Add(new OpcReadNode("ns=2;s=Machine/Job/Number"));
        }

        return commands;
    }

        

    public void LeerVariables()
    {

        List<OpcReadNode> dataitems = ObtenerCommands();

        int num_items = dataitems.Count;

        double num_pasos = num_items * 1.0 / READ_LIMIT;
        num_pasos = Math.Ceiling(num_pasos);
        //Debug.Log("Haremos pasos: " + num_pasos);

        ClienteOPC.Connect();

        // Recorrer la lista en pasos
        for (int i = 0; i < num_pasos; i++)
        {
            // Calcular el índice inicial y final para cada paso
            int indiceInicio = i * READ_LIMIT;
            int indiceFinal = Math.Min((i + 1) * READ_LIMIT, dataitems.Count);

            // Acceder a los elementos en el rango actual
            List<OpcReadNode> elementosDelPaso = dataitems.GetRange(indiceInicio, indiceFinal - indiceInicio);

            
            IEnumerable<OpcValue> job = ClienteOPC.ReadNodes(elementosDelPaso);

            int j = 0;
            foreach(var valor in job)
            {
                this.direcciones[elementosDelPaso[j].NodeId.ToString()].valor = valor;
            }


        }
        // close the connection
        ClienteOPC.Disconnect();
    }

    public void PedirVariable(string nombre)
    {
        this.Atributos[nombre].LeerVariable = true;
    }

    public object getVariable(string nombre)
    {
        return this.Atributos[nombre].valor;
    }

    public void EscribirVariable(string variable, string valor)
    {
        AtributoOPC atributo = this.Atributos[variable];
       
        this.ClienteOPC.Connect();
        Debug.Log("Voy a escribir " + valor);
        this.ClienteOPC.WriteNode(atributo.Direccion, Conversor.ToS7Type(valor, atributo.TipoVariable));

        // close the connection
        this.ClienteOPC.Disconnect();
    }


    public void AgregarAtributo(string nombreAtributo, string direccion, VarType variable)
    {
        if (this.Atributos.ContainsKey(nombreAtributo))
        {
            throw new Exception("El plc ya contiene un atributo llamado " + nombreAtributo);
        }

        AtributoOPC atr = new AtributoOPC(direccion, variable);
        Atributos.Add(nombreAtributo, atr);
        direcciones.Add(atr.Direccion, atr);
    }
}
