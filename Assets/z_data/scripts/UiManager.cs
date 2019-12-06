using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject progressbarGroup;
    [SerializeField] Image[] ballColorGroup;
    [SerializeField] Text prevLevelText;
    [SerializeField] Text nextLevelText;
    [SerializeField] Text tapToPlayText;
    [SerializeField] Text ballCountText;
    [SerializeField] Image progressbarFiller;
    [SerializeField] Image nextLevelCircle;
    [SerializeField] GameObject tryAgainPopup;
    [SerializeField] GameObject levelCompletedPopup;
    [SerializeField] Image bombImage;

    Tweener bombTweener;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    public void Setup()
    {
        levelCompletedPopup.SetActive(false);
        tryAgainPopup.SetActive(false);
        tapToPlayText.gameObject.SetActive(false);
        hideProgressbarGroup();
        UpdateProgressbar(0f);
        nextLevelCircle.color = Color.white;
    }

    public void ShowProgressbarGroup()
    {
        progressbarGroup.SetActive(true);
    }

    public void hideProgressbarGroup()
    {
        progressbarGroup.SetActive(false);
    }

    public void SetBgColor(Color _color)
    {
        foreach (Image img in ballColorGroup)
        {
            img.color = _color;
            nextLevelText.color = _color;
        }
    }

    public void SetLevelText(int _val)
    {
        prevLevelText.text = (_val + 1).ToString();
        nextLevelText.text = (_val + 2).ToString();
    }

    public void UpdateProgressbar(float _value)
    {
        float xsize = Mathf.Clamp(_value, 0f, 1f);
        progressbarFiller.transform.localScale = new Vector3(xsize, 1f, 1f);
    }

    public void AnimateBomb()
    {
        // bombImage.rectTransform.DOShakeAnchorPos(,)
        bombTweener = bombImage.rectTransform.DOShakeAnchorPos(1f, 10f, 10, 90, true).SetLoops(-1);
    }

    public void StopBombAnimation()
    {
        bombTweener.Kill();
    }

    public void LevelCompleted()
    {
        nextLevelCircle.color = ballColorGroup[0].color;
        nextLevelText.color = Color.white;
        ShowLevelCompeltedPopup();
    }

    public void UpdateBallCountText(int _value)
    {
        if (_value < 0)
            _value = 0;
        ballCountText.text = _value.ToString();
    }

    public void ShowTryAgainPopup()
    {
        tryAgainPopup.SetActive(true);
    }

    public void ShowLevelCompeltedPopup()
    {
        levelCompletedPopup.SetActive(true);
    }
}
