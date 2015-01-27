using UnityEngine;
using System.Collections;

public class VolcanoTouchScript : MonoBehaviour {
	//public Material[] mate;
	public VolcanoScript volcanoScripto;
	private bool raiseYouMadafacka = true;
	private ParticleSystem volcanoFire;

	void Start(){
		volcanoScripto = GameObject.Find ("Main Camera").GetComponent<VolcanoScript>();
		volcanoFire = transform.GetComponentInChildren<ParticleSystem> ();
	}

	void Update(){
		if(transform.position.y < 0.3f && raiseYouMadafacka)
			transform.position += new Vector3 (0, 1.5f * Time.deltaTime, 0);
	}

	public void OnTouchDown(){
		//renderer.material = mate [1];
		//volcanoScripto.onTouch ++;
		raiseYouMadafacka = false;
		volcanoFire.enableEmission = false;
	}

	public void OnTouchStay(){
		if(volcanoScripto.timeAuxTouch > volcanoScripto.timeToTouch && volcanoScripto.onTouch == volcanoScripto.cantidadVolcanoes)
            Application.LoadLevel("Live");
		else if(volcanoScripto.onTouch == volcanoScripto.cantidadVolcanoes)
			volcanoScripto.timeAuxTouch += Time.deltaTime;
	}

	public void OnTouchExit(){
		//renderer.material = mate [0];
		//volcanoScripto.onTouch --;
		raiseYouMadafacka = true;
		volcanoFire.enableEmission = true;
	}
}
