using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Handicap
{
    public string property;
    public float percentage;
    [TextArea][NonReorderable] public string[] dialogue;
}
