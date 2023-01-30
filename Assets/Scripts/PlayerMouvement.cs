using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    private Rigidbody2D rb;
    private Vector3 direction;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x =  Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction = direction.normalized;

        animator.SetFloat("Horizontal",direction.x);
        animator.SetFloat("Vertical", direction.y);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + speed * Time.deltaTime * direction);
    }


}
