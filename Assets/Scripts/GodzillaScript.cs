using UnityEngine;
using System.Collections;

public class GodzillaScript : MonoBehaviour {

    public RaycastHit hit;
    int godzillaLive;
    bool win, lose;
    float gameTimer, endTimer;
    public Sprite godzillaDead;
    public Sprite destroyedBuildings;
	private PlayerScript stats;
	private GameObject player;
	private bool audioONCE = true;

	void Start () 
    {
        win = lose = false;
        godzillaLive = 10;
        gameTimer = endTimer = 0;

		player = GameObject.Find ("PlayerStats");
		stats = player.GetComponent<PlayerScript> ();
        stats.levelsSucceded++;
        stats.lastLevelPlayed = Application.loadedLevel;

		stats.audios [1].clip = stats.sonidos [10];
	}
	
	void Update () 
    {
		if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
				var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider.tag == "Godzilla"){
						godzillaLive--;
						stats.audios [1].Play ();
					}
				}
			}
		}
		
		gameTimer += Time.deltaTime;
		if (gameTimer > 4 && !win)
			lose = true;
		
		if (godzillaLive <= 0 && !lose)
        {
            win = true;

			if(audioONCE){
				stats.audios [1].clip = stats.sonidos [8];
				stats.audios [1].Play ();
				audioONCE = false;
			}
        }

        if (win)
        {
            endTimer += Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().sprite = godzillaDead;
            gameObject.transform.localPosition = new Vector3(4.28f, transform.localPosition.y - 1.0f, -0.5f);

            if (endTimer > 2)
                Application.LoadLevel("Live");
        }

        if (lose)
        {
                endTimer += Time.deltaTime;
                GameObject.Find("Buildings").GetComponent<SpriteRenderer>().sprite = destroyedBuildings;

                if (endTimer > 2)
                {
                    stats.lives--;

				switch(stats.lives){
				case 2:
					stats.audios [1].clip = stats.sonidos [11];
					break;
				case 1:
					stats.audios [1].clip = stats.sonidos [12];
					break;
				case 0:
					stats.audios [1].clip = stats.sonidos [13];
					break;
				}
				stats.audios [1].Play ();
				
                    if (stats.lives != 0)
                        Application.LoadLevel("Live");
                    else
                        Application.LoadLevel("LoserScreen");
                }
        }
	}
}
