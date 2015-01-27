using UnityEngine;
using System.Collections;

public class MistScript : MonoBehaviour {
	public GameObject mist;
	private PlayerScript player;
	public float mistSpeed;
	public GameObject[] runers;
	private int runersEnMeta = 0;
	public int muertos;
    bool win;
    bool lose;
    float gameEnding;
    int temp;
	private bool audioONCE = true;

	void Start () {
		mistSpeed = Random.Range (25.0f, 35.0f);
		player = GameObject.Find ("PlayerStats").GetComponent<PlayerScript>();
		runers = GameObject.FindGameObjectsWithTag("Runner");

		player.audios [1].clip = player.sonidos [0];
		player.audios [1].Play ();

		player.levelsSucceded ++;
        player.lastLevelPlayed = Application.loadedLevel;

        win = false;
        lose = false;
	}
	
	void Update () {
		mist.transform.position += new Vector3 (mistSpeed * Time.deltaTime, 0, 0);

		foreach(GameObject r in runers){
			if(r.transform.position.x > 67)
				runersEnMeta ++;
		}

		if(runersEnMeta >= 3 && !lose){
            win = true;
		}

        if (muertos >= 3 && !win)
            lose = true;

        if (win)
        {
            gameEnding += Time.deltaTime;
            if (gameEnding > 3)
            {
				if(audioONCE){
					player.audios [1].clip = player.sonidos [8];
					player.audios [1].Play ();
					audioONCE = false;
				}

                gameEnding += Time.deltaTime;
                if (gameEnding > 3)
                    Application.LoadLevel("Live");
            }
        }

        if (lose)
        {
            gameEnding += Time.deltaTime;
            if (gameEnding > 1)
            {
                player.lives--;

				switch(player.lives){
				case 2:
					player.audios [1].clip = player.sonidos [11];
					break;
				case 1:
					player.audios [1].clip = player.sonidos [12];
					break;
				case 0:
					player.audios [1].clip = player.sonidos [13];
					break;
				}
				player.audios [1].Play ();

                if (player.lives != 0)
                    Application.LoadLevel("Live");
                else
                    Application.LoadLevel("LoserScreen");
            }
        }
			
	}
}