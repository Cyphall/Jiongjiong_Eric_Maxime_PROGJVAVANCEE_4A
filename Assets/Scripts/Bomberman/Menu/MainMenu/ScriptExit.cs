﻿using UnityEngine;

namespace Bomberman.Menu.MainMenu
{
    public class ScriptExit : MonoBehaviour
    {
        public void Do_Exit_game()
        {
            Debug.Log("Vous avez quitté le jeu");
            Application.Quit();
        }
    }
}