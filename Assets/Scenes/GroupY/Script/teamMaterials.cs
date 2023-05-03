using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TeamY2023/teamMaterials scriptable object", fileName = "TeamMaterials")]
public class teamMaterials : ScriptableObject
{
    [Tooltip("the material at index 0 should be the neutral material")]
    public Material[] materials;
}
