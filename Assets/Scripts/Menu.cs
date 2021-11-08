using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _showMenu;

    [SerializeField] private GameObject _menu;

    public delegate (int, int) MenuIsActive();
    public static event MenuIsActive IsActive;

    private void Start()
    {
        PlayerHealth.IsActive += ShowMenuWithoutResume;
    }
    public void RestartGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void ResumeGame() 
    {
        _showMenu.gameObject.SetActive(true);
        Time.timeScale = 1;
        _menu.gameObject.SetActive(false);
    }

    public void SetTexts(int scoreText, int highScoreText) 
    {
        _scoreText.text = "Score: " + scoreText.ToString();
        _highScoreText.text = "Highscore: " + highScoreText.ToString();
    }

    public void DeletePlayerdPrefData()
    {
        PlayerPrefs.DeleteAll();
        GetScores();
    }
    public void ShowMenu() 
    {
        _showMenu.gameObject.SetActive(false);
        Time.timeScale = 0;
        _menu.gameObject.SetActive(true);
        GetScores();
    }

    public void GetScores() 
    {
        var scores = IsActive();
        int scoreText = scores.Item1;
        int highScoreText = scores.Item2;
        SetTexts(scoreText, highScoreText);
    }

    public void ShowMenuWithoutResume(bool isActive) 
    {
        _resume.gameObject.SetActive(isActive);
        ShowMenu();
    }

    private void OnDestroy()
    {
        PlayerHealth.IsActive -= ShowMenuWithoutResume;
    }
}
