using QRTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hologramas
{

    public class HologramasManager : MonoBehaviour
    {

        public List<Escena_PLC> escenas = new List<Escena_PLC>();
        public QRCodesVisualizer LectorObjetos;

        // Start is called before the first frame update
        void Start()
        {
            LectorObjetos.eventoHolo += QRLeido;
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void QRLeido(object sender, HoloEventArgs evento)
        {
            escenas.ForEach(escena => escena.Activar(evento));
        }
    }
}