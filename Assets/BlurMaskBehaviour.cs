using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlurMaskBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float m_minBlur = 0f;
    [SerializeField] private float m_maxBlur = 1.5f;
    [SerializeField] private Material m_blurPrefab;

    private float m_blur = 0f;
    private float m_time = 0f;
    private Material m_blurMaterial;
    private bool m_mousePointOver = false;

    private void Awake()
    {
        m_blurMaterial = Instantiate(m_blurPrefab);
        GetComponent<Image>().material = m_blurMaterial;
        m_blur = m_maxBlur;
        m_blurMaterial.SetFloat("_Size", m_blur);
    }

    private void Update()
    {
        if(m_mousePointOver && m_blur != m_minBlur)
        {
            m_time -= Time.deltaTime;
            m_time = Mathf.Clamp(m_time, 0f, m_maxBlur);
            m_blur = m_maxBlur * m_time;
            m_blurMaterial.SetFloat("_Size", m_blur);
        }
        else if(!m_mousePointOver && m_blur != m_maxBlur)
        {
            m_time += Time.deltaTime;
            m_time = Mathf.Clamp(m_time, 0f, m_maxBlur);
            m_blur = m_maxBlur * m_time;
            m_blurMaterial.SetFloat("_Size", m_blur);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_time = m_blur / m_maxBlur;
        m_mousePointOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_time = m_blur / m_maxBlur;
        m_mousePointOver = false;
    }
}
