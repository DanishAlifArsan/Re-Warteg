
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CutoffMask : Image
{
    public override UnityEngine.Material materialForRendering{
        get{
            UnityEngine.Material material = new UnityEngine.Material(base. materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material;
        }
    }
    
}
