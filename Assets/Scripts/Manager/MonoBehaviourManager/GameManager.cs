using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏总管理，负责管理所有的管理者
/// </summary>
public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;// 玩家的管理，负责保存以及加载各种玩家以及游戏的信息
    public FactoryManger factoryManger;// 工厂管理，负责管理各种类型的工厂以及对象池
    public AudioSourceManager audioSourceManager;// 负责控制音乐的播放和停止以及游戏中各种音效的播放
    public UIManager uiManager;// 负责管理UI的管理者
    public Stage currentStage;

    private static GameManager _instance;
    public static GameManager Instance {
        get {
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _instance = this;
        playerManager = new PlayerManager();
        factoryManger = new FactoryManger();
        audioSourceManager = new AudioSourceManager();
        uiManager = new UIManager();
        uiManager.mUIFacade.currentSceneState.EnterScene();
    }

    public GameObject CreateItem(GameObject itemGo) {
        GameObject go = Instantiate(itemGo);

        return go;
    }

    //获取Sprite资源
    public Sprite GetSprite(string resourcePath)
    {
        return factoryManger.spriteFactory.GetSingleResources(resourcePath);
    }

    //获取Audio Clip资源
    public AudioClip GetAudioClip(string resourcePath)
    {
        return factoryManger.audioClipFactory.GetSingleResources(resourcePath);
    }

    //获取RuntimeAnimator资源
    public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath)
    {
        return factoryManger.runtiemAnimatorContollerFactory.GetSingleResources(resourcePath);
    }

    //获取游戏物体
    public GameObject GetGameObjectResource(FactoryType factoryType,string resourcePath)
    {
        return factoryManger.factoryDict[factoryType].GetItem(resourcePath);
    }
    //将游戏物体放回对象池
    public void PushGameObjectToFactory(FactoryType factoryType, string resourcePath,GameObject itemGo) {
        Debug.LogError("factoryType="+ factoryType + "  resourcePath=" + resourcePath+ "   itemGo="+ itemGo.name);
        factoryManger.factoryDict[factoryType].PushItem(resourcePath,itemGo);
        Debug.LogError("resourcePath=" + resourcePath);
    }

}
