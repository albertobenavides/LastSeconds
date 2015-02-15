using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float level;
    public int lastLevelPlayed;
    public int levelsSucceded;
    public int lives;
    public Sprite [] livesStatus;
    GameObject worldStatus;
    public AudioClip[] sonidos;
    public AudioSource[] audios;

	// Use this for initialization
	void Start () 
    {
        level = 1.0f;
        levelsSucceded = 0;
        lives = 3;
		lastLevelPlayed = 0;
        worldStatus = GameObject.Find("livesStatus");
        audios = GameObject.Find("PlayerStats").GetComponents<AudioSource>();

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void ShowStatus()
    {
        worldStatus.GetComponent<SpriteRenderer>().sprite = livesStatus[lives];
        worldStatus.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideStatus()
    {
        worldStatus.GetComponent<SpriteRenderer>().enabled = false;
    }
}
