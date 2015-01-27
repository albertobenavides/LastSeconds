using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class VolcanoScript : MonoBehaviour {
	public int cantidadVolcanoes;
	private List<Vector3> posVolcanoes;
	private bool posok = false;
	public float spawnTime;
	private float timerAux;
	private int volcanoPos = 0;
	public GameObject volcano;
	private RaycastHit hit;
	public AudioClip[] shotSound;
	public AudioSource fuenteSonido;
	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;
	public LayerMask touchInputMask;
	public int onTouch;
	public float timeToTouch;
	public float timeAuxTouch;
	public Text debugTXT;
	public float loseTime;
	public float loseTimeAux;
	private PlayerScript stats;
	private bool audioONCE = true;

	void Start () {
		stats = GameObject.Find ("PlayerStats").GetComponent<PlayerScript> ();
		stats.levelsSucceded ++;
        stats.lastLevelPlayed = Application.loadedLevel;
		stats.audios [1].clip = stats.sonidos [1];
		stats.audios [1].Play ();

		posVolcanoes = new List<Vector3>();
		while(!posok){
			posVolcanoes.Add(new Vector3(Mathf.RoundToInt(Random.Range (-1.0f, 1.0f)*2),
			                         0,
			                         Mathf.RoundToInt(Random.Range (-1.0f, 1.0f))));
			
			posVolcanoes = posVolcanoes.Distinct().ToList();
			
			if(posVolcanoes.Count == cantidadVolcanoes)
				posok = true;
		}
	}
	
	
	void Update () 
    {
		multiTosh ();

		if(timerAux > spawnTime && volcanoPos < cantidadVolcanoes)
        {
			Quaternion current = Quaternion.Euler(-90,0,0);
			Instantiate (volcano, posVolcanoes[volcanoPos] , current);
			volcanoPos ++;
			timerAux = 0;
		}
		else
			timerAux += Time.deltaTime;

		onTouch = Input.touchCount;

		if(loseTimeAux > loseTime)
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
			
			if (stats.lives != 0){
                    Application.LoadLevel("Live");
				if(audioONCE){
					stats.audios [1].clip = stats.sonidos [8];
					stats.audios [1].Play ();
					audioONCE = false;
				}
			}
                else
                    Application.LoadLevel("LoserScreen");
		}
		else{
			loseTimeAux += Time.deltaTime;
		}
	}

	private void multiTosh(){
		if (Input.touchCount > 0) {
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();
			
			foreach (Touch touch in Input.touches){
				Ray ray = camera.ScreenPointToRay(touch.position);
				
				if(Physics.Raycast(ray, out hit, touchInputMask)){
					GameObject recipient = hit.transform.gameObject;
					touchList.Add (recipient);
					
					if(touch.phase == TouchPhase.Began){
						recipient.SendMessage("OnTouchDown", SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Stationary){
						recipient.SendMessage("OnTouchStay", SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended){
						recipient.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			
			foreach(GameObject g in touchesOld){
				if(!touchList.Contains(g)){
					g.SendMessage("OnTouchExit", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
