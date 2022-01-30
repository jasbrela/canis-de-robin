using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible Data", menuName = "Collectible Data")]
public class Collectible : ScriptableObject
{
    public Collectibles type;
    public Sprite sprite;
    [TextArea(5, 5)] public string message;
    public string cancelMsg;
    public string collectMsg;

}
