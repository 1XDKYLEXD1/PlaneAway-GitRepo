using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XBOXParty;

namespace PlaneAway
{
    public class WaitingForSceneSwitch : MonoBehaviour
    {
        int m_timer;
        bool m_cantadd;

        void Update()
        {
            m_timer++;

            if (m_cantadd == false)
            {
                if (m_timer > 3)
                {
                    InputManager.Instance.BindButton("Start_P0", KeyCode.Q, ButtonState.OnPress);
                    m_cantadd = true;
                }
            }

            if (m_timer > 4)
            {
                if (InputManager.Instance.GetButton("Start_P0"))
                {
                    SceneManager.LoadScene("PlaneAwayGame");
                }
            }
        }
    }
}
