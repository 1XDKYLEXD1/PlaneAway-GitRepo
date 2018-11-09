using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneAway
{
    public class WaitUntilTheGameCanBegin : MonoBehaviour
    {
        bool m_canbegin;

        public bool CanIBeginYet
        {
            get { return m_canbegin; }
        }
    }
}
