using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 小关卡选择面板
/// </summary>
public class GameNormalLevelPanel : BasePanel
{
    private string filePath;//文件路径
    public int currentBigLevelID;//大关卡ID
    public int currentLevelID;//小关卡ID
    private string theSpritePath;

    private Transform levelContentTrans;//关卡列表的Scroll View的Content
    private Transform emp_TowerTrans;//在地图下显示塔的trans
    private Image img_BGLeft;//左下角的装饰图片
    private Image img_BGRight;//右下角的装饰图片
    private Image img_Carrot;//萝卜的状态(胜利后的奖杯  金萝卜>银萝卜>普通萝卜)
    private Image img_AllClear;//消除全部道具的奖杯
    private Text text_TotalWaves;//当前关卡总共有几波怪的text
    private Button btn_Start;//开始游戏按钮

    private PlayerManager playerManager;
    private SlideScrollView slideScrollView;
    private List<GameObject> levelContentImageGos;//关卡图片的列表
    private List<GameObject> towerContentImageGos;//塔图片的列表

    protected override void Awake()
    {
        base.Awake();
        filePath = "GameOption/Normal/Level/";
        playerManager = mUIFacade.mPlayerManager;
        levelContentImageGos = new List<GameObject>();
        towerContentImageGos = new List<GameObject>();
        levelContentTrans = transform.Find("Scroll View/Viewport/Content");
        emp_TowerTrans = emp_TowerTrans.Find("Emp_Tower");
        img_BGLeft = transform.Find("Img_BGLeft").GetComponent<Image>();
        img_BGRight = transform.Find("Img_BGRight").GetComponent<Image>();
        text_TotalWaves = transform.Find("Img_TotalWaves/Tex_TotalWaves").GetComponent<Text>();
        btn_Start = transform.Find("Btn_Start").GetComponent<Button>();
        slideScrollView = transform.Find("Scroll View").GetComponent<SlideScrollView>();
        currentBigLevelID = 1;
        currentLevelID = 1;
    }

    //更新地图UI的方法(动态UI)
    public void UpdateMapUI(string spritePath) {
        img_BGLeft.sprite = mUIFacade.GetSprite(spritePath + "BG_Left");
        img_BGRight.sprite = mUIFacade.GetSprite(spritePath + "BG_Right");
        for (int i = 0; i < 5; i++)
        {
            levelContentImageGos.Add(CreateUIAndSetUIPosition("Img_Level", levelContentTrans));
            //更换关卡图片
            levelContentImageGos[i].GetComponent<Image>().sprite = mUIFacade.GetSprite(spritePath+ "Level_"+i+1);
            Stage stage = playerManager.unLockedNormalModelLevelList[(currentBigLevelID-1)*5+i];
            levelContentImageGos[i].transform.Find("Img_Carrot").gameObject.SetActive(false);
            levelContentImageGos[i].transform.Find("Img_AllClear").gameObject.SetActive(false);

            if (stage.unLocked)
            {//已解锁
                if (stage.mAllClear)
                {
                    levelContentImageGos[i].transform.Find("Img_AllClear").gameObject.SetActive(true);
                }
                if (stage.mCarrotState != 0)
                {
                    Image carrotImage = levelContentImageGos[i].transform.Find("Img_Carrot").GetComponent<Image>();
                    carrotImage.gameObject.SetActive(true);
                    carrotImage.sprite = mUIFacade.GetSprite(filePath + "Carrot_" + stage.mCarrotState);
                    levelContentImageGos[i].transform.Find("Img_Lock").gameObject.SetActive(false);
                    levelContentImageGos[i].transform.Find("Img_BG").gameObject.SetActive(false);
                }

            }
            else
            {//未解锁
                if (stage.mIsRewardLevel)
                {//奖励关卡
                    levelContentImageGos[i].transform.Find("Img_Lock").gameObject.SetActive(false);
                    levelContentImageGos[i].transform.Find("Img_BG").gameObject.SetActive(true);
                    Image monsterPetImage = levelContentImageGos[i].transform.Find("Img_BG/Img_Monster").GetComponent<Image>();
                    //TODO 在Stage里添加隐藏关卡解锁条件的宝宝ID
                    monsterPetImage.sprite = mUIFacade.GetSprite("MonsterNest/Monster/Baby/"+currentBigLevelID);
                    monsterPetImage.SetNativeSize();
                    monsterPetImage.transform.localScale = new Vector3(2,2,2);
                }
                else
                {//不是奖励关卡
                    levelContentImageGos[i].transform.Find("Img_Lock").gameObject.SetActive(true);
                    levelContentImageGos[i].transform.Find("Img_BG").gameObject.SetActive(false);
                }
            }
        }
        //TODO 这里的5换成小地图个数
        //设置滚动视图Content的大小
        slideScrollView.SetContentLength(5);
    }

    //销毁地图卡
    private void DestoryMapUI() {
        if (levelContentImageGos.Count>0)
        {
            //TODO jia将5换成PlayManager里的值
            for (int i = 0; i < 5; i++)
            {
                mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory, "Img_Level", levelContentImageGos[i]);
            }
            slideScrollView.InitScrollLength();
            levelContentImageGos.Clear();
        }
    }

    //更新静态UI
    public void UpdateLevelUI(string SpritePath)
    {
        if (towerContentImageGos.Count>=0)
        {
            for (int i = 0; i < towerContentImageGos.Count; i++)
            {
                //直接替换有极小概率出现从当前图片闪现到目标图片的效果
                //为避免这种情况就事先置空一下
                towerContentImageGos[i].GetComponent<Image>().sprite = null;
                mUIFacade.PushGameObjectToFactory(FactoryType.UIFactory, "Img_Tower", towerContentImageGos[i]);
            }
        }
        Stage stage = playerManager.unLockedNormalModelLevelList[(currentBigLevelID-1)*5+currentLevelID-1];
        if (stage.unLocked)
        {
            //已解锁
            btn_Start.interactable = false;
        }
        else
        {
            //未解锁
            btn_Start.interactable = true;
        }
        text_TotalWaves.text = stage.mTotalRound.ToString();
        for (int i = 0; i < stage.mTowerIDListLength; i++)
        {
            towerContentImageGos.Add(CreateUIAndSetUIPosition("Img_Tower", emp_TowerTrans));
            towerContentImageGos[i].GetComponent<Image>().sprite = mUIFacade.GetSprite(filePath + "Tower/Tower_" + stage.mTowerIDList[i]);
        }
    }

    //进入退出当前页面的方法
    /// <summary>
    /// 外部调用   进入当前页面
    /// </summary>
    /// <param name="currentBigLevel"></param>
    public void ToThisPanel(int currentBigLevel) {
        currentBigLevelID = currentBigLevel;
        currentLevelID = 1;
        EnterPanel();
    }
    public override void InitPanel()
    {
        base.InitPanel();
        gameObject.SetActive(false);
    }
    public override void EnterPanel()
    {
        base.EnterPanel();
        gameObject.SetActive(true);
        theSpritePath = filePath + currentBigLevelID + "/";
        DestoryMapUI();
        UpdateMapUI(theSpritePath);
        UpdateLevelUI(theSpritePath);
        slideScrollView.Init();
    }
    public override void UpdatePanel()
    {
        base.UpdatePanel();
        theSpritePath = filePath + currentBigLevelID + "/";
        UpdateLevelUI(theSpritePath);
    }
    public override void ExitPanel()
    {
        base.ExitPanel();
        gameObject.SetActive(false);
    }
    public void ToGamePanel()
    {
        GameManager.Instance.currentStage = playerManager.unLockedNormalModelLevelList[(currentBigLevelID-1)*5+currentLevelID-1];
        mUIFacade.currentScenePanelDict[StringManager.GameLoadPanel].EnterPanel();
        mUIFacade.ChangeSceneState(new NormalModelSceneState(mUIFacade));
    }
    //加载资源的方法
    /// <summary>
    /// 资源预加载 (此方法在功能上没有作用，只是预先加载一次可能要用到的资源，减轻资源多了过后程序卡的情况)
    /// </summary>
    private void LoadResource()
    {
        mUIFacade.GetSprite(filePath + "AllClear");
        mUIFacade.GetSprite(filePath + "Carrot_1");
        mUIFacade.GetSprite(filePath + "Carrot_2");
        mUIFacade.GetSprite(filePath + "Carrot_3");
        //TODO 这里的1，4  1，5都存在PlayerManager里
        for (int i = 1; i < 4; i++)
        {
            string spritePath = filePath + i + "/";
            mUIFacade.GetSprite(spritePath + "BG_Left");
            mUIFacade.GetSprite(spritePath + "BG_Right");
            for (int j = 1; j < 6; j++)
            {
                mUIFacade.GetSprite(spritePath + "Level_" + j);
            }
            for (int j = 1; j < 13; j++)
            {
                mUIFacade.GetSprite(filePath + "Tower/Tower_" + j);
            }
        }
    }

    /// <summary>
    /// 实例化UI
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="parentTrans"></param>
    /// <returns></returns>
    public GameObject CreateUIAndSetUIPosition(string uiName,Transform parentTrans) {
        GameObject itemGo=mUIFacade.GetGameObjectResource(FactoryType.UIFactory,uiName);
        itemGo.transform.SetParent(parentTrans);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
        return itemGo;
    }

    public void ToNextLevel() {
        currentLevelID++;
        UpdatePanel();
    }
    public void ToLastLevel()
    {
        currentLevelID--;
        UpdatePanel();
    }
}
