using Hologramas;
using MixedReality.Toolkit.SpatialManipulation;
using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuMano : MonoBehaviour
{

    public HologramasManager manager;

    private GameObject toggle;
    
    // Start is called before the first frame update
    void Start()
    {
        if(manager != null)
        {
            manager.cambioQR += RecibirEvento;
        }
        else
        {
            throw new System.Exception("Asigna un HologramManager al menu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecibirEvento(object sender, ChangeQREventArgs evento)
    {
        var nombre = "ManipularMenu";
        Debug.Log("Evento qr changed recibido " + evento.Ahora);
        PressableButton[] buttons = GetComponentsInChildren<PressableButton>(true);

        if (buttons != null)
        {
            foreach (var item in buttons)
            {
                if (item.name == nombre) item.ForceSetToggled(false);
            }
        }
        else
        {
            Debug.LogError("No se encontró PressableButton en el objeto " + nombre);
        }
    }

    public void ActivarManipulador()
    {
        manager.GetEscenasActuales().ForEach(e =>
        {
            if (e.TryGetComponent<ObjectManipulator>(out var obj))
            {
                obj.enabled = true;
            }
        });
    }

    public void DesactivarManipulador()
    {
        manager.GetEscenasActuales().ForEach(e =>
        {
            if (e.TryGetComponent<ObjectManipulator>(out var obj))
            {
                obj.enabled = false;
            }
        });
    }

    public void AnclarMenu()
    {
        manager.GetEscenasActuales().ForEach(e =>
        {
            if (e.TryGetComponent<RadialView>(out var obj))
            {
                obj.enabled = false;
            }
        });
    }

    public void LiberarMenu()
    {
        manager.GetEscenasActuales().ForEach(e =>
        {
            if (e.TryGetComponent<RadialView>(out var obj))
            {
                obj.enabled = true;
            }
        });
    }


}
