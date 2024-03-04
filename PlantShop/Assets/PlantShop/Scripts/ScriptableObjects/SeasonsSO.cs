using UnityEngine;

[CreateAssetMenu(fileName = "SeasonsSO", menuName = "SeasonsSO", order = 0)]
public class SeasonsSO : ScriptableObject
{
    public Season season;
    public float growthModifier;
    //Spawning Timer modifiers
    public float pestModifier;
    public float pruningModifier;
    public float waterModifier;
    //Response Timer Modifiers
    public float pestActionModifier;
    public float pruningActionModifier;
    public float waterActionModifier;
}
public enum Season //The Seasons of the year
{
    Summer,
    Autumn,
    Winter,
    Spring,
}
