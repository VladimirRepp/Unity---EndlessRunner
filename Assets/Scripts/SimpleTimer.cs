using UnityEngine;

public class SimpleTimer : MonoBehaviour
{
    [SerializeField] private float _needTime;

    private float _timeEleplsed = 0;
    
    private void Update()
    {
        _timeEleplsed += Time.deltaTime;

        if(_timeEleplsed >= _needTime)
        {
            Debug.Log("Время пришло!");
            _timeEleplsed = 0;
        }
    }
}
