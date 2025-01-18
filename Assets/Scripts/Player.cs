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

    private bool check2;
    private bool check3;
    private bool check4;

    private Vector3 origin;
    private Vector3 newStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lifes = 3;
        check2 = false;
        check3 = false;
        check4 = false;
        origin = new Vector3(-8.0f, 3.38f, 0);
        newStartPoint = origin;
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
            transform.position = newStartPoint;
        }

        if (collision.CompareTag("Collect"))
        {
            score++;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Check2"))
        {
            if (check2 == false)
            {
                newStartPoint = new Vector3(-0.41f, -3.68f, 0);
                score += 10;
                check2 = true;
            }
        }

        if (collision.CompareTag("Check3"))
        {
            if (check3 == false)
            {
                newStartPoint = new Vector3(4.3f, -0.1f, 0);
                score += 20;
                check3 = true;
            }
        }

        if (collision.CompareTag("Win"))
        {
            if (check4 == false)
            {
                score += 5;
                check4 = true;
            }
        }
    }
}