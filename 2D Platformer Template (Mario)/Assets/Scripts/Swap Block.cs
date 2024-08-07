using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBlock : MonoBehaviour
{

    public Animator anim;
    public bool isBlue;
    bool willChange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").GetComponent<PlayerMovement>().blockToggle)
        {
            if (isBlue)
            {
                anim.Play("Swapblock Off");
             
            }
            else
            {
                anim.Play("Swapblock On");
            }
          
        }
        else
        {
            if (isBlue)
            {
                anim.Play("Swapblock On");

            }
            else
            {
                anim.Play("Swapblock Off");
            }


        }
    }
   
}
