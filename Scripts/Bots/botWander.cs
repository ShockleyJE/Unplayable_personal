using UnityEngine;
using System.Collections;

/// Created: HI_ANON
/// Last Edited: 
/// Note: Boundaries must not include enviornmental hazards

/// Creates wandering behaviour for a CharacterController.
/// For prototype use in sandbox for Unplayable

[RequireComponent(typeof(CharacterController))]
public class botWander : MonoBehaviour
{
    //movement speed
    public float speed = 10;
    //direction change speed
    public float directionChangeInterval = 1;
    //max degree of change
    public float maxHeadingChange = 30;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    //find current forward
    Vector3 forward
    {
        get { return transform.TransformDirection(Vector3.forward); }
    }
    
    /// <summary>
    /// initiation with random angle
    /// </summary>
    void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        // Turn transform
        transform.eulerAngles = new Vector3(0, heading, 0);
        //Begin movement
        StartCoroutine(NewHeadingRoutine());
    }
    /// <summary>
    /// rotates over time, effected by interval
    /// moves CC forward
    /// </summary>
    void Update()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        controller.SimpleMove(forward * speed);
    }

    /// <summary>
    /// on collision find new direction
    /// </summary>
    /// <param name="hit"></param>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag != "Boundary")
        {
            return;
        }

        // Bounce off the wall using angle of reflection
        var newDirection = Vector3.Reflect(forward, hit.normal);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, newDirection);
        heading = transform.eulerAngles.y;
        NewHeading();
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeading()
    {
        //initialize vars for max rotation
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);

        //rotate target
        targetRotation = new Vector3(0, heading, 0);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// ^Important due to slow, stick, etc effects
    /// </summary>
    IEnumerator NewHeadingRoutine()
    {
        while (true)
        {
            NewHeading();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
}
