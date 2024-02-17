using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public int win_Counter = 0;

    public List<Checker> checkers = new List<Checker>();

    public float timeCounter = -0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
    }

    public async Task AnimationRoutine()
    {
        timeCounter = Time.time + 1.5f;
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
        else { Debug.Log("Stop Interaction"); }
    }
}
