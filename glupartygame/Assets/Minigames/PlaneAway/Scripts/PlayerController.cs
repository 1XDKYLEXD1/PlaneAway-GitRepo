using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XBOXParty;

namespace TestProject
{
    public class PlayerController : MonoBehaviour
    {
        int m_playernumber;
        float m_movementspeed;

        bool m_onground;

        Rigidbody m_myrigibody;
        Vector3 m_movevelocity;

        private float m_extramomentum;
        public float extraMomentum { get { return (m_extramomentum); } set { m_extramomentum = value; } }
        private float m_stun;
        public float stunTime { get { return (m_stun); } set { m_stun = value; } }

        //Material m_material;

        void Start()
        {
            m_movementspeed = 7;
            m_myrigibody = GetComponent<Rigidbody>();
            //m_material = GetComponent<Material>();
        }

        void Update()
        {
            Debug.Log("Stun + " + m_stun);


            Move(m_playernumber);

            if (m_stun <= 0)
            {
                Jump(m_playernumber);
            }
            else
            { m_stun -= 1 * Time.deltaTime; }
        }

        void Move(int playernumber)
        {
            if (m_stun <= 0)
            {
                Vector2 moveinput = new Vector2(InputManager.Instance.GetAxis("PlaneAway_Horizontal_P" + m_playernumber), 0f);
                m_movevelocity = (moveinput.normalized * m_movementspeed);
                m_movevelocity.x += m_extramomentum;
            }
            else
            {
                m_movevelocity.x = m_extramomentum;
            }
            m_myrigibody.MovePosition(m_myrigibody.position + m_movevelocity * Time.deltaTime);
        }

        void Jump(int playernumber)
        {
            if (InputManager.Instance.GetButton("PlaneAway_Jump_P" + playernumber) && m_onground == true)
            {
                m_myrigibody.velocity = new Vector3(0, 8.5f, 0);
                m_onground = false;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Tag 0"))
            {
                m_onground = true;
            }
        }

        /*
        public void SetPlayerColor(Color color)
        {
            m_material.color = color;   
        }
        */

        public int playerNumber
        {
            get { return m_playernumber; }
            set { m_playernumber = value; }
        }
    }
}
