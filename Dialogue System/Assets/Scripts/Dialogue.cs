using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/NPC")][System.Serializable]
public class Dialogue : ScriptableObject{

    public new string name;

    [TextArea(3,10)]
    public string[] sentences;

    [TextArea(3,10)]
    public string[] branches;

    [TextArea(3, 10)]
    public string[] branchesInBranches;
}
