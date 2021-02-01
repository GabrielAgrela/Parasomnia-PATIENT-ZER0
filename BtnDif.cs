using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDif : MonoBehaviour
{
    [SerializeField]
    private AudioClip btnDif;

    AudioSource audioSource;

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
