using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

    public Vector3 target;
    public GameObject Board;
    public int currentGirdID = 1;
    public int diceValue;
    public NavMeshAgent agent;
    public bool isMyTurn = false;
    public GameObject targetGrid;
    



    // Use this for initialization
    void Start () {
        Board = FindObjectOfType<BoardManager>().gameObject;
        target = Board.transform.GetChild(1).gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target;
		currentGirdID = 1;

    }
	
	// Update is called once per frame
	void Update () {
		if(isMyTurn == true)
        {
            // find distance to my target
            // if less then stopping distance - call EndOfStep ()
            float distance = Vector3.Distance(target, transform.position);
            if (distance <= agent.stoppingDistance) {
                EndOfStep();
            }
        }
	}

    public void PlayerTurn()
    {
        Debug.Log("Player " + name + " moving " + diceValue + " squares");
        isMyTurn = true;
        DiceRoll();
    }

    void DiceRoll()
    {
        currentGirdID = currentGirdID + 1;
        diceValue = diceValue - 1;
        target = Board.transform.GetChild(currentGirdID).gameObject.transform.position;
        agent.destination = target;
    }

    void EndOfStep()
    {
        Debug.Log(name + " has finished the step " + currentGirdID);

        if (currentGirdID >= 100)
        {
			//Game Over
            Application.LoadLevel(Application.loadedLevel);
        }

        if (diceValue > 0) {
            DiceRoll();
        } else {
            //EndOfTurn();
            CheckForSnakesOrLadders();
        }
    }

    void EndOfTurn() {
        Debug.Log(name + " has finished the turn");
        isMyTurn = false;
        GetComponentInParent<PlayerManager>().PlayerFinishedMovement();
    }

    void CheckForSnakesOrLadders() {
        targetGrid = Board.transform.GetChild(currentGirdID).gameObject;
        if ((targetGrid.GetComponent ("Ladder") as Ladder)!=null) {
            currentGirdID = targetGrid.GetComponent<Ladder>().gridID;
            target = Board.transform.GetChild(currentGirdID).gameObject.transform.position;
            agent.destination = target;
            
            return;
        }
        if ((targetGrid.GetComponent("Snake") as Snake) != null)
        {
            currentGirdID = targetGrid.GetComponent<Snake>().gridID;
            target = Board.transform.GetChild(currentGirdID).gameObject.transform.position;
            agent.destination = target;
            return;
        }
        EndOfTurn();
    }

}
