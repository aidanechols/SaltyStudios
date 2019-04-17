using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Background", menuName = "Background/NPC")][System.Serializable]
public class Background : ScriptableObject
{

    [TextArea(2,10)]
    public string[] dialogue;

    public Sprite[] characterSprite;

    public Vector3[] positions;
}
