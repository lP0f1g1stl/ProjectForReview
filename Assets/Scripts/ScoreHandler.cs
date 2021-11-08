using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private int _highScore;
    private int _score;

    private void Start()
    {
        Object.IsClicked += ChangeScore;
        PlayerHealth.IsActive += IsActive;
        Menu.IsActive += ShowScores;
        ChangeScoreText();
        LoadScore();
    }

    private void ChangeScore(int points) 
    {
        _score += points;
        ChangeScoreText();
    }

    private void ChangeScoreText() 
    {
        _scoreText.text = "Score: " + _score.ToString();
    }

    private void IsActive(bool isActive) 
    {
        SaveScore();
    }

    private void SaveScore() 
    {
        if(_score > _highScore) PlayerPrefs.SetInt("HighScore", _score);
    }

    private void LoadScore() 
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
    }

    private (int, int) ShowScores()
    {
        SaveScore();
        _scoreText.gameObject.SetActive(false);
        LoadScore();
        var scores = (_score, _highScore);
        return scores;
    }

    private void OnDestroy()
    {
        Object.IsClicked -= ChangeScore;
        PlayerHealth.IsActive -= IsActive;
        Menu.IsActive -= ShowScores;
    }
}
