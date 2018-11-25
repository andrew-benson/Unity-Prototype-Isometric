using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour {
	public Texture2D levelImage;
    public GameObject player;
    public float groundY = 0;

    [Serializable]
    public struct LevelGeneratorColourMatch
    {
        public Color c;
        public GameObject go;
    }

    public LevelGeneratorColourMatch[] coloursToGameObjects;
    private Dictionary<Color, GameObject> colourToGameObjectDict = new Dictionary<Color, GameObject>();
    // Use this for initialization
    void Awake () {

        PopulateDictionary();

        var pixelCountWidth = levelImage.width;
        var pixelCountHeight = levelImage.height;
        Transform startTransform = player.transform;

        for (int x = 0; x < pixelCountWidth; x++)
        {
            for (int y = 0; y < pixelCountHeight; y++)
            {
                var pixelColour = levelImage.GetPixel(x, y);


                if (pixelColour == Color.green)
                {
                    startTransform.position = new Vector3(x, groundY + 1, y);
                    startTransform.parent = this.transform;
                }
                    


                GameObject go = null;
                if (colourToGameObjectDict.TryGetValue(pixelColour, out go))
                {
                    GameObject levelObject = Instantiate(go);
                    levelObject.transform.position = new Vector3(x, groundY, y);
                    levelObject.transform.parent = this.transform;
                }
            }
        }

        // Rotate everything by 45 degrees
        transform.eulerAngles = new Vector3(0, 45, 0);


        // After the rotation
        player.transform.position = startTransform.position;
    }

    private void PopulateDictionary()
    {
        foreach (var colourMatch in coloursToGameObjects)
        {
            colourToGameObjectDict.Add(colourMatch.c, colourMatch.go);
        }
    }

}
