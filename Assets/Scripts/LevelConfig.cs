using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Data/Level Config", order = 51)]
public class LevelConfig : ScriptableObject
{
    [Header("Main Settings")]
    public int stepActivation = 10;
    public float speed = 10;

    [Header("Group Settings")]
    public int countGroup = 3;
    public float stepByGroup = 100;

    [Header("Obstacle Prefabs Settings")]
    public GameObject[] obstaclePrefabs;
    public int countObstaclePerGroup;

    [Header("Obstacles Group Settings")]
    [Range(0, 2)] public int initialIndexLine = 0;
    public float stepObstacle = 10;
    public EObstacleGenerationStrategy[] generationStrategyForGroup;
}
