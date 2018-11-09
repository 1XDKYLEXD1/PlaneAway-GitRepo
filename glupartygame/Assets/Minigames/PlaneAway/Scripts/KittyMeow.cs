using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittyMeow : MonoBehaviour
{
    [SerializeField]Animator m_myanimator;
    float m_kittytimer;

    void Update()
    {
        m_kittytimer += Time.deltaTime;
        
        if(m_kittytimer > 20f)
        {
            m_myanimator.SetInteger("KittyAnimationState", 1);
        }
        if(m_kittytimer > 27f)
        {
            m_myanimator.SetInteger("KittyAnimationState", 0);
        }
    }

}
