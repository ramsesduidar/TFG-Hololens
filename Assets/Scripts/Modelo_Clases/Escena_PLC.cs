using Hologramas;
using MixedReality.Toolkit.SpatialManipulation;
using QRTracking;
using S7.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class Escena_PLC : Escena
{
    public string DireccionIP;
    public CpuType Modelo = CpuType.S71200;
    public short RackCPU = 0;
    public short SlotCPU = 1;

    public string atributos_file;

    //public List<AtributoDTO> atributos;
    public List<DataBlock> bloquesDatos;

    [Serializable]
    public class DataBlock
    {
        public string name;
        public List<AtributoDTO> atributos;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.DireccionIP != null)
        {
            this.My_Plc = new Clase_PLC(this.DireccionIP, this.Modelo, this.RackCPU, this.SlotCPU);

            if (this.bloquesDatos != null)
            {
                foreach(DataBlock bloque in bloquesDatos)
                {
                    bloque.atributos.ForEach((a) =>
                    {
                        try
                        {
                            ((Clase_PLC) this.My_Plc).AgregarAtributo(a.Nombre, a.Tipo_Dato, a.Tipo_Variable, a.Direccion_Tipo_Dato, a.StartByteAdr, a.BitAdr, a.Cantidad);
                        } catch (Exception e) { 
                            throw new Exception(e.Message + " en el bloque de datos " + bloque.name);

                        }
                    });
                }
            }
            
        }

    }

    // Update is called once per frame
    /*void Update()
    {
        // Comprobar si ya hay una operación de escritura en curso
        if (!leyendo)
        {
            // Iniciar una tarea asíncrona para escribir la variable en el PLC
            Task.Run( () =>
            {
                // Marcar que se está realizando una operación de lectura para evitar iniciar otra
                leyendo = true;

                // Llamar a la función de lectura en el PLC
                try
                {
                    this.LeerVariables();
                }
                catch (Exception ex)
                {
                    Debug.Log("Lectura task run realizada con errores: " + ex.Message);
                }

                // Marcar que la operación de lectura ha finalizado
                leyendo = false;
            });
        }
    }

    public void LeerVariables()
    {
        if (this.My_Plc != null)
            this.My_Plc.LeerVariables();
    }

    public object LeerVariable(string nombre)
    {
        if(this.My_Plc == null) 
            return null;
        
        return this.My_Plc.getVariable(nombre);
    }

    public void PedirVariable(string nombre)
    {
        if (this.My_Plc != null)
           this.My_Plc.PedirVariable(nombre);
    }

    public void EscribirVariable(string nombre, string valor)
    {
        if(this.My_Plc != null)
            this.My_Plc.EscribirVariable(nombre, valor);
    }


    public bool Activar(HoloEventArgs evento)
    {
        
        if(evento.ValorQR == this.qrcode_name)
        {
            gameObject.SetActive(true);

            if (instanciaHolograma == null && hologramaPrefab != null)
            {
                instanciaHolograma = Instantiate(hologramaPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                instanciaHolograma.GetComponent<SpatialGraphNodeTracker>().Id = evento.Tracker;

            }

            if (instanciaHolograma != null)
                instanciaHolograma.SetActive(true);

            return true;
        } else
        {
            // si se desactiva el objeto con esto activo luegolos botones no funcionan
            if (TryGetComponent<ObjectManipulator>(out var obj))
            {
                obj.enabled = false;
            }
            gameObject.SetActive(false);

            if (instanciaHolograma != null) instanciaHolograma.SetActive(false);

            return false;
        }

    }*/
}
