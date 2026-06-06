using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        var scoringSystem = FindObjectOfType<ScoringSystem>();

        if (scoreText == null)
        {
            return;
        }

        if (scoringSystem == null)
        {
            scoreText.text = "You scored 0";
            return;
        }

        scoreText.text = $"You scored {scoringSystem.totalScore}";
    }
}