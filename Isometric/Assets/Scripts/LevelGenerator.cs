using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
	public Texture2D levelImage;
	// Use this for initialization
	void Awake () {

		var pixelCountWidth = levelImage.width;
		var pixelCountHeight = levelImage.height;

		for (int x = 0; x < pixelCountWidth; x++)
		{
			for (int y = 0; y < pixelCountHeight; y++)
			{
				var pixelColour = levelImage.GetPixel(x, y);
					Debug.Log("Pixel at " + x.ToString() +"," + y.ToString() + " colour: " + pixelColour.ToString() );

				if (pixelColour == Color.black)
				{
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = new Vector3(x, 0, y);
					cube.transform.parent = this.transform;
					Debug.Log("Found a black pixel");
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
