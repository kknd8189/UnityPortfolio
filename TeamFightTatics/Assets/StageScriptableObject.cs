using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/StageData", order = int.MaxValue)]

public class StageScriptableObject : ScriptableObject
{
    public int[] Amount;
    public int[] Index;
}
