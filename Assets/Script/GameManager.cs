using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public TableDeck tableDeck;
    public PlayerManager player;
    public PlayerManager dealer;
    public TextMeshProUGUI tmpWinLose;

    private void Start()
    {
        StartGame();
    }

    public void OnClickReturnToLobby()
    {
        SceneHelper.LoadScene(SceneHelper.LobbyScene);
    }

    public void StartGame()
    {
        tmpWinLose.gameObject.SetActive(false);
        this.tableDeck.createDeck();
        this.tableDeck.ShuffleDeck();
        player.SetIsPlayerTurn(true);
        dealer.SetIsPlayerTurn(true);
        DealCards();
    }

    public void DealCards()
    {
        player.Hit(tableDeck);
        dealer.Hit(tableDeck);
        player.Hit(tableDeck);
        dealer.Hit(tableDeck);
        dealer.ShowFirstCard();
    }
    public void OnPlayerHit()
    {
        player.Hit(tableDeck);
    }
    public void OnPlayerStand()
    {
        if (player.playerScore > 21)
        {
            CheckWinner();
            return;
        }
        player.SetIsPlayerTurn(false);
        dealer.ShowAllCards();
        while (dealer.playerScore < player.playerScore)
        {
            dealer.Hit(tableDeck);
        }
        CheckWinner();
    }

    public void ResetGame()
    {
        tableDeck.ClearDeck();
        player.ClearCards();
        dealer.ClearCards();
        StartGame();
    }

    public void CheckWinner()
    {
        if (player.playerScore > 21)
        {
            ShowResult("Player Bust");
        }
        else if (dealer.playerScore > 21)
        {
            ShowResult("Dealer Bust");
        }
        else if (player.playerScore == dealer.playerScore)
        {
            ShowResult("Draw");
        }
        else if (player.playerScore > dealer.playerScore)
        {
            ShowResult("Player Win");
        }
        else
        {
            ShowResult("Dealer Win");
        }
    }

    private void ShowResult(string result)
    {
        tmpWinLose.gameObject.SetActive(true);
        tmpWinLose.text = result;
        Debug.Log(result);
    }
}