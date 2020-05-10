using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 状态模式案例
/// </summary>
public class DPState : MonoBehaviour
{

    //public enum PersonState {
    //    EatMeals,
    //    Work,
    //    Sleep
    //}
    //public PersonState personState;

    //private void Start()
    //{
    //    personState = PersonState.Work;
    //    if (personState==PersonState.Work)
    //    {
    //        Debug.Log("工作");
    //    }
    //    else if (personState ==PersonState.EatMeals)
    //    {
    //        Debug.Log("吃饭");
    //    }
    //    else
    //    {
    //        Debug.Log("睡觉");
    //    }
    //}


    Context context;
    private void Start()
    {
        context = new Context();
        context.SetState(new EatMeals(context));
        context.Handle();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            context.SetState(new Work(context));
            context.Handle();
        }
    }

}

public interface IState
{
    void Handle();
}

/// <summary>
/// Context:抽象理解，大概意思是当前的状态
/// 当前的意思为“状态机”
/// </summary>
public class Context
{
    private IState mState;

    public void SetState(IState state) {
        mState = state;
    }

    public void Handle() {
        mState.Handle();//当前状态下需要执行的方法
    }
}

public class EatMeals : IState
{
    //这里的Context作用是记录当前的状态机
    //状态机有多个所以要记录当前实例化方法的状态机是哪个
    //例张三可以吃，李四也能吃。。。
    private Context mContext;
    public EatMeals(Context context)
    {
        mContext = context;
    }

    public void Handle()
    {
        Debug.Log("吃饭");
    }
}
public class Work : IState
{
    private Context mContext;

    public Work(Context context)
    {
        mContext = context;
    }

    public void Handle()
    {
        Debug.Log("工作");
    }
}
public class Sleep : IState
{
    private Context mContext;

    public Sleep(Context context)
    {
        mContext = context;
    }

    public void Handle()
    {
        Debug.Log("睡觉");
    }
}