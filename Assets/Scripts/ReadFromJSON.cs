using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadFromJSON : MonoBehaviour
{
    [SerializeField] private TextAsset allWords;
    
    public List<string> commonWordsList;

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

        commonWordsList = new List<string>(wordData.commonWords);

        for (int i = 0; i < commonWordsList.Count; i++)
        {
            commonWordsList[i] = commonWordsList[i].ToUpper();
        }
    }
}
