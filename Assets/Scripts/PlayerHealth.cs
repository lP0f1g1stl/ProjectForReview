using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _playerHealth;
    [SerializeField] private Text _playerHealthText;

    public delegate void GameIsActive(bool isActive);
    public static event GameIsActive IsActive;

    private void Start()
    {
        UpdatePlayerHealthText();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Object>()) 
        {
            _playerHealth -= collision.GetComponent<Object>().GetDamage();
            collision.GetComponent<Object>().StartParticlesAndSoundsOnDestroy();
            CheckPlayerHealth();
            UpdatePlayerHealthText();
        }
    }

    private void CheckPlayerHealth() 
    {
       if(_playerHealth <= 0) 
        {
            Time.timeScale = 0;
            _playerHealth = 0;
            IsActive?.Invoke(false);
        }
        UpdatePlayerHealthText();
    }
    private void UpdatePlayerHealthText() 
    {
        _playerHealthText.text = "HP: " + _playerHealth.ToString();
    }
}
