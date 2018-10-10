using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debree : MonoBehaviour {

    [SerializeField]
    private debreebuilder mybuilder; public debreebuilder _mybuilder { get { return (mybuilder); } set { mybuilder = value; } }
    private float debreespeed = 2;
    private float stunlgth;
	
	void Update () {
        transform.position += transform.forward * (debreespeed * Time.deltaTime);
	}

    private void flightpattern()
    {

    }
    private void OnEnable()
    {
        gameObject.GetComponent<Renderer>().material = mybuilder.debreeTexture;
        debreespeed = mybuilder.flightSpeed;
        stunlgth = mybuilder.stunDuration;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "DebreeFlightLimit")
        {
            gameObject.GetComponent<poolItem>().BackToPool();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<playermovement>()._stun = 2;
            gameObject.GetComponent<poolItem>().BackToPool();
        }
       
    }
}
