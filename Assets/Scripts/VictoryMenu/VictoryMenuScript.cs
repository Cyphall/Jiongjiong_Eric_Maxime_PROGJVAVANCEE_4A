using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryMenuScript : MonoBehaviour
{
    [SerializeField] private Text victoryTextComponent;
    
    private List<string> _possibleEndTexts = new List<string>() {"Draw", "Victory Of Player 1", "Victory Of Player 2"};

    public void OpenMenu(int result)
    {
        gameObject.SetActive(true);
        victoryTextComponent.text = _possibleEndTexts[result];
    }
}
