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
        // todo: заранее загрузить рекламу дл€ показа
    }

    public void ShowRevarded()
    {
        // todo: запустить рекламу через API рекламодател€ 
        Debug.Log("--> ShowRevarded called!");

        // »митаци€ прослушки API
        bool isRevard = UnityEngine.Random.Range(0f, 1f) >= 0.5f ? true : false;

        // ƒопустим: надо дать вознагрождени€ 
        if (isRevard)
            OnRevarded?.Invoke();

        // »ли: ошибка
        else
            OnErrorRevarded?.Invoke();
    }

    public void ShowInterstion()
    {
        // todo: запустить рекламу через API рекламодател€ 
        Debug.Log("--> ShowInterstionAdv called!");
    }
}
