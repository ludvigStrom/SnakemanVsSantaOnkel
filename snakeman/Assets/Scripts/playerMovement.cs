using UnityEngine;
using UnityEngine.InputSystem;
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


    public void OnMove(InputValue value)
    {
        movement_vector = value.Get<Vector2>();

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
    }

    void Update()
    {
        addParticleFx();
    }

    void FixedUpdate() {

        movement_vector *= (Mathf.Abs(movement_vector.x) == 1 && Mathf.Abs(movement_vector.y) == 1) ? .7f : 1;

        rbody.AddForce(movement_vector * Time.deltaTime * speed);

        lastPosition = this.transform.position;
    }

    public GameObject GetPlayerObject()
    {
        return this.gameObject;
    }

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


    private bool sliding()
    {
        if (movement_vector == Vector2.zero && lastPosition != this.transform.position)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
