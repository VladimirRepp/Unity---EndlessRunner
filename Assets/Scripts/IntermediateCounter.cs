using UnityEngine;

public class IntermediateCounter : MonoBehaviour
{
    [SerializeField] private int displayFrequency = 3; // раз в три показа

    private void Start()
    {
        if ((++AdvManager.Instance.CountLaunchIntermediateScene) % displayFrequency == 0)
            AdvManager.Instance.ShowInterstion();
    }
}
