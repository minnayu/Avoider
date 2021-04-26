using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 0.1f;
  
    public Animator animator;
    public bool HasKey = false;

    Vector3 startingPos;

    //half a second click
    private float ClickTime;
    private bool doubleClicked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - ClickTime;
            if (timeSinceLastClick <= .2f)
            {
                doubleClicked = true;
            }
            ClickTime = Time.time;

            if (doubleClicked)
            {
                Debug.Log("Player speed doubled");
                speed = 6f;
                Invoke("ResetSpeed", 1.5f);
            }

            StopAllCoroutines();
            StartCoroutine(MoveTo(transform.position, mouseInSpace, speed));

        }

    }

    void ResetSpeed()
    {
        speed = 3f;
        Debug.Log("Player speed reset");
    }


    IEnumerator MoveTo(Vector3 start, Vector3 destination, float speed)
    {
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void ResetPlayer()
    {
        this.gameObject.transform.position = startingPos;
    }

    
}
