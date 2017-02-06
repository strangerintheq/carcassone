using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carcassone : MonoBehaviour {

	private Places places;
	private Tiles tiles;


	void Start () {
		places = new Places ();
		tiles = new Tiles ();
		new UI ();
	}

	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			tiles.GetCurrent().transform.Rotate (new Vector3 (0, 90, 0));
			places.Validate (tiles.GetCurrent ().GetComponent<Tile>());
		}
	}
		
	void OnMouseOver() {
		Vector3 hit = Raycaster.getHitPosition (0);
		if (Util.isEmptyVector(hit) || null == tiles) {
			return;
		}

		tiles.GetCurrent().transform.position = hit;
	}

	void OnMouseDown() {
		if (!Input.GetMouseButtonDown (0)) {
			return;
		}

		Vector3 hit = Raycaster.getHitPosition (0.01f);
		if (Util.isEmptyVector (hit)) {
			return;
		}
			
		GameObject place = places.Get (hit);
		if (null == place) {
			return;
		}

		if (!place.GetComponent<HasSides> ().Match (tiles.GetCurrent ().GetComponent<Tile> ())) {
			UI.DisplayMessage ("wrong place");
			return;
		} 

		tiles.PlaceCurrent (hit);
		Score.Calc (tiles);
		tiles.Next ();
		places.Update (tiles);
	}
		

}
