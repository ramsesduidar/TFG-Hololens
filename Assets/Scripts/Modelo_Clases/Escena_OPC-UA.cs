/*using S7.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escena_OPC_UA : Escena
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
                foreach (DataBlock bloque in bloquesDatos)
                {
                    bloque.atributos.ForEach((a) =>
                    {
                        try
                        {
                            ((Clase_PLC)this.My_Plc).AgregarAtributo(a.Nombre, a.Tipo_Dato, a.Tipo_Variable, a.Direccion_Tipo_Dato, a.StartByteAdr, a.BitAdr, a.Cantidad);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message + " en el bloque de datos " + bloque.name);

                        }
                    });
                }
            }

        }


    }
}*/