using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneAway
{
    public class WaitUntilTheGameCanBegin : MonoBehaviour
    {
        bool m_canbegin;
        float m_timer;

        void Update()
        {
            m_timer += Time.deltaTime;
        }

        public bool CanIBeginYet
        {
            get { return m_canbegin; }
        }

        public float WaitTimer
        {
            get { return m_timer; }
        }

        public void LetTheGamesBegin()
        {
            m_canbegin = true;
        }
    }
}
