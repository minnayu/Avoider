using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public enum KeyType
    {
        Exit
    }

    public KeyType keyType;

    private float floatingTimer = 0f;
    private float floatingMax = 1f;
    private float floatingDir = 0.01f;

    private void FixedUpdate()
    {
        if (floatingTimer < floatingMax)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + floatingDir);
            floatingTimer += Time.fixedDeltaTime;
        }
        else
        {
            floatingDir *= -1;
            floatingTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<PlayerController>().HasKey = true;
            Debug.Log("Player has key");
            Destroy(this.gameObject);
        }
    }
}
