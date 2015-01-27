using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoonScript : MonoBehaviour {
	public float moonSize;
	public float moonIncreaser;
	private float closerBitch;
	public GameObject sizeIndicator;
    float timer;
	private PlayerScript stats;
	private float timerAudio, timerAudioAUX;
    bool win, lose;
    float gameEnding;
	private bool audioONCE = true;

	void Start () {
		stats = GameObject.Find ("PlayerStats").GetComponent<PlayerScript> ();
		stats.levelsSucceded ++;
        stats.lastLevelPlayed = Application.loadedLevel;
		moonIncreaser = 0.3f;
        timer = 3;

        win = lose = false;
        gameEnding = 0;

		stats.audios [1].clip = stats.sonidos [4];
		timerAudio = 1;
	}
	
	void Update () {

		if(timerAudio < timerAudioAUX){
			stats.audios [1].Play ();
			timerAudioAUX = 0;
		}
		else{
			timerAudioAUX += Time.deltaTime;
		}

        if (transform.localScale.x > 3.6f && transform.localScale.x < 4.20f)
            GameObject.Find("Halo").GetComponent<SpriteRenderer>().color = Color.green;
        else
            GameObject.Find("Halo").GetComponent<SpriteRenderer>().color = Color.red;

		if(Input.acceleration.z > 0){
			moonSize = Mathf.Lerp(0.5f, 1, Input.acceleration.z);
		}
		else if(Input.acceleration.z < 0){
			moonSize = Mathf.Lerp(0.5f, 0, Mathf.Abs(Input.acceleration.z));
		}

		transform.localScale = Vector3.Lerp (new Vector3(2.5f,2.5f,2.5f),
		                                     new Vector3(8,8,8),
		                                     moonSize);



		closerBitch += Time.deltaTime;
		transform.localScale += new Vector3 (moonIncreaser * closerBitch,
		                                     moonIncreaser * closerBitch, 
		                                     moonIncreaser * closerBitch);

        timer -= Time.deltaTime;
        if (timer <= 0 && !lose && !win)
        {
            if (transform.localScale.x > 3.5f && transform.localScale.x < 4.3f)
            {
                win = true;
				if(audioONCE)
                {
					stats.audios [1].clip = stats.sonidos [8];
					stats.audios [1].Play ();
					audioONCE = false;
				}
            }
            else
            {
                lose = true;
            }
        }

        if (win)
        {
            gameEnding += Time.deltaTime;
            if (gameEnding > 3)
                Application.LoadLevel("Live");
        }

        if (lose)
        {
            gameEnding += Time.deltaTime;
            if (gameEnding > 3)
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
