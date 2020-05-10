using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPFactory : MonoBehaviour
{
    private void Start()
    {
        FactoryIPhone8 factoryIPhone8 = new FactoryIPhone8();
        factoryIPhone8.CreateIPhone();
        factoryIPhone8.CreateIPhoneCharger();
        
        FactoryIPhoneX factoryIPhoneX = new FactoryIPhoneX();
        factoryIPhoneX.CreateIPhone();
        factoryIPhoneX.CreateIPhoneCharger();
    }
}
//工厂模式分为简单工厂模式、工厂方法模式和抽象工厂模式

//抽象工厂
//手机
public interface IPhone
{

}

public class IPhone8 : IPhone
{
    public IPhone8() {
        Debug.Log("IPhone8");
    }
}

public class IPhoneX : IPhone
{
    public IPhoneX()
    {
        Debug.Log("IPhoneX");
    }
}
//充电器
public interface IPhoneCharger
{

}

public class IPhone8Charger : IPhoneCharger
{
    public IPhone8Charger()
    {
        Debug.Log("IPhone8Charger");
    }
}

public class IPhoneXCharger : IPhoneCharger
{
    public IPhoneXCharger()
    {
        Debug.Log("IPhoneXCharger");
    }
}

public interface IFactory
{
    IPhone CreateIPhone();
    IPhoneCharger CreateIPhoneCharger();
}

public class FactoryIPhone8 : IFactory
{
    public IPhone CreateIPhone()
    {
        return new IPhone8();
    }

    public IPhoneCharger CreateIPhoneCharger()
    {
        return new IPhone8Charger();
    }
}
public class FactoryIPhoneX : IFactory
{
    public IPhone CreateIPhone()
    {
        return new IPhoneX();
    }

    public IPhoneCharger CreateIPhoneCharger()
    {
        return new IPhoneXCharger();
    }
}

////简单工厂模式
//public class IPhone {
//    public IPhone(){}
//}

//public class IPhone8 : IPhone {
//    public IPhone8() { }
//}

//public class IPhoneX : IPhone {
//    public IPhoneX() { }
//}

//public interface IFactory {
//    IPhone CreateIPhone();
//}

//public class FactoryIPhone8 : IFactory
//{
//    public IPhone CreateIPhone()
//    {
//        return new IPhone8();
//    }
//}
//public class FactoryIPhoneX : IFactory
//{
//    public IPhone CreateIPhone()
//    {
//        return new IPhoneX();
//    }
//}

//使用工厂模式的原因
//public class BullectOne : MonoBehaviour
//{
//    //共有代码
//    private AudioClip audioClip;
//    private AudioSource audioSource;
//    private void Start()
//    {
//        audioClip = Resources.Load<AudioClip>("****");
//        audioSource.clip = audioClip;
//        Destroy(gameObject, 4f);
//    }
//    //其他特有代码
//}
//public class BullectTwo : MonoBehaviour
//{
//    //共有代码
//    private AudioClip audioClip;
//    private AudioSource audioSource;
//    private void Start()
//    {
//        audioClip = Resources.Load<AudioClip>("****");
//        audioSource.clip = audioClip;
//        Destroy(gameObject, 4f);
//    }
//    //其他特有代码
//}
//public class BullectThree : MonoBehaviour
//{
//    //共有代码
//    private AudioClip audioClip;
//    private AudioSource audioSource;
//    private void Start()
//    {
//        audioClip = Resources.Load<AudioClip>("****");
//        audioSource.clip = audioClip;
//        Destroy(gameObject, 4f);
//    }
//    //其他特有代码
//}
//public class ButtonOne : MonoBehaviour
//{
//    //共有代码
//    private AudioClip audioClip;
//    private AudioSource audioSource;
//    private void Start()
//    {
//        audioClip = Resources.Load<AudioClip>("****");
//        audioSource.clip = audioClip;
//        Destroy(gameObject, 4f);
//    }
//    //其他特有代码
//}
