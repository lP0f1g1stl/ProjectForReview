using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _object; // префаб нужного объекта(шар/звезда и т.д)
    [SerializeField] private Vector2 _objectVelocity;
    [SerializeField] private GameObject[] _objects; // пулл объектов
    [SerializeField] private SpawnData _spawnData; // создаете SO с входными данными(цвет, урон, кол-во очков) и цепляете сюда

    private Vector2 _curObjectVelocity;
    private float _spawnTime;
    private int _curObjectId;

    private void Start()
    {
        _curObjectVelocity = _objectVelocity;
        SetSpawnTimeBySpeedAndQuantity();
        CreateObjects();
        StartGame();
    }
    private void SetSpawnTimeBySpeedAndQuantity()
    {
        _spawnTime = (5 / -_curObjectVelocity.y + 3) / _objects.Length; // немного магических цифр (5 - расстоние, 3 - время анимации с запасом)
    }
    private void CreateObjects() 
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
            LaunchBall(_curObjectId);
            _curObjectId++;
            if (_curObjectId == _objects.Length) _curObjectId = 0;
            IncreaseGameSpeed();
            yield return new WaitForSeconds(_spawnTime);
        }
    }
    private void IncreaseGameSpeed() 
    {
        ChangeObjectVelocity();
        SetSpawnTimeBySpeedAndQuantity();
    }
    private void LaunchBall(int i) 
    {
        float posY = Random.Range(-3f, 3f);
        _objects[i].GetComponent<Object>().SetObjectPosition(posY);
        _objects[i].SetActive(true);
        _objects[i].GetComponent<Object>().SetVelocity(_curObjectVelocity);
        int rand = Random.Range(0, _spawnData.GetDataArrayLenght());
        var data = _spawnData.GetData(rand);
        _objects[i].GetComponent<Object>().SetObjectData(data.Item1, data.Item2, data.Item3);
    }
    private void ChangeObjectVelocity()
    {
        _curObjectVelocity += _objectVelocity * 0.01f;
    }
}
