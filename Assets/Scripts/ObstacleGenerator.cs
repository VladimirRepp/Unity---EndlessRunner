using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleGenerator : ObserverLevelConfig
{ 
    [Header("Event Throwers Settings")]
    [SerializeField] private GameLogic gameLogic;

    [Header("Global Settings")]
    [SerializeField] private Treadmill treadmill;

    [Header("Group Settings")]
    [SerializeField] private int initialPositionByZ = 10;
    [SerializeField] private Transform endPosition;

    [Header("Position Settings")]
    [SerializeField] private int firstLineByX = -5;
    [SerializeField] private int secondLineByX = 0;
    [SerializeField] private int thirdLineByX = 5;

    private LevelConfig _levelConfig;

    private int _currentIndexStrategyForGroup = 0;
    private int _currentIndexLine;
    private bool _diractionLeftToRight = true;

    private List<GameObject> _groups;
    private int _currentIndexGroup = 0;
    private bool _isChecking = false;

    private Coroutine _checkGroupRoutine;

    public override void Startup(LevelConfig config)
    {
        _levelConfig = config;

        InitGroups();

        _checkGroupRoutine = StartCoroutine(CheckGroupRoutine());
    }

    public override void UpadteSettings(LevelConfig config)
    {
        _levelConfig = config;
    }

    private void OnDisable()
    {
        if (_checkGroupRoutine != null)
            StopCoroutine(_checkGroupRoutine);
    }

    private void OnDestroy()
    {
        _isChecking = false;
    }

    private IEnumerator CheckGroupRoutine()
    {
        if (_groups.Count == 0)
            yield break;

        while (true)
        {
            if (_groups.Count > 0)
            {
                GameObject firstGroup = _groups[0];

                 if (firstGroup != null && 
                    firstGroup.transform.position.z <= endPosition.position.z)
                {
                    // Удаляем только первую группу
                    Destroy(firstGroup);
                    _groups.RemoveAt(0);

                    // Создаем новую группу 
                    GameObject newGroup = GenerateObstaclesInGroup();
                    _groups.Add(newGroup);
                }
            }

            yield return null;
        }
    }

    private void InitGroups()
    {
        _groups = new ();

        for (int i = 0; i < _levelConfig.countGroup; i++)
        {
            _groups.Add(GenerateObstaclesInGroup());
        }
    }

    private GameObject GenerateObstaclesInGroup()
    {
        float groupStartZ = 0;

        GameObject group = new GameObject($"ObstaclesGroup_{_currentIndexGroup}");
        group.transform.parent = treadmill.transform;

        EObstacleGenerationStrategy strategyForThisGroup = GetNextStrategy();

        ResetGenerationState(strategyForThisGroup);

        for (int i = 0; i < _levelConfig.countObstaclePerGroup; i++)
        {
            if (_levelConfig.obstaclePrefabs.Length == 0) continue;

            GameObject obstacle = Instantiate(
                _levelConfig.obstaclePrefabs[Random.Range(0, _levelConfig.obstaclePrefabs.Length)]
            );

            Vector3 new_position = Vector3.zero;

            switch (strategyForThisGroup)
            {
                case EObstacleGenerationStrategy.Random:
                    new_position = GetNewPosition_Random(i);
                    break;
                    
                case EObstacleGenerationStrategy.CheckersPattern:
                    new_position = GetNewPosition_CheckersPattern(i);
                    break;

                case EObstacleGenerationStrategy.SnakePattern:
                    new_position = GetNewPosition_SnakePattern(i);
                    break;
            }

            obstacle.transform.localPosition = new_position;
            obstacle.transform.parent = group.transform;

            groupStartZ = obstacle.transform.position.z;
        }

        // todo: проверить шаг генерации групп
        // некотрые группы наслаиваются друг на друга 
        if (_currentIndexGroup == 0)
        {
            groupStartZ = initialPositionByZ;
        }
        else
        {
            Vector3 groupPos = _groups[_groups.Count - 1].transform.position;
            groupStartZ += groupPos.z + _levelConfig.stepByGroup;
        }

        group.transform.position = new Vector3(0, 0, groupStartZ);

        _currentIndexGroup++;
        return group;
    }

    private EObstacleGenerationStrategy GetRandomStrategy()
    {
        int index = Random.Range(0, _levelConfig.
            generationStrategyForGroup.Length);
        return _levelConfig.generationStrategyForGroup[index];
    }

    private EObstacleGenerationStrategy GetNextStrategy()
    {
        int index = _currentIndexStrategyForGroup < _levelConfig.
            generationStrategyForGroup.Length - 1 ?
            _currentIndexStrategyForGroup + 1 : 0;
        _currentIndexStrategyForGroup = index;

        return _levelConfig.generationStrategyForGroup[index];
    }

    private void ResetGenerationState(EObstacleGenerationStrategy strategy)
    {
        _currentIndexLine = _levelConfig.initialIndexLine;
        _diractionLeftToRight = true;

        if (strategy == EObstacleGenerationStrategy.CheckersPattern)
            _currentIndexLine = 1;
    }

    private Vector3 GetNewPosition_Random(int i)
    {
        int prevIndexLine = _currentIndexLine;
        _currentIndexLine = Random.Range(0, 3);

        if (prevIndexLine == _currentIndexLine)
        {
            if (prevIndexLine == 0)
            {
                _currentIndexLine++;
            }
            else if (prevIndexLine == 1)
            {
                _currentIndexLine = Random.Range(0, 2) == 0 ? 0 : 2;
            }
            else
            {
                _currentIndexLine--;
            }
        }

       return GetPositionByLine(_currentIndexLine, _levelConfig.stepObstacle * i);
    }

    private Vector3 GetNewPosition_SnakePattern(int i)
    {
        Vector3 new_position = GetPositionByLine(_currentIndexLine, _levelConfig.stepObstacle * i);

        if (_diractionLeftToRight)
        {
            if (_currentIndexLine < 2)
            {
                _currentIndexLine++;
            }
            else
            {
                _currentIndexLine = 1;
                _diractionLeftToRight = false;
            }
        }
        else
        {
            if (_currentIndexLine > 0)
            {
                _currentIndexLine--;
            }
            else
            {
                _currentIndexLine = 1;
                _diractionLeftToRight = true;
            }
        }

        return new_position;
    }

    private Vector3 GetNewPosition_CheckersPattern(int i)
    {
        Vector3 new_position = GetPositionByLine(_currentIndexLine, _levelConfig.stepObstacle * i);
        _currentIndexLine = _currentIndexLine < 2 ? _currentIndexLine + 1 : 0;
        return new_position;
    }

    private Vector3 GetPositionByLine(int lineIndex, float zPosition)
    {
        switch (lineIndex)
        {
            case 0:
                return new Vector3(firstLineByX, -0.1f, zPosition);
            case 1:
                return new Vector3(secondLineByX, -0.1f, zPosition);
            case 2:
                return new Vector3(thirdLineByX, -0.1f, zPosition);
            default:
                return Vector3.zero;
        }
    }
}

public enum EObstacleGenerationStrategy
{
    Random,
    SnakePattern,
    CheckersPattern
}