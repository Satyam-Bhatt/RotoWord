using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledLetterController : MonoBehaviour
{
    [SerializeField] private GameObject[] disabledLetters;
    
    private int index = 0;

    private void Start()
    {
        foreach(GameObject g in disabledLetters)
        {
            g.SetActive(false);
        }
    }

    public void EnableLetter()
    {
        if (index >= disabledLetters.Length)
        {
            disabledLetters[index].SetActive(true);
            index++;
        }
        else
        {
            Debug.Log("All Letter Finish");
        }
    }
}
