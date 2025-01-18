using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public void OnClickStartGame()
    {
        SceneHelper.LoadScene(SceneHelper.BlackJackScene);
    }
    public void OnClickExitGame()
    {
        Application.Quit();
    }
}