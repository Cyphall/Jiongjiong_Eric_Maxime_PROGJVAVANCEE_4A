using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bomberman.Menu.MainMenu
{
    public class ChangeSceneScript : MonoBehaviour
    {
        [SerializeField] public string sceneToLoad;
    
        public void ChangeScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
