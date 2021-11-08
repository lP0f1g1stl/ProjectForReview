using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Spawn Data", menuName = "Spawn Data")]
public class SpawnData : ScriptableObject
{
    [SerializeField] private Data[] _data;

    public int GetDataArrayLenght() 
    {
        return _data.Length;
    }
    public (Color, int, int) GetData(int id) 
    {
        var data = _data[id].GetSpawnData();
        return data;
    }
}
[System.Serializable]
public struct Data
{
    [SerializeField] private Color _color;
    [SerializeField] private int _damage;
    [SerializeField] private int _points;

    public (Color, int, int) GetSpawnData()
    {
        var data = (_color, _damage, _points);
        return data;
    }
}

