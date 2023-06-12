using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : Mover
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    
    //private BoxCollider2D boxCollider;

    private RaycastHit2D hit;

    //private Vector2 movement;
    
    

    protected override  void Start()
    {
      //boxCollider = GetComponent<BoxCollider2D>();
      base.Start();
      spriteRenderer = GetComponent<SpriteRenderer>();

      DontDestroyOnLoad(gameObject);

    }
    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
          return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

       

    }
    private void FixedUpdate()
    {
       
        //movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        // Make sure we can move in this direction
       // hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
       // if(hit.collider == null)
       // {
       //     transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        //}

        //Same, but for x-axis
       // hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
      //  if(hit.collider == null)
       // {
       //     transform.Translate(0, moveDelta.x * Time.deltaTime, 0, 0);
        //}
    
       // if(isAlive)
       //UpdateMotor(new Vector3(moveDelta.x, moveDelta.y, 0));
       
     
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }
    public void SetLevel(int level)
    {
        for(int i = 0; i < level; i++)
            OnLevelUp();
    }

}
