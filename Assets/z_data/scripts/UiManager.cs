using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    public void Setup()
    {
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
        // print("_value " + _value);
        // print("xsize " + xsize);
        progressbarFiller.transform.localScale = new Vector3(xsize, 1f, 1f);
    }

    public void LevelCompleted()
    {
        nextLevelCircle.color = ballColorGroup[0].color;
        nextLevelText.color = Color.white;
    }
}
