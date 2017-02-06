using System;
using UnityEngine;
using System.Collections.Generic;

public class Castle {
	Tile start;

	
	public Castle (Tile start, SideDirection dir, Tiles tiles) {
		this.start = start;
		if (start.center == TileCenter.CASTLE) {
			
		} else {
			build (start, dir);
		}
	}

	void build (Tile current, SideDirection dir) {
		
	}

	public void debug () {
		Debug.Log ();
	}
	
}



