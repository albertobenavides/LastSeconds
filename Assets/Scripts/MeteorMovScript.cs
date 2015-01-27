using UnityEngine;
using System.Collections;

public class MeteorMovScript : MonoBehaviour {
	public GameObject earth;
	public float increaser;
	public float timeToDie;

	void Start () 
    {
		earth = GameObject.Find ("Mundo");
	}
	

	void Update () {
		transform.position = Vector3.Lerp (transform.position,
		                                   earth.transform.position,
		                                   Time.deltaTime);

		if(timeToDie < 0)
			Destroy(this.gameObject);
		else
			timeToDie -= Time.deltaTime;
	}

    void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.back, 1f);
    }
}
