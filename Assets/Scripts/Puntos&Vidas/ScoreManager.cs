using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    //public AudioSource pointSound;
    public TextMeshProUGUI scoreTextMesh; // Referencia directa
    public AudioSource pointSound;
    public AudioSource pointLostSound;
    private int score = 0;

    private void Start()
    {
        scoreTextMesh = GameObject.Find("Score_txt").GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }

    public void AddPoint()
    {
        //pointSound?.Play(); // Solo reproduce sonido si está asignado
        score++;
        pointSound.Play();
        UpdateScoreText();
    }

    public void RemovePoint()
    {
        //pointSound?.Play();
        if (score > 0){ 
            score--;
        } 
        pointLostSound.Play();

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreTextMesh != null)
        {
            scoreTextMesh.text = "SCORE: " + score;
        }
    }

    public int GetScore()
    {
        return score;
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        Debug.Log("Puntuación reiniciada.");
    }

}
