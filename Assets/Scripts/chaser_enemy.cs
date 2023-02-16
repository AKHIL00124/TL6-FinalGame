using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaser_enemy : MonoBehaviour
{
    private GameManager gm;

    private float followDistance;

    [SerializeField]
    private float maxVelocity;

    private float currentVelocity;
    private float acceleration;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        if(gm == null){
            gm = GameManager.Instance;
        }

        followDistance = 10;
        currentVelocity = 0;
        acceleration = 5;

        target = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        // ----- Sees if target is in range and follows accordingly -----
        float targetDistance = distance_from_target(target);

            // Determines distance and target direction
        if((MathF.Abs(targetDistance) < followDistance) && (targetDistance > 0)){
            currentVelocity += (acceleration * Time.deltaTime);
        }
        else if((MathF.Abs(targetDistance) < followDistance) && (targetDistance < 0)){
            currentVelocity -= (acceleration * Time.deltaTime);
        }
            // Handles decceleration when not needing to move.
        else{
            if(currentVelocity > 0){
                currentVelocity -= (acceleration * Time.deltaTime);
            }
            else if(currentVelocity < 0){
                currentVelocity += (acceleration * Time.deltaTime);
            }
            if(MathF.Abs(currentVelocity) < 1){
            currentVelocity = 0;
            }
        }

            // Protects object from breaking speed limit (maxVelocity)
        if(currentVelocity > maxVelocity){
            currentVelocity -= (acceleration * Time.deltaTime);
        }
        else if(currentVelocity < -maxVelocity){
            currentVelocity += (acceleration * Time.deltaTime);
        }
        // -----

        // Applies movement change
        transform.position += (Vector3.right * currentVelocity * Time.deltaTime);
        // transform.rotation = transform.rotation.zAngle + Quaternion.AngleAxis((6.28f * currentVelocity), Vector3.forward);
        transform.Rotate(0, 0, (-6.28f * currentVelocity), Space.Self);
    }

    // Checks to horizontal distance between Enemy and Player so enemy only follows when in range.
    private float distance_from_target(GameObject target){
        float myPosition = gameObject.transform.position[0];
        float targetPosistion = target.transform.position[0];

        return targetPosistion - myPosition;
    }

    private void OnCollisionEnter2D(Collision2D hit){
        
        // ----- Detects if Enemy has hit player -----
        if(hit.gameObject.tag == "Player"){
            Debug.Log("Hit Player");
            gm.take_damage();
            // Destroy(gameObject);
        }
        // -----
    }
}
