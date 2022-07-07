using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Handicap
{
    public string property;
    public float percentage;

    public Handicap(string _property, float _percentage)
    {
        property = _property;
        percentage = _percentage;
    }
}
