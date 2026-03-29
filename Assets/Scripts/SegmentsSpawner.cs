using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Точка входа <br/>
/// Инициализация игры, запуск уровня 
/// </summary>
public class SegmentsSpawner : MonoBehaviour
{
    [Header("Global Settings")]
    [SerializeField] private Treadmill _treadmill;

    [Header("Move Settings")]
    [SerializeField] private float _moveSpeed;

    [Header("Spawn Settings")]
    [SerializeField] private Transform _endPoint;

    [SerializeField] private GameObject[] _segmentsPregabs;

    [Header("Pool Settings")]
    [SerializeField] private int _poolSize = 6;
    [SerializeField] private int _countActivePerTime = 3;
    [SerializeField] private float _stepSpawn = 10;

    private List<GameObject> _segmentsPool;

    private int _headIndexPool;  // голова пула сегментов 
    private int _tailIndexPool;  // хвост пула сегментов 

    private void Awake()
    {
        if (_countActivePerTime > _poolSize)
        {
            Debug.LogError("_countActivePerTime > _poolSize");
        }

        _headIndexPool = 0;
        _tailIndexPool = _countActivePerTime - 1;

        InitPool();
    }

    private void InitPool()
    {
        // note: нужна ли проверка префабов для пула? (скорее да)
        // todo: проверка на повторения + проверка на префаб (для оптимизации кода)

        _segmentsPool = new();

        int j = 0;
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject segment = Instantiate(_segmentsPregabs[j]);
            j = j < _segmentsPregabs.Length - 1 ? j + 1 : 0;

            //if (segment.TryGetComponent(out Treadmill segmentMovement))
            //{
            //    segmentMovement.moveSpeed = _moveSpeed;
            //}
            //else
            //{
            //    Debug.LogError("SegmentMovement IN segment IS NULL!");
            //}

            segment.transform.parent = _treadmill.transform;

            Vector3 new_pos = new(0, 0, i * _stepSpawn);
            segment.transform.position = new_pos;

            if (i >= _countActivePerTime)
                segment.SetActive(false);

            _segmentsPool.Add(segment);
        }
    }

    private void Update()
    {
        if (_segmentsPool[_headIndexPool].transform.position.z
            - _stepSpawn * 0.5f <=
            _endPoint.position.z)
        {
            ReorderSegments();
        }
    }

    private void ReorderSegments()
    {
        // 1) выключаем голову 
        _segmentsPool[_headIndexPool].SetActive(false);

        // 2) переключаем индекс головы (на следующую)
        _headIndexPool = _headIndexPool < _segmentsPool.Count - 1 ?
            _headIndexPool + 1 : 0;

        // 3) включаем следующий за хвостом 
        int nextToTail = _tailIndexPool < _segmentsPool.Count - 1 ?
            _tailIndexPool + 1 : 0;
        _segmentsPool[nextToTail].SetActive(true);

        // 4) перемещаем его на позицию за текущим хвостом
        Vector3 currentPosNextToTail = _segmentsPool[_tailIndexPool]
            .transform.position;
        currentPosNextToTail.z += _stepSpawn - (_stepSpawn * 0.01f);
        _segmentsPool[nextToTail].transform.position =
            currentPosNextToTail;

        // 5) переключаем хвост на следующий сегмент (новый хвост)
        _tailIndexPool = nextToTail;
    }
}