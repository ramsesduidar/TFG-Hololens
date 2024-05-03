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


public abstract class Escena : MonoBehaviour
{

    public ILectorPLC My_Plc;

    public bool leer_auto = true;

    public string qrcode_name;
    public GameObject hologramaPrefab;

    private GameObject instanciaHolograma;

    private bool leyendo;

    // Update is called once per frame
    void Update()
    {
        // Comprobar si ya hay una operación de escritura en curso
        if (!leyendo)
        {
            // Iniciar una tarea asíncrona para escribir la variable en el PLC
            Task.Run(() =>
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
        if (this.My_Plc == null)
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
        if (this.My_Plc != null)
            this.My_Plc.EscribirVariable(nombre, valor);
    }


    public bool Activar(HoloEventArgs evento)
    {

        if (evento.ValorQR == this.qrcode_name)
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
        }
        else
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

    }
}
