﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptExit : MonoBehaviour
{
    public void Do_Exit_game()
    {
        Debug.Log("Vous avez quittez le jeu");
        Application.Quit();
    }
}
