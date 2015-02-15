using UnityEngine;
using System.Collections;

public class LiveStatus : MonoBehaviour {

    public Sprite [] Lives;
    PlayerScript player;
    float timer;
    int rando;

	void Start () 
    {
        player = GameObject.Find("PlayerStats").GetComponent<PlayerScript>();
        timer = 0;
        rando = player.lastLevelPlayed;
        Random.seed = (int)System.DateTime.Now.Ticks;
        while (rando == player.lastLevelPlayed)
            rando = Random.Range(3, 11);
	}
	
	void Update () 
    {
        timer += Time.deltaTime;
        gameObject.GetComponent<SpriteRenderer>().sprite = Lives[player.lives];
        if (timer > 3)
            Application.LoadLevel(rando);
	}
}
