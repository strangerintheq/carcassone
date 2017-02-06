using UnityEngine;

public class CameraDrag : MonoBehaviour {
	
	public float dragSpeed = 0.02f;
	public int max = 10;

	private Vector3 dragOrigin;
	private int button = 1;

	void Start() {
		transform.position = new Vector3 (0.5f, 10, 0.5f);
	}

	void Update() {
		
		if (Input.GetMouseButtonDown(button)) {
			dragOrigin = Input.mousePosition;
			return;
		}

		if (!Input.GetMouseButton(button)) 
			return;

		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

		float x = transform.position.x;
		float z = transform.position.z;

		if (move.x > 0 && x > max || move.x < 0 && x < -max)
			move.x = 0;

		if (move.z > 0 && z > max || move.z < 0 && z < -max)
			move.z = 0;
	
		transform.Translate(move, Space.World);  
	}

}
