using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneAway
{
    public class poolItem : MonoBehaviour
    {

        [SerializeField]
        private poolSpawn m_myPool; public poolSpawn myPool { get { return (m_myPool); } set { m_myPool = value; } }
        public void BackToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
