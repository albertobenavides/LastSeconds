using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WakAMoleScript : MonoBehaviour {
	private float randoX, randoZ;
	public GameObject moleMan;
	public float spawnTime;
	private float timerAux;
	public RaycastHit hit;
	public AudioClip[] shotSound;
	public AudioSource fuenteSonido;
	public int cantidadMole;
	private List<Vector3> posMoles;
	private bool posok = false;
	private int molePos = 0;

	void Start () {
		posMoles = new List<Vector3>();
		while(!posok){
			posMoles.Add(new Vector3(Mathf.RoundToInt(Random.Range (-1.0f, 1.0f)*4.2f),
			                         1,
			                         Mathf.RoundToInt(Random.Range (-1.0f, 1.0f)*4.2f)));
			
			posMoles = posMoles.Distinct().ToList();
			
			if(posMoles.Count == cantidadMole)
				posok = true;
		}
	}
	
	void Update () {
		gunShot ();

		if(timerAux > spawnTime && molePos < cantidadMole){
			Instantiate (moleMan, posMoles[molePos] , Quaternion.identity);
			molePos ++;
			timerAux = 0;
		}
		else{
			timerAux += Time.deltaTime;
		}
	}

	private void gunShot(){
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				var ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.tag == "MoleMan") {
						Destroy(hit.collider.gameObject);
						//recipient = hit.collider.transform.parent.gameObject;
					}
					fuenteSonido.clip = shotSound [0];
					fuenteSonido.Play ();
				}
			}
		}
	}
}
