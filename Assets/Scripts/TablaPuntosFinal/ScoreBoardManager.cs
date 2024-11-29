// using System.Collections.Generic;
// // using System.Collections;
// using UnityEngine.UI;
// using UnityEngine;
// using TMPro;

// [System.Serializable]
// public class PlayerScore
// {
//     public string playerName;
//     public int score;

//     public PlayerScore(string name, int score)
//     {
//         this.playerName = name;
//         this.score = score;
//     }
// }

// public class ScoreboardManager : MonoBehaviour
// {
//     public TextMeshProUGUI scoreboardText; // Texto donde se mostrará la tabla
//     public GameObject scoreboardPanel;    // Panel donde se muestra la tabla
//     private List<PlayerScore> scores = new List<PlayerScore>();

//     public void AddScore(string playerName, int score)
//     {
//         scores.Add(new PlayerScore(playerName, score));
//         scores.Sort((a, b) => b.score.CompareTo(a.score)); // Ordenar de mayor a menor
//         UpdateScoreboardUI();
//     }

//     private void UpdateScoreboardUI()
//     {
//         if (scoreboardText != null)
//         {
//             scoreboardText.text = "Tabla de puntuaciones:\n";
//             int position = 1;
//             foreach (var score in scores)
//             {
//                 // scoreboardText.text += $"{score.playerName}: {score.score}\n";
//                 scoreboardText.text += $"{position}. {score.playerName}: {score.score}\n";
//                 position++;
//             }
//         }
//     }
//     public void ShowScoreboard()
//     {
//         if (scoreboardPanel != null)
//         {
//             scoreboardPanel.SetActive(true);
//         }
//     }

//     public void ResetScores()
//     {
//         scores.Clear();
//         UpdateScoreboardUI();
//     }
// }
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerScore // Clase que no necesita MonoBehaviour
{
    public string playerName;
    public int score;

    public PlayerScore(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
}

public class ScoreBoardManager : MonoBehaviour // Este sí hereda de MonoBehaviour
{
    public TextMeshProUGUI scoreboardText; // Texto para mostrar puntuaciones
    public GameObject scoreboardPanel;    // Panel donde se muestra la tabla

    private List<PlayerScore> scores = new List<PlayerScore>();

    public void AddScore(string playerName, int score)
    {
        scores.Add(new PlayerScore(playerName, score));
        scores.Sort((a, b) => b.score.CompareTo(a.score)); // Ordenar de mayor a menor
        UpdateScoreboardUI();
    }

    private void UpdateScoreboardUI()
    {
        if (scoreboardText != null)
        {
            scoreboardText.text = "Tabla de puntuaciones:\n";
            int position = 1;
            foreach (var score in scores)
            {
                scoreboardText.text += $"{position}. {score.playerName}: {score.score}\n";
                position++;
            }
        }
    }

    public void ShowScoreboard()
    {
        if (scoreboardPanel != null)
        {
            scoreboardPanel.SetActive(true);
        }
    }

    public void ResetScores()
    {
        scores.Clear();
        UpdateScoreboardUI();
    }
}

