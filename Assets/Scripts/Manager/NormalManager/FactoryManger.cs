using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工厂管理，负责管理各种类型的工厂以及对象池
/// </summary>
public class FactoryManger : MonoBehaviour
{
    //存储所有GameObject对象的工厂
    public Dictionary<FactoryType, IBaseFactory> factoryDict = new Dictionary<FactoryType, IBaseFactory>();
    //AudioClip的工厂
    public AudioClipFactory audioClipFactory;
    //Sprite的工厂
    public SpriteFactory spriteFactory;
    //AnimatorContoller的工厂
    public RuntiemAnimatorContollerFactory runtiemAnimatorContollerFactory;

    public FactoryManger()
    {
        factoryDict.Add(FactoryType.UIPanelFactory, new UIPanelFactory());
        factoryDict.Add(FactoryType.UIFactory, new UIFactory());
        factoryDict.Add(FactoryType.GameFactory, new GameFactory());
        audioClipFactory = new AudioClipFactory();
        spriteFactory = new SpriteFactory();
        runtiemAnimatorContollerFactory = new RuntiemAnimatorContollerFactory();

    }
}
