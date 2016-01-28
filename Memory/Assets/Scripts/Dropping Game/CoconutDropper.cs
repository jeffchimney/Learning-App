using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoconutDropper : MonoBehaviour {
	[SerializeField] private SpawnCoconuts coconut; // hold the coconut prefab into this variable in the inspector
	private SpawnCoconuts thisCoconut;
	public Sprite [] coconutImages; // store all of the sprites to be instantiated into this array
	[SerializeField] private AudioClip[] letterSounds; // store each letter sound
	private List<SpawnCoconuts> allCoconuts = new List<SpawnCoconuts>();
    AudioSource playAudio;
	int spawnMin = 2; // min time of dropper
	int spawnMax = 5; // max time of dropper

	// Use this for initialization
	// Instantiate all of the prefabs off screen, and then change the position to begin height of dropper
	void Awake(){
		Vector3 position = this.transform.position;
		position.y = 6;
		this.transform.position = position;
	}

	void Start () {
        playAudio = GetComponent<AudioSource>();
        createCoconuts ();
		Spawn ();
	}
	
	// Update is called once per frame, change the position of the dropper to varying x-values
	void Update () {
		Vector3 dropperPosition = this.transform.position; 
		dropperPosition.x = Random.Range (-5, 10);
		this.transform.position = dropperPosition;
	}

	// this method will be used to create the variety of letters that will be dropping, storing them into an array.
	public void createCoconuts(){
		for (int i = 0; i < coconutImages.Length; i++) {
			SpawnCoconuts currCoconut;
			if (i == 0)
			{
				currCoconut = coconut;
			}
			else
			{
				currCoconut = Instantiate(coconut) as SpawnCoconuts;
			}

			thisCoconut = currCoconut.setCoconut (coconutImages [i], letterSounds[i], i);
			//currCoconut.GetComponent<Rigidbody2D> ().gravityScale = 0;
			allCoconuts.Add (thisCoconut); // store all of the coconuts that are created into an array so we can use this for dropping
		}

	}

	//Spawn method used to drop the coconuts randomly
	void Spawn(){
		int rand = Random.Range (0, allCoconuts.Count);
        playAudio.PlayOneShot(letterSounds[rand]); // play the corresponding letter sound on drop
        Instantiate(allCoconuts[rand], transform.position, Quaternion.identity);
		Invoke("Spawn", Random.Range (spawnMin, spawnMax)); // after random time between min and max, respawn
	}
}
