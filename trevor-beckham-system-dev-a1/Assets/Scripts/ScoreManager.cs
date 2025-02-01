using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //While I know an audio manager script would be more industry standard, I tosed the background audio
    //here so it wouldn't interupt other sources.
    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    private int score = 0;
    public Text scoreText;

    private void Update()
    {
        scoreText.text = "Score: " + score;

    }

    public void AddScore(int linesCleared)
    {
        switch (linesCleared)
        {
            case 1: score += 100; break;
            case 2: score += 300; break;
            case 3: score += 500; break;
            case 4: score += 700; break;
            case 5: score += 600; break;
        }

        Debug.Log($"Score; {score}");
    }
}
