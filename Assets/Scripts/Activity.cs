using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "Activity")]
public class Activity : ScriptableObject
{
    [TextArea(3, 5)]
    public string writtenDescription;
    [TextArea(3, 5)]
    public string writtenThought;
    public ActivityImpact[] allImpacts;
}


[Serializable]
public class ActivityImpact
{
    public PersonTag person;
    public float joyGain;
}
