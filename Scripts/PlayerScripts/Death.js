
var Explosion : GameObject
var Body : GameObject

function Start () {

}

function OnCollisionEnter (col : Collision)
{
    if(col.gameObject.name == "Death_Node")
    {
	Instantiate(Explosion, transform.position, Quaternion.identity)
        Destroy(gameObject);
    }
}
