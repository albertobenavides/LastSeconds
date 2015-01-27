using UnityEngine;
using System.Collections;

public class MistRunnerScript : MonoBehaviour {
	public float runSpeed;
	public MistScript muertos;
	public Sprite deadSprite;

	void Start () {
		muertos = GameObject.Find ("Main Camera").GetComponent<MistScript> ();
		runSpeed = Random.Range (15.0f,25.0f);
	}
	

	void Update () {
		transform.position += new Vector3 (runSpeed * Time.deltaTime, 0, 0);
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Meteor"){
			runSpeed = 0;
			muertos.muertos ++;
			this.GetComponent<Animator>().enabled = false;
			this.GetComponent<SpriteRenderer>().sprite = deadSprite;
			//Destroy(this.gameObject);
		}
	}
}
