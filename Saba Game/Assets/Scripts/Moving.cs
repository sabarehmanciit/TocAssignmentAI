using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Moving : MonoBehaviour
{
   // FirstPersonController fpc;
    public bool collide;
    public float health;
    public bool still;
    public bool run;
    public bool pos;
    public Vector3 posi;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "fruit")
        {
            other.gameObject.SetActive(false);
          ////  health = health + 1;
        
        }
        
        if (other.transform.tag == "obst")
        {
            collide = true;
         //   health = health - 10;
            pos = true;
        }
    }
     void movement()
    {
        if (GameObject.FindObjectOfType<FirstPersonController>().m_CharacterController.velocity == Vector3.zero)
        {
            Debug.Log("stand");
            run = false;
            still = true;
   }
        else if (GetComponent<FirstPersonController>().m_IsWalking)
        {
            Debug.Log("walk");
            run = true;
            still = false;
        }
    }
    void Start()
    {
        health = 0;
        run = false;
        pos = false;
        collide = false;
        still = false;
        // fpc = GameObject.FindObjectOfType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        if(pos == true)
        {
            pos = false;
            posi = new Vector3(148.66f, 1.67f, 135.83f);
            transform.position = posi;

        }
    }
}
