using UnityEngine;
using System.Collections;

public class MeteorScript : MonoBehaviour {
	public RaycastHit hit;
	public AudioClip[] shotSound;
	public AudioSource fuenteSonido;
	private GameObject recipient;
	public float timeToSpawn;
	private float timeAux;
	public GameObject spawner;
	public GameObject[] meteors;
    private GameObject player;
    private int rando;
	private Vector3 randoPos;
	public int meteorsToShow;
	private PlayerScript stats;
    bool win;
    float gameEnding;
	private bool audioONCE = true;

	void Start () 
    {
        player = GameObject.Find ("PlayerStats");
		stats = player.GetComponent<PlayerScript> ();
		fuenteSonido = player.GetComponent<AudioSource>();
        timeAux = 0;
		stats.levelsSucceded ++;
        stats.lastLevelPlayed = Application.loadedLevel;
		meteorsToShow = player.GetComponent<PlayerScript>().levelsSucceded + 1;
        if (meteorsToShow > 3)
            meteorsToShow = 3;
        win = false;
        gameEnding = 0;
	}
	
	
	void Update () {
		gunShot ();

		if(timeAux > timeToSpawn && meteorsToShow >= 1){
			rando = Random.Range(0,3);
			randoPos = new Vector3 (0, Random.Range(-10,20), 0);
			Instantiate (meteors[rando], spawner.transform.position + randoPos , Quaternion.identity);
			stats.audios [1].clip = stats.sonidos [5];
			stats.audios [1].Play ();
			timeAux = 0;
			meteorsToShow --;
		}
		else{
			timeAux += Time.deltaTime;
		}

		if (meteorsToShow < 1) 
        {
            win = true;
		}

        if (win)
        {
			if(audioONCE){
				stats.audios [1].clip = stats.sonidos [8];
				stats.audios [1].Play ();
				audioONCE = false;
			}
            gameEnding += Time.deltaTime;
            if (gameEnding > 2)
                Application.LoadLevel("Live");
        }

	}
	
	private void gunShot(){
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				var ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.tag == "Meteor") {
						stats.audios [1].clip = stats.sonidos [3];
						stats.audios [1].Play ();
						Destroy(hit.collider.gameObject);
					}
				}

			}
		}
	}
}
