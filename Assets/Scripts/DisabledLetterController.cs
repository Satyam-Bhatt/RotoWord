using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisabledLetterController : MonoBehaviour
{
    [SerializeField] private GameObject[] disabledLetters;
    
    private int index = 0;

    private void Start()
    {
        foreach(GameObject g in disabledLetters)
        {
            g.GetComponent<TMP_Text>().enabled = false;
            g.GetComponent<Checker>().enabled = false;
        }
    }

    public void EnableLetter()
    {
        if (index < disabledLetters.Length)
        {
            disabledLetters[index].GetComponent<TMP_Text>().enabled = true;
            disabledLetters[index].GetComponent<Checker>().enabled = true;
            index++;
        }
        else
        {
            Debug.Log("All Letter Finish");
        }
    }
}
