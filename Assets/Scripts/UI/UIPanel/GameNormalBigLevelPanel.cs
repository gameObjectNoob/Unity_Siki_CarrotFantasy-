using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 大关卡选择面板
/// </summary>
public class GameNormalBigLevelPanel : BasePanel
{
    public Transform bigLevelContentTrans;//滚动视图的content
    public int bigLevelPageCount;//大关卡总数
    private SlideScrollView slideScrollView;
    private PlayerManager playerManager;
    private Transform[] bigLevelPage;//大关卡数组

    private bool hasRigisterEvent;

    protected override void Awake()
    {
        base.Awake();
        playerManager = mUIFacade.mPlayerManager;
        bigLevelPage = new Transform[bigLevelPageCount];
        slideScrollView = transform.Find("Scroll View").GetComponent<SlideScrollView>();
        //显示大关卡信息
        for (int i = 0; i < bigLevelPageCount; i++)
        {
            bigLevelPage[i] = bigLevelContentTrans.GetChild(i);
            //TODO 将这里的"5"换成playerManager里的一个数组
            ShowBigLevelState(playerManager.unLockedNormalModelBigLevelList[i],playerManager.unLockedNormalModelLevelNum[i],5,bigLevelPage[i],i+1);
        }
        hasRigisterEvent = true;
    }

    private void OnEnable()
    {
        for (int i = 0; i < bigLevelPageCount; i++)
        {
            bigLevelPage[i] = bigLevelContentTrans.GetChild(i);
            //TODO 将这里的"5"换成playerManager里的一个数组
            ShowBigLevelState(playerManager.unLockedNormalModelBigLevelList[i], playerManager.unLockedNormalModelLevelNum[i], 5, bigLevelPage[i], i + 1);
        }
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        slideScrollView.Init();
        gameObject.SetActive(true);
    }
    public override void ExitPanel()
    {
        base.ExitPanel();
        gameObject.SetActive(false);
    }

    //显示大关卡信息
    public void ShowBigLevelState(bool unLocked,int unLockedLevelNum,int totalNum,Transform theBigLevelButtonTrans,int bigLevelID) {
        if (unLocked)//已解锁
        {
            theBigLevelButtonTrans.Find("Img_Lock").gameObject.SetActive(false);
            theBigLevelButtonTrans.Find("Img_Page").gameObject.SetActive(true);
            theBigLevelButtonTrans.Find("Img_Page/Tex_Page").GetComponent<Text>().text = unLockedLevelNum+"/"+totalNum;

            Button theBigLevelButton = theBigLevelButtonTrans.GetComponent<Button>();
            theBigLevelButton.interactable = false;
            if (!hasRigisterEvent)
            {
                theBigLevelButton.onClick.AddListener(() =>
                {
                    //退出大关卡选择界面
                    mUIFacade.currentScenePanelDict[StringManager.GameNormalBigLevelPanel].ExitPanel();
                    //进入小关卡选择界面
                    GameNormalLevelPanel gameNormalLevelPanel = mUIFacade.currentScenePanelDict[StringManager.GameNormalLevelPanel] as GameNormalLevelPanel;
                    gameNormalLevelPanel.ToThisPanel(bigLevelID);
                    //设置
                    GameNormalOptionPanel gameNormalOptionPanel = mUIFacade.currentScenePanelDict[StringManager.GameNormalOptionPanel] as GameNormalOptionPanel;
                    gameNormalOptionPanel.isInBigLevelPanel = false;
                });
            }
        }
        else//未解锁
        {
            theBigLevelButtonTrans.Find("Img_Lock").gameObject.SetActive(true);
            theBigLevelButtonTrans.Find("Img_Page").gameObject.SetActive(false);
            theBigLevelButtonTrans.GetComponent<Button>().interactable = false;
        }
    }

    //翻页按钮方法
    public void ToNextPage()
    {
        slideScrollView.ToNextPage();
    }
    public void ToLastPage()
    {
        slideScrollView.ToLastPage();
    }
}