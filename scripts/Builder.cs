using System;
using UnityEngine;

public class Builder {
	
	private GameObject o;

	public static Builder primitive(PrimitiveType type) {
		return new Builder (GameObject.CreatePrimitive(type));
	}
		
	public static Builder empty() {
		return new Builder (new GameObject ());
	}

	public static Builder wrap(GameObject obj) {
		return new Builder (obj);
	}
				
	private Builder(GameObject obj) {
		o = obj;
	}
		
	public GameObject Get() {
		return o;
	}
		
	public Builder position(Vector3 point) {
		o.transform.localPosition = point;
		return this;
	}
		
	public Builder scale(Vector3 scale) {
		o.transform.localScale = scale;
		return this;
	}
		
	public Builder rotation(float x, float y, float z) {
		o.transform.localEulerAngles = new Vector3(x, y, z);
		return this;
	}

	public Builder name(string name) {
		o.name = name;
		return this;
	}

	public Builder parent(GameObject obj) {
		o.transform.parent = obj.transform;
		return this;
	}
		
	public Builder texture (Texture2D texture) {
		o.GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Unlit/Texture");
		o.GetComponent<MeshRenderer> ().material.SetTexture("_MainTex", texture);
		return this;
	}
		
	public Builder disableColliders() {
		foreach(Collider collider in o.GetComponents<Collider>()) {
			collider.enabled = false;
		}
		return this;
	}

	public Builder color (Color color) {
		o.GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Unlit/Color");
		o.GetComponent<MeshRenderer> ().material.color = color;
		return this;
	}
		
	public Builder position(float x, float y, float z) {
		return position(new Vector3(x, y, z));
	}

	public Builder scale(float xyz) {
		return scale(new Vector3(xyz, xyz, xyz));
	}

	public Builder scale(float x, float y, float z) {
		return scale(new Vector3(x, y, z));
	}
		
}


