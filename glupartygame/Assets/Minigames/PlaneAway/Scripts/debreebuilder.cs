using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "debreebuilder")]
public class debreebuilder : ScriptableObject {
    [SerializeField]
    private float m_flightSpeed; public float flightSpeed { get { return (m_flightSpeed); } set { m_flightSpeed = value; } }
    [SerializeField]
    private Material m_debreeTexture; public Material debreeTexture { get { return (m_debreeTexture); } set { m_debreeTexture = value; } }
    [SerializeField]
    private Vector3 m_debreeSpawnSize; public Vector3 debreeSpawnsize { get { return (m_debreeSpawnSize); } set { m_debreeSpawnSize = value; } }
    [SerializeField]
    private float m_cooldown; public float cooldown { get { return (m_cooldown); } set { m_cooldown = value; } }
    [SerializeField]
    private float m_stunDuration; public float stunDuration { get { return (m_stunDuration); } set { m_stunDuration = value; } }
}
