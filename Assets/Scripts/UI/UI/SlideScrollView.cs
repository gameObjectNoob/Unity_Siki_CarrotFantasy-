using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 通过改变content的pos来实现滑动
/// 限制为只能单滑
/// </summary>
public class SlideScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private RectTransform contentTrans;//content的RectTransform
    private ScrollRect scrollRect;//要操作的ScrollView
    private float beginMousePositionX;//开始滑动的鼠标位置的X值
    private float endMousePositionX;  //结束滑动的鼠标位置的X值

    public int cellLength;//单元格长度
    public int spacing;//间隙长度
    public int leftOffset;//左偏移量
    private float moveOneItemLength;//移动一个item的长度

    private Vector3 currentContentLocalPos; //当前content的相对位置
    private Vector3 contentInitPos;//content的初始位置，后面init时使用
    private Vector2 contentTransSize;//Content初始大小

    public int totalItemNum;//单元格数量
    private int currentIndex;//当前单元格索引

    public Text pageText;

    public bool needSendMessage;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        contentTrans = scrollRect.content;

        moveOneItemLength = cellLength + spacing;
        currentContentLocalPos = contentTrans.localPosition;
        contentTransSize = contentTrans.sizeDelta;
        contentInitPos = contentTrans.localPosition;
        currentIndex = 1;
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }

    public void Init()
    {
        currentIndex = 1;

        //因为这里的Init方法是在SlideScrollView的父物体上调用的
        //所以会先于Awake方法执行
        //导致contentTrans为空的错误
        //而这两句代码是将contentTrans的位置设置为初始位置
        //再加上Awake执行后就是初始值
        //所以可以加个安全校验，使他在Awake没有执行的时候不赋值
        if (contentTrans != null)
        {
            contentTrans.localPosition = contentInitPos;
            currentContentLocalPos = contentInitPos;
            
        }
        if (pageText!=null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
    }
    //外部调用 下一页
    public void ToNextPage()
    {
        float moveDistance = 0;
        if (currentIndex >= totalItemNum)
        {
            return;
        }
        moveDistance = -moveOneItemLength;
        currentIndex++;
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        UpdatePanel(true);
        DOTween.To(() =>
                   contentTrans.localPosition,
                   lerpValue => contentTrans.localPosition = lerpValue,
                   currentContentLocalPos + new Vector3(moveDistance, 0, 0),
                   0.5f).SetEase(Ease.InOutQuint);
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    //外部调用 上一页
    public void ToLastPage()
    {
        float moveDistance = 0;
        if (currentIndex <= 1)
        {
            return;
        }
        moveDistance = moveOneItemLength;
        currentIndex--;
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }
        UpdatePanel(false);
        DOTween.To(() =>
                   contentTrans.localPosition,
                   lerpValue => contentTrans.localPosition = lerpValue,
                   currentContentLocalPos + new Vector3(moveDistance, 0, 0),
                   0.5f).SetEase(Ease.InOutQuint);
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endMousePositionX = Input.mousePosition.x;
        float offset = 0;
        float moveDistance = 0;//这次需要移动的距离——通过正负判断左右
        offset = beginMousePositionX - endMousePositionX;
        if (offset > 0)//右滑
        {
            if (currentIndex >= totalItemNum)
            {
                return;
            }
            UpdatePanel(true);
            moveDistance = -moveOneItemLength;
            currentIndex++;
        }
        else//左滑
        {
            if (currentIndex <= 1)
            {
                return;
            }
            UpdatePanel(false);
            moveDistance = moveOneItemLength;
            currentIndex--;
        }
        if (pageText != null)
        {
            pageText.text = currentIndex.ToString() + "/" + totalItemNum;
        }

        DOTween.To(() =>
                    contentTrans.localPosition,
                    lerpValue => contentTrans.localPosition = lerpValue,
                    currentContentLocalPos + new Vector3(moveDistance, 0, 0),
                    0.5f).SetEase(Ease.InOutQuint);
        currentContentLocalPos += new Vector3(moveDistance, 0, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginMousePositionX = Input.mousePosition.x;
    }

    //外部调用 设置Content的大小
    public void SetContentLength(int itemNum)
    {
        contentTrans.sizeDelta = new Vector2(contentTrans.sizeDelta.x + (cellLength + spacing) * (itemNum - 1), contentTrans.sizeDelta.y);
        totalItemNum = itemNum;
    }

    //初始化Content大小
    public void InitScrollLength()
    {
        contentTrans.sizeDelta = contentTransSize;
    }

    //发送翻页信息的方法
    public void UpdatePanel(bool toNext)
    {
        if (!needSendMessage)
        {
            return;
        }
        if (toNext)
        {
            gameObject.SendMessageUpwards("ToNextLevel");
        }
        else
        {
            gameObject.SendMessageUpwards("ToLastLevel");
        }
    }
}
