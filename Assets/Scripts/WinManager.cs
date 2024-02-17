using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [HideInInspector] public int win_Counter = 0;
    [HideInInspector] public List<Checker> checkers = new List<Checker>();
    [HideInInspector] public float timeCounter = 0f;
    [HideInInspector] public bool isRunning = false;


    // Update is called once per frame
    void Update()
    {
        if (win_Counter == 0)
        {
            Debug.Log("You won!");
        }
    }

    public async void testCase()
    {
        if(checkers.Count > 0)
        {
            await AnimationRoutine();
        }
        else isRunning = false;
    }

    public async Task AnimationRoutine()
    {
        timeCounter = Time.time + 1.5f; //to control the amount of time next word comes and previous one is destroyed
        isRunning = true;
        while (Time.time < timeCounter)
        {
            checkers[0].letterCollectionAnimation.isPlaying = true;
            foreach (LetterCollectionAnimation l in checkers[0].letterCollectionAnimationFromHit) { l.isPlaying = true; }
            await Task.Yield();
        }

        if (checkers.Count > 0)
        {
            checkers[0].DestroyThis();
            checkers.Remove(checkers[0]);
            testCase();
        }
        else {
            Debug.Log("Stopped");
            isRunning = false; 
        }
    }
}
