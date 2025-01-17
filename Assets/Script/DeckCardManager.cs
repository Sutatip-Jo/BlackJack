using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class DeckCardManager : MonoBehaviour
{
    public Transform transformDeck;
    public List<Card> deckCards = new List<Card>();
    public PoolCard poolCard;

    void Start()
    {
        createDeck();
    }
    private void createDeck()
    {
        for (int i = 0; i < CardAssets.Instance.cardFaces.Count; i++)
        {
            Sprite sprite = CardAssets.Instance.cardFaces[i];
            for (int j = 1; j <= 13; j++)
            {
                // Card card = Instantiate(CardAssets.Instance.cardPrefab, transformDeck);
                // card.SetScore((cardScore)j);
                // card.SetIconCard(sprite, (cardFaces)i);
                // deckCards.Add(card);
                deckCards.Add(poolCard.GetPool((cardFaces)i, (cardScore)j, sprite));
            }
        }
    }
    public void AddCard(Card card)
    {
        deckCards.Add(card);
    }
    public void RemoveCard(Card card)
    {
        deckCards.Remove(card);
    }
    public void ShuffleDeck()
    {
        for (int i = 0; i < deckCards.Count; i++)
        {
            Card temp = deckCards[i];
            int randomIndex = UnityEngine.Random.Range(i, deckCards.Count);
            deckCards[i] = deckCards[randomIndex];
            deckCards[randomIndex] = temp;
        }
    }
}
