using UnityEngine;

/// <summary>
/// Отвечает за генерацию препятствий на дороге
/// </summary>
public class Treadmill : ObserverLevelConfig
{
    private float _moveSpeed;
    private bool _canMove = true;

    private void Update()
    {
        if (!_canMove)
            return;

        transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime,
            Space.World);
    }

    public void StartMove()
    {
        _canMove = true;
    }

    public void StopMove()
    {
        _canMove = false;
    }

    public override void Startup(LevelConfig config)
    {
        _moveSpeed = config.speed;
    }

    public override void UpadteSettings(LevelConfig config)
    {
        _moveSpeed = config.speed;
    }
}
