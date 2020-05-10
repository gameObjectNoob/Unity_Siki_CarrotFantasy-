using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOP : MonoBehaviour
{
    private void Start()
    {
        BaseHero myHero = new Garen();
       

        myHero.SkillQ();
        myHero.SkillW();
        myHero.hp = 10;
    }
}

public interface IHero {
    void SkillQ();
    void SkillW();
    void SkillE();
    void SkillR();
}

public class BaseHero :IHero
{
    public int hp;

    public void SkillQ()
    {
        Debug.Log("玩家按下了Q键");
    }
    public virtual void SkillW()
    {
        Debug.Log("玩家按下了W键");
    }
    public void SkillE()
    {
        Debug.Log("玩家按下了E键");
    }
    public void SkillR()
    {
        Debug.Log("玩家按下了R键");
    }
}

public class Garen : BaseHero
{
    public new void SkillQ()
    {
        base.SkillQ();
        Debug.Log("致命打击");
    }
    public override void SkillW()
    {
        Debug.Log("勇气");
    }
    public void SkillE()
    {
        Debug.Log("审判");
    }
    public void SkillR()
    {
        Debug.Log("德玛西亚正义");
    }
}
public class Tryndamere : BaseHero
{
    public void SkillQ()
    {
        Debug.Log("嗜血杀戮");
    }
    public void SkillW()
    {
        Debug.Log("蔑视");
    }
    public void SkillE()
    {
        Debug.Log("旋风斩");
    }
    public void SkillR()
    {
        Debug.Log("无尽怒火");
    }
}
