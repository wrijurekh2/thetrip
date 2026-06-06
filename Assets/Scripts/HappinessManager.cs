using System;
using System.Collections.Generic;
using UnityEngine;

public class HappinessManager : MonoBehaviour
{
    [SerializeField]
    private PersonGraphic[] allPeople;
    public List<PersonData> people { get; private set; } = new();

    public class PersonData
    {
        public float joy = 0;
        public readonly PersonTag tag;
        public readonly PersonGraphic graphic;
        public int daysOutsideCircle = 0;

        public PersonData(PersonTag tag, PersonGraphic graphic)
        {
            this.tag = tag;
            this.graphic = graphic;
        }

        public bool isOutsideCircle => joy < -0.01f;
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
        foreach (var person in allPeople)
        {
             people.Add(new PersonData(person.personTag, person));
        }
        
    }

    public void StartDay(Day day)
    {
        FindAnyObjectByType<ChoiceListController>().LoadDay(day);
        // Each selection is expected to increase net happiness by 1
        // You can use this to calculate how much happiness the day should start with to achieve the target difficulty
        float desiredNetHappiness = day.expectedNetHappinessAtEndOfDay - day.totalAllowedSelections;

        float currentNetHappiness = 0;
        foreach (var person in people)
        {
            currentNetHappiness += person.joy;
        }
        // Calculate how much each person's happiness needs to be adjusted to reach the happiness quota for the start of the day
        // This makes happiness gain between days reasonably independant so that good choices on day 1 don't make the game trivial later
        // Also makes it possible to balance days independantly
        float adjustmentPerPerson = (desiredNetHappiness - currentNetHappiness) / people.Count;
        foreach (var person in people)
        {
            person.joy += adjustmentPerPerson;
        }
    }

    public void EndDay()
    {
        foreach (var person in people)
        {
            if (!person.isOutsideCircle)
            {
                person.daysOutsideCircle = 0;
            }
            else
            {
                person.daysOutsideCircle++;
            }
        }
    }

    public void RegisterActivity(Activity activity)
    {
        foreach (var impact in activity.allImpacts)
        {
            GetPersonData(impact.person).joy += impact.joyGain;
        }
        foreach (var person in people)
        {
            person.graphic.UpdateHappiness(person.joy);
            Debug.Log(person.tag.ToString() + person.joy);
        }
    }
}
