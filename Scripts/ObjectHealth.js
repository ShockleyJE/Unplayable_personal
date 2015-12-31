var Health : int
var Body : GameObject

function takeDamage(int damage)
  Health -= damage
function Update()
  if (Health < 1)
      Instantiate(Body, transform.location, Quaternion.identity) //Spawn the body at the current location of the object
      Destroy(gameObject)
