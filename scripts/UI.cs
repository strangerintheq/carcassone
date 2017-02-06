using System;
using UnityEngine;
using UnityEngine.UI;

public class UI {
	
	private static Text text;

	public UI() {
		GameObject newCanvas = new GameObject("Canvas");
		Canvas c = newCanvas.AddComponent<Canvas>();
		c.renderMode = RenderMode.ScreenSpaceOverlay;

		newCanvas.AddComponent<CanvasScaler>();
		newCanvas.AddComponent<GraphicRaycaster>();

		GameObject panel = new GameObject("Panel");
		panel.transform.SetParent (newCanvas.transform, false);
		panel.AddComponent<CanvasRenderer>();

		RectTransform t = panel.AddComponent<RectTransform> ();
		t.anchorMin = new Vector2 (0, 0.9f);
		t.anchorMax = new Vector2 (1, 1);

		text = panel.AddComponent<Text>();
		text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		text.fontSize = 30;
		text.alignment = TextAnchor.MiddleCenter;

	}

	public static void DisplayMessage(string message) {
		text.text = message;
	}
}


