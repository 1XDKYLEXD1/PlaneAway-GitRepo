using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneAway
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]AudioSource m_battlemusic;
        [SerializeField] WaitUntilTheGameCanBegin m_waitmechanic;

        bool m_selfcancel;

        void Update()
        {
            if (m_selfcancel == false)
            {
                if (m_waitmechanic.CanIBeginYet == true)
                {
                    m_battlemusic.Play();
                    m_selfcancel = true;
                }
        }
        }
    }
}
