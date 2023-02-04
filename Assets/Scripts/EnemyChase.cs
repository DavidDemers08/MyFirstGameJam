using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public float distance;
    public LayerMask layerMask;
    private Vector2 direction;
    private bool chasing = false;
    private Rigidbody2D rb;
    private float speed = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (chasing)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 visionDirection = player.transform.position - transform.position;
            visionDirection.Normalize();

            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, visionDirection, distance, layerMask);

            if (raycastHit.collider != null)
            {
                chasing = false;
                if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    direction = visionDirection;
                    chasing = true;
                }
            }
            else
            {
                chasing = false;
            }
        }

        
    }
}
