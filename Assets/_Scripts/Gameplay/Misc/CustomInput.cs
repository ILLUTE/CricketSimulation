using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CustomInput : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform m_RectTransform;
    private Canvas m_mainCanvas;

    private Vector2 defaultPos;

    public BowlSpeed bowlType;

    private CanvasGroup canvasGroup;

    private bool isAllowed = true;

    private void Awake()
    {
        m_RectTransform = this.GetComponent<RectTransform>();
        m_mainCanvas = FindObjectOfType<Canvas>();
        canvasGroup = this.GetComponent<CanvasGroup>();

        defaultPos = m_RectTransform.anchoredPosition;

        GameManager.OnDeliveryStarted += OnDeliveryStarted;
        GameManager.OnDeliveryCompleted += OnDeliveryCompleted;
    }

    private void OnDeliveryCompleted(Ball obj)
    {
        isAllowed = true;
    }

    private void OnDeliveryStarted(Ball obj)
    {
        isAllowed = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isAllowed)
        {
            canvasGroup.alpha = 0.5f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isAllowed)
        {
            m_RectTransform.anchoredPosition += eventData.delta / m_mainCanvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isAllowed)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            m_RectTransform.anchoredPosition = defaultPos;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_RectTransform.DOJumpAnchorPos(m_RectTransform.anchoredPosition, 0.1f, 1, 0.5f).OnComplete(() =>
        {
            BowlingDelivery.Instance.SetBowlSpeed(bowlType);
        });
    }
}
