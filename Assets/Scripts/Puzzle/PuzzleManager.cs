using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public Activator[] activators;
    public GameObject[] puzzleObjects;
    void Start()
    {
        if (puzzleObjects.Length != activators.Length) return;
        for (int i = 0; i < puzzleObjects.Length; i++) { 
            activators[i].puzzle = puzzleObjects[i];
        }
    }

}
