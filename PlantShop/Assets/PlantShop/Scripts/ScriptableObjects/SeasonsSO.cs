using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SeasonsSO", menuName = "SeasonsSO", order = 0)]
public class SeasonsSO : ScriptableObject
{
    public float growthModifier;
    public float pestModifier;
    public float pruningModifier;
    public float waterModifier;
}
