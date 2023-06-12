using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 originalSize;
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.2f;
    protected float xSpeed = 0.2f;
    public Vector2 movement;

   protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();

    }
    // Update is called once per frame
  

    
    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3 (input.x * xSpeed, input.y * ySpeed, 0);

        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

            transform.Translate(moveDelta * Time.deltaTime);

            //Add push vector, if any
            moveDelta += pushDirection;

            //Reduce push force every frame, based off recovery speed
            pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
            



        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, -new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Wall", "Human"));

        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);

        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, -new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Wall", "Human"));

        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);

        }
    }
    
      private void FixedUpdate()
    {
       
    }

    }