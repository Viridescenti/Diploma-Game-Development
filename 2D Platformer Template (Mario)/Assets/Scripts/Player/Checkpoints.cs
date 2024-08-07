using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{

    PlayerMovement playerScript;
    public Transform respawnPoint;
    Collider2D coll;
    Animator anim;
    public GameObject WinSprite;

    public bool isEnding;


    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    private void Awake()
    {        
        playerScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(isEnding)
            {
                WinSprite.SetActive(true);
            }
            else
            {
                playerScript.UpdateCheckpoint(respawnPoint.position);
                coll.enabled = false;
                anim.Play("Checkpoint After");
                return;
            }
          
        }
     
    }

}
