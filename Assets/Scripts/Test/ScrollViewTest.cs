using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollViewTest : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private ScrollRect scrollRect;
    private RectTransform content;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.LogError("开始拖动");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.LogError("拖动中");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.LogError("结束拖动");
    }

    private void Awake()
    {
        //scrollRect = GetComponent<ScrollRect>();
        ////scrollRect.onValueChanged.AddListener(PrintValue);

        //content = scrollRect.content;


        ////当前UI的世界坐标
        //Debug.Log("当前UI的世界坐标" + content.position);
        ////当前UI的局部坐标
        //Debug.Log("当前UI的局部坐标" + content.localPosition);
        ////当前UI的宽度(从左到右的长度)
        //Debug.Log("Width=" + content.rect.right);
        //Debug.Log("Width=" + content.rect.xMax);
        //Debug.Log("Width=" + content.rect.width);

        //当前UI的左坐标
        //Debug.Log("当前UI的左坐标=" + rectTransform.rect.xMin);
        //Debug.Log("当前UI的右坐标=" + rectTransform.rect.x);//并不是当前UI的x坐标

        //当前UI的高度
        //Debug.Log("当前UI的高度=" + rectTransform.rect.xMin);

        //这里要注意，他只是当前transform的x轴向的方向
        //就像transform.right自身方向的右方
        //Debug.Log(rectTransform.right);

        //当前UI底部相对于顶部的相对长度，负数为向下延展，正数为向上延展
        //Debug.Log(rectTransform.rect.y);

        //当前UI的宽高
        //Debug.Log(rectTransform.sizeDelta);

        //这里的420是想要增加的宽度
        //这里的高度则是设置多少就是多少(这是什么操作？黑人问号？)
        //rectTransform.sizeDelta = new Vector2(420, rectTransform.sizeDelta.y);
        //rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 840);

        //水平滚动位置0到1的值，0表示左侧
        //这个不能与ContentSizeFitter组件一起用
        //Debug.Log(scrollRect.horizontalNormalizedPosition);
        //scrollRect.horizontalNormalizedPosition = 1;
        //Debug.Log("aaa" + scrollRect.horizontalNormalizedPosition);
    }

    public void PrintValue(Vector2 vector2) {
        Debug.LogError("Value="+vector2);
    }
}
