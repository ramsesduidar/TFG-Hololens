using S7.Net.Types;
using S7.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.XR.ARSubsystems.XRFaceMesh;

public interface ILectorPLC
{
    void LeerVariables();
    void PedirVariable(string nombre);

    object getVariable(string nombre);

    void EscribirVariable(string variable, string valor);


}
