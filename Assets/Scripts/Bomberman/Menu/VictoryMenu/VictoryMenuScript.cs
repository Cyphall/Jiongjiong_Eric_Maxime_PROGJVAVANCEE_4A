using UnityEngine;
using UnityEngine.UI;

namespace Bomberman.Menu.VictoryMenu
{
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
}
