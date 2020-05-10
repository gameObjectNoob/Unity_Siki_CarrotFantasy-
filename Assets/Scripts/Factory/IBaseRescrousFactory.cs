using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 其他种类资源工厂的接口，每种工厂获取的资源不同，所以我们用泛型接口 
/// </summary>
public interface IBaseRescrousFactory<T> {
    T GetSingleResources(string resourcePath);


}
