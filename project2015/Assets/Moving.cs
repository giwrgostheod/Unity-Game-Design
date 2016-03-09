using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Moving : MonoBehaviour {
	public GameObject hero1;
	private Ray ray;
	private RaycastHit hit;
	public double gridSpacing = 1.0; 
	private GameObject target;
	private GameObject newHero;
	private GameObject objectToMove;
	private GameObject enemy;
	private int nextNameNumber = 0;
	private string TargetTower;
	private int HeroColor;		// 1 for red, 2 for green
	private static AstarAI help;
	private static int counter_red = 0;
	private static int counter_green = 0;

	// Use this for initialization
	void Start () {
		// initial values in case player doesn't choose a type
		HeroColor = 1;
		TargetTower = "RedTower";
	}

	Vector3 GetGridPosition (Vector3 originalPosition ) {
		
		float newx = (Mathf.Round ((float)originalPosition.x / (float)gridSpacing) * (float)gridSpacing);
		float newz = (Mathf.Round ((float)originalPosition.z / (float)gridSpacing) * (float)gridSpacing);
	
		if ((newx%2)!=0)  newx++ ;
		if ((newz%2)!=0)  newz++ ;

		Vector3 convertPosition = new Vector3 (newx, (originalPosition.y), newz);
		return convertPosition;
	}

	IEnumerator MoveObject (Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
		float i = 0.0f;
		float rate = 1.0f/time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return new WaitForFixedUpdate(); 
		}
		Destroy(thisTransform.gameObject);
	}

	// Update is called once per frame
	public void Update () {

	//	if (HeroColor == 1)
	//		TargetTower = "RedTower";
	//	else if (HeroColor == 2)
	//		TargetTower = "Tower";
		//cc = GameObject.Find ("count").GetComponent<"Destroy"> ();
		if(Input.GetMouseButtonDown(0))
		{
			TargetTower = "Tower";
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f)) {
				if (hit.collider.name=="Plane"){
					//if( ( (HeroColor == 1) && (counter_red < 20)) || (HeroColor == 2)){
					//Debug.Log ("About to create new hero in (" + hit.point.x +", " + hit.point.y + ", " + hit.point.z +")\n");
					newHero = (GameObject)Instantiate(hero1, GetGridPosition(hit.point) + new Vector3(0, 1, 0), Quaternion.identity);
					if (HeroColor == 1){
						newHero.gameObject.GetComponent<Renderer>().material.color = new Color(220, 0, 0, 0);
					}
					else if (HeroColor == 2){
						newHero.gameObject.GetComponent<Renderer>().material.color = new Color(0, 220, 0, 0);
						}

					newHero.name = "hero"+nextNameNumber;
					objectToMove = GameObject.Find("hero"+nextNameNumber);
					
					enemy = FindClosestEnemy(objectToMove);
					if ((enemy.name == "Green Cube 1") || (enemy.name=="Green Cube 2")  ) counter_green +=1;
					else if ((enemy.name == "Red Cube 1") || (enemy.name=="Red Cube 2")) counter_red +=1;
					Debug.Log (enemy.name);
					Debug.Log ("object " + objectToMove.name + " will move to " + enemy.name);
					nextNameNumber++;
					//hero1.transform.position = Vector3.MoveTowards(transform.position, target.gameObject.transform.position, 10);    	
					//StartCoroutine(MoveObject (objectToMove.transform, objectToMove.transform.position, enemy.transform.position, 1));
					help = (AstarAI)objectToMove.GetComponent("AstarAI");
					help.targetPosition = enemy.transform.position ;
						help.transform.position= objectToMove.transform.position;//}
				}
			}	 
			}


		else if(Input.GetMouseButtonDown(1))
		{
				if (HeroColor == 1){
				TargetTower = "RedTower";
				}
				else if (HeroColor == 2){

				TargetTower = "GreenTower";}
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f)) {
				if (hit.collider.name=="Plane"){ 

					if( ( (HeroColor == 1) && (counter_red < 20)) || ((HeroColor == 2) && (counter_green < 20 ))){
						//Debug.Log ("About to create new hero in (" + hit.point.x +", " + hit.point.y + ", " + hit.point.z +")\n");
						newHero = (GameObject)Instantiate(hero1, GetGridPosition(hit.point) + new Vector3(0, 1, 0), Quaternion.identity);
						if (HeroColor == 1){
							newHero.gameObject.GetComponent<Renderer>().material.color = new Color(220, 0, 0, 0);
						}
						else if (HeroColor == 2){

							newHero.gameObject.GetComponent<Renderer>().material.color = new Color(0, 220, 0, 0);
						}
						newHero.name = "hero"+nextNameNumber;
						objectToMove = GameObject.Find("hero"+nextNameNumber);
						
						enemy = FindClosestEnemy(objectToMove);
						if ((enemy.name == "Green Cube 1") || (enemy.name=="Green Cube 2") ) counter_green +=1;
						else if ((enemy.name == "Red Cube 1") || (enemy.name=="Red Cube 2")) counter_red +=1;
						Debug.Log (enemy.name);
						Debug.Log ("object " + objectToMove.name + " will move to " + enemy.name);
						nextNameNumber++;
						
						//hero1.transform.position = Vector3.MoveTowards(transform.position, target.gameObject.transform.position, 10);    	
						//StartCoroutine(MoveObject (objectToMove.transform, objectToMove.transform.position, enemy.transform.position, 1));
						help = (AstarAI)objectToMove.GetComponent("AstarAI");
						help.targetPosition = enemy.transform.position ;
						help.transform.position= objectToMove.transform.position;}
				}
			}	 
		}









		
	}
	GameObject FindClosestEnemy(GameObject whichObject) {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag(TargetTower);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = whichObject.transform.position;

		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}


	public void goRed(){
		HeroColor = 1;
		Debug.Log("Red hero chosen");
	}
	
	public void goGreen(){
		HeroColor = 2;
		Debug.Log("Green hero chosen");
	}
	
}