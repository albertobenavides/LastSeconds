using UnityEngine;
using System.Collections;

public class LastManScript : MonoBehaviour {
	private PlayerScript stats;
    float timer;
    public Sprite win;
    public Sprite lose;
    bool winer;
    bool loser;
    float gameEnding;
	private bool soundOnce = true;
	private bool audioONCE = true;
	
	void Start () 
    {
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerScript>();
        stats.levelsSucceded++;
        timer = Random.Range(2, 5);
        winer = loser = false;
        gameEnding = 0;
        stats.lastLevelPlayed = Application.loadedLevel;
	}

	void Update () 
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !loser)
        {
            winer = true;
			if(audioONCE){
				stats.audios [1].clip = stats.sonidos [8];
				stats.audios [1].Play ();
				audioONCE = false;
			}

			if (soundOnce)
            {
				stats.audios [1].clip = stats.sonidos [7];
				stats.audios [1].Play ();
				soundOnce = false;
			}
        }

        if (Input.touchCount > 0 && !winer)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began){
                loser = true;

				if(soundOnce)
                {
					stats.audios [1].clip = stats.sonidos [6];
					stats.audios [1].Play ();
					soundOnce = false;
				}
			}
        }

        if (winer)
        {
            gameEnding += Time.deltaTime;
            if (gameEnding < 2)
                GameObject.Find("Fondo").GetComponent<SpriteRenderer>().sprite = win;
            else if (gameEnding > 3)
                Application.LoadLevel("Live");
        }

        if (loser)
        {
            gameEnding += Time.deltaTime;
            if (gameEnding < 2)
                GameObject.Find("Fondo").GetComponent<SpriteRenderer>().sprite = lose;
            else if (gameEnding > 3)
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