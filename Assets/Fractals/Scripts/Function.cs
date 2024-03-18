using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Function 
{
    public string name;
    public Sprite image;
    public string operation;
    public float cX;
    public float cY;

    public Complex Execute(Complex z,Complex c)
    {
        Complex result = c;
        string[] sums = operation.Split('+');
        foreach (string sum in sums)
        {
            string[] multipliers = sum.Split('*');
            Complex partialMultiple = new Complex(1, 0);
            foreach (string multiplier in multipliers)
            {
                switch (multiplier)
                {
                    case "z":
                        partialMultiple = Complex.Multiply(partialMultiple , z);
                        break;
                    default:
                        break;
                }
            }
            result = Complex.Sum(result,partialMultiple);
        }
        return result;
    }
}
