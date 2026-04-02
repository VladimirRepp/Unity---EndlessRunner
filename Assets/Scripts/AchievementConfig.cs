using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievementConfig", menuName = "Data/Achievement Config", order = 51)]
public class AchievementConfig : ScriptableObject
{
    public int id;
    public Sprite icon;
    public string header;
    public string description;
    public int referenceState;

    public bool Verify(int currentState)
    {
        return currentState == referenceState;
    }
}
