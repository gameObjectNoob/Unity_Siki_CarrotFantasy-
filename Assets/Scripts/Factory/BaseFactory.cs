using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏物体类型的工厂基类
/// </summary>
public class BaseFactory : IBaseFactory
{
    //当前拥有的GameObject类型资源(UI，UIpanel，Game)——存储的的是游戏物体的资源
    protected Dictionary<string, GameObject> factoryDict = new Dictionary<string, GameObject>();    

    //对象池字典
    protected Dictionary<string, Stack<GameObject>> objectPoolDict = new Dictionary<string, Stack<GameObject>>();
    //对象池(存储的游戏物体)

    //加载路径
    protected string loadPath;
    public BaseFactory() {
        loadPath = "Prefabs/";
    }

    //放入对象池方法
    public void PushItem(string itemName, GameObject item)
    {
        item.SetActive(false);
        item.transform.SetParent(GameManager.Instance.transform);
        if (objectPoolDict.ContainsKey(itemName))
        {
            objectPoolDict[itemName].Push(item);
        }
        else
        {
            Debug.LogError("当前字典没有"+ itemName + "的栈");
        }
    }

    //取实列
    public GameObject GetItem(string itemName)
    {
        GameObject itemGo = null;
        if (objectPoolDict.ContainsKey(itemName))//有对象池的栈时
        {
            if (objectPoolDict[itemName].Count==0)
            {
                GameObject go = GetResource(itemName);
                itemGo = GameManager.Instance.CreateItem(go);
            }
            else
            {
                itemGo = objectPoolDict[itemName].Pop();
                itemGo.SetActive(true);
            }
        }
        else//没有对象池的栈时
        {
            objectPoolDict.Add(itemName,new Stack<GameObject>());
            //这里的go是作为资源存在的，而不是游戏中的物体
            GameObject go = GetResource(itemName);
            //在这里才是生成游戏物体
            itemGo = GameManager.Instance.CreateItem(go);
        }
        if (itemGo==null)
        {
            Debug.LogError(itemName+"的实例获取失败");
        }

        return itemGo;
    }

    //获取资源
    private GameObject GetResource(string itemName) {
        GameObject itemGo = null;
        string itemLoadPath = loadPath + itemName;
        if (factoryDict.ContainsKey(itemName))
        {
            itemGo = factoryDict[itemName];
        }
        else
        {
            itemGo = Resources.Load<GameObject>(itemLoadPath);
            factoryDict.Add(itemName,itemGo);
        }
        if (itemGo==null)
        {
            Debug.LogError(itemName + "的资源获取失败了" + ",失败路径：" + itemLoadPath);
        }

        return itemGo;
    }
}