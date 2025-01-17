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
    public Dictionary<string, IObjectPool<Card>> dicPool = new Dictionary<string, IObjectPool<Card>>();
    [SerializeField] private Transform poolPoint;

    public void SetPooling(cardFaces faces, cardScore score)
    {
        IObjectPool<Card> card = new ObjectPool<Card>(OnCreatePool, OnGetPool, OnReleasePool);
        if (dicPool.ContainsKey(CardHelper.GetCardId(faces, score).ToString()))
        {
            return;
        }
        dicPool.Add(CardHelper.GetCardId(faces, score).ToString(), card);
    }
    public Card GetPool(cardFaces faces, cardScore score, Sprite sprite)
    {
        if (!dicPool.ContainsKey(CardHelper.GetCardId(faces, score).ToString()))
        {
            SetPooling(faces, score);
        }
        Card card = dicPool[CardHelper.GetCardId(faces, score).ToString()].Get();
        card.SetScore(score);
        card.SetIconCard(sprite, faces);
        return card;
    }
    public void ReleasePool(Card card)
    {
        dicPool[CardHelper.GetCardId(card.face, card.score).ToString()].Release(card); ;
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

