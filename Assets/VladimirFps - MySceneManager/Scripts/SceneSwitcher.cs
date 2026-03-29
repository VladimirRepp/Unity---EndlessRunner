using UnityEngine;
using VladimirFps.Utils;

public class SceneSwitcher : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey || Input.GetMouseButton(0))
        {
            if (MySceneManager.GetCurrentSceneName() == "DEMO 1")
            {
                MySceneManager.LoadScene("DEMO 2");
            }
            else
            {
                MySceneManager.LoadScene("DEMO 1");
            }
        }
    }
}
