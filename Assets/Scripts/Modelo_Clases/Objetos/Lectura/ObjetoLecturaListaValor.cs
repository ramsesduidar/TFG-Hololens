using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class ObjetoLecturaListaValor : ObjetoLectura
{

    private string[] valores = {"E0-Reposo", "E1-Generando Palet", "E10-Moviendo Palet",
                                "E20-Palet entrando a rueda", "E30-Para motor 1", "E40-Rotando rueda",
                                "E50-Enviando a Palet Izq", "E60-Enviando a Palet Dcho", "E61-Volviendo a reposo",
                                "Estado inválido"};
    
    protected override string GenerarCadena(string entrada)
    {

        int pos = this.getPos(entrada);
        
        return valores[pos];
    }

    private int getPos(string entrada)
    {
        switch (entrada)
        {
            case "0":
                return 0;
            case "1":
                return 1;
            case "10":
                return 2;
            case "20":
                return 3;
            case "30":
                return 4;
            case "40":
                return 5;
            case "50":
                return 6;
            case "60":
                return 7;
            case "61":
                return 8;
        }
        return 9;
    }
}
