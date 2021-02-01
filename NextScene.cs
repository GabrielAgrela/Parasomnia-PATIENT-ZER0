using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] orderToShow;

    [SerializeField]
    private GameObject continueBtn;
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        orderToShow[i].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        checkIfSpacePressed();
    }

    private void checkIfSpacePressed()
    {
        if (Input.GetKeyDown("space") && i < orderToShow.Length-1)
        {
            orderToShow[i].SetActive(false);
            i++;
            orderToShow[i].SetActive(true);
        }
        
        if (i >= orderToShow.Length-1)
        {
            continueBtn.SetActive(false);
        }
    }
    public void changeScene()
    {
        SceneManager.LoadScene("SceneGame");
    }
    public void changeScene2()
    {
        SceneManager.LoadScene("SceneGame2");
    }
    public void changeScene3()
    {
        SceneManager.LoadScene("SceneGame3");
    }
}
