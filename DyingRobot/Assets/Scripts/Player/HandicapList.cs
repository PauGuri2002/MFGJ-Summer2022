using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dying Robot/Handicap List")]
public class HandicapList : ScriptableObject
{
    [SerializeField][NonReorderable] private Handicap[] handicaps;

    public Handicap[] Handicaps => handicaps;
}