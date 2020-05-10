using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// UI中介，上层与管理者们做交互，下层与UI面板做交互
/// </summary>
public class UIFacade
{
    //管理者
    private UIManager mUIManager;
    private GameManager mGameManager;
    private AudioSourceManager mAudioSourceManager;
    public PlayerManager mPlayerManager;
    //UI面板
    public Dictionary<string, IBasePanel> currentScenePanelDict = new Dictionary<string, IBasePanel>();
    //其他成员变量
    private GameObject mask;
    private Image maskImage;
    public Transform canvasTransform;
    //场景状态
    public IBaseSceneState currentSceneState;
    public IBaseSceneState lastSceneState;


    public UIFacade(UIManager uIManager)
    {
        mGameManager = GameManager.Instance;
        mPlayerManager = mGameManager.playerManager;
        mUIManager = uIManager;
        mAudioSourceManager = mGameManager.audioSourceManager;
        InitMask();
    }

    //初始化遮罩
    public void InitMask() {
        canvasTransform = GameObject.Find("Canvas").transform;
        // mask = mGameManager.factoryManger.factoryDict[FactoryType.UIFactory].GetItem("Img_Mask");
        mask = CreateUIAndSetUIPosition("Img_Mask");
        maskImage = mask.GetComponent<Image>();
    }

    //改变当前场景的状态
    public void ChangeSceneState(IBaseSceneState baseSceneState) {
        lastSceneState = currentSceneState;
        ShowMask();
        currentSceneState = baseSceneState;
    }

    //显示遮罩
    public void ShowMask() {
        mask.transform.SetSiblingIndex(10);
        Tween t = DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, Color.black, 2f);
        t.OnComplete(ExitSceneComplete);
    }

    //离开当前场景
    private void ExitSceneComplete() {
        Debug.LogError("lastSceneState=" + lastSceneState);
        lastSceneState.ExitScene();
        currentSceneState.EnterScene();
        HideMask();
    }

    //隐藏遮罩
    public void HideMask()
    {
        mask.transform.SetSiblingIndex(10);
        DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0,0,0,0), 2f);
    }
    //实例化当前场景所有面板，并存入字典
    public void InitDict() {
        foreach (var item in mUIManager.currentScenePanelDict)
        {
            item.Value.transform.SetParent(canvasTransform);
            item.Value.transform.localPosition = Vector3.zero;
            item.Value.transform.localScale = Vector3.one;
            IBasePanel basePanel = item.Value.GetComponent<IBasePanel>();
            if (basePanel==null)
            {
                Debug.LogError("获取面板上IBasePanel脚本失败");
            }
            basePanel.InitPanel();
            currentScenePanelDict.Add(item.Key,basePanel);
        }
    }

    //清空UIPanel字典
    public void ClearDict() {
        currentScenePanelDict.Clear();
        Debug.LogError("mUIManager=" + mUIManager);
        mUIManager.ClearDict();
        Debug.LogError("currentScenePanelDict="+mUIManager);
    }

    //添加UIPanel到UIManager字典
    public void AddPanelToDict(string uiPanelName) {
        mUIManager.AddPanelToDict(uiPanelName);
    }

    //实例化UI
    public GameObject CreateUIAndSetUIPosition(string uiName) {
        GameObject itemGo = GetGameObjectResource(FactoryType.UIFactory,uiName);
        itemGo.transform.SetParent(canvasTransform);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
        return itemGo;
    }

    //获取资源
    public Sprite GetSprite(string resourcePath) {
        return mGameManager.GetSprite(resourcePath);
    }
    public AudioClip GetAudioSource(string resourcePath) {
        return mGameManager.GetAudioClip(resourcePath);
    }
    public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath) {
        return mGameManager.GetRuntimeAnimatorController(resourcePath);
    }
    //获取游戏物体
    public GameObject GetGameObjectResource(FactoryType factoryType, string resourcePath)
    {
        return mGameManager.GetGameObjectResource(factoryType, resourcePath);
    }
    //将游戏物体放回对象池
    public void PushGameObjectToFactory(FactoryType factoryType, string resourcePath, GameObject itemGo)
    {
        mGameManager.PushGameObjectToFactory(factoryType,resourcePath,itemGo);
    }

    //开关音乐
    public void CloseOrOpenBGMusic()
    {
        mAudioSourceManager.closeOrOpenBGMusic();
    }
    public void CloseOrOpenEffectMusic()
    {
        mAudioSourceManager.closeOrOpenEffectMusic();
    }

} 