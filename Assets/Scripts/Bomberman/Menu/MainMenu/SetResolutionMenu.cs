using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bomberman.Menu.MainMenu
{
    public class SetResolutionMenu : MonoBehaviour
    {
        [SerializeField] private Dropdown resolutionDropdown;
        private Resolution[] resolutions;
        // Start is called before the first frame update
        private void Start()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
        
            int currentResolutionIndex = 0;
            List<string> options = new List<string>();

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].width == Screen.currentResolution.width)
                {
                    currentResolutionIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetFullScreen(bool isFullEcran)
        {
            Screen.fullScreen = isFullEcran;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
        }
    }
}
