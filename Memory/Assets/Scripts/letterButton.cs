using UnityEngine;
using System.Collections;

public class letterButton : MonoBehaviour {
	public Color highlightColor = Color.grey;

	public void OnMouseEnter()
	{
		
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null)
		{
			sprite.color = highlightColor;
		}
	}
	public void OnMouseExit()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null)
		{
			sprite.color = Color.white;
		}
	}
	
	public void OnMouseDown()
	{

			Application.LoadLevel("Settings");
			transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

		
		
	}
	public void OnMouseUp()
	{
		transform.localScale = Vector3.one;
	
	}
}

