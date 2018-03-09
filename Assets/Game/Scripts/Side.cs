using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Side : MonoBehaviour {

    public GameObject dice;
    
    public int id;

    private void Start()
    {
        dice = GetComponentInParent<Dice>().gameObject;
        string name = gameObject.name;
        id = Convert.ToInt32(name);
    }

    void OnTriggerEnter(Collider other)
    {
        dice.BroadcastMessage("Side", id);
    }
}
