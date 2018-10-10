using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject
{
    public class poolSpawn : MonoBehaviour
    {

        [SerializeField]
        private int m_itemAmount; public int itemAmount { get { return (m_itemAmount); } set { m_itemAmount = value; } }
        [SerializeField]
        private GameObject m_item; public GameObject item { get { return (m_item); } set { m_item = value; } }
        [SerializeField]
        private bool m_expandible; public bool expandible { get { return (m_expandible); } set { m_expandible = value; } }

        private List<GameObject> m_pooledItems = new List<GameObject>();

        void Start()
        {
            MakeItem(m_itemAmount);
        }

        void Update()
        {

        }

        private void MakeItem(int amountOfItems)
        {
            GameObject _placeholderItem;
            for (int i = 0; i < amountOfItems; i++)
            {
                _placeholderItem = Instantiate(m_item);
                _placeholderItem.GetComponent<poolItem>().myPool = this;
                m_pooledItems.Add(_placeholderItem);
                _placeholderItem.SetActive(false);
                _placeholderItem = null;
            }
        }
        public void ActivateNextItem(Vector3 itemPosition, Quaternion itemRotation, Transform parent, debreebuilder deberd)
        {
            for (int i = 0; i < m_pooledItems.Count; i++)
            {
                if (m_pooledItems[i].activeSelf == false)
                {
                    m_pooledItems[i].transform.position = itemPosition;
                    m_pooledItems[i].transform.rotation = itemRotation;
                    m_pooledItems[i].GetComponent<Debree>()._mybuilder = deberd;
                    m_pooledItems[i].transform.SetParent(parent);
                    m_pooledItems[i].SetActive(true);
                    break;
                }
                else if (i == m_pooledItems.Count - 1 && m_expandible)
                {
                    MakeItem(1);
                    m_pooledItems[i + 1].transform.position = itemPosition;
                    m_pooledItems[i + 1].transform.rotation = itemRotation;
                    m_pooledItems[i + 1].GetComponent<Debree>()._mybuilder = deberd;
                    m_pooledItems[i + 1].transform.SetParent(parent);
                    m_pooledItems[i + 1].SetActive(true);
                    break;
                }
            }
        }
        public void DeactivateAll()
        {
            for (int i = 0; i < m_pooledItems.Count; i++)
            {
                m_pooledItems[i].SetActive(false);
            }
        }
        public void CleanUp()
        {
            for (int i = m_pooledItems.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(m_pooledItems[i]);
            }
            GameObject.Destroy(gameObject);
        }
    }
}
