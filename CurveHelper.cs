using System;
using System.Collections.Generic;
using UnityEngine;

public static class CurveHelper
{
    public static double Factorial( int n )
    {
        if (n <= 1)
            return 1;
        else
            return n * Factorial(n - 1);
    }

    public static double BinomialCoefficient( int n, int k )
    {
        return Factorial(n) / (Factorial(n - k) * Factorial(k));
    }

    public static double BernsteinPolynomialValue( int i, int n, double t )
    {
        return BinomialCoefficient(n, i) * Math.Pow(t, i) * Math.Pow(1 - t, n - i);
    }

    public static Vector3 BezierCurveValue( double t, List<Vector3> controlPoints )
    {
        Vector3 result = new Vector3();
        int n = controlPoints.Count;

        for(int i=0; i<n; i++)
        {
            result += controlPoints[i] * (float)(BernsteinPolynomialValue(i, n-1, t));
        }

        return result;
    }
}
