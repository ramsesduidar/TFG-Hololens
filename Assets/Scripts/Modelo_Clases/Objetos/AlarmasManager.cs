using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

// Clase para los datos de la alarma
public class AlarmaEventArgs : EventArgs
{
    public string NombreAlarma { get; }
    public string Descripcion { get; }

    public AlarmaEventArgs(string nombreAlarma, string descripcion)
    {
        this.NombreAlarma = nombreAlarma;
        this.Descripcion = descripcion;
    }
}

public class AlarmasManager : MonoBehaviour
{
    public List<Alarma> alarmas;
    public GameObject alarmaPrefab;
    private Dictionary<string, GameObject> listado = new Dictionary<string, GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        if (alarmas != null)
        {
            alarmas.ForEach(a =>
            {
                a.AlarmaEncendida += AgregarAlarma;
                a.AlarmaApagada += EliminarAlarma;
            });
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alarmas != null)
        {
            alarmas.ForEach(a =>
            {
                a.Update();

            });
        }
    }

    void AgregarAlarma(object sender, AlarmaEventArgs e)
    {
        string texto = $"<size=8>{e.NombreAlarma}</size><size=6>\r\n<alpha=#88>{e.Descripcion}</size>";

        if (listado.ContainsKey(e.NombreAlarma)) return;
        
        // Instanciar un nuevo objeto a partir del prefab preasignado
        GameObject nuevoObjeto = Instantiate(alarmaPrefab, transform.position, Quaternion.identity);

        // Asignar el objeto recién instanciado como hijo del objeto asociado al script
        nuevoObjeto.transform.SetParent(transform);

        // Restablecer la escala y rotación del objeto hijo
        nuevoObjeto.transform.localScale = Vector3.one;
        nuevoObjeto.transform.localRotation = Quaternion.identity;

        TextMeshProUGUI[] texts = nuevoObjeto.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI item in texts)
        {
            if (item.name == "Text")
                item.text = texto;
        }
        
            

        listado.Add(e.NombreAlarma, nuevoObjeto);
    }

    void EliminarAlarma(object sender, AlarmaEventArgs e)
    {
        string text = $"<size=8>${e.NombreAlarma}</size><size=6>\r\n<alpha=#88>{e.Descripcion}</size>";
        if (!listado.ContainsKey(e.NombreAlarma)) return;

        GameObject objeto = listado[e.NombreAlarma];
        listado.Remove(e.NombreAlarma);
        Destroy(objeto);
    }
}
