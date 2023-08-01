using System;
using UnityEngine;

public class Perk : MonoBehaviour
{
    protected float Coef;
    protected String Effector;
    public String GetEffector()
    {
        return Effector;
    }

    public float GetCoef()
    {
        return Coef;
    }
}
