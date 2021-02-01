using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    [SerializeField]
    private GameObject gm;
    private float timer;
    private bool isFaling = false;
    private Collision2D faller;

    [SerializeField]
    private AudioClip fallSound;

    [SerializeField]
    private AudioClip holeClose;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.Find("Main Camera");
        audioSource.PlayOneShot(holeClose, 0.7F);
    }

   
    void Update()
    {
        if (Mathf.Round(timer) <= 1)
        {
           // print(Mathf.Round(timer));
            timer += Time.deltaTime;
        }
       
    }

    private void OnMouseOver()
    {
        GameMaster game = gm.GetComponent<GameMaster>();
        if (Input.GetMouseButtonDown(1)) {
            transform.GetComponent<Animator>().SetBool("close", true);
            game.decrementNumHoles();
            game.holePressed = false;
            audioSource.PlayOneShot(holeClose, 0.7F);
            Destroy(this.gameObject, .8f);
        }
    }

    // called when the cube hits the floor
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "NPC")
        {
            if (Mathf.Round(timer) > 1)
            {
                

                col.transform.GetComponent<Animator>().SetBool("cima", false);
                col.transform.GetComponent<Animator>().SetBool("baixo", false);
                col.transform.GetComponent<Animator>().SetBool("esquerda", false);
                col.transform.GetComponent<Animator>().SetBool("direita", false);
                col.transform.GetComponent<Animator>().SetBool("fall", true);
                col.transform.GetComponent<randomMove>().standStill();
                col.transform.GetComponent<Rigidbody2D>().isKinematic = true;
                col.transform.position = this.transform.position;
                audioSource.PlayOneShot(fallSound, 0.7F);
                Vector3 pos = col.transform.position;
                pos.y = pos.y+1;
                col.transform.position = pos;

                Destroy(col.gameObject, .5f);
            }
            
            
        }
        
       
          
    }
}
