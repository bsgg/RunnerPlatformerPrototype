using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    private Transform                           m_Player;
    [SerializeField] private Vector3            m_OffsetPosition;
    [SerializeField] private Vector3            m_OffsetView;

    public void Init(Transform player)
    {
        m_Player = player;
        transform.position = m_Player.position + m_OffsetPosition;
        transform.LookAt(m_Player.position + m_OffsetView);
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            m_Player.position + m_OffsetView,
            0.1f);
        transform.LookAt(m_Player.position + m_OffsetView);
    }
}
