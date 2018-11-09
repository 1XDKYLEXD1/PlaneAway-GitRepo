using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XBOXParty;

namespace TestProject
{
    public class PlaneAwayGameManager : MonoBehaviour
    {
        private List<int> m_positions;
        [SerializeField] List<GameObject> m_allplayers;
        [SerializeField] RandomSpawner m_randomspawner;
        List<GameObject> m_currentaliveplayers;
        int m_playercount;

        void Awake()
        {
            //The pure purpose of this list is to let you know who ended on place(position) 1, place 2 and so on...
            m_positions = new List<int>();

            m_currentaliveplayers = new List<GameObject>();

            m_playercount = GlobalGameManager.Instance.PlayerCount;

            for (int i = 0; i < m_playercount; ++i)
            {
                Debug.Log("Playercount : "+m_playercount);

                //Only sets the playing players active and gives them the number they are playing as
                m_allplayers[i].SetActive(true);
                m_allplayers[i].GetComponent<PlayerController>().playerNumber = i;
                //m_allplayers[i].GetComponent<PlayerController>().SetPlayerColor(GlobalGameManager.Instance.GetPlayerColor(i));
                

                //Binding every button per player ---- ONLY WORKS IF THERE IS NO KEYBOARD PLAYER
                //InputManager.Instance.BindAxis("PlaneAway_Horizontal_P" + m_playercount, m_playercount, ControllerAxisCode.LeftStickX);
                //InputManager.Instance.BindButton("PlaneAway_Jump_P" + m_playercount, m_playercount, ControllerButtonCode.A, ButtonState.OnPress);
            }

            for (int p = 0; p < m_allplayers.Count ; p++)
            {
                if(m_allplayers[p].gameObject.activeSelf == true)
                {
                    m_currentaliveplayers.Add(m_allplayers[p]);
                }
            }
            m_randomspawner.playerList = m_currentaliveplayers;

            InputManager.Instance.BindAxis("PlaneAway_Horizontal_P0", KeyCode.D, KeyCode.A);
            InputManager.Instance.BindAxis("PlaneAway_Horizontal_P1", 0, ControllerAxisCode.LeftStickX);
            InputManager.Instance.BindAxis("PlaneAway_Horizontal_P2", 1, ControllerAxisCode.LeftStickX);
            InputManager.Instance.BindAxis("PlaneAway_Horizontal_P3", 2, ControllerAxisCode.LeftStickX);

            InputManager.Instance.BindButton("PlaneAway_Jump_P0", KeyCode.W, ButtonState.OnPress);
            InputManager.Instance.BindButton("PlaneAway_Jump_P1", 0, ControllerButtonCode.A, ButtonState.OnPress);
            InputManager.Instance.BindButton("PlaneAway_Jump_P2", 1, ControllerButtonCode.A, ButtonState.OnPress);
            InputManager.Instance.BindButton("PlaneAway_Jump_P3", 2, ControllerButtonCode.A, ButtonState.OnPress);
        }

        void Update()
        {
            Debug.Log("How many players alive? " + m_currentaliveplayers.Count);

            if (m_currentaliveplayers.Count < 2)
            {
                UpdateStandings(m_currentaliveplayers[0].GetComponent<PlayerController>().playerNumber);
                SubmitEndResult();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            //Miss nog aanpassen - ff kijken
            m_randomspawner.playerList = m_currentaliveplayers;

            if(other.tag == "Tag 1")
            {
                UpdateStandings(other.gameObject.GetComponent<PlayerController>().playerNumber);
                m_currentaliveplayers.Remove(other.gameObject);
            }
        }

        public void UpdateStandings(int playerid)
        {
            m_positions.Add(playerid);
            if(m_positions.Count > 0)
            {
                Debug.Log(playerid);
            }
        }

        public void SubmitEndResult()
        {
            m_positions.Reverse();
            GlobalGameManager.Instance.SubmitGameResults(m_positions);
        }
    }
}
