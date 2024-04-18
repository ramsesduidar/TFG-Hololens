using QRTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hologramas
{

    public class HologramasManager : MonoBehaviour
    {

        public List<Escena_PLC> escenas = new List<Escena_PLC>();
        public QRCodesVisualizer LectorObjetos;

        private List<Escena_PLC> actuales = new List<Escena_PLC>();

        // Start is called before the first frame update
        void Start()
        {
            LectorObjetos.eventoHolo += QRLeido;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public List<Escena_PLC> GetEscenasActuales()
        {
            return actuales;
        }

        public void QRLeido(object sender, HoloEventArgs evento)
        {
            escenas.ForEach(escena => {
                if (escena.Activar(evento)) 
                    actuales.Add(escena);
                else actuales.Remove(escena);
            });
                
        }
    }
}