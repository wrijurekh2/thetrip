using System;
using System.Collections.Generic;
using UnityEngine;

public class HapinessManager : MonoBehaviour
{
    [SerializeField]
    private PersonTag[] allPeopleTags;
    private Dictionary<PersonTag, float> joyValues = new Dictionary<PersonTag, float>();
    [SerializeField]
    private Decision exampleDecision;

    private void Awake()
    {
        foreach (var person in allPeopleTags)
        {
            joyValues[person] = 0;
        }
        RegisterActivity(exampleDecision.activityOptions[0]);
        RegisterActivity(exampleDecision.activityOptions[1]);
    }
    public void RegisterActivity(Activity activity)
    {
        foreach (var impact in activity.allImpacts)
        {
            joyValues[impact.person] += impact.joyGain;
        }
        foreach (var person in joyValues.Keys)
        {
            var joy = joyValues[person];
            Debug.Log("Person: " + person + " Joy: " + joy);
            // TODO: Put update clause for front-end here
        }
    }
}
