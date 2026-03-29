using System.Collections;
using TMPro;
using UnityEngine;

public class SkinDecorator : MonoBehaviour
{
    [Header("Skin Settings")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private float stepBySkins;
    [SerializeField] private SkinConfig[] skinConfigs;

    public Transform StartPoint => startPoint;
    public float StepBySkins => stepBySkins;
    public SkinConfig[] SkinConfigs => skinConfigs;

    private void Awake()
    {
        StartCoroutine(CreateSkinRoutine());
    }

    private IEnumerator CreateSkinRoutine()
    {
        for(int i = 0; i < skinConfigs.Length; i++)
        {
            if(i != 0 && i % 10 == 0)
                yield return null;

            GameObject skin = Instantiate(skinConfigs[i].prefab);
            skin.transform.parent = transform;
            skin.transform.position = new Vector3(i * stepBySkins, 0, 0);
        }
    }
}
