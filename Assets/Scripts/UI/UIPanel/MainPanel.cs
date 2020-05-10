using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainPanel : BasePanel
{
    private Animator carrotAnimator;
    private Transform monsterTrans;
    private Transform cloudTrans;

    private Tween[] mainPanelTween;//0 右   1 左
    private Tween exitTween;

    protected override void Awake()
    {
        base.Awake();
        transform.SetSiblingIndex(8);
        carrotAnimator = transform.Find("Emp_Carrot").GetComponent<Animator>();
        carrotAnimator.Play("CarrotGrow");
        monsterTrans = transform.Find("Img_Monster").GetComponent<Transform>();
        cloudTrans = transform.Find("Img_Cloud").GetComponent<Transform>();

        mainPanelTween = new Tween[2];
        mainPanelTween[0] = transform.DOLocalMoveX(1920, 0.5f);
        mainPanelTween[0].SetAutoKill(false);
        mainPanelTween[0].Pause();
        mainPanelTween[1] = transform.DOLocalMoveX(-1920, 0.5f);
        mainPanelTween[1].SetAutoKill(false);
        mainPanelTween[1].Pause();
    }
    //j进入退出方法
    public override void EnterPanel()
    {
        transform.SetSiblingIndex(8);
        carrotAnimator.Play("CarrotGrow");
        if (exitTween!=null)
        {
            exitTween.PlayBackwards();
        }
        cloudTrans.gameObject.SetActive(true);
    }
    public override void ExitPanel()
    {
        exitTween.PlayForward();
        cloudTrans.gameObject.SetActive(false);
    }
    //UI动画播放
    private void PlayUITween() {
        //怪物动画
        monsterTrans.DOLocalMoveY(600f, 1.5f).SetLoops(-1,LoopType.Yoyo);
        //云动画
        cloudTrans.DOLocalMoveX(1300,8f).SetLoops(-1,LoopType.Restart);
    }
    public void MoveToRight()
    {
        exitTween = mainPanelTween[0];
        mUIFacade.currentScenePanelDict[StringManager.SetPanel].EnterPanel();
    }
    public void MoveToLeftt()
    {
        exitTween = mainPanelTween[1];
        mUIFacade.currentScenePanelDict[StringManager.HelpPanel].EnterPanel();
    }
    //场景状态切换的方法
    public void ToNormalModeScene()
    {
        mUIFacade.currentScenePanelDict[StringManager.GameLoadPanel].EnterPanel();
        mUIFacade.ChangeSceneState(new NormalGameOptionSceneState(mUIFacade));
    }
    public void ToBossModeScene()
    {
        mUIFacade.currentScenePanelDict[StringManager.GameLoadPanel].EnterPanel();
        mUIFacade.ChangeSceneState(new BossGameOptionSceneState(mUIFacade));
    }
    public void ToMonsterNestScene()
    {
        mUIFacade.currentScenePanelDict[StringManager.GameLoadPanel].EnterPanel();
        mUIFacade.ChangeSceneState(new MonsterNestSceneState(mUIFacade));
    }
    public void ExitGame() {
        Application.Quit();
    }
}
