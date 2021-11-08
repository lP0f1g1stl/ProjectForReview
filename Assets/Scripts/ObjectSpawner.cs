using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _spawnTime;
    [SerializeField] private Vector2 _ballVelocity;
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private SpawnData _spawnData;

    private int _curBall;

    private void Start()
    {
        CreateBalls();
        StartGame();
    }

    private void CreateBalls() 
    {
        for(int i = 0; i < _objects.Length; i++) 
        {
            _objects[i] = Instantiate(_object, gameObject.transform);
            _objects[i].SetActive(false);
        }
    }

    private void StartGame() 
    {
        StartCoroutine(SpawnTimer());
    }
    private IEnumerator SpawnTimer() 
    {
        while (true)
        {
            LaunchBall(_curBall);
            _curBall++;
            if (_curBall == _objects.Length) _curBall = 0;
            IncreaseGameSpeed();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    private void IncreaseGameSpeed() 
    {
        ChangeSpawnTime();
        ChangeBallVelocity();
    }
    private void LaunchBall(int i) 
    {
        float posY = Random.Range(-3f, 3f);
        _objects[i].GetComponent<Object>().SetBallPosition(posY);
        _objects[i].SetActive(true);
        _objects[i].GetComponent<Object>().SetVelocity(_ballVelocity);
        int rand = Random.Range(0, _spawnData.GetDataArrayLenght());
        var data = _spawnData.GetData(rand);
        _objects[i].GetComponent<Object>().SetBallData(data.Item1, data.Item2, data.Item3);

    }
    private void ChangeBallVelocity()
    {
        _ballVelocity *= 1.01f;
    }
    private void ChangeSpawnTime()
    {
        _spawnTime *= 0.99f;
    }
}
