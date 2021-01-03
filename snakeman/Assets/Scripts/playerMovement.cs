using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class playerMovement : MonoBehaviour {

    [SerializeField]
    private int playerIndex = 0;

    public float speed;
    public int playerId;

    private Rigidbody2D rbody;
    public Animator anim;

    private Vector3 lastPosition;
    private Vector2 movement_vector;

    private ParticleSystem particles;

    

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    private void Awake(){
        anim = GetComponent<Animator>();
    }

    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        particles = GetComponentInChildren<ParticleSystem>();
        lastPosition = this.transform.position;
	}

    public void SetInputVector(Vector2 input){

        movement_vector = input;

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);
        } else{
            anim.SetBool("isWalking", false);
        }
    }

    void Update(){
        addParticleFx();
    }

    void FixedUpdate() {
        rbody.AddForce(movement_vector * Time.deltaTime * speed);
        lastPosition = this.transform.position;
    }

    public GameObject GetPlayerObject(){
        return this.gameObject;
    }

    private void addParticleFx()
    {
        if (particles != null)
        {
            var em = particles.emission;
            if (sliding() && !particles.emission.enabled)
            {
                em.enabled = true;
            }
            else if (!sliding() && particles.emission.enabled)
            {
                em.enabled = false;
            }
        }
    }

    private bool sliding()
    {
        if (movement_vector == Vector2.zero && lastPosition != this.transform.position){
            return true;
        }else{
            return false;
        }
    }
}
