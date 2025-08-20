using UnityEngine;
using System;

[Serializable]
public struct CustomRange
{
    [SerializeField] private float _min;
    [SerializeField] private float _max;

    public CustomRange(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public readonly float RandomValue => UnityEngine.Random.Range(_min, _max);
}
