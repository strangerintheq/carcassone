using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasSides : MonoBehaviour {

	public TileSide top;
	public TileSide right;
	public TileSide bottom;
	public TileSide left;

	public string C(TileSide side) {
		return firstChar(side.ToString ());
	}

	public static string firstChar(string str){
		return str.ToCharArray () [0].ToString();
	}

	public string AsString() {
		return C(top) + C(right) + C(bottom) + C(left);
	}

	public void AddSide (SideDirection sideType, TileSide side) {
		switch (sideType) {
		case SideDirection.TOP: 
			top = side;
			return;
		case SideDirection.BOTTOM:
			bottom = side;
			return;
		case SideDirection.LEFT:
			left = side;
			return;
		case SideDirection.RIGHT:
			right = side;
			return;
		}
	}

	public TileSide GetSideValue(SideDirection sideType) {
		switch (sideType) {
		case SideDirection.TOP: 
			return top;
		case SideDirection.BOTTOM:
			return bottom;
		case SideDirection.LEFT:
			return left;
		case SideDirection.RIGHT:
			return right;
		}
		return TileSide.UNDEFINED;
	}
		
	public bool Match(Tile other) {
		bool result = true;
		if (top != TileSide.UNDEFINED)
			result &= top == other.GetRotatedSideValue(SideDirection.TOP);
		if (right != TileSide.UNDEFINED)
			result &= right == other.GetRotatedSideValue(SideDirection.RIGHT);
		if (bottom != TileSide.UNDEFINED)
			result &= bottom == other.GetRotatedSideValue(SideDirection.BOTTOM);
		if (left != TileSide.UNDEFINED)
			result &= left == other.GetRotatedSideValue(SideDirection.LEFT);
		return result;
	}
}


