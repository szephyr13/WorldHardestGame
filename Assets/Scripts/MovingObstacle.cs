using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float movingX;
    [SerializeField] private float movingY;
    [SerializeField] private float speed;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        transform.Translate(new Vector2(movingX, movingY).normalized * speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            movingX *= -1;
            movingY *= -1;
        }
    }
}
