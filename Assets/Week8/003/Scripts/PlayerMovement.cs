using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public PathSystem ps;
    public float time = 120;
    public float score = 0;
    public TMP_Text text;
    public TMP_Text scoreText;
    public GameObject loseScreen;

    public bool canMove = false;

    void Start()
    {
        Time.timeScale = 1;
        loseScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();
        }
        if (ps.seedScreen.activeSelf)
            return;
        time -= Time.deltaTime;
        score += Time.deltaTime;

        Vector2 target = transform.position;
        bool inGrid = false;

        if (Input.GetKeyDown(KeyCode.W))
            target = new Vector2(transform.position.x, transform.position.y + ps.cellSize);
        if (Input.GetKeyDown(KeyCode.S))
            target = new Vector2(transform.position.x, transform.position.y - ps.cellSize);
        if (Input.GetKeyDown(KeyCode.A))
            target = new Vector2(transform.position.x - ps.cellSize, transform.position.y);
        if (Input.GetKeyDown(KeyCode.D))
            target = new Vector2(transform.position.x + ps.cellSize, transform.position.y);

        foreach (MyGridCell cell in ps.gridCellList)
        {
            if (target == cell.location)
                inGrid = true;
        }

        if (inGrid && canMove)
            transform.position = target;

        if (time <= 0)
        {
            canMove = false;
            Time.timeScale = 0;
            loseScreen.SetActive(true);
            scoreText.text = Mathf.Round(score).ToString();
        }

        text.text = Mathf.Round(time).ToString() ;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("ProcGen003");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("End"))
        {  
            ps.NextLevel();
        }
        else if (collision.CompareTag("Pickup"))
        {
            if (!collision.gameObject.GetComponent<Pickup>().collected)
            {
                time += collision.gameObject.GetComponent<Pickup>().time;
                collision.gameObject.GetComponent<Pickup>().collected = true;
            }
            Destroy(collision.gameObject);
        }
    }
}
