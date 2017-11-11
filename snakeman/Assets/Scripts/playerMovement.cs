using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

    public float speed;
    public int playerId;

    private Rigidbody2D rbody;
    public Animator anim;

    private Vector3 lastPosition;
    private Vector2 movement_vector;

    private ParticleSystem ps;

    
    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();

        lastPosition = this.transform.position;
	}


    void Update()
    {
        //get input
        movement_vector = new Vector2(Input.GetAxisRaw("Horizontal" + playerId), Input.GetAxisRaw("Vertical" + playerId)); //raw = binaray GetAxis anlog

        //set walking animation variables
        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        addParticleFx();
    }

    void FixedUpdate() {
        
        //correct diagonal speed
        movement_vector *= (Mathf.Abs(movement_vector.x) == 1 && Mathf.Abs(movement_vector.y) == 1) ? .7f : 1;

        //apply force
        rbody.AddForce(movement_vector * Time.deltaTime * speed);

        //store position
        lastPosition = this.transform.position;
    }

    //apply particle effect if sliding (moving but no player inputs)
    private void addParticleFx()
    {
        if (ps != null)
        {
            var em = ps.emission;
            if (sliding() && !ps.emission.enabled)
            {
                em.enabled = true;
            }
            else if (!sliding() && ps.emission.enabled)
            {
                em.enabled = false;
            }
        }
    }

    //checks if the player is sliding. (no input but moves
    private bool sliding()
    {
        if (movement_vector == Vector2.zero && lastPosition != this.transform.position)
        {
            //Debug.Log("Sliding");
            return true;
        }
        else
        {
            return false;
        }
    }
}
