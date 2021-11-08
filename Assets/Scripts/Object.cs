using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    private int _damage;
    private int _points;

    private bool _isActive;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particleSystem;

    public delegate void OnClick(int points);
    public static event OnClick IsClicked;

    private void Awake()
    {
        _rb = GetComponent <Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        _isActive = true;
        PlayerHealth.IsActive += IsActive;
    }
    private void SetColor(Color ballColor) 
    {
        _spriteRenderer.color = ballColor;
        _particleSystem.startColor = ballColor;
    }

    public int GetDamage() 
    {
        return _damage;
    }
    public void SetVelocity(Vector2 velocity) 
    {
        _rb.velocity = velocity;
    }
    public void SetBallData(Color color, int damage,int points) 
    {
        _damage = damage;
        _points = points;
        SetColor(color);
    }

    public void SetBallPosition(float rand)
    {
        gameObject.transform.position = new Vector2(rand, 3);
    }

    public void StartParticlesAndSoundsOnDestroy() 
    {
        _particleSystem.Play();
        _spriteRenderer.color = Color.clear;
        _rb.velocity = new Vector2(0, 0);
        IsClicked?.Invoke(0);
    }
    private void IsActive(bool isActive) 
    {
        _isActive = isActive;
    }
    private void OnMouseDown()
    {
        if (_isActive) 
        {
            IsClicked?.Invoke(_points);
            StartParticlesAndSoundsOnDestroy();
            StartCoroutine(Disable());
        }
    }

    private IEnumerator Disable() 
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerHealth.IsActive -= IsActive;
    }
}
