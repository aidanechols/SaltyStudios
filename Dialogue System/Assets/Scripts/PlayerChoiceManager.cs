using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceManager : MonoBehaviour {

    public int[] choices;
    private int count = 0;

    // Yes = 0
    // No = 1
    public void SetNextChoice(int value)
    {
        choices[count] = value;
        count++;
    }

    public int GetChoice(int index)
    {
        return choices[index];
    }
}
