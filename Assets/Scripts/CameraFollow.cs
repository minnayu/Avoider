using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerController pc;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(
            pc.transform.position.x,
            pc.transform.position.y,
            this.transform.position.z);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

