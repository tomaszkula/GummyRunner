using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    [SerializeField] float timeToOpen = 3f;

    Animator myAnimator;
    Collider2D myCollider;
    LevelManager levelManager;

    float timeWaiting;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() && levelManager.CanWin())
        {
            timeWaiting += Time.deltaTime;
            if (timeWaiting >= timeToOpen)
            {
                myAnimator.SetTrigger("Opening");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && levelManager.CanWin())
        {
            timeWaiting = 0;
        }
    }

    private void PlayerDisappearAnimation()
    {
        FindObjectOfType<SceneLoader>().LoadNextLevel();
    }
}
