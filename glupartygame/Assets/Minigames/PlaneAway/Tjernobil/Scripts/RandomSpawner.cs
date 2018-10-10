using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawner : MonoBehaviour {

    private poolSpawn m_myPool;
    [SerializeField]
    private List<GameObject> m_activePlayerlist; public List<GameObject> playerList { get { return (m_activePlayerlist); } set { m_activePlayerlist = value; } }
    private float m_playerRadiusX;
    private float m_playerRadiusY;
    private GameObject m_limit;
    [SerializeField]
    private List<debreebuilder> m_debreetype;
    private int m_currentType;
    private List<float> m_cooldowns = new List<float>();   
    private List<List<float>> m_playerSpecificCooldowns = new List<List<float>>();
    [SerializeField]
    private float m_spawnCooldown =0;
    [SerializeField]
    private float m_spawnCL=0;
    private void Start()
    {
        m_myPool = gameObject.GetComponent<poolSpawn>();
        m_limit = GameObject.Find("DebreeFlightLimit");
        for (int i = 0; i < m_debreetype.Count; i++)
        {
            m_cooldowns.Add(m_debreetype[i].cooldown);
        }
    }
    public void AdjustCooldowns()
    {        
        foreach (GameObject player in m_activePlayerlist)
        {
            List<float> _tickingcooldowns = new List<float>();
            for (int i = 0; i < m_debreetype.Count; i++)
            {
                _tickingcooldowns.Add(m_debreetype[i].cooldown);
            }
            m_playerSpecificCooldowns.Add(_tickingcooldowns);
        }
    }
    private void Update()
    {
        if (m_spawnCL <= 0)
        {
            SpawnNext();
            m_spawnCL = m_spawnCooldown;
        }
        else
        {
            m_spawnCL -= 1 * Time.deltaTime;
        }
      
        CountCooldowns();       
    }
    public void WinCheck()
    {
        if(playerList.Count == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void SpawnNext()
    {
        for (int i = 0; i < m_activePlayerlist.Count; i++)
        {           
            m_currentType = ChooseDebree(i);
            if(m_currentType>-1){ 
            
            m_playerRadiusX = m_activePlayerlist[i].transform.localScale.x + m_debreetype[m_currentType].debreeSpawnsize.x;
            m_playerRadiusY = m_activePlayerlist[i].transform.localScale.y + m_debreetype[m_currentType].debreeSpawnsize.y;
            Vector3 _boi = new Vector3(m_activePlayerlist[i].transform.position.x - m_playerRadiusX / 2 + Random.Range(0, m_playerRadiusX),
                m_activePlayerlist[i].transform.position.y - m_playerRadiusY / 2 + Random.Range(0, m_playerRadiusY), transform.position.z);
            if (_boi.x - m_debreetype[m_currentType].debreeSpawnsize.x / 2 < transform.position.x - m_limit.transform.localScale.x / 2 ||
                _boi.y - m_debreetype[m_currentType].debreeSpawnsize.y / 2 < transform.position.y - m_limit.transform.localScale.y / 2 ||
                _boi.x + m_debreetype[m_currentType].debreeSpawnsize.x / 2 > transform.position.x + m_limit.transform.localScale.x / 2 ||
                _boi.y + m_debreetype[m_currentType].debreeSpawnsize.y / 2 > transform.position.y + m_limit.transform.localScale.y / 2)
            {

            }
            else
            {
                m_myPool.ActivateNextItem(_boi, transform.rotation, transform, m_debreetype[m_currentType]);
                m_playerSpecificCooldowns[i][m_currentType] = m_cooldowns[m_currentType];
            }
        }
        }
    }
    private void CountCooldowns()
    {
        for (int j = 0; j < m_playerSpecificCooldowns.Count; j++)
        {
            for (int i = 0; i < m_cooldowns.Count; i++)
            {
                if (m_playerSpecificCooldowns[j][i] > 0)
                {
                    m_playerSpecificCooldowns[j][i] -= 1 * Time.deltaTime;
                }
            }
        }
    }
    private int ChooseDebree(int playerint)
    {
        List<int> _possible = new List<int>();
        for(int i = 0;i<m_playerSpecificCooldowns[playerint].Count; i++)
        {
            if (m_playerSpecificCooldowns[playerint][i] <= 0)
            {
                _possible.Add(i);
            }
        }
        if (_possible.Count >0)
        {
            return (_possible[Random.Range(0, _possible.Count)]);
        }
        else { return(-1); }
    }
}
