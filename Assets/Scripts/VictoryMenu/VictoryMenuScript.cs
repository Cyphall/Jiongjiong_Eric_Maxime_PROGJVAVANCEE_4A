using UnityEngine;
using UnityEngine.UI;

public class VictoryMenuScript : MonoBehaviour
{
    [SerializeField] private Text victoryTextComponent;

    public void OpenMenu(string winnerName)
    {
        gameObject.SetActive(true);
        
        // if winnerName == null, draw
        victoryTextComponent.text = winnerName != null ? $"Victory of {winnerName}!" : "Draw";
    }
}
