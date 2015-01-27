using UnityEngine;
using System.Collections;

public class LiveStatus : MonoBehaviour {

    public Sprite [] Lives;
    PlayerScript player;
    float timer;

	void Start () 
    {
        player = GameObject.Find("PlayerStats").GetComponent<PlayerScript>();
        timer = 0;
	}
	
	void Update () 
    {
        timer += Time.deltaTime;
        gameObject.GetComponent<SpriteRenderer>().sprite = Lives[player.lives];
        if (timer > 3)
        {
            int rando;
            do
                rando = Random.Range(3, 11);
            while (rando == player.lastLevelPlayed);
            Application.LoadLevel(rando);
        }
	}
}
