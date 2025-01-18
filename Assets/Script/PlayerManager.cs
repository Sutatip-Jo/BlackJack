using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    private Dictionary<int, Card> playerCard = new Dictionary<int, Card>();
    public Transform transformPlayer;
    public int playerScore { get; private set; }
    public TextMeshProUGUI tmpScore;
    public bool isPlayerTurn;
    public void SetIsPlayerTurn(bool isActive)
    {
        this.isPlayerTurn = isActive;
    }
    public void SetScoreText()
    {
        if (tmpScore != null)
        {
            // tmpScore.text = "Score: " + playerScore;
            tmpScore.text = playerScore.ToString();
        }
    }
    public void Hit(TableDeck deck)
    {
        if (playerScore > 21 || !isPlayerTurn)
        {
            return;
        }
        Card card = deck.GetTopCard();
        ReceiveCard(card);
        deck.RemoveCard(card);
        ShowCard(card);
        if (playerScore > 21)
        {
            Stand();
        }
    }
    public void Stand()
    {
        isPlayerTurn = false;
        GameManager.Instance.OnPlayerStand();
    }
    public void AddCard(Card card)
    {
        if (playerCard.ContainsKey(CardHelper.GetCardId(card.suit, card.rank)))
        {
            Debug.Log("Card already exists");
            return;
        }
        playerCard.Add(CardHelper.GetCardId(card.suit, card.rank), card);
    }
    public void RemoveCard(Card card)
    {
        playerCard.Remove(CardHelper.GetCardId(card.suit, card.rank));
    }
    public void ShowCard(Card card)
    {
        if (playerCard.ContainsKey(CardHelper.GetCardId(card.suit, card.rank)))
        {
            card.ShowFront();
            card.SetIsFaceUp(true);
        }
        CalculateScore();
    }
    public void HideCard(Card card)
    {
        if (playerCard.ContainsKey(CardHelper.GetCardId(card.suit, card.rank)))
        {
            card.ShowBack();
            card.SetIsFaceUp(false);
        }
        CalculateScore();
    }
    public void ShowFirstCard()
    {
        HideAllCards();
        playerCard.First().Value.ShowFront();
        playerCard.First().Value.SetIsFaceUp(true);
        CalculateScore();
    }
    public void ShowAllCards()
    {
        foreach (KeyValuePair<int, Card> card in playerCard)
        {
            card.Value.ShowFront();
            card.Value.SetIsFaceUp(true);
        }
        CalculateScore();
    }
    public void HideAllCards()
    {
        foreach (KeyValuePair<int, Card> card in playerCard)
        {
            card.Value.ShowBack();
            card.Value.SetIsFaceUp(false);
        }
        CalculateScore();
    }
    public void ClearCards()
    {
        foreach (KeyValuePair<int, Card> card in playerCard)
        {
            PoolCard.Instance.ReleasePool(card.Value);
        }
        playerCard.Clear();
        playerScore = 0;
        SetScoreText();
    }
    public void ReceiveCard(Card card)
    {
        AddCard(card);
        card.transform.SetParent(transformPlayer);
    }

    private void CalculateScore()
    {
        playerScore = 0;
        int aceCount = 0;

        foreach (var card in playerCard.Values)
        {
            if (!card.isFaceUp)
            {
                continue;
            }
            switch (card.rank)
            {
                case CardRanks.Ace:
                    aceCount++;
                    playerScore += 11;
                    break;
                case CardRanks.Two:
                    playerScore += 2;
                    break;
                case CardRanks.Three:
                    playerScore += 3;
                    break;
                case CardRanks.Four:
                    playerScore += 4;
                    break;
                case CardRanks.Five:
                    playerScore += 5;
                    break;
                case CardRanks.Six:
                    playerScore += 6;
                    break;
                case CardRanks.Seven:
                    playerScore += 7;
                    break;
                case CardRanks.Eight:
                    playerScore += 8;
                    break;
                case CardRanks.Nine:
                    playerScore += 9;
                    break;
                case CardRanks.Ten:
                case CardRanks.Jack:
                case CardRanks.Queen:
                case CardRanks.King:
                    playerScore += 10;
                    break;
            }
        }

        while (playerScore > 21 && aceCount > 0)
        {
            playerScore -= 10;
            aceCount--;
        }
        SetScoreText();
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }
}