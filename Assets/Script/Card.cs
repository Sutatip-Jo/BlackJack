using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject frontCard;
    public GameObject backCard;
    public Image frontImage;
    public Image backImage;
    public Image iconCardTop;
    public Image iconCardBottom;
    public TextMeshProUGUI tmpRankTop;
    public TextMeshProUGUI tmpRankBottom;
    public CardSuits suit { get; private set; }
    public CardRanks rank { get; private set; }
    public bool isFaceUp;

    public void SetIsFaceUp(bool isActive)
    {
        this.isFaceUp = isActive;
    }
    public void SetSuitCard(Sprite sprite, CardSuits suits)
    {
        this.suit = suits;
        if (iconCardTop != null)
            iconCardTop.sprite = sprite;
        if (iconCardBottom != null)
            iconCardBottom.sprite = sprite;
    }
    public void SetRank(CardRanks rank)
    {
        this.rank = rank;
        string rankName = CardHelper.GetRankToShort(rank);
        if (tmpRankTop != null)
            tmpRankTop.text = rankName;
        if (tmpRankBottom != null)
            tmpRankBottom.text = rankName;
    }
    public void ShowFront()
    {
        if (frontCard != null)
            frontCard.gameObject.SetActive(true);
        if (backCard != null)
            backCard.gameObject.SetActive(false);
    }
    public void ShowBack()
    {
        if (frontCard != null)
            frontCard.gameObject.SetActive(false);
        if (backCard != null)
            backCard.gameObject.SetActive(true);
    }
    public void SetFrontImage(Sprite sprite)
    {
        if (frontImage != null)
            frontImage.sprite = sprite;
    }
    public void SetBackImage(Sprite sprite)
    {
        if (backImage != null)
            backImage.sprite = sprite;
    }
}
