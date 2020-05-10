using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责管理UI的管理者
/// </summary>
public class UIManager
{
    public UIFacade mUIFacade;//UI中介
    public Dictionary<string, GameObject> currentScenePanelDict;//存放UI对象的Dictionary
    private GameManager mGameManager;

    public UIManager()
    {
        mGameManager = GameManager.Instance;
        currentScenePanelDict = new Dictionary<string, GameObject>();
        mUIFacade = new UIFacade(this);
        mUIFacade.currentSceneState = new StartLoadSceneState(mUIFacade);
    }

    //添加UIPanel到UIManager字典
    public void AddPanelToDict(string uiPanelName)
    {
        currentScenePanelDict.Add(uiPanelName, mUIFacade.GetGameObjectResource(FactoryType.UIPanelFactory, uiPanelName));
    }

    //将UIPanel放回工厂
    private void PushUIPanel(string uiPanelName,GameObject uiPanelGo) {
        mGameManager.PushGameObjectToFactory(FactoryType.UIPanelFactory,uiPanelName,uiPanelGo);
    }

    //清空字典
    public void ClearDict()
    {
        foreach (var item in currentScenePanelDict)
        {
            Debug.LogError("name="+ item.Value.name.Substring(0, item.Value.name.Length - 7)+ ",item.Value="+item.Value.name);
            PushUIPanel(item.Value.name.Substring(0,item.Value.name.Length-7),item.Value);
            Debug.LogError("ClearDictForeachEndOne");
        }

        currentScenePanelDict.Clear();
        Debug.LogError("ClearDictForeachEnd");
    }
}
