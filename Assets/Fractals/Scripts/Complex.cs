using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Complex
{
    public float x { get; set; }
    public float y { get; set; }

    public Complex(float xValue, float yValue)
    {
        x = xValue;
        y = yValue;
    }

    public static Complex Sum(Complex comp1, Complex comp2)
    {
        return new Complex(comp1.x + comp2.x, comp1.y + comp2.y);
    }
    public static Complex Substract(Complex comp1, Complex comp2)
    {
        return new Complex(comp1.x - comp2.x, comp1.y - comp2.y);
    }
    public static Complex Multiply(Complex comp1, Complex comp2)
    {
        return new Complex(comp1.x * comp2.x - comp1.y * comp2.y, comp1.y * comp2.x + comp1.x * comp2.y);
    }
}

