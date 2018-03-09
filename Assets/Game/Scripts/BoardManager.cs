using UnityEngine;

public class BoardManager : MonoBehaviour {
	// Store board grid positions
    public Vector3[] grid;

	void Reset () {
        grid = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            grid[i] = transform.GetChild(i).gameObject.transform.position;
        }
    }
}
