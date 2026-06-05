using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision", menuName = "Decision")]
public class Day : ScriptableObject
{
    public int totalAllowedSelections;
    public Activity[] activityOptions;
}

[Serializable]
public class Activity
{
    [TextArea(3,20)]
    public string writtenDescription;
    public ActivityImpact[] allImpacts;
}

[Serializable]
public class ActivityImpact
{
    public PersonTag person;
    public float joyGain;
}