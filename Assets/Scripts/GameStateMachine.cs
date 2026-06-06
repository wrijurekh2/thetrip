using System.Collections;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField]
    private Day[] allDays;
    [SerializeField]
    private NotebookManager notebookManager;
    [SerializeField]
    private HappinessManager happinessManager;
    [SerializeField]
    private float startDayDelay;
    public GameState currentState { get; private set; } = GameState.StartDay;
    private int currentDayIndex = 0;
    private ScoringSystem scoringSystem;
    public Day currentDay { get => allDays[currentDayIndex]; }

    private void Awake()
    {
        scoringSystem = GetComponent<ScoringSystem>();
    }

    private void Start()
    {
        StartNewDay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && currentState == GameState.Decisions)
        {
            DecisionsComplete();
        }
    }

    public void StartDayComplete()
    {
        if (currentState != GameState.StartDay)
        {
            Debug.LogError("Invalid transition from StartDay");
        }
        currentState = GameState.Decisions;
    }
    public void DecisionsComplete()
    {
        if (currentState != GameState.Decisions)
        {
            Debug.LogError("Invalid transition from Decisions");
        }
        currentState = GameState.Scoring;
        happinessManager.EndDay();
        var scoreCard = scoringSystem.CalculateScoreForDay();
        notebookManager.SwitchToScoreScreen(scoreCard);
        
    }

    public void ScoringComplete()
    {
        if (currentState != GameState.Decisions)
        {
            Debug.LogError("Invalid transition from Scoring");
        }
        // TODO: Add last day logic, this will currently just go out of bounds
        StartNewDay();
    }

    private void StartNewDay()
    {
        currentDayIndex++;
        currentState = GameState.StartDay;
        happinessManager.StartDay(currentDay);
        StartCoroutine(StartDayRoutine());
    }

    private IEnumerator StartDayRoutine()
    {
        notebookManager.MoveOffScreen();
        while (notebookManager.isBusy) yield return null;
        notebookManager.SwitchToDecisionScreen();
        yield return new WaitForSeconds(startDayDelay);
        notebookManager.MoveOnScreen();
        while (notebookManager.isBusy) yield return null;
        StartDayComplete();
    }
}

public enum GameState
{
    StartDay,
    Decisions,
    Scoring
}