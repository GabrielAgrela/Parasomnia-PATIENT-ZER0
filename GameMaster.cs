using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private int cooldownHole;       // in seconds

    private int cdHole;
    private float timer;
    private int speed = 10;

    public GameObject[] NPCTypes;
   // public GameObject zezinho;

    public bool holePressed;

    [SerializeField]
    private int maxNumHoles = 5;
    private int currentNumHoles;
    public int numberNPC = 10;
    private int numberNPCInitial = 0;

    public float spawnTimer = 0.01f;
    int oldscore=0;

    [SerializeField]
    private GameObject holeObject;

    [SerializeField]
    private GameObject parentHole;  // location of parent of hole
    public GameObject parentNPC;

    private GameObject[] npcs;
    public GameObject npcCoiso;
    private GameObject infectedNPC;

    [SerializeField]
    private AudioClip openHole;
    AudioSource audioSource;



    void Start() {
        numberNPCInitial = numberNPC;
        cdHole = cooldownHole;
        holePressed = false;
        currentNumHoles = 0;
        Spawn();



    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Spawn();
        DecreaseCooldownHole();
        spawnHoles();
    }

    public int GetCooldownHole()
    {
        return cooldownHole;
    }

    public int GetCurrentCooldownHole()
    {
        return cdHole;
    }

    public GameObject GetInfected() {
        return infectedNPC;
    }

    public void decrementNumHoles()
    {
        currentNumHoles--;
    }

    public int countPlayers()
    {
        if (parentNPC.GetComponent<masterNPC>().speed > 0)
        {
            oldscore = GameObject.FindGameObjectsWithTag("NPC").Length;
            return GameObject.FindGameObjectsWithTag("NPC").Length;
        }
            
        else return oldscore;
        
    }

    void Spawn()
    {
        if (timer > spawnTimer && numberNPC > 0)
        {
            int random = (Random.Range(0, 3));
            numberNPC--;
            timer = timer - spawnTimer;
            int spawnPointX = Random.Range(-5, 7);
            int spawnPointY = Random.Range(3, -4);
            Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, 0);
            if (random == 0)
                Instantiate(NPCTypes[0], spawnPosition, Quaternion.identity).transform.SetParent(parentNPC.transform);
            else if (random ==1)
                Instantiate(NPCTypes[1], spawnPosition, Quaternion.identity).transform.SetParent(parentNPC.transform);
            else
                Instantiate(NPCTypes[2], spawnPosition, Quaternion.identity).transform.SetParent(parentNPC.transform);
        }
        else if (numberNPC == 0)
        {
            numberNPC = -1;
            int randNum = Random.Range(0, numberNPCInitial);
            npcs = GameObject.FindGameObjectsWithTag("NPC");
            infectedNPC = npcs[randNum];
            print("infectedn:" + infectedNPC);
        }

    }

    private void DecreaseCooldownHole()
    {
        if (holePressed == true) {
            timer += Time.deltaTime;

            if (Mathf.Round(timer) == 1)
            {
                cdHole--;
                timer = 0;
            }

            if (cdHole == 0)
            {
                cdHole = cooldownHole;
                timer = 0f;
                holePressed = false;
            }
            print("cd: " + cdHole);
        }
    }
   
    private void spawnHoles()
    {
        if (currentNumHoles < maxNumHoles)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (parentNPC.GetComponent<masterNPC>().speed > 0)
                {
                    Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    spawnPosition.z = 0.0f;
                    GameObject objectInstance = Instantiate(holeObject, spawnPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
                    objectInstance.transform.localScale = new Vector3(0.6f, 0.6f);
                    objectInstance.transform.SetParent(parentHole.transform);
                    currentNumHoles++;
                    audioSource.PlayOneShot(openHole, 0.7F);
                }

                else print("xd");

               
                
            }

        }
    }
}
