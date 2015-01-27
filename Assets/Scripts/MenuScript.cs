using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
	private AudioSource[] audios;
    public Sprite[] redoSequence;
    float timer;
    bool redo;
    bool once;
    bool end;
    public Text score;

    public void StartGame()
    {
		GameObject.Find ("PlayerStats").GetComponent<AudioListener> ().enabled = true;
		audios = GameObject.Find ("PlayerStats").GetComponents<AudioSource>();
		audios [0].Play ();
		audios [1].Play ();
        Application.LoadLevel(5);// (Random.Range(3, 11));
        timer = 0;
        end = once = redo = false;
    }

    void Update()
    {
        if (GameObject.Find("PlayerStats").GetComponent<PlayerScript>().lives == 0 && !end)
        {
            GameOver();
            end = true;
        }
        if (redo)
        {
            if (!once)
            {
                Destroy(GameObject.Find("Canvas"));
                once = true;
            }
            timer += Time.deltaTime;
            if (timer < 1)
                gameObject.GetComponent<SpriteRenderer>().sprite = redoSequence[0];
            else if (timer < 1.5)
                gameObject.GetComponent<SpriteRenderer>().sprite = redoSequence[1];
            else if (timer < 2)
                gameObject.GetComponent<SpriteRenderer>().sprite = redoSequence[2];
            else if (timer < 2.5)
                gameObject.GetComponent<SpriteRenderer>().sprite = redoSequence[3];
            else if (timer < 3)
            {
                GameObject playerStatus = GameObject.Find("PlayerStats");
                Destroy(playerStatus.gameObject);
                Application.LoadLevel("Menu");
            }
        }
    }


    public void RestartGame()
    {
        redo = true;
    }

	public void ExitApp(){
		Application.Quit();
	}

    public void GameOver()
    {
        int score = GameObject.Find("PlayerStats").GetComponent<PlayerScript>().levelsSucceded * 1000000 + Random.Range(1000, 1000000);
        GameObject.Find("Score").GetComponent<Text>().text = "Lives Saved: \n" + score.ToString();
    }
}
