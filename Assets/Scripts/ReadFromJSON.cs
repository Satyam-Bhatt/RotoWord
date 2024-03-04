using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadFromJSON : MonoBehaviour
{
    [SerializeField] private TextAsset allWords;
    
    public Dictionary<string, bool> commonWordsList;

    //[System.Serializable]
    public class WordData
    {
        public string description;
        public string[] commonWords;
    }

    [SerializeField] private WordData wordData = new WordData();

    private void Start()
    {
        wordData = JsonUtility.FromJson<WordData>(allWords.text);

        commonWordsList = new Dictionary<string, bool>();

        foreach (string word in wordData.commonWords)
        {
            commonWordsList.Add(word.ToUpper(), true);
        }
    }
}
