using QRTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramasManager : MonoBehaviour
{
    
    public List<Escena_PLC> escenas = new List<Escena_PLC>();
    public QRCodesVisualizer QRCodesVisualizer;
    
    // Start is called before the first frame update
    void Start()
    {
        QRCodesVisualizer.eventoHolo += QRLeido;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void QRLeido(object sender, HoloEventArgs evento)
    {
        escenas.ForEach(escena => escena.Activar(evento.ValorQR, evento.Tracker));
    }
}
