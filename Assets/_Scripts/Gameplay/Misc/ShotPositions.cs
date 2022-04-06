using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPositions : MonoBehaviour
{
    [SerializeField]
    private Transform m_ShotTransform;

    private void Awake()
    {
        if(m_ShotTransform != null)
        {
            m_ShotTransform = this.GetComponent<Transform>();
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(m_ShotTransform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
#endif
}
