using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XBOXParty;

namespace PlaneAway
{
    public class PlayerController : MonoBehaviour
    {
        int m_playernumber;
        float m_movementspeed;

        bool m_onground;

        Rigidbody m_myrigibody;
        Vector3 m_movevelocity;

        Animator m_myanimator;

        WaitUntilTheGameCanBegin m_waitforbegin;

        private float m_extramomentum;
        public float extraMomentum { get { return (m_extramomentum); } set { m_extramomentum = value; } }
        private float m_stun;
        public float stunTime { get { return (m_stun); } set { m_stun = value; } }

        //Material m_material;

        void Start()
        {
            m_movementspeed = 7;
            m_myrigibody = GetComponent<Rigidbody>();
            m_myanimator = GetComponent<Animator>();
            m_waitforbegin = GameObject.FindObjectOfType<WaitUntilTheGameCanBegin>();
            //m_material = GetComponent<Material>();

            m_myrigibody.useGravity = false;
        }

        void Update()
        {
            if(m_waitforbegin.CanIBeginYet == true)
            {
                m_myrigibody.useGravity = true;

                Move(m_playernumber);

                if (m_stun <= 0)
                {
                    Jump(m_playernumber);
                }
                else
                { m_stun -= 1 * Time.deltaTime; }
            }
        }

        void Move(int playernumber)
        {
            if (m_stun <= 0)
            {

                //If moving to the left change the rotation so that you move that way.
                if (InputManager.Instance.GetAxis("PlaneAway_Horizontal_P" + m_playernumber) < 0)
                {
                    if (m_myanimator.GetInteger("PlayerAnimationState") != 2)
                    {
                        m_myanimator.SetInteger("PlayerAnimationState", 1);
                    }
                    if (transform.rotation.y > 0)
                    {
                        transform.Rotate(Vector3.up, -180);
                    }
                }

                //Same for the right.
                if (InputManager.Instance.GetAxis("PlaneAway_Horizontal_P" + m_playernumber) > 0)
                {
                    if (m_myanimator.GetInteger("PlayerAnimationState") != 2)
                    {
                        m_myanimator.SetInteger("PlayerAnimationState", 1);
                    }
                    if (transform.rotation.y < 0)
                    {
                        transform.Rotate(Vector3.up, 180);
                    }
                }

                if (InputManager.Instance.GetAxis("PlaneAway_Horizontal_P" + m_playernumber) == 0)
                {
                    if (m_myanimator.GetInteger("PlayerAnimationState") != 2)
                    {
                        m_myanimator.SetInteger("PlayerAnimationState", 0);
                    }
                }


                    Vector2 moveinput = new Vector2(InputManager.Instance.GetAxis("PlaneAway_Horizontal_P" + m_playernumber), 0f);
                m_movevelocity = (moveinput.normalized * m_movementspeed);
                m_movevelocity.x += m_extramomentum;
            }
            else
            {
                m_movevelocity.x = m_extramomentum;

                if(m_myanimator.GetInteger("PlayerAnimationState") != 2)
                {
                    m_myanimator.SetInteger("PlayerAnimationState", 0);
                }
            }

            m_myrigibody.MovePosition(m_myrigibody.position + m_movevelocity * Time.deltaTime);
        }

        void Jump(int playernumber)
        {
            if (InputManager.Instance.GetButton("PlaneAway_Jump_P" + playernumber) && m_onground == true)
            {
                m_myanimator.SetInteger("PlayerAnimationState", 2);
                m_myrigibody.velocity = new Vector3(0, 8.5f, 0);
                m_onground = false;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Tag 0"))
            {
                m_onground = true;

                if(m_myanimator.GetInteger("PlayerAnimationState") != 1)
                m_myanimator.SetInteger("PlayerAnimationState", 0);
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
