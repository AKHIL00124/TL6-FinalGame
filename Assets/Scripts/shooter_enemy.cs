using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter_enemy : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField]
    private float shootDistance;
    [SerializeField]
    private float shootSpeed;

    private float targetAngle;
    private GameObject target;

    private float timeSinceShot;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceShot = 0;

        target = GameObject.FindGameObjectsWithTag("Player")[0];
    }


    // Update is called once per frame
    void Update()
    {
        // When the player is close enough to the shooter
        if(Mathf.Abs(distance_from_target(target)) < shootDistance){
            // Calculates the angular posistion of the target and turns to it
            float y = target.transform.position.y - transform.position.y;
            float x = target.transform.position.x - transform.position.x;
            targetAngle = Mathf.Atan2(y, x);
            targetAngle = (targetAngle * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            
            // Controls what happens when shooting and how often.
            if(Time.time >= (shootSpeed + timeSinceShot)){
                // Debug.Log("Fire");
                GameObject clone = Instantiate(bullet, transform.position, Quaternion.AngleAxis(targetAngle, Vector3.forward));

                timeSinceShot = Time.time;
            }
        }
    }

    // Checks to horizontal distance between Enemy and Player so enemy only follows when in range.
    private float distance_from_target(GameObject target){
        float myPosition = gameObject.transform.position[0];
        float targetPosistion = target.transform.position[0];

        return targetPosistion - myPosition;
    }
}