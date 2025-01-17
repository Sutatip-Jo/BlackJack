using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    private Dictionary<int, Card> playerCard = new Dictionary<int, Card>();
    public Transform transformPlayer;
    public int playerScore { get; private set; }
    public TextMeshProUGUI tmpScore;
    public void SetScoreText()
    {
        if (tmpScore != null)
        {
            // tmpScore.text = "Score: " + playerScore;
            tmpScore.text = playerScore.ToString();
        }
    }
    public void AddCard(Card card)
    {
        if (playerCard.ContainsKey(CardHelper.GetCardId(card.suit, card.rank)))
        {
            Debug.Log("Card already exists");
            return;
        }
        playerCard.Add(CardHelper.GetCardId(card.suit, card.rank), card);
        CalculateScore();
    }
    public void RemoveCard(Card card)
    {
        playerCard.Remove(CardHelper.GetCardId(card.suit, card.rank));
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
        card.ShowFront();
    }

    private void CalculateScore()
    {
        playerScore = 0;
        int aceCount = 0;

        foreach (var card in playerCard.Values)
        {
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