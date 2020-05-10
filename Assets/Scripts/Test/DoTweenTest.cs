using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoTweenTest : MonoBehaviour
{
    private Image maskImage;
    private Tween maskTween;

    private void Awake()
    {
        maskImage = GetComponent<Image>();

        ////1.DoTween的静态方法
        //DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0,0,0,0),2f);
        ////详细分解
        //DOTween.To(
        //    () => 
        //    maskImage.color//想要改变的值
        //    , toColor //每次doTween经过计算得到的结果
        //    => maskImage.color = toColor, //将计算结果赋值给想要改变的目标值
        //    new Color(0, 0, 0, 0)//目标值
        //    , 2f//改变时间
        //    );
        //DOTween.To(() => 想要改变的值, 每次doTween经过计算得到的结果 ,将计算结果赋值给想要改变的目标值,目标值, 改变时间);

        ////2.doTween直接作用于transform的方法
        //Tween tween= transform.DOLocalMoveX(100,0.5f);
        //tween.Play();//播放动画——可以无限正播但是相对于倒播只能播放一次，不能倒播多次
        //tween.PlayForward();
        //正播动画——可以多次倒播
        //tween.PlayBackwards();
        //倒播动画
        ////结论：直接不论倒播还是先正播再倒播，不存在直接倒播的情况(猜测：或许是因为物体直接在终点所以动画没有效果？)

        ////3.循环使用动画
        //maskTween = transform.DOLocalMoveX(100, 0.5f);
        //maskTween.SetAutoKill(false);//设置动画是否在播放后被杀死——默认为true
        //maskTween.Pause();
        //暂停动画

        ////4.动画的事件回调
        //Tween tween= transform.DOLocalMoveX(500, 0.5f);
        //tween.OnComplete(CompleteMethod);
        //添加回调函数，在动画结束后调用 如果设置了Loop则会在Loop完成后调用

        ////5.设置动画的缓动函数以及循环状跟次数
        //Tween tween = transform.DOLocalMoveX(500, 0.5f);
        //tween.SetEase(Ease.InOutBounce);//设置动画播放的曲线
        //tween.SetLoops(-1,LoopType.Yoyo);
        //设置动画播放的(次数，方式)
        //LoopType.Incremental
        //LoopType.Restart
        //LoopType.Yoyo
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        maskTween.PlayForward();
    //    }
    //    else if(Input.GetMouseButtonDown(1))
    //    {
    //        maskTween.PlayBackwards();
    //    }
    //}

    //private void CompleteMethod() {
    //    DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 0), 2f);
    //}
}
