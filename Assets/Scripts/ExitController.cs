﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    public GameObject Panel;
    public GameObject PanelText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && collision.gameObject.GetComponent<PlayerController>().HasKey)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
