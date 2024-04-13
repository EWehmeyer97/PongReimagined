using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject matchPoint;
    public Text scoreText;
    public Text scoreHighlight;
    public Animator anim;
    int score = 0;
    int playTo = 5;

    public void PointScored()
    {
        anim.SetTrigger("Play");
        score++;
        if (score == playTo - 1)
            matchPoint.SetActive(true);
        else if (score == playTo)
            victoryScreen.SetActive(true);
    }
    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        scoreHighlight.text = score.ToString();

        if (score != playTo)
            BallManager.Instance.SpawnBall();
    }
}
