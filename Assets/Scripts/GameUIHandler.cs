using UnityEngine;
using VladimirFps.Utils;

public class GameUIHandler : MonoBehaviour
{
    public void OnClick_RestatThisLevele()
    {
        MySceneManager.LoadScene(MySceneManager.GetCurrentSceneIndex());
    }

    public void OnClick_ReturnToMainMenu()
    {
        MySceneManager.LoadScene(0);
    }

    public void OnClick_ShowRevardedAdv()
    {
        AdvManager.Instance.ShowRevarded();
    }
}
