using Hologramas;
using MixedReality.Toolkit.SpatialManipulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMano : MonoBehaviour
{

    public HologramasManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
