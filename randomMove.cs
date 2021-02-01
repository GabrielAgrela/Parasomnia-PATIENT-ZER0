using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMove : MonoBehaviour
{
    int incrementNumber = 0;
    int randomDirection = 0;
    public Animator animator;
    float randomTilt = 0;
    public int changeDirectionTime = 400;
    public float speed;
    int still = 0;
    void Start()
    {
        
        animator = this.GetComponent<Animator>();
        InvokeRepeating("SetRandomPos", 0, UnityEngine.Random.Range(0.001f, 0.005f));
    }
    private void Update()
    {
        if(still==0)
         speed = this.transform.parent.GetComponent<masterNPC>().speed;
    }
    void changeAnimation(string direction)
    { 
        animator.SetBool("cima", false);
        animator.SetBool("baixo", false);
        animator.SetBool("esquerda", false);
        animator.SetBool("direita", false);
        switch (direction)
        {
            case "cima": animator.SetBool("cima", true); break;
            case "baixo": animator.SetBool("baixo", true); break;
            case "esquerda": animator.SetBool("esquerda", true); break;
            case "direita": animator.SetBool("direita", true); break;
        }
    }

    public void standStill()
    {
        still = 1;
        randomTilt = 0;
    }

    void SetRandomPos()
    {
        if (incrementNumber > changeDirectionTime)
        {
            incrementNumber = 0;
        }
            
        
        if (incrementNumber == 0)
        {
            randomDirection = UnityEngine.Random.Range(0, 4);
            randomTilt = UnityEngine.Random.Range(-speed, speed);
        }
       // print(randomTilt);
        switch (randomDirection)
        {
            case 0:
                changeAnimation("direita");
                transform.Translate(speed, randomTilt, 0);
              //  print("0");
                break;
            case 1:
                changeAnimation("esquerda");
                transform.Translate(-speed, randomTilt, 0);
               // print("1");
                break;
            case 2:
                changeAnimation("cima");
                transform.Translate(randomTilt, speed, 0);
               //print("2");
                break;
            case 3:
                changeAnimation("baixo");
                transform.Translate(randomTilt, -speed, 0);
               // print("3");
                break;
            
            default:
                randomDirection = UnityEngine.Random.Range(0, 4);
                break;
        }
        incrementNumber++;
        //print(incrementNumber);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        /*if (coll.gameObject.tag == "wall")
        {
            randomDirection++;
            SetRandomPos();
        }
        if (coll.gameObject.tag == "NPC")
        {
            
        }*/
        randomDirection++;
        SetRandomPos();
    }

}
