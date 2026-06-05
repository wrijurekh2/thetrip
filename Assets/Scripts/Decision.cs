using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision", menuName = "Decision")]
public class Decision : ScriptableObject
{
    public Activity[] activityOptions;
}

[Serializable]
public class Activity
{
    [TextArea(15,5)]
    public string writtenDescription;
    public ActivityImpact[] allImpacts;
}

[Serializable]
public class ActivityImpact
{
    public PersonTag person;
    public int joyGain;
}