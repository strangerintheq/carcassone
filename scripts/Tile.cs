using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : HasSides {
	
	public int index;
	public Texture2D image;
	public TileCenter center;

	public override string ToString() {
		return "Tile " + index.ToString ("00") + " " + AsString () + firstChar(center.ToString ());
	}

	// возвращает сторону тайла
	public TileSide GetRotatedSideValue(SideDirection sideType) {
		var shift = (int) transform.rotation.eulerAngles.y / 90;
		var index = (int) sideType;
		index = index - shift;
		if (index < 1) {
			index += 4;
		}
		return GetSideValue ((SideDirection)index);
	}

	// проверяет есть ли у тайла указанная сторона
	public bool AnySideIs (TileSide side) {
		return side == top || side == right || side == bottom || side == left;
	}

	// получает направления тайла, с указанной стороной, с учетом поворота
	public List<SideDirection> GetRotatedSides (TileSide side) {
		List<SideDirection> directions = new List<SideDirection> ();
		foreach (SideDirection sideDirection in System.Enum.GetValues (typeof(SideDirection))) {
			
			if (sideDirection != SideDirection.UNDEFINED && 
				side == GetRotatedSideValue (sideDirection)) {

				directions.Add (sideDirection);
			}
		}
		return directions;
	}

	public List<SideDirection> GetOppositeRoadDirections(SideDirection fromDirection) {
		List<SideDirection> roads = GetRotatedSides (TileSide.ROAD);
		roads.Remove (fromDirection);
		return roads;
	}

	// возвращает координаты соседний тайлов
	public Vector3 GetNeighbourCoordinates (SideDirection sideDirection) {
		Vector3 p = transform.localPosition;
		switch (sideDirection) {
		case SideDirection.TOP: 
			return new Vector3(p.x, 0, p.z + 1);
		case SideDirection.BOTTOM:
			return new Vector3(p.x, 0, p.z - 1);
		case SideDirection.LEFT:
			return new Vector3(p.x - 1, 0, p.z);
		case SideDirection.RIGHT:
			return new Vector3(p.x + 1, 0, p.z);
		}
		return Vector3.zero;
	}
		
}
