using System;
using UnityEngine;

public class AdvManager : Singleton<AdvManager>, IInitialized
{
    private int _countLaunchIntermediateScene = 0;

    public int CountLaunchIntermediateScene
    {
        get => _countLaunchIntermediateScene;
        set => _countLaunchIntermediateScene = value;
    }

    public Action OnRevarded;
    public Action OnErrorRevarded;

    public void Startup()
    {
        // todo: здесь инициализация рекламного SDK для показа
    }

    public void ShowRevarded()
    {
        // todo: показать рекламу через API рекламной сети 
        Debug.Log("--> ShowRevarded called!");

        // вызов рекламного API - имитация результата
        bool isRevard = UnityEngine.Random.Range(0f, 1f) >= 0.5f ? true : false;

        // проверка: если реклама вознаграждена 
        if (isRevard)
            OnRevarded?.Invoke();

        // иначе: ошибка
        else
            OnErrorRevarded?.Invoke();
    }

    public void ShowRevarded(Action<bool> callback)
    {
        // todo: показать рекламу через API рекламной сети 
        Debug.Log("--> ShowRevarded called!");

        // вызов рекламного API - имитация результата
        bool isRevard = UnityEngine.Random.Range(0f, 1f) >= 0.5f ? true : false;

        // проверка: если реклама вознаграждена 
        if (isRevard)
            callback?.Invoke(true);

        // иначе: ошибка
        else
            callback?.Invoke(false);
    }

    public void ShowInterstion()
    {
        // todo: показать рекламу через API рекламной сети 
        Debug.Log("--> ShowInterstionAdv called!");
    }
}