using Hologramas;
using QRTracking;
using S7.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Escena_PLC : MonoBehaviour
{

    public Clase_PLC My_Plc;

    public bool leer_auto = true;

    public string DireccionIP;
    public CpuType Modelo = CpuType.S71200;
    public short RackCPU = 0;
    public short SlotCPU = 1;

    public string atributos_file;

    public string qrcode_name;
    public GameObject hologramaPrefab;

    private GameObject instanciaHolograma;

    //public List<AtributoDTO> atributos;
    public List<DataBlock> bloquesDatos;

    [Serializable]
    public class DataBlock
    {
        public string name;
        public List<AtributoDTO> atributos;

        public void addAtributo()
        {

        }
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
                            this.My_Plc.AgregarAtributo(a.Nombre, a.Tipo_Dato, a.Tipo_Variable, a.Direccion_Tipo_Dato, a.StartByteAdr, a.BitAdr, a.Cantidad);
                        } catch (Exception e) { 
                            throw new Exception(e.Message + " en el bloque de datos " + bloque.name);

                        }
                    });
                }
            }
            
        }

        // Llama al método 'MiMetodo' después de un retraso inicial de 2 segundos,
        // y luego lo repite cada 0.5 segundos.
        //InvokeRepeating("LeerVariables", 0.1f, 1.0f);
    }

    private void Init(string direccionIP, CpuType modelo, short rackCPU, short slotCPU)
    {
        this.DireccionIP = direccionIP;
        this.Modelo = modelo;
        this.RackCPU = rackCPU;
        this.SlotCPU = slotCPU;

        this.Start();

    }

    // Update is called once per frame
    void Update()
    {
        if(leer_auto)
            LeerVariables();
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


    public void Activar(HoloEventArgs evento)
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
        } else
        {
            gameObject.SetActive(false);

            if (instanciaHolograma != null) instanciaHolograma.SetActive(false);
        }

    }
}
