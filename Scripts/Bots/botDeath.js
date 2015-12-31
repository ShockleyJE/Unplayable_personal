var living = 1
var Body : GameObject
void Update()
    Instantiate(Body, transform.position, Quaternion.identity)
    Destroy(gameObject)
