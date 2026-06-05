using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision", menuName = "Decision")]
public class Day : ScriptableObject
{
    [field: SerializeField]
    public int totalAllowedSelections {  get; private set; }
    [field: SerializeField]
    public float expectedNetHappinessAtEndOfDay { get; private set; }
    [field: SerializeField]
    public string dayDisplayTitle { get; private set; }
    [field: SerializeField]
    public Activity[] activityOptions { get; private set; }
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