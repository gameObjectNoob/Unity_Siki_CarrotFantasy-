using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// 通过scrollRect.horizontalNormalizedPosition实现滑动效果
/// 我理解为通过比例实现滑动效果——即0为左，1为右
/// 支持翻一页或者多翻
/// 后面可以试着用自己的方法实现一下
/// </summary>
public class SildeCanCoverScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private float contentLength;//容器长度
    private float beginMousePostionX;//开始滑动的鼠标位置的X值
    private float endMousePostionX;  //结束滑动的鼠标位置的X值
    private ScrollRect scrollRect;//操作的组件
    private float lastProportion;//上一个位置比例

    public int cellLength;//每个单元格长度
    public int spacing;//间隙
    public int leftOffset;//左偏移量

    private float upperLimit;//上限值
    private float lowerLimit;//下限值
    private float firstItemLength;//移动第一个单元格的距离
    private float oneItemLength;//滑动一个单元格需要的距离
    private float oneItemProportion;//滑动一个单元格所占比例

    public int totalItemNum;//单元格数量
    private int currentIndex;//当前单元格索引

    public Text pageText;//显示页数的文本

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        contentLength = scrollRect.content.rect.xMax - 2 * leftOffset - cellLength;
        firstItemLength = cellLength / 2 + leftOffset;
        oneItemLength = cellLength + spacing;
        oneItemProportion = oneItemLength / contentLength;
        upperLimit = 1 - firstItemLength / contentLength;
        lowerLimit = firstItemLength / contentLength;

        currentIndex = 1;
        scrollRect.horizontalNormalizedPosition = 0;
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }

    //初始化
    public void Init() {
        lastProportion = 0;
        currentIndex = 1;
        if (scrollRect != null)
        {
            scrollRect.horizontalNormalizedPosition = 0;
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginMousePostionX = Input.mousePosition.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float offSetX = 0;
        endMousePostionX = Input.mousePosition.x;

        //这里*2的原因是因为Input.mousePosition的坐标系和firstItemLength的坐标系不是同一个坐标系
        //需要转换，然而老师并不想讲这个，并且也没有做转换而是直接"*2"了事，跟转换比起来这种方式有一定的误差
        //想要深入了解的话需要去学习"3D数学"课
        offSetX = (beginMousePostionX - endMousePostionX) * 2;

        if (Mathf.Abs(offSetX) > firstItemLength)//执行滑动动作的条件是要大于第一个长度
        {
            if (offSetX > 0)//右滑
            {
                if (currentIndex >= totalItemNum)//如果已经到了最右边就直接return出去
                {
                    return;
                }

                //当次可以移动的数量
                //因为firstItemLength是个特殊值所以先减去他
                //然后用剩下的值除以oneItemLength就可以得出除第一格外需要移动的格子数
                //最后再把第一格加进来就是总的移动数
                int moveCount = (int)((offSetX - firstItemLength) / oneItemLength) + 1;
                currentIndex += moveCount;
                if (currentIndex > totalItemNum)
                {//安全校验
                    currentIndex = totalItemNum;
                }
                //档次需要移动的比例
                //上一次已经存在的单元格比例加上这一次需要移动的比例
                //lastProportion += oneItemProportion * moveCount;
                lastProportion = oneItemProportion * (currentIndex-1);
                if (lastProportion >= upperLimit)
                {
                    lastProportion = 1;
                }
            }
            else//左滑
            {
                if (currentIndex <= 1)
                {
                    return;
                }

                int moveCount = (int)((offSetX + firstItemLength) / oneItemLength) - 1;
                currentIndex += moveCount;
                if (currentIndex <= 1)
                {
                    currentIndex = 1;
                }

                lastProportion = oneItemProportion * (currentIndex - 1);
                if (lastProportion <= lowerLimit)
                {
                    lastProportion = 0;
                }
            }
            if (pageText != null)
            {
                pageText.text = currentIndex.ToString() + "/" + totalItemNum;
            }
        }

        DOTween.To(() => 
        scrollRect.horizontalNormalizedPosition, 
        lerpValue => scrollRect.horizontalNormalizedPosition = lerpValue, 
        lastProportion, 
        0.5f
        ).SetEase(Ease.InOutQuint);
    }
}
