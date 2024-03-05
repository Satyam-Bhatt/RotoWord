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
            if(g.GetComponent<TMP_Text>() != null) g.GetComponent<TMP_Text>().enabled = false;
            else g.GetComponentInChildren<TMP_Text>().enabled = false;
            if(g.GetComponent<Checker>() != null) g.GetComponent<Checker>().enabled = false;
        }
    }

    public void EnableLetter()
    {
        if (index < disabledLetters.Length)
        {
            if(disabledLetters[index].GetComponent<TMP_Text>() != null) disabledLetters[index].GetComponent<TMP_Text>().enabled = true;
            else disabledLetters[index].GetComponentInChildren<TMP_Text>().enabled = true;
            disabledLetters[index].GetComponent<Checker>().enabled = true;

            if(disabledLetters[index].GetComponent<BoxCollider2D>() != null) disabledLetters[index].GetComponent<BoxCollider2D>().enabled = true;
            else disabledLetters[index].GetComponentInChildren<BoxCollider2D>().enabled = true;

            index++;
        }
        else
        {
            Debug.Log("All Letter Finish");
        }
    }
}
