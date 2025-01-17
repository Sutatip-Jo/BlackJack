using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TableDeck : MonoBehaviour
{
    public Transform transformDeck;
    public Dictionary<int, Card> deckCards = new Dictionary<int, Card>();

    public void createDeck()
    {
        for (int i = 0; i < CardAssets.Instance.suitCardIcon.Count; i++)
        {
            Sprite sprite = CardAssets.Instance.suitCardIcon[i];
            for (int j = 1; j <= 13; j++)
            {
                deckCards.Add(CardHelper.GetCardId((CardSuits)i, (CardRanks)j), PoolCard.Instance.GetPool((CardSuits)i, (CardRanks)j, sprite));
            }
        }
        ShuffleDeck();
    }
    public void AddCard(Card card)
    {
        deckCards.Add(CardHelper.GetCardId(card.suit, card.rank), card);
    }
    public void RemoveCard(Card card)
    {
        deckCards.Remove(CardHelper.GetCardId(card.suit, card.rank));
    }
    public void ResetDeck()
    {
        foreach (KeyValuePair<int, Card> card in deckCards)
        {
            PoolCard.Instance.ReleasePool(card.Value);
        }
        deckCards.Clear();
        createDeck();
    }
    public void ShuffleDeck()
    {
        var shuffledDeck = deckCards.OrderBy(x => UnityEngine.Random.value).ToDictionary(item => item.Key, item => item.Value);
        deckCards = shuffledDeck;
    }
    public Card GetCard(int id)
    {
        return deckCards[id];
    }
    public Card GetTopCard()
    {
        return deckCards.First().Value;
    }
}
