using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PitchBlock : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform block_pos;
    private MeshRenderer m_Renderer;

    private bool BallSelected
    {
        get
        {
            return GameManager.Instance.IsBowlingSpeedSelected();
        }
    }
    private static bool IsSetPosition = false;

    private void Awake()
    {
        if (block_pos == null)
        {
            block_pos = this.GetComponent<Transform>();
        }

        if (m_Renderer == null)
        {
            m_Renderer = this.GetComponent<MeshRenderer>();
        }

        GameManager.OnShotPlayed += OnShotPlayed;
    }

    private void OnShotPlayed(int obj)
    {
        IsSetPosition = false;
    }

    public void SetPosition()
    {
        if (BallSelected)
        {
            GameManager.Instance.SetBowlingLength(block_pos);

            IsSetPosition = true;
        }
    }

    public void OnDropEvent(PointerEventData eventData)
    {
        GameObject pointerData = eventData.pointerDrag;

        if (pointerData != null)
        {
            CustomInput temp = pointerData.GetComponent<CustomInput>();

            if (temp != null)
            {
                GameManager.Instance.SetBowlingSpeed(temp.bowlType);
                SetPosition();
                GameManager.Instance.InitiateBowling();
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetPosition();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsSetPosition)
        {
            m_Renderer.material.color = Color.red;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            m_Renderer.material.color = Color.white;
    }
}
