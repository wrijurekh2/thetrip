using System;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : MonoBehaviour
{
    [SerializeField]
    private PersonTag[] allPeopleTags;
    private List<PersonData> people = new();
    [SerializeField]
    private Day exampleDay;

    private class PersonData
    {
        public float joy = 0;
        public readonly PersonTag tag;

        public PersonData(PersonTag tag)
        {
            this.tag = tag;
        }
    }

    private PersonData GetPersonData(PersonTag tag)
    {
        foreach (PersonData personData in people)
        {
            if (personData.tag == tag) return personData;
        }
        Debug.LogError("PersonData queried did not exist");
        return null;
    }

    private void Awake()
    {
        foreach (var tag in allPeopleTags)
        {
             people.Add(new PersonData(tag));
        }
        RegisterActivity(exampleDay.activityOptions[0]);
        RegisterActivity(exampleDay.activityOptions[1]);
    }

    public void StartDay(Day day)
    {
        // Each selection is expected to increase net happiness by 1
        // You can use this to calculate how much happiness the day should start with to achieve the target difficulty
        float netHappinessAtStart = day.expectedNetHappinessAtEndOfDay - day.totalAllowedSelections;

    }

    public void RegisterActivity(Activity activity)
    {
        foreach (var impact in activity.allImpacts)
        {
            GetPersonData(impact.person).joy += impact.joyGain;
        }
        foreach (var person in people)
        {
            Debug.Log("Person: " + person.tag + " Joy: " + person.joy);
            // TODO: Put update clause for front-end here
        }
    }
}
