using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Depenedncies Settings")]
    [SerializeField] private PlayerInput input;

    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float step;
    [SerializeField] private float distanceAccuracy = 0.01f;
    [SerializeField] private int countLines = 3;
    [SerializeField] private int initialIndexLine = 1;

    private int _currentIndexLine;
    private Vector3 _movePosition;

    private void Awake()
    {
        _currentIndexLine = initialIndexLine;
        _movePosition = transform.position;
    }

    private void OnEnable()
    {
        input.OnLeft += HandleInputLeft;
        input.OnRight += HandleInputRight;
    }

    private void OnDisable()
    {
        input.OnLeft -= HandleInputLeft;
        input.OnRight -= HandleInputRight;
    }

    private void Update()
    {
        if (Vector3.Distance(_movePosition, transform.position) >= distanceAccuracy)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                _movePosition, 
                speed * Time.deltaTime);
        }
        else
            transform.position = _movePosition;
    }

    private void HandleInputRight()
    {
        if (_currentIndexLine == countLines - 1)
            return;

        _currentIndexLine++;
        _movePosition.x += step;
    }

    private void HandleInputLeft()
    {
        if (_currentIndexLine == 0)
            return;

        _currentIndexLine--;
        _movePosition.x -= step;
    }
}
