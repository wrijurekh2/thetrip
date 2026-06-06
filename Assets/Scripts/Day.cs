using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "Day")]
public class Day : ScriptableObject
{
    [field: SerializeField]
    public int totalAllowedSelections {  get; private set; }
    [field: SerializeField]
    public float expectedNetHappinessAtEndOfDay { get; private set; }
    [field: SerializeField]
    public Sprite backgroundImage { get; private set; }
    [field: SerializeField]
    public string location { get; private set; }
    [field: SerializeField]
    public string dayDisplayTitle { get; private set; }
    [field: SerializeField]
    public Activity[] activityOptions { get; private set; }
	[field: SerializeField]
	public AudioClip backgroundMusic { get; private set; }
}