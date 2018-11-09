using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XBOXParty;

namespace PlaneAway
{
    public class PlaneAwayGameManager : MonoBehaviour
    {
        private List<int> m_positions;
        [SerializeField] List<GameObject> m_allplayers;
        [SerializeField] List<Sprite> m_places;
        [SerializeField] List<Sprite> m_countdownnumbers;
        [SerializeField] RandomSpawner m_randomspawner;
        [SerializeField] WaitUntilTheGameCanBegin m_waittillbegin;
        [SerializeField] Image m_countdownimage;
        [SerializeField] GameObject m_spawner;
        List<GameObject> m_currentaliveplayers;
        int m_playercount;

        //Variables who are for gameplay purposes
        bool m_stopwithactivating;
        bool m_stopwithcountdown;
        int m_activeplayers;
        float m_countdowntimer;

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
                //m_allplayers[i].SetActive(true);
                m_allplayers[i].GetComponent<PlayerController>().playerNumber = i;
                //m_allplayers[i].GetComponent<PlayerController>().SetPlayerColor(GlobalGameManager.Instance.GetPlayerColor(i));
                

                //Binding every button per player ---- ONLY WORKS IF THERE IS NO KEYBOARD PLAYER
                //InputManager.Instance.BindAxis("PlaneAway_Horizontal_P" + m_playercount, m_playercount, ControllerAxisCode.LeftStickX);
                //InputManager.Instance.BindButton("PlaneAway_Jump_P" + m_playercount, m_playercount, ControllerButtonCode.A, ButtonState.OnPress);
            }

            for (int p = 0; p < m_allplayers.Count; p++)
            {               
                m_currentaliveplayers.Add(m_allplayers[p]);               
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

            //Lelijk gecodeerd xD
            if(m_stopwithactivating == false)
            {
                if(m_waittillbegin.WaitTimer > 1.5f && m_playercount > 0)
                {
                    m_allplayers[0].SetActive(true);
                }

                if (m_waittillbegin.WaitTimer > 3f && m_playercount > 1)
                {
                    m_allplayers[1].SetActive(true);
                    if(m_playercount == 2)
                    {
                        m_stopwithactivating = true;
                    }
                }

                if (m_waittillbegin.WaitTimer > 4.5f && m_playercount > 2)
                {
                    m_allplayers[2].SetActive(true);
                    if (m_playercount == 3)
                    {
                        m_stopwithactivating = true;
                    }
                }

                if (m_waittillbegin.WaitTimer > 6f && m_playercount > 3)
                {
                    m_allplayers[3].SetActive(true);
                    if (m_playercount == 4)
                    {
                        m_stopwithactivating = true;
                    }
                }
            }

            if(m_stopwithcountdown == false)
            {
                if (m_stopwithactivating == true)
                {
                    m_countdownimage.gameObject.SetActive(true);
                    m_countdowntimer += Time.deltaTime;

                    if(m_countdowntimer > 1)
                    {
                        m_countdownimage.sprite = m_countdownnumbers[1];
                    }
                    if (m_countdowntimer > 2)
                    {
                        m_countdownimage.sprite = m_countdownnumbers[0];
                    }
                    if(m_countdowntimer > 3)
                    {
                        m_countdownimage.gameObject.SetActive(false);
                        m_waittillbegin.LetTheGamesBegin();
                        m_spawner.SetActive(true);
                        m_stopwithcountdown = true;
                    }
                }
            }

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
