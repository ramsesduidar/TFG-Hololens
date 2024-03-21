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
    public TextMeshPro texto;
    
    public bool read_auto = true;

    void Start()
    {
        if (escena==null)
            escena = GetComponentInParent<Escena_PLC>();

        if (texto==null)
            texto = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        // Actualizar el texto con el valor leído en cada frame
        if (read_auto)
        {
            escena.PedirVariable(nombreVariable);
            texto.text = escena.LeerVariable(nombreVariable).ToString();
        }

    }
}
