using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject dice;
    public Rigidbody diceRB;
    public int currentDiceValue;
    [Range (2,8)]
    public int playerNumber = 2;
    public bool isPlayerMoving = false;
    public int currentPlayer = 1;
    public float diceTorque, diceForce;
    public int frames = 100;
    [Range(0.1f, 100)]
    public float timeScale = 1;

    // Use this for initialization
    void Start () {
        diceRB = dice.GetComponent<Rigidbody>();
        RollDice();


    }
	
	// Update is called once per frame
	void Update () {

        Time.timeScale = timeScale;

        if (frames > 0)
        {
            frames--;
        }
        else {
            currentDiceValue = dice.GetComponent<Dice>().value;
            if (Mathf.Abs(diceRB.velocity.magnitude) < 0.001f && isPlayerMoving == false && currentDiceValue > 0)
            {
                GameObject player = transform.GetChild(currentPlayer - 1).gameObject;
                player.GetComponent<Player>().diceValue = currentDiceValue;
                player.GetComponent<Player>().PlayerTurn();
                isPlayerMoving = true;
            }
        }

        
	}

    public void PlayerFinishedMovement()
    {
        Debug.Log("Player " + currentPlayer + " just finished movement");
        isPlayerMoving = false;
        currentDiceValue = 0;
        currentPlayer++;
        if (currentPlayer > playerNumber) {
            currentPlayer = 1;
        }
        RollDice();
    }

    void RollDice() {
        //diceRB.AddTorque(new Vector3 (Random.Range (-diceTorque,diceTorque), Random.Range(-diceTorque, diceTorque), Random.Range(-diceTorque, diceTorque)));
        diceRB.AddForce(new Vector3 (Random.Range(-diceForce / 2, diceForce / 2), diceForce, Random.Range(-diceForce / 2, diceForce / 2)));
        diceRB.angularVelocity = new Vector3(Random.Range(-diceTorque, diceTorque), Random.Range(-diceTorque, diceTorque), Random.Range(-diceTorque, diceTorque));
        frames = 10;
    }

}
