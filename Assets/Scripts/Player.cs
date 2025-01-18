using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float inputH;
    private float inputV;
    [SerializeField] private float speed;

    public int score;
    public int lifes;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lifes = 3;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        MovementLimits();
    }


    private void PlayerMovement()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * speed * Time.deltaTime);

    }
    private void MovementLimits()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
        float clampedY = Mathf.Clamp(transform.position.y, -4.6f, 4.6f);
        transform.position = new Vector3(clampedX, clampedY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            transform.position = new Vector3(-8, 3.38f, 0);
        }

        if (collision.CompareTag("Collect"))
        {
            score++;
            Destroy(collision.gameObject);
        }
    }
}