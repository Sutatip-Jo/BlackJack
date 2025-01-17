using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DealerManager : MonoBehaviour
{
    public TableDeck tableDeck;
    public PlayerManager player;
    public PlayerManager dealer;
    public TextMeshProUGUI tmpWinLose;
    private bool isPlayerTurn = true;

    private void Start()
    {
        StartGame();

    }

    public void StartGame()
    {
        tmpWinLose.gameObject.SetActive(false);
        this.tableDeck.createDeck();
        this.tableDeck.ShuffleDeck();
        isPlayerTurn = true;
        DealCards();
    }

    public void DealCards()
    {
        Hit(player);
        Hit(dealer);
        Hit(player);
        Hit(dealer);
    }

    public void OnClickHit()
    {
        Hit(player);
    }

    public void Hit(PlayerManager player)
    {
        if (isPlayerTurn == false)
        {
            return;
        }
        Card card = tableDeck.GetTopCard();
        player.ReceiveCard(card);
        this.tableDeck.RemoveCard(card);
        if (player.playerScore > 21)
        {
            Stand();
        }
    }

    public void Stand()
    {
        isPlayerTurn = false;
        if (player.playerScore > 21)
        {
            CheckWinner();
            return;
        }
        while (dealer.playerScore < player.playerScore)
        {
            Hit(dealer);
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