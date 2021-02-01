using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{

    [SerializeField]
    private GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "SCORE: " + gm.GetComponent<GameMaster>().countPlayers();
    }
}
