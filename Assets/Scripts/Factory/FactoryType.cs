using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 不同种类的游戏物体工厂 
/// </summary>
public enum FactoryType
{
    UIPanelFactory,
    UIFactory,
    GameFactory//这里可以细分比如子弹工厂，怪物工厂，塔工厂等等
}
