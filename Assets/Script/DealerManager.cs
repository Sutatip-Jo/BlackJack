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
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        tableDeck.createDeck();
        tableDeck.ShuffleDeck();
        DealCard();
    }
    public void DealCard()
    {
        Hit(player);
        Hit(dealer);
        Hit(player);
        Hit(dealer);
    }

    public void Hit(PlayerManager player)
    {
        Card card = tableDeck.GetTopCard();
        player.ReceiveCard(card);
        tableDeck.RemoveCard(card);
        if (player.playerScore > 21)
        {
            Stand();
        }
    }
    public void Stand()
    {
        while (dealer.playerScore < 17)
        {
            dealer.ReceiveCard(tableDeck.GetTopCard());
        }
    }
    public void ResetGame()
    {
        player.ClearCard();
        dealer.ClearCard();
        tableDeck.ResetDeck();
        StartGame();
    }

    public void CheckWinner()
    {
        if (player.playerScore > 21)
        {
            Debug.Log("Player Bust");
        }
        else if (dealer.playerScore > 21)
        {
            Debug.Log("Dealer Bust");
        }
        else if (player.playerScore == dealer.playerScore)
        {
            Debug.Log("Draw");
        }
        else if (player.playerScore > dealer.playerScore)
        {
            Debug.Log("Player Win");
        }
        else
        {
            Debug.Log("Dealer Win");
        }
    }
}