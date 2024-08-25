using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterUI : MonoBehaviour
{
    public Image DisplayImage;
    public TMP_Text Title;
    public TMP_Text Description;
    public TMP_Text Log;
    public Button BtnExecute, BtnRetreat, BtnContinue;
    public RectTransform EventPanel, ResultPanel;

    public void SetUIContents(Sprite _sprite, string _title, string _description)
        => StartCoroutine(SetUIContentsIE(_sprite, _title, _description));

    public void SetDescription(string _description)
        => StartCoroutine(SetDescriptionIE(_description));

    public void SetResultLog(string _log) => Log.text = _log;

    public void ShowButton(Button _btn) => _btn.gameObject.SetActive(true);
    public void HideButton(Button _btn) => _btn.gameObject.SetActive(false);

    public void ShowResult()
    {
        EventPanel.DOAnchorPosY(-1000, 1);
        ResultPanel.DOAnchorPosY(0, 1);
    }

    public void ShowEncounter()
    {
        EventPanel.DOAnchorPosY(0, 1);
        ResultPanel.DOAnchorPosY(-1000, 1);
    }

    IEnumerator SetUIContentsIE(Sprite _sprite, string _title, string _description)
    {
        HideContents();
        yield return new WaitForSeconds(1);

        DisplayImage.sprite = _sprite;
        Title.text = _title;
        Description.text = _description;
        ShowContents();
    }

    IEnumerator SetDescriptionIE(string _des)
    {
        Description.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        Description.text = _des;
        Description.DOFade(1, 1f);
    }

    

    void ShowContents()
    {
        DisplayImage.DOFade(1, 1);
        Title.DOFade(1, 1);
        Description.DOFade(1, 1);
    }

    void HideContents()
    {
        DisplayImage.DOFade(0, 1);
        Title.DOFade(0, 1);
        Description.DOFade(0, 1);
    }
}
