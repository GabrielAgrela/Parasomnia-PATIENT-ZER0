using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    [SerializeField]
    private GameObject gm;

    [SerializeField]
    private bool isInfected;
    // Start is called before the first frame update

    [SerializeField]
    private bool hasVirus;

    [SerializeField]
    private int timeToDie;      // time to die in seconds

    private GameObject winText;
    private GameObject loseText;

    [SerializeField]
    private GameObject parentWinLose;

    public AudioClip deathSound;
    public AudioClip gameOverSound;
    AudioSource audioSource;

    [SerializeField]
    private GameObject playAgainText;


    int fitacola = 1;
    int fitacola2 = 1;
    int finish = 0;

    private float timer, timer2;

    Animator animator;

    void Start()
    {
        
        gm = GameObject.Find("Main Camera");
        parentWinLose = GameObject.Find("Canvas");
        audioSource = GetComponent<AudioSource>();
        playAgainText = parentWinLose.transform.Find("PlayAgainText").gameObject;
        winText = parentWinLose.transform.Find("WinText").gameObject;
        loseText = parentWinLose.transform.Find("LoseText").gameObject;
        animator = GetComponent<Animator>();
        isInfected = false;
        hasVirus = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (gm.GetComponent<GameMaster>().numberNPC <= 0)
        {
            setInfected();
        }

        if (hasVirus)
        {
            death();
        }
        if ((winText.active || loseText.active) && fitacola2 ==1)
        {
            fitacola2 = 0;
            audioSource.PlayOneShot(gameOverSound, 1F);
        }

    }

    public bool GetInfected()
    {
        return isInfected;
    }

    public void SetHasVirus()
    {
        hasVirus = true;
    }

    void setInfected()
    {
        if (this.gameObject == gm.GetComponent<GameMaster>().GetInfected())
        {
           // print("Infected: " + gameObject.name);
            isInfected = true;
        }
    }


    void death()
    {
        timer2 += Time.deltaTime;
        if (Mathf.Round(timer2) == timeToDie && fitacola ==1)
        {
            fitacola = 0;
            print("--------------------");
            transform.GetChild(0).GetComponent<Animator>().SetBool("contaminated", false);
            this.gameObject.GetComponent<randomMove>().speed = 0;
            //animator.SetBool("death", true);
            audioSource.PlayOneShot(deathSound, 0.7F);
            Destroy(this.gameObject, 2f);
            print("--------------------2");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (Mathf.Round(timer) > 5)
        {
            if (isInfected && col.gameObject.tag == "NPC")
            {
                print(col.gameObject.name);
                col.gameObject.GetComponent<Infected>().SetHasVirus();
                col.transform.GetChild(0).GetComponent<Animator>().SetBool("contaminated", true);
            }
        }
        
    }

    void OnDestroy()
    {
        
        if (isInfected)
        {
            winText.SetActive(true);
            this.transform.parent.GetComponent<masterNPC>().speed = 0;
            playAgainText.SetActive(true);
        }

        if (gm.GetComponent<GameMaster>().countPlayers() <= 1)
        {
            loseText.SetActive(true);
            this.transform.parent.GetComponent<masterNPC>().speed = 0;
            playAgainText.SetActive(true);
        }
    }

}
