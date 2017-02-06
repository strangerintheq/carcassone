using System;
using UnityEngine;
using System.Collections.Generic;

public class Score {
	
	public static void Calc (Tiles tiles) {
		calcCastles (tiles);
		calcRoads (tiles);
		calcMonasteries (tiles);
	}

	private static void calcCastles (Tiles tiles) {
		Tile current = tiles.GetCurrentTile ();
		foreach (SideDirection dir in current.GetRotatedSides (TileSide.CASTLE)) {
			Castle castle = new Castle (current, dir, tiles);
			castle.debug ();
		}
	}

	private static void calcRoads (Tiles tiles) {
		Tile current = tiles.GetCurrentTile ();
		List<Road> finished = new List<Road> ();
		foreach (SideDirection dir in current.GetRotatedSides (TileSide.ROAD)) {
			Road road = new Road (current, dir, tiles);
			if (road.isFinished ()) {
				finished.Add(road);
			}
		}

		if (current.center == TileCenter.ROAD) {
			tryCombine (current, finished);
		} else {
			finished = collapse (finished);	
		}
			
		foreach (Road r in finished) {
			r.debug ();
		}
	}

	static void tryCombine (Tile current, List<Road> finished) {
		if (finished.Count < 2) {
			finished.Clear ();
			return;
		}

		Road combined = Road.combine (finished [0], finished [1]);
		finished.Clear ();
		finished.Add (combined);
	}

	static List<Road> collapse (List<Road> finished) {
		List<Road> collapsed = new List<Road> ();
		foreach (Road road in finished) {
			if (!contains (collapsed, road)) {
				collapsed.Add (road);
			}
		}
		return collapsed;
	}

	private static bool contains(List<Road> roads, Road road){
		foreach (Road r in roads) {
			if (road.Equals (r)) {
				return true;
			}
		}
		return false;
	}

	private static void calcMonasteries (Tiles tiles) {
		Vector3 point = tiles.GetCurrent ().transform.localPosition;
		foreach (Tile tile in tiles.getWithNeighbourTiles (point)) {
			if (tile.center == TileCenter.MONASTERY && 
				tiles.getWithNeighbourTiles(tile.gameObject.transform.localPosition).Count == 9) {
				UI.DisplayMessage (tile + " monastery completed");
			}
		}
	}

}


