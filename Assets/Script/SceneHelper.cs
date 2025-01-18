using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHelper
{
    public const string LobbyScene = "Lobby";
    public const string BlackJackScene = "BlackJackScene";

    public static void LoadScene(string sceneKey)
    {
        SceneManager.LoadScene(sceneKey);
    }
}