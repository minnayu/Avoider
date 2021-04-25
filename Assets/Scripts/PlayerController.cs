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
    private const float DOUBLE_CLICK_TIME = 0.5f;
    private float lastClickTime;
    private bool HasSpeedBoost = false;

    public int speedModAmount = 0;
    private float speedTimeMax = 1.5f;
    private float speedTimeCur = 0f;

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
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
            {
                Debug.Log("Double Click");
                HasSpeedBoost = true;
                if (HasSpeedBoost && speedTimeCur < speedTimeMax)
                {
                    Debug.Log("speed boost");
                    speed = speedModAmount;
                    //StartCoroutine(Wait());
                    speedTimeCur += Time.fixedDeltaTime;
                }
                else
                {
                    speedTimeCur = 0f;
                    speed = 3;
                    HasSpeedBoost = false;
                }
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(MoveTo(transform.position, mouseInSpace, speed));
            }
            
            lastClickTime = Time.time;
        }
        

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Speed boost over");
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
