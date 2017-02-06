using UnityEngine;


public static class Raycaster {
	
	public static Vector3 getHitPosition (float y) {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll (ray, Mathf.Infinity);
		foreach (RaycastHit hit in hits) {
			if (hit.collider.gameObject.name.Contains("Board")) {
				return new Vector3(round(hit.point.x), y, round(hit.point.z));
			}
		}
		return Vector3.zero;
	}

	private static float round(float value) {
		return (value > 0 ? Mathf.Floor (value) : Mathf.Floor (value)) + 0.5f;
	}
}

