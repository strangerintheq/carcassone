using System;
using System.Collections.Generic;
using UnityEngine;

public class Road {
	
	private List<Tile> tilesInRoad = new List<Tile>();

	private Tile start;
	private SideDirection direction;
	private Tiles tiles;
	private bool cycle;
	private bool finished;

	public Road(Tile start, SideDirection direction, Tiles tiles) {
		this.tiles = tiles;
		this.direction = direction;
		this.start = start;
		travel (start, direction);
	}

	public Road(){}

	private void travel(Tile tile, SideDirection sideDirection) {
		Vector3 point = tile.GetNeighbourCoordinates (sideDirection);
		Tile next = tiles.GetPlacedTileAt (point);

		tilesInRoad.Add (tile);

		if (null == next) { // следующего тайла нет 
			return; 
		}

		if (next.Equals (tiles.GetCurrentTile ())) { // если пришли к текущему тайлу, то дорога замкнута
			cycle = true;
			finished = true;
			return; 
		}

		if (next.center != TileCenter.ROAD) { // дорога закончилась
			finished = true;
			tilesInRoad.Add (next);
			return; 
		}

		SideDirection neighbourTileSide = oppositeSide (sideDirection);
		travel (next, next.GetOppositeRoadDirections (neighbourTileSide) [0]);
	}

	private static SideDirection oppositeSide(SideDirection sideDirection){
		switch (sideDirection) {
		case SideDirection.TOP: 
			return SideDirection.BOTTOM;
		case SideDirection.BOTTOM:
			return SideDirection.TOP;
		case SideDirection.LEFT:
			return SideDirection.RIGHT;
		case SideDirection.RIGHT:
			return SideDirection.LEFT;
		}
		return SideDirection.UNDEFINED;
	}

	public int count() {
		return tilesInRoad.Count;
	}

	public bool isFinished() {
		return finished;
	}

	public void debug () {
		Debug.Log (start + " " + direction + " road: " + count () + ", finished=" + finished + ", cycle: " + cycle);
	}

	public bool Equals(Road other) {
		bool equal = true;
		foreach (Tile t in tilesInRoad) {
			equal &= other.tilesInRoad.Contains (t);
		}
		return equal;
	}

	public static Road combine (Road road1, Road road2) {
		Road combined = new Road ();
		addAll (combined.tilesInRoad, road1.tilesInRoad);
		addAll (combined.tilesInRoad, road2.tilesInRoad);
		combined.finished = true;
		return combined;
	}

	private static void addAll(List<Tile> target, List<Tile> toAdd){
		foreach(Tile t in toAdd) {
			if (target.Contains(t)) {
				continue;
			}

			target.Add(t);
		}
	}
}


