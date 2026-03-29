using System;
using UnityEngine;

public class PlayerChecksObstacles : MonoBehaviour
{
    [SerializeField] private bool isCitCodeImmortality = false;
    [SerializeField] private string obstacleTag = "Obstacle";

    public Action OnCrashed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(obstacleTag))
        {
            if (isCitCodeImmortality)
            {
                Debug.Log("--> CitCodeImmortality is Active AND obstacle was detected!");
                return;
            }

            OnCrashed?.Invoke();
        }
    }
}
