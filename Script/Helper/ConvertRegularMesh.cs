using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertRegularMesh : MonoBehaviour
{
    [ContextMenu ("Convertendo para uma Mesh regular")]
    void Convert(){
        SkinnedMeshRenderer skinnedmeshrenderer = GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.sharedMesh = skinnedmeshrenderer.sharedMesh;
        meshRenderer.sharedMaterials = skinnedmeshrenderer.sharedMaterials;

        DestroyImmediate(skinnedmeshrenderer);
        DestroyImmediate(this);
    }
}
