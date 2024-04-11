using S7.Net;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoLectura : MonoBehaviour
{
    public string nombreVariable;
    public Escena_PLC escena;
    public TextMeshProUGUI texto;

    public IGenCadena generador;

    public bool read_auto = true;

    void Start()
    {
        if (escena==null)
            escena = GetComponentInParent<Escena_PLC>();

        if (texto==null)
            texto = GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        // Actualizar el texto con el valor leído en cada frame
        if (read_auto)
        {
            escena.PedirVariable(nombreVariable);
            string valor = escena.LeerVariable(nombreVariable).ToString();
            //Debug.Log("hemos leido: " + valor + " en: " + nombreVariable);
            texto.text = this.GenerarCadena(valor);
            
        }

    }

    protected virtual string GenerarCadena(string entrada)
    {
        string salida = entrada;

        return salida;
    }
}
