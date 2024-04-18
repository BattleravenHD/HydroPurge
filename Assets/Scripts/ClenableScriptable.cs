using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCleaning", menuName = "Cleaning/CleaningItem", order = 1)]
public class ClenableScriptable : ScriptableObject
{
    public string LayerTag;
    public Color particleColor;
    public Color liquidColourMain;
    public Color liquidColourSecondary;
}
