using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dying Robot/Handicap List")]
public class HandicapList : ScriptableObject
{
    [SerializeField][NonReorderable] private Handicap[] handicaps;

    public Handicap[] Handicaps => handicaps;
}


[System.Serializable]
public class Handicap
{
    public string property;
    public float percentage;
    [NonReorderable] public DialoguePart[] dialogue;
}


[System.Serializable]
public class DialoguePart
{
    [TextArea] public string dialogueText;
    public AudioClip dialogueSound;
}