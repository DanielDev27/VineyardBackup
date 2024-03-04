using UnityEngine;

[CreateAssetMenu(fileName = "SeasonsSO", menuName = "SeasonsSO", order = 0)]
public class SeasonsSO : ScriptableObject
{
    public Season season;
    public float growthModifier;
    //Spawning Timers
    public float pestSpawnTimer;
    public float pruningSpawnTimer;
    public float waterSpawnTimer;
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
