using UnityEngine;

[CreateAssetMenu(fileName = "NewSkinConfig", menuName = "Data/Skin Config", order = 51)]
public class SkinConfig : ScriptableObject
{
    [Header("Settings")]
    public int id;
    public string name;
    public int cost;
    public GameObject prefab;
}
