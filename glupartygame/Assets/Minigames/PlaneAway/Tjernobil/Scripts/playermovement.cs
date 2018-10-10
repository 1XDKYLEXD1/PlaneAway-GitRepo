using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour {
    [SerializeField]
    private RandomSpawner spwen;
    [SerializeField]
    private KeyCode[] movement;
    private Vector3 movespeeds;
    [SerializeField]
    private float acceleration=2;
    [SerializeField]
    private float jumpacceleration=2;
    [SerializeField]
    private float gravity =20;
    [SerializeField]
    private Material stuns;
    [SerializeField]
    private Material nostuns;
    [SerializeField]
    private float extramomentum; public float _extramomentum { get { return (extramomentum); } set { extramomentum = value; } }
    private float stun = 0; public float _stun { get { return (stun); } set { stun = value; } }
	// Use this for initialization
	void Start () {
        spwen.playerList.Add(gameObject);
        spwen.AdjustCooldowns();
	}
	
	// Update is called once per frame
	void Update () {
        if (stun > 0)
        {
            stun -= 1 * Time.deltaTime;
        }
        if (stun <= 0)
        {
            if (Input.GetKey(movement[0]))
            {
                movespeeds.x = -acceleration;
            }
            else
            if (Input.GetKey(movement[1]))
            {
                movespeeds.x = +acceleration;
            }
            else
            {
                movespeeds.x = 0;
            }
            if (Input.GetKeyDown(movement[2]))
            {
                movespeeds.y = jumpacceleration;
            }
            movespeeds.x += extramomentum;
            gameObject.GetComponent<Renderer>().material = nostuns;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = stuns;
            movespeeds.x = extramomentum;
        }
        if (movespeeds.y > -gravity)
        {
            movespeeds.y -= gravity * Time.deltaTime;
        }
        
        
        gameObject.GetComponent<CharacterController>().Move(movespeeds *Time.deltaTime);
        
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.name == "DebreeFlightLimit")
        {
            spwen.playerList.Remove(gameObject);
            spwen.AdjustCooldowns();
            spwen.WinCheck();
        }
    }

}
