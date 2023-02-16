using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{

    private bool Grounded;
    private Animator anim;

    [SerializeField]
    HealthBar healthBar;
    private GameManager gm;

    [SerializeField]
    private float maxVelocity;
    
    private float horizontalVelocity;
    private float verticalVelocity;
    private float acceleration;
    
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        // references for gameobject and animator 
      //  body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if(gm == null){
            gm = GameManager.Instance;
        }

        acceleration = 10;

        horizontalVelocity = 0;
        verticalVelocity = 0;

        onGround = false;
    }


    private void Jump(){    // jump animation 
            verticalVelocity = maxVelocity * 2;
            Grounded = false;
    }

    // Update is called once per frame
    void Update() {


    
        anim.SetBool("Ground", Grounded);
        anim.SetBool("run", horizontalVelocity != 0);

        // player flip
        if (horizontalVelocity > 0.01) {
            transform.localScale = Vector3.one;
        }
        else if (horizontalVelocity < -0.01) {
            transform.localScale = new Vector3(-1,1,1);
        }

        // ----- Handles Horizontal Movement Input -----
        if(Input.GetKey(KeyCode.D)){
            horizontalVelocity += (acceleration * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.A)){
            horizontalVelocity -= (acceleration * Time.deltaTime);
        }
            // If not moving, decellerate
        else{   
            if(horizontalVelocity > 0){
                horizontalVelocity -= (acceleration * Time.deltaTime);
            }
            else if(horizontalVelocity < 0){
                horizontalVelocity += (acceleration * Time.deltaTime);
            }
            if(MathF.Abs(horizontalVelocity) < 1){
                horizontalVelocity = 0;
            }
        }

            // Protects the player from going faster than maxVelocity
        if(horizontalVelocity > maxVelocity){
            horizontalVelocity -= (acceleration * Time.deltaTime);
        }
        else if(horizontalVelocity < -maxVelocity){
            horizontalVelocity += (acceleration * Time.deltaTime);
        }
        // -----

        // ----- Handles wheather the player can jump, and jump input. -----
            // Simulates gravity for player object.
        if(!onGround){
            verticalVelocity -= 2 * (acceleration * Time.deltaTime);
        }



            // Does the action of Jumping
        if(Input.GetKey(KeyCode.Space) && onGround){
            // Debug.Log("Jump");
            Jump();
        }
        // ----- 

        // ----- Applies the changes to posistion -----
        transform.Translate(Vector3.right * horizontalVelocity * Time.deltaTime);
        transform.Translate(Vector3.up * verticalVelocity * Time.deltaTime);
        // -----

        // Debug.Log(gameObject.transform.position);
        if(transform.position.y <= -7){
            gm.game_over();
        }
    }

    private void OnCollisionEnter2D(Collision2D coll){

        if (coll.gameObject.tag == "Floor"){
            Grounded = true;
        }

        // ----- Checks Every collided object to see if any are below and therefore "ground" -----
        foreach(ContactPoint2D hit in coll.contacts){
            if(hit.point.y < transform.position.y){
                onGround = true;
                
                if(verticalVelocity < 0){
                    verticalVelocity = 0;
                }
                // Debug.Log("onGround set to true");
            }
            else if((hit.point.y > transform.position.y) && (verticalVelocity > 0)){
                verticalVelocity = 0;
            }
            else if((hit.point.x > transform.position.x) && (horizontalVelocity > 0)){
                horizontalVelocity = 0;
            }
            else if((hit.point.x < transform.position.x) && (horizontalVelocity < 0)){
                horizontalVelocity = 0;
            }
        }
        // -----
    }

    private void OnCollisionExit2D(Collision2D coll){
        
        // ----- onGround sets to false if no longer colliding with object below player -----
        onGround = false;
        foreach(ContactPoint2D hit in coll.contacts){
            if(hit.point.y < transform.position.y){
                onGround = true;
                // Debug.Log("onGround set to false");
            }
        }
        // -----
    }    
}
