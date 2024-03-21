using S7.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Conversor
{
    public static object ToS7Type(string valor, VarType tipo)
    {

        switch (tipo)
        {
            case VarType.Bit:
                return Convert.ToBoolean(valor);
          
            case VarType.Byte:
                return Convert.ToByte(valor);
                
            case VarType.Word:
                return Convert.ToUInt16(valor);
                
            case VarType.DWord:
                return Convert.ToUInt32(valor);

            case VarType.Int:
                return Convert.ToInt16(valor);

            case VarType.DInt:
                return Convert.ToInt32(valor);

            case VarType.Real:
                return Convert.ToSingle(valor);

            case VarType.LReal:
                return Convert.ToDouble(valor);

            case VarType.String:
                return valor;

            case VarType.S7String:
                return valor;

            case VarType.S7WString:
                return valor;

            case VarType.Timer:
                return Convert.ToDouble(valor);

            case VarType.Counter:
                return Convert.ToUInt32(valor);

            case VarType.DateTime:
                return Convert.ToDateTime(valor);

            case VarType.DateTimeLong:
                return Convert.ToDateTime(valor);

            default:
                throw new Exception("El tipo VarType " + tipo + " no es un tipo válido");
        }

        
    }

    private static byte[] ConvertStringToByteArray(string myString)
    {
        // convert myString into ByteArray
        // for S7-String-Format:
        // 1. Byte = maximum length of the String
        // 2. Byte = actual length of the String         

        int maximumLength = 50; // <-- according to the length of your S7-String, here STRING[50]
        int actualLength = myString.Length;
        byte[] strin = System.Text.Encoding.Default.GetBytes(myString);
        byte[] bytearray = new byte[actualLength + 2];

        bytearray[0] = Convert.ToByte(maximumLength);
        bytearray[1] = Convert.ToByte(actualLength);

        // copy string-array into bytearray, begin at 3. byte 
        for (int i = 2; i < bytearray.Length; i++)
        {
            bytearray[i] = strin[i - 2];
        }

        return bytearray;
    }


    /*public enum VarType
    {
        Bit,
        Byte,
        Word,
        DWord,
        Int,
        DInt,
        Real,
        String,
        StringEx,
        Timer,
        Counter
    }*/
}
