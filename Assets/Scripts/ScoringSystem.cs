using System.Text;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    [SerializeField] private HappinessManager happinessManager;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int totalScore { get; private set; } = 0;
    public string CalculateScoreForDay()
    {
        StringBuilder sb = new();
        int score = 5;
        sb.AppendLine("Dear diary,\n");
        sb.AppendLine("Base score: 5 points");
        foreach (var person in happinessManager.people)
        {
            if (person.isOutsideCircle)
            {
                int pointLoss = person.daysOutsideCircle;
                sb.AppendLine(person.tag + " felt left out today. (-" + pointLoss + "points). I will need to include them tomorrow.");
                score -= pointLoss;
            }
        }
        sb.AppendLine("\nYour score for the day: " + score);
        totalScore += score;
        sb.AppendLine("Your total score so far: " +  totalScore);
        return sb.ToString();
    }
}
