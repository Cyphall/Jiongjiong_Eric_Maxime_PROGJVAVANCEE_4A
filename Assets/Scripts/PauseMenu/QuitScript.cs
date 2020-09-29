using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitScript : MonoBehaviour
{
    [SerializeField] public string SceneToLoadQuit;
    
    public void ChangeSceneQuit()
    {
        
        SceneManager.LoadScene(SceneToLoadQuit);
        Time.timeScale = 1f;
    }
}
