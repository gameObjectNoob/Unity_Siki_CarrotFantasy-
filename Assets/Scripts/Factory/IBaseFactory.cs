using UnityEngine;
/// <summary>
/// 游戏物体工厂接口
/// </summary>
public interface IBaseFactory {

    GameObject GetItem(string itemName);

    void PushItem(string itemName,GameObject item);
    
}