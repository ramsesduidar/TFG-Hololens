using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using S7.Net;
using TMPro.Examples;
using MixedReality.Toolkit.UX.Experimental;


public class Demo_PLC : MonoBehaviour
{

    public TextMeshPro texto1;
    UnityEngine.TouchScreenKeyboard keyboard;
    public static string keyboardText = "";

    int valor = 0;
    private string atributo;

    //private Clase_PLC plc;

    private Escena_PLC canvasPLC;

    private struct testStruct
    {
        public bool encendido;
        public int llenado;
    }

    public void BotonPresionado()
    {
        Debug.Log("BotonPresionado");

        this.canvasPLC.My_Plc.LeerVariables();
        var result = this.canvasPLC.My_Plc.Atributos[this.atributo].getValor();
        texto1.text = result + "";

        //this.plc.LeerVariables();
        //var result = this.plc.Atributos[this.atributo].getValor();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.atributo = "Porcentaje";

        canvasPLC = GetComponentInParent<Escena_PLC>();

        /*this.plc = new Clase_PLC("127.0.0.1", "S71200", 0, 1);
        this.plc.AgregarAtributo(this.atributo, DataType.DataBlock, VarType.DInt, 1, 2);*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
