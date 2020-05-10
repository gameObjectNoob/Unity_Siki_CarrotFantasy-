using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    ////饿汉式单例——在实例化Singleton时就给_instance赋值
    ////没有着线程安全问题 但是 有实例化顺序的问题
    //private static Singleton _instance;
    //public static Singleton Instance
    //{
    //    get
    //    {
    //        return _instance;
    //    }
    //}
    //private void Awake()
    //{
    //    _instance = this;
    //}

    ////懒汉式单例——在使用时才会赋值
    ////有着线程安全问题 但是 没有实例化顺序的问题
    //private static Singleton _instance;
    //public static Singleton Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = new Singleton();
    //        }
    //        return _instance;
    //    }
    //}
}

public abstract class SingletonTemplate<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance {
        get {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this as T;
    }
}
