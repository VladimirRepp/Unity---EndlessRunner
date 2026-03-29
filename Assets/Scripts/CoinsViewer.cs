using TMPro;
using UnityEngine;

public class CoinsViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;

    public void Display(int coins)
    {
        coinsText.text = coins.ToString();
    }
}
