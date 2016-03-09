#pragma strict

var guiScore:GUIText;
static var scoreRed1=0;
static var scoreGreen1=0;
static var scoreRed2=0;
static var scoreGreen2=0;
static var scoreTextGreen1 = "Green_Score1: 0";
static var scoreTextRed1 = "Red_Score1: 0";
static var scoreTextGreen2 = "Green_Score2: 0";
static var scoreTextRed2 = "Red_Score2: 0";
static var count=0;
static var totalSpheres=0;
var expSound :AudioClip;
var vol: float;
function Start () {
		
}

function Update () {
	if (totalSpheres == 40)
		
		Application.Quit();
		
}

function OnCollisionEnter (col : Collision){
	

	if (col.gameObject.name == "Red Cube 1"){
		var audio = GetComponent.<AudioSource>();
		audio.Play();
		yield WaitForSeconds(0.3);
		//Destroy(this.gameObject);
		this.gameObject.SetActive(false);
		scoreRed1 += 1;
  		totalSpheres += 1;
  		if (scoreRed1 == 10){ 
  			Destroy(col.gameObject);
  			}
  		scoreTextRed1 = "Red_Score1 : " +scoreRed1;
  		

  	
	}
	if (col.gameObject.name == "Red Cube 2"){
		audio = GetComponent.<AudioSource>();
		audio.Play();
		yield WaitForSeconds(0.3);
		this.gameObject.SetActive(false);
		scoreRed2 += 1;
  		totalSpheres += 1;
  		if (scoreRed2 == 10){ 
  			Destroy(col.gameObject);
  			}
  		scoreTextRed2 = "Red_Score2 : " +scoreRed2;
  		
	}
	if (col.gameObject.name == "Green Cube 1"){
		audio = GetComponent.<AudioSource>();
		audio.Play();
		yield WaitForSeconds(0.3);
		this.gameObject.SetActive(false);
		scoreGreen1 += 1;
		totalSpheres += 1;
  		if (scoreGreen1 == 10){ 
  			Destroy(col.gameObject);
  			}
  		scoreTextGreen1 = "Green_Score1 : " +scoreGreen1;	
  		
	}
	if (col.gameObject.name == "Green Cube 2"){
		audio = GetComponent.<AudioSource>();
		audio.Play();
		yield WaitForSeconds(0.3);
		this.gameObject.SetActive(false);
		scoreGreen2 += 1;
		totalSpheres += 1;
  		if (scoreGreen2 == 10){ 
  			
  			Destroy(col.gameObject);}
  		scoreTextGreen2 = "Green_Score2 : " +scoreGreen2;	
  		
	}
	
}
	
function OnGUI(){
 	GUI.Box(new Rect(10,100,120,20),scoreTextRed1);
 	GUI.Box(new Rect(10,150,120,20),scoreTextGreen1);
 	GUI.Box(new Rect(10,200,120,20),scoreTextRed2);
 	GUI.Box(new Rect(10,250,120,20),scoreTextGreen2);
}