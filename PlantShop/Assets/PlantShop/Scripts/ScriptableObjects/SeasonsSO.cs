using UnityEngine;

[CreateAssetMenu(fileName = "SeasonsSO", menuName = "SeasonsSO", order = 0)]
public class SeasonsSO : ScriptableObject
{
    public Season season;
    public float growthModifier;
    //Timer modifiers
    public float pestModifier;
    public float pruningModifier;
    public float waterModifier;
}
public enum Season //The Seasons of the year
{
    Summer,
    Autumn,
    Winter,
    Spring,
}
