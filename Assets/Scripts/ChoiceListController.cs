using UnityEngine;
using System.Collections.Generic;

public class ChoiceListController : MonoBehaviour
{
    [SerializeField] private HappinessManager happinessManager;
    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private Transform choicesParent;
    [SerializeField] private float spacingY = 0.6f;

    private readonly List<ChoiceButton> spawnedButtons = new();
    private readonly List<Activity> selectedActivities = new();
    private int remainingSelections;
    private Day currentDay;

    public void LoadDay(Day day)
    {
        currentDay = day;
        remainingSelections = day.totalAllowedSelections;
        selectedActivities.Clear();
        ClearButtons();

        for (int i = 0; i < day.activityOptions.Length; i++)
        {
            var activity = day.activityOptions[i];
            Debug.Log("Spawning button for: " + activity.writtenDescription);

            // spawn button as child of choicesParent
            var go = Instantiate(choiceButtonPrefab, choicesParent);

            // stack buttons downward from parent position
            go.transform.localPosition = new Vector3(0, -i * spacingY, 0);

            var btn = go.GetComponent<ChoiceButton>();
            Debug.Log("ChoiceButton component: " + btn);    
            btn.Setup(activity, this);
            spawnedButtons.Add(btn);
        }
    }

    public void OnChoiceClicked(ChoiceButton btn)
        {
            if (selectedActivities.Contains(btn.activity))
            {
                selectedActivities.Remove(btn.activity);
                btn.SetSelected(false);
                remainingSelections++;
            }
            else
            {
                if (remainingSelections <= 0) return;

                selectedActivities.Add(btn.activity);
                btn.SetSelected(true);
                remainingSelections--;
                
                // fire immediately on click
                happinessManager.RegisterActivity(btn.activity);
            }

            UpdateDisabledStates();
        }

    void UpdateDisabledStates()
    {
        bool isFull = remainingSelections <= 0;
        foreach (var btn in spawnedButtons)
        {
            bool isSelected = selectedActivities.Contains(btn.activity);
            btn.SetDisabled(!isSelected && isFull);
        }
    }

    void ClearButtons()
    {
        foreach (var btn in spawnedButtons)
            Destroy(btn.gameObject);
        spawnedButtons.Clear();
    }
}