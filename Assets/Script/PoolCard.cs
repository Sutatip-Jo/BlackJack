using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolCard : MonoBehaviour
{
    #region Singleton
    private static PoolCard instance = null;
    public static PoolCard Instance
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
        if (poolPoint == null)
        {
            poolPoint = this.transform;
        }
    }
    #endregion
    public Dictionary<int, IObjectPool<Card>> dicPool = new Dictionary<int, IObjectPool<Card>>();
    [SerializeField] private Transform poolPoint;

    public void SetPooling(CardSuits suit, CardRanks rank)
    {
        IObjectPool<Card> card = new ObjectPool<Card>(OnCreatePool, OnGetPool, OnReleasePool);
        if (dicPool.ContainsKey(CardHelper.GetCardId(suit, rank)))
        {
            return;
        }
        dicPool.Add(CardHelper.GetCardId(suit, rank), card);
    }
    public Card GetPool(CardSuits suit, CardRanks rank, Sprite sprite)
    {
        if (!dicPool.ContainsKey(CardHelper.GetCardId(suit, rank)))
        {
            SetPooling(suit, rank);
        }
        Card card = dicPool[CardHelper.GetCardId(suit, rank)].Get();
        card.SetRank(rank);
        card.SetSuitCard(sprite, suit);
        return card;
    }
    public void ReleasePool(Card card)
    {
        dicPool[CardHelper.GetCardId(card.suit, card.rank)].Release(card); ;
    }

    public Card OnCreatePool()
    {
        return Instantiate(CardAssets.Instance.cardPrefab, poolPoint);
    }
    public void OnGetPool(Card card)
    {
        card.gameObject.SetActive(true);
    }
    public void OnReleasePool(Card card)
    {
        card.transform.SetParent(poolPoint);
        card.gameObject.transform.localPosition = Vector3.zero;
        card.gameObject.SetActive(false);
    }

}

