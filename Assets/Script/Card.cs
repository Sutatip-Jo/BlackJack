using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image frontImage;
    public Image backImage;
    public Image iconCardTop;
    public Image iconCardBottom;
    public TextMeshProUGUI scoreTop;
    public TextMeshProUGUI scoreBottom;

    public cardFaces face { get; private set; }
    public cardScore score { get; private set; }

    public void SetIconCard(Sprite sprite, cardFaces face)
    {
        this.face = face;
        if (iconCardTop != null)
            iconCardTop.sprite = sprite;
        if (iconCardBottom != null)
            iconCardBottom.sprite = sprite;
    }
    public void SetScore(cardScore score)
    {
        this.score = score;
        if (scoreTop != null)
            scoreTop.text = score.ToString();
        if (scoreBottom != null)
            scoreBottom.text = score.ToString();
    }
    public void ShowFront()
    {
        if (frontImage != null)
            frontImage.gameObject.SetActive(true);
        if (backImage != null)
            backImage.gameObject.SetActive(false);
    }
    public void ShowBack()
    {
        if (frontImage != null)
            frontImage.gameObject.SetActive(false);
        if (backImage != null)
            backImage.gameObject.SetActive(true);
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
