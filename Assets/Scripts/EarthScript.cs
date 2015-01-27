using UnityEngine;
using System.Collections;

public class EarthScript : MonoBehaviour {

    private PlayerScript stats;
    public bool loser;

	void Start()
    {
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerScript>();
        loser = false;
	}

    void OnTriggerEnter(Collider other)
    {
        loser = true;
	}

    void Update()
    {
        gameObject.transform.Rotate(Vector3.down, 0.2f);

        if (loser)
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
