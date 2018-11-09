using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneAway
{
    public class Airplane : MonoBehaviour
    {
        [SerializeField]
        private float m_kantelSpeed; public float kantelSpeed { get { return (m_kantelSpeed); } set { m_kantelSpeed = value; } }
        [SerializeField]
        private float m_maxKantelHoek; public float maxKantelHoek { get { return (m_maxKantelHoek); } set { m_maxKantelHoek = value; } }
        [SerializeField]
        private int m_fallDelay = 10;
        [SerializeField]
        private float m_playerFallSpeedAmplifier = 2;
        private List<GameObject> m_allGroundedPlayers;

        void Start()
        {
            m_allGroundedPlayers = new List<GameObject>();
        }

        void Update()
        {
            Tilt();
            PlayerFallForce();
        }

        private void PlayerFallForce()
        {
            for (int i = 0; i < m_allGroundedPlayers.Count; i++)
            {
                if (transform.rotation.eulerAngles.z >= 360 - m_maxKantelHoek && transform.rotation.eulerAngles.z < 360 - m_fallDelay)
                {
                    m_allGroundedPlayers[i].GetComponent<PlayerController>().extraMomentum = m_playerFallSpeedAmplifier *
                    (1 - ((transform.rotation.eulerAngles.z - (360 - m_maxKantelHoek) - m_fallDelay) / (360 - m_maxKantelHoek - m_fallDelay)));
                }
                else
                if (transform.rotation.eulerAngles.z <= m_maxKantelHoek && transform.rotation.eulerAngles.z > m_fallDelay)
                {
                    m_allGroundedPlayers[i].GetComponent<PlayerController>().extraMomentum = -m_playerFallSpeedAmplifier *
                        ((transform.rotation.eulerAngles.z - m_fallDelay) / (m_maxKantelHoek - m_fallDelay));
                }
                else
                {
                    m_allGroundedPlayers[i].GetComponent<PlayerController>().extraMomentum = 0;
                }

            }
        }

        private void Tilt()
        {
            if (m_allGroundedPlayers.Count > 0)
            {
                float _tilt = 0;
                for (int i = 0; i < m_allGroundedPlayers.Count; i++)
                {
                    if (m_allGroundedPlayers[i].transform.position.x > transform.position.x)
                    {
                        _tilt -= Vector3.Distance(m_allGroundedPlayers[i].transform.position, transform.position);
                    }
                    else if (m_allGroundedPlayers[i].transform.position.x < transform.position.x)
                    {
                        _tilt += Vector3.Distance(m_allGroundedPlayers[i].transform.position, transform.position);
                    }
                }
                if (_tilt > 0 && transform.rotation.eulerAngles.z < 360 - m_maxKantelHoek && transform.rotation.eulerAngles.z + (_tilt * m_kantelSpeed) * Time.deltaTime > m_maxKantelHoek)
                {
                    transform.rotation = Quaternion.Euler(0, 0, m_maxKantelHoek);
                }
                else
                if (_tilt < 0 && transform.rotation.eulerAngles.z + (_tilt * m_kantelSpeed) * Time.deltaTime < 360 - m_maxKantelHoek && transform.rotation.eulerAngles.z > m_maxKantelHoek)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 360 - m_maxKantelHoek);
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, (_tilt * m_kantelSpeed) * Time.deltaTime));
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            //TAG 1 STAAT VOOR PLAYER
            if (other.tag == "Tag 1")
            {
                m_allGroundedPlayers.Add(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Tag 1")
            {
                m_allGroundedPlayers.Remove(other.gameObject);
                other.gameObject.GetComponent<PlayerController>().extraMomentum = 0;
            }
        }
    }
}
