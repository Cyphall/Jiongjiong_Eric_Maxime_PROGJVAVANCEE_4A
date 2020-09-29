using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameScript : MonoBehaviour
{
  
    public static bool GameisPaused = false;
    [SerializeField] private GameObject PauseMenuUI;
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
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
        Time.timeScale = 0f;
    }

    public void GameisUnPause()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
