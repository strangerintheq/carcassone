using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Places {

	private List<GameObject> places = new List<GameObject> ();

	private GameObject placesObject = new GameObject("Places");

	private Texture2D roadSideTexture = Resources.Load<Texture2D> ("textures/sides/road");
	private Texture2D planeSideTexture = Resources.Load<Texture2D> ("textures/sides/plane");
	private Texture2D castleSideTexture = Resources.Load<Texture2D> ("textures/sides/castle");

	public Places() {
		placesObject.transform.parent = GameObject.Find ("Board").transform;
		places.Add (createPlace (0.5f, 0.5f));
	}

	public void Update (Tiles tiles) {
		Clear ();

		foreach (GameObject obj in tiles.GetPlaced()) {
			Vector3 p = obj.transform.position;
			Tile tile = obj.GetComponent<Tile> ();
			addPlace (p.x + 1, p.z, tiles, tile.GetRotatedSideValue(SideDirection.RIGHT), SideDirection.LEFT);
			addPlace (p.x - 1, p.z, tiles, tile.GetRotatedSideValue(SideDirection.LEFT), SideDirection.RIGHT);
			addPlace (p.x, p.z + 1, tiles, tile.GetRotatedSideValue(SideDirection.TOP), SideDirection.BOTTOM);
			addPlace (p.x, p.z - 1, tiles, tile.GetRotatedSideValue(SideDirection.BOTTOM), SideDirection.TOP);
		}		

		Validate (tiles.GetCurrent ().GetComponent<Tile>());
	}

	public void Validate(Tile tile) {
		foreach (GameObject place in places) {
			place.GetComponent<MeshRenderer> ().material.color = 
				place.GetComponent<HasSides> ().Match (tile) ? Color.white : Color.gray;
		}
	}

	private void addPlace (float x, float z, Tiles tiles, TileSide side, SideDirection direction) {

		Vector3 point = new Vector3 (x, 0, z);

		if (tiles.Contains (point)) {
			return;
		}

		if (Contains (point)) {
			addSide (Util.GetObjectWithLocation (places, point), direction, side);
			return;
		}

		GameObject place = createPlace (x, z);

		places.Add (place);

		addSide (place, direction, side);
	}

	GameObject createPlace(float x, float z){
		GameObject place = Builder.primitive (PrimitiveType.Cube)
			.name ("Place " + Format (x) + " " + Format (z))
			.parent (placesObject)
			.disableColliders()
			.position (x, 0, z)
			.color (Color.white)
			.scale(1, 0.1f, 1)
			.Get();

		place.AddComponent<HasSides> ();
		return place;
	}

	void addSide(GameObject place, SideDirection direction, TileSide side) {
		
		place.GetComponent<HasSides> ().AddSide (direction, side);
		int rotation = direction == SideDirection.LEFT || direction == SideDirection.RIGHT ? 90 : 0;

		Builder.primitive (PrimitiveType.Cube)
			.name (side.ToString ())
			.disableColliders()
			.parent(place)
			.position (GetPosition (direction))
			.rotation(0, rotation , 0)
			.scale (1, 1, 0.1f)
			.texture (GetTexture (side));
	}
		

	Vector3 GetScale (SideDirection sideType) {
		switch (sideType) {
		case SideDirection.TOP: 
		case SideDirection.BOTTOM:
			return new Vector3(1, 1, 0.15f);
		case SideDirection.LEFT:
		case SideDirection.RIGHT:
			return new Vector3(0.15f, 1, 1);
		}
		return Vector3.zero;
	}

	Texture2D GetTexture (TileSide side) {
		switch (side) {
		case TileSide.CASTLE: 
			return castleSideTexture;
		case TileSide.PLANE:
			return planeSideTexture;
		case TileSide.ROAD:
			return roadSideTexture;
		}
		return Texture2D.blackTexture;
	}


	Vector3 GetPosition (SideDirection sideType) {
		switch (sideType) {
		case SideDirection.TOP: 
			return new Vector3(0, 0.1f, 0.45f);
		case SideDirection.BOTTOM:
			return new Vector3(0, 0.1f, -0.45f);
		case SideDirection.LEFT:
			return new Vector3(-0.45f, 0.1f, 0);
		case SideDirection.RIGHT:
			return new Vector3(0.45f, 0.1f, 0);
		}
		return Vector3.zero;
	}

	string Format(float value) {
		value -= 0.5f;
		return value.ToString();
	}

	void Clear () {
		foreach (GameObject place in places) {
			GameObject.Destroy (place);
		}
		places.Clear ();
	}

	public bool Contains (Vector3 hit) {
		return null != Get(hit);
	}

	public GameObject Get (Vector3 hit) {
		return Util.GetObjectWithLocation (places, hit);
	}

}
