using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("MovementSystem")]
    private float inputH;
    private float inputV;
    [SerializeField] private float speed;

    [Header("User Interface")]
    public int score;
    public int lifes;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifesText;

    private bool check2;
    private bool check3;
    private bool check4;

    private int mode;
    //0 = initial menu
    //1 = game
    //2 = pause menu
    //3 = win menu
    [SerializeField] private GameObject initialMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject wonMenuUI;

    

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
        mode = 0;

    }

    // Update is called once per frame
    void Update()
    {
        UIControls();
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

    private void UIControls()
    {
        if (mode == 0) //initial menu
        {
            initialMenuUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            wonMenuUI.SetActive(false);
            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                mode = 1;
            }
        }
        else if (mode == 1) //game
        {
            initialMenuUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            wonMenuUI.SetActive(false);
            Time.timeScale = 1f;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mode = 2;
            }
        }
        else if (mode == 2) //pause menu
        {
            initialMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            wonMenuUI.SetActive(false);
            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mode = 1;
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (mode == 3) //win menu
        {
            initialMenuUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            wonMenuUI.SetActive(true);
            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
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
            scoreText.text = "Points: " + score;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Check2"))
        {
            if (check2 == false)
            {
                newStartPoint = new Vector3(-0.41f, -3.68f, 0);
                score += 10;
                scoreText.text = "Points: " + score;
                check2 = true;
            }
        }

        if (collision.CompareTag("Check3"))
        {
            if (check3 == false)
            {
                newStartPoint = new Vector3(4.3f, -0.1f, 0);
                score += 20;
                scoreText.text = "Points: " + score;
                check3 = true;
            }
        }

        if (collision.CompareTag("Win"))
        {
            if (check4 == false)
            {
                score += 4;
                scoreText.text = "Points: " + score;
                mode = 3;
                check4 = true;
            }
        }
    }
}