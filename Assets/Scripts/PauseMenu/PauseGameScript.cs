using System.Collections;
using System.Collections.Generic;
using Bomberman.GameManager;
using UnityEngine;

public class PauseGameScript : MonoBehaviour
{
  
    [SerializeField]
    private GameObject PauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManagerScript.Instance.Running)
            {
                GameisUnPause();
            }
            else
            {
                GameisPause();
            }
        }
    }
    
    public void GameisPause()
    {
        PauseMenuUI.SetActive(true);
        GameManagerScript.Instance.Running = false;
    }

    public void GameisUnPause()
    {
        PauseMenuUI.SetActive(false);
        GameManagerScript.Instance.Running = true;
    }
}
