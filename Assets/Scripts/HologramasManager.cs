using QRTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Hologramas
{
    public class ChangeQREventArgs : EventArgs
    {
        public string Antes { get; }
        public string Ahora { get; }

        public ChangeQREventArgs(string antes, string ahora)
        {
            this.Antes = antes;
            this.Ahora = ahora;
        }
    }

    public class HologramasManager : MonoBehaviour
    {

        public List<Escena_PLC> escenas = new List<Escena_PLC>();
        public QRCodesVisualizer LectorObjetos;

        private List<Escena_PLC> actuales = new List<Escena_PLC>();

        private string currentQR;
        public event EventHandler<ChangeQREventArgs> cambioQR;

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
            return escenas.FindAll( escena =>
            {
                return escena.isActiveAndEnabled;
            });
        }

        public void QRLeido(object sender, HoloEventArgs evento)
        {
            
            if(currentQR != evento.ValorQR)
            {
                this.cambioQR?.Invoke(this, new ChangeQREventArgs(currentQR, evento.ValorQR));
            }

            escenas.ForEach(escena => {
                if (escena.Activar(evento)) 
                    actuales.Add(escena);
                else actuales.Remove(escena);
            });
            
            currentQR = evento.ValorQR;
        }
    }
}