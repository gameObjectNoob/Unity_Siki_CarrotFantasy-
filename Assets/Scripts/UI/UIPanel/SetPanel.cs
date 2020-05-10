using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SetPanel : BasePanel
{
    private GameObject optionPageGo;
    private GameObject statisicsPageGo;
    private GameObject producerPageGo;
    private GameObject panel_ResetGo;

    private Tween setPanelTwen;
    private bool playBGMusic = true;
    private bool playEffectMusic = true;
    public Sprite[] btnSprites;//0.音效开 1音效关  2.背景音乐开 3.背景音乐关 
    private Image img_Btn_EffectAudio;
    private Image img_Btn_BGAudio;
    public Text[] statisicesTexts;

    protected override void Awake()
    {
        base.Awake();
        setPanelTwen = transform.DOLocalMoveX(0, 0.5f);
        setPanelTwen.SetAutoKill(false);
        setPanelTwen.Pause();

        optionPageGo = transform.Find("OptionPage").gameObject;
        statisicsPageGo = transform.Find("StatisticsPage").gameObject;
        producerPageGo = transform.Find("ProducerPage").gameObject;
        img_Btn_EffectAudio = optionPageGo.transform.Find("Btn_EffectAudio").GetComponent<Image>();
        img_Btn_BGAudio = optionPageGo.transform.Find("Btn_BGAudio").GetComponent<Image>();
        panel_ResetGo = transform.Find("Panel_Reset").gameObject;
    }

    public override void InitPanel()
    {
        transform.localPosition = new Vector3(-1920, 0, 0);
        transform.SetSiblingIndex(2);
    }
    //显示页面的方法
    public void ShowOptionPage()
    {
        optionPageGo.SetActive(true);
        statisicsPageGo.SetActive(false);
        producerPageGo.SetActive(false);
    }
    public void ShowStatisicsPage()
    {
        optionPageGo.SetActive(false);
        statisicsPageGo.SetActive(true);
        producerPageGo.SetActive(false);
        ShowStatistics();
    }
    public void ShowProducerPage()
    {
        optionPageGo.SetActive(false);
        statisicsPageGo.SetActive(false);
        producerPageGo.SetActive(true);
    }

    //进入退出的方法
    public override void EnterPanel()
    {
        ShowOptionPage();
        MoveToCenter();
    }

    public override void ExitPanel()
    {
        setPanelTwen.PlayBackwards();
        mUIFacade.currentScenePanelDict[StringManager.MainPanel].EnterPanel();
        InitPanel();
    }

    public void MoveToCenter() {
        setPanelTwen.PlayForward();
    }
    //音乐处理
    public void CloserOrOpenBGMusic()
    {
        playBGMusic = !playBGMusic;
        mUIFacade.CloseOrOpenBGMusic();
        if (playBGMusic)
        {
            img_Btn_BGAudio.sprite = btnSprites[2];
        }
        else
        {
            img_Btn_BGAudio.sprite = btnSprites[3];
        }
    }
    public void CloserOrOpenEffectMusic()
    {
        playEffectMusic = !playEffectMusic;
        mUIFacade.CloseOrOpenEffectMusic();
        if (playEffectMusic)
        {
            img_Btn_EffectAudio.sprite = btnSprites[0];
        }
        else
        {
            img_Btn_EffectAudio.sprite = btnSprites[1];
        }
    }
    //数据显示
    public void ShowStatistics() {
        PlayerManager playerManager = mUIFacade.mPlayerManager;
        statisicesTexts[0].text = playerManager.adventrueModelNum.ToString();
        statisicesTexts[1].text = playerManager.burrideLevelNum.ToString();
        statisicesTexts[2].text = playerManager.bossModelNum.ToString();
        statisicesTexts[3].text = playerManager.coin.ToString();
        statisicesTexts[4].text = playerManager.killMonsterNum.ToString();
        statisicesTexts[5].text = playerManager.killBossNum.ToString();
        statisicesTexts[6].text = playerManager.clearItemNum.ToString();
    }

    //重置游戏
    public void ResetGame() {

    }

    public void ShowResetPanel() {
        panel_ResetGo.SetActive(true);
    }
    public void CloseResetPanel()
    {
        panel_ResetGo.SetActive(false);
    }
}
