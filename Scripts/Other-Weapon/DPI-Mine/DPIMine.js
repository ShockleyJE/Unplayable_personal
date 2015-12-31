
var Explosion:GameObject

function Start () {

}

function OnCollisionEnter (col : Collision)
{
    if(col.gameObject.name == "Player")
    {
	Instantiate(Explosion, transform.position, Quaternion.identity)
	//Do whatever you want to the DPI here   
        Destroy(gameObject);
    }
}
