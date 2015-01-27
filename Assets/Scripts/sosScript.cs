using UnityEngine;
using System.Collections;

public class sosScript : MonoBehaviour {

    int timer;
    int choice;
    bool pushed;
    public Sprite[] morse;
    public Sprite[] winLose;
    private PlayerScript stats;
    bool win;
    bool lose;
    float gameEnding;
    bool playOnce;
	private bool audioONCE = true;
	
	void Start () 
    {
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerScript>();
        stats.levelsSucceded++;
        timer = 0;
        GameObject.Find("Capsule").renderer.material.color = Color.red;
        choice = Random.Range(1, 4);
        string temp = "Cube" + choice.ToString();
        GameObject.Find(temp).renderer.material.color = Color.gray;
        pushed = false;
        win = false;
        lose = false;

		stats.audios [1].clip = stats.sonidos [2];
		stats.audios [1].Play ();

        gameEnding = 0;

        playOnce = false;
    }
	
	void FixedUpdate () 
    {
        if (!win || !lose)
        {
            if (timer == 0)
            {
                GameObject.Find("Sphere1").renderer.material.color = Color.red;
                GameObject.Find("Capsule").transform.position = new Vector3(-6.6f, 0.4f, 0.0f);
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
            }
            if (timer == 10)
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
            if (timer == 15)
            {
                GameObject.Find("Sphere2").renderer.material.color = Color.red;
                GameObject.Find("Sphere1").renderer.material.color = Color.white;
                GameObject.Find("Capsule").transform.position = new Vector3(-5.4f, 0.4f, 0.0f);
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
            }
            if (timer == 25)
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
            if (timer == 30)
            {
                GameObject.Find("Sphere3").renderer.material.color = Color.red;
                GameObject.Find("Sphere2").renderer.material.color = Color.white;
                GameObject.Find("Capsule").transform.position = new Vector3(-4.2f, 0.4f, 0.0f);
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
            }
            if (timer == 40)
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
            if (timer >= 45 && timer < 65)
            {
                if (choice == 1)
                {
                    if (Input.touchCount > 0)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            pushed = true;
                            gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
                        }
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
                    GameObject.Find("Cube1").renderer.material.color = Color.red;
                }
                GameObject.Find("Sphere3").renderer.material.color = Color.white;
                GameObject.Find("Capsule").transform.position = new Vector3(-2.3f, 0.4f, 0.0f);
            }
            if (timer > 60 && timer < 65)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
            }
            if (timer >= 65 && timer < 85)
            {
                if (choice == 1 && timer == 65)
                    if (!pushed)
                        lose = true;

                if (choice == 2)
                {
                    if (Input.touchCount > 0)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            pushed = true;
                            gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
                        }
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
                    GameObject.Find("Cube2").renderer.material.color = Color.red;
                }
                GameObject.Find("Cube1").renderer.material.color = Color.white;
                GameObject.Find("Capsule").transform.position = new Vector3(0.0f, 0.4f, 0.0f);
            }
            if (timer > 80 && timer < 85)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
            }
            if (timer >= 85 && timer < 105)
            {
                if (choice == 2 && timer == 85)
                    if (!pushed)
                        lose = true;

                if (choice == 3)
                {
                    if (Input.touchCount > 0)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            pushed = true;
                            gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
                        }
                    }
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
                    GameObject.Find("Cube3").renderer.material.color = Color.red;
                }
                GameObject.Find("Cube2").renderer.material.color = Color.white;
                GameObject.Find("Capsule").transform.position = new Vector3(2.3f, 0.4f, 0.0f);
            }
            if (timer > 100 && timer < 105)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
            }
            if (timer == 105)
            {
                if (choice == 3)
                    if (!pushed)
                        lose = true;
                GameObject.Find("Sphere4").renderer.material.color = Color.red;
                GameObject.Find("Cube3").renderer.material.color = Color.white;
                GameObject.Find("Capsule").transform.position = new Vector3(4.2f, 0.4f, 0.0f);
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[1]; ;
            }
            if (timer == 115)
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
            if (timer == 120)
            {
                GameObject.Find("Sphere5").renderer.material.color = Color.red;
                GameObject.Find("Sphere4").renderer.material.color = Color.white;
                GameObject.Find("Capsule").transform.position = new Vector3(5.4f, 0.4f, 0.0f);
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
            }
            if (timer == 130)
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[0];
            if (timer == 135 && !lose)
            {
                GameObject.Find("Sphere6").renderer.material.color = Color.red;
                GameObject.Find("Sphere5").renderer.material.color = Color.white;
                GameObject.Find("Capsule").transform.position = new Vector3(6.6f, 0.4f, 0.0f);
                gameObject.GetComponent<SpriteRenderer>().sprite = morse[1];
                win = true;
                
				if(audioONCE){
					stats.audios [1].clip = stats.sonidos [8];
					stats.audios [1].Play ();
					audioONCE = false;
				}
            }
            timer++;
        }

        if (win)
        {
            if (!playOnce)
            {
                foreach (GameObject iterator in GameObject.FindGameObjectsWithTag("Morse"))
                    iterator.GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("TimeTAPPopButton").GetComponent<SpriteRenderer>().enabled = false;
                playOnce = true;
            }
            gameEnding += Time.deltaTime;
            if (gameEnding < 1)
                GameObject.Find("Fondo").GetComponent<SpriteRenderer>().sprite = winLose[2];
            else if (gameEnding < 1.5)
                GameObject.Find("Fondo").GetComponent<SpriteRenderer>().sprite = winLose[3];
            else if (gameEnding < 2)
                GameObject.Find("Fondo").GetComponent<SpriteRenderer>().sprite = winLose[2];
            else if (gameEnding < 2.5)
                GameObject.Find("Fondo").GetComponent<SpriteRenderer>().sprite = winLose[3];
            else if (gameEnding > 3)
                Application.LoadLevel("Live");
        }

        if (lose)
        {
            if (!playOnce)
            {
                stats.audios[1].clip = stats.sonidos[9];
                stats.audios[1].Play();
                playOnce = true;
                foreach (GameObject iterator in GameObject.FindGameObjectsWithTag("Morse"))
                    iterator.GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("TimeTAPPopButton").GetComponent<SpriteRenderer>().enabled = false;
            }

            gameEnding += Time.deltaTime;
            if (gameEnding < 1)
                GameObject.Find("Fondo").GetComponent<SpriteRenderer>().sprite = winLose[0];
            else if (gameEnding < 2)
                GameObject.Find("Fondo").GetComponent<SpriteRenderer>().sprite = winLose[1];
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
