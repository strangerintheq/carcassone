using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Tiles {
	
	private List<GameObject> tiles = new List<GameObject>();
	private List<GameObject> placed = new List<GameObject>();

	private GameObject board = new GameObject ("Tiles on board");
	private GameObject deck = new GameObject ("Tiles in deck");
	private GameObject current;

	public Tiles() {
		
		board.transform.parent = GameObject.Find("Board").transform;
		string rulesSource = Resources.Load<TextAsset> ("rules/carcassone").text;
		Rules rules = JsonUtility.FromJson<Rules> (rulesSource);

		for (int i = 0; i < rules.tiles.Length; i++) {
			GameObject tile = create (rules.tiles[i], i);
			tile.transform.parent = deck.transform;
			tiles.Add(tile);
		}

		ActivateTile (rules.startTile);

		Shuffle (tiles);
	}

	private void ActivateTile(int index) {
		current = tiles [index];
		tiles.Remove (current);
		current.SetActive (true);
	}

	public void Next () {
		ActivateTile (0);
	}
		
	public void PlaceCurrent (Vector3 hit) {
		current.transform.parent = board.transform;
		current.transform.position = hit;
		placed.Add (current);
	}
		
	private GameObject create(Description description, int index) {
		
		GameObject tileObject = new GameObject();

		Tile tile = tileObject.AddComponent<Tile> () as Tile;
		tile.index = index + 1;
		tile.image = Resources.Load<Texture2D> ("textures/cards/" + description.image);
		tile.top = TileSideFromString(description.top);
		tile.right = TileSideFromString(description.right);
		tile.bottom = TileSideFromString(description.bottom);
		tile.left = TileSideFromString(description.left);
		tile.center = TileCenterFromString (description.center);

		Builder.primitive (PrimitiveType.Cube)
			.disableColliders ()
			.parent (tileObject)
			.rotation (0, 180, 0)
			.scale (1, 0.1f, 1)
			.position (0, 0.05f, 0)
			.texture (tile.image);
		
		tileObject.SetActive (false);
		tileObject.name = tile.ToString ();
		return tileObject;
	}

	public TileSide TileSideFromString(string value) {
		return (TileSide) System.Enum.Parse (typeof (TileSide), value.ToUpper());
	}

	public TileCenter TileCenterFromString(string value) {
		return (TileCenter) System.Enum.Parse (typeof (TileCenter), value.ToUpper());
	}

	public bool Contains (Vector3 point) {
		return Util.HasObjectWithLocation (placed, point);
	}

	public List<Tile> getWithNeighbourTiles (Vector3 point) {
		List<Tile> result = new List<Tile> ();
		for (int x = -1; x < 2; x++) {
			for (int z = -1; z < 2; z++) {
				GameObject tileObject = Util.GetObjectWithLocation (placed, new Vector3(point.x + x, 0, point.z + z));
				if (null != tileObject) {
					result.Add (tileObject.GetComponent<Tile> ());
				}
			}
		}
		return result;
	}

	private void Shuffle(List<GameObject> array) {
		for (int i = array.Count - 1; i > 0; i--) {
			int r = Random.Range(0, i);
			GameObject tmp = array[i];
			array[i] = array[r];
			array[r] = tmp;
		}
	}

	public List<GameObject> GetPlaced () {
		return placed;
	}

	public GameObject GetCurrent () {
		return current;
	}

	public Tile GetCurrentTile () {
		return current.GetComponent<Tile>();
	}

	public Tile GetPlacedTileAt (Vector3 point) {
		GameObject tile = Util.GetObjectWithLocation (placed, point);
		if (null != tile)
			return tile.GetComponent<Tile>();
		return null;
	}
}



