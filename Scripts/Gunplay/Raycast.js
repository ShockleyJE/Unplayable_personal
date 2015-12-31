#pragma strict

var Effect : Transform;
var damage = 100;

function Update () {
	
	var hit : RaycastHit;
	var ray : Ray = Camera.main.ScreenPointToRay(Vector3(Screen.width*0.5, Screen.height*0.5, 0));
	
	if (Input.GetMouseButtonDown(0))
	{
		if (Physics.Raycast (ray, hit, 100))
		{
			//var particleClone = Instantiate(Effect, hit.point, Quaternion.LookRotation(hit.normal)); This isn't required. If we want a particle to appear on impact then uncomment this
			Destroy(particleClone.gameObject, 2);
			hit.transform.SendMessage("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
	
}
