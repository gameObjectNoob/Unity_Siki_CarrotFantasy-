using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家的管理，负责保存以及加载各种玩家以及游戏的信息
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public int adventrueModelNum;//已解锁冒险模式地图数量
    public int burrideLevelNum;  //已解锁隐藏地图数量
    public int bossModelNum;     //已解锁boss模式地图数量
    public int coin;             //总共获得金钱数量
    public int killMonsterNum;   //打死怪物总数
    public int killBossNum;      //打死boss总数
    public int clearItemNum;     //摧毁道具总数

    /// <summary>
    /// 大关卡解锁状态信息
    /// </summary>
    public List<bool> unLockedNormalModelBigLevelList;
    /// <summary>
    /// 小关卡信息
    /// </summary>
    public List<Stage> unLockedNormalModelLevelList;
    /// <summary>
    /// 小关卡解锁数
    /// </summary>
    public List<int> unLockedNormalModelLevelNum;

    //怪物窝
    public int cookies; //饼干
    public int milk;    //牛奶
    public int nest;    //窝
    public int diamands;//钻石

    //用于测试
    public PlayerManager()
    {
        adventrueModelNum = 100;
        burrideLevelNum = 100;
        bossModelNum = 100;
        coin = 100;
        killMonsterNum = 100;
        killBossNum = 100;
        clearItemNum = 100;
        unLockedNormalModelBigLevelList = new List<bool>()
        {
            true,true,true
        };
        unLockedNormalModelLevelList = new List<Stage>()
        {
            new Stage(10,2,new int[]{ 1,2},false,0,1,1,true,false),
            new Stage(10,2,new int[]{ 5,2},false,0,2,1,true,false),
            new Stage(10,2,new int[]{ 3,2},false,0,3,1,true,false),
            new Stage(10,2,new int[]{ 4,2},false,0,4,1,true,false),
            new Stage(10,2,new int[]{ 1,2},false,0,5,1,true,true),
            new Stage(10,2,new int[]{ 1,2},false,0,1,2,true,false),
            new Stage(10,2,new int[]{ 1,2},false,0,2,1,true,false),
            new Stage(10,2,new int[]{ 1,2},false,0,3,1,true,false),
            new Stage(10,2,new int[]{ 1,2},false,0,4,1,true,false),
            new Stage(10,2,new int[]{ 1,2},false,0,5,1,true,false),
        };
        unLockedNormalModelLevelNum = new List<int>()
        {
            2,2,2
        };
    }

}
