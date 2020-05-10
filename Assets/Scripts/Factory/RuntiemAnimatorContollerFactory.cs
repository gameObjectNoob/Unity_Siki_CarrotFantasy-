using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画控制器资源工厂
/// </summary>
public class RuntiemAnimatorContollerFactory : IBaseRescrousFactory<RuntimeAnimatorController>
{
    protected Dictionary<string, RuntimeAnimatorController> factoryDict = new Dictionary<string, RuntimeAnimatorController>();
    protected string loadPath;

    public RuntiemAnimatorContollerFactory()
    {
        loadPath = "Animator/";
    }

    public RuntimeAnimatorController GetSingleResources(string resourcePath)
    {
        RuntimeAnimatorController itemGo = null;
        string itemLoadPath = loadPath + resourcePath;
        if (factoryDict.ContainsKey(resourcePath))
        {
            itemGo = factoryDict[resourcePath];
        }
        else
        {
            itemGo = Resources.Load<RuntimeAnimatorController>(itemLoadPath);
            factoryDict.Add(resourcePath, itemGo);
        }
        if (itemGo == null)
        {
            Debug.LogError(resourcePath + "的资源获取失败了" + ",失败路径：" + itemLoadPath);
        }

        return itemGo;
    }
}
