// правила игры
[System.Serializable]
public class Rules {
	public int startTile;
	public Description[] tiles;
}

// описание тайла
[System.Serializable]
public class Description {
	public string image;
	public string top;
	public string right;
	public string bottom;
	public string left;
	public string center;
}