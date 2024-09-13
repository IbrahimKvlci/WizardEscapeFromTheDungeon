using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Dashing dashing;

    [Header("Mesh Related")]
    [SerializeField] private float meshRefreshRate = 0.1f;
    [SerializeField] private float meshDestroyDelay = 3f;

    [Header("Shader Related")]
    [SerializeField] private Material mat;
    [SerializeField] private string shaderVarRef;
    [SerializeField] private float shaderVarRate=0.1f;
    [SerializeField] private float shaderVarRefreshRate=0.05f;

    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshRenderers;
    [SerializeField] private Transform posToSpawn;

    private void Update()
    {
        if (dashing.IsDashing)
            StartCoroutine(ActiveTrail());
    }

    IEnumerator ActiveTrail()
    {
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            GameObject gObj = new GameObject();
            gObj.transform.SetPositionAndRotation(posToSpawn.position, posToSpawn.rotation);

            MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
            MeshFilter mf = gObj.AddComponent<MeshFilter>();

            Mesh mesh = new Mesh();
            skinnedMeshRenderers[i].BakeMesh(mesh);

            mf.mesh = mesh;
            mr.material = mat;

            StartCoroutine(AnimateMaterialFloat(mr.material,0,shaderVarRate,shaderVarRefreshRate));

            Destroy(gObj, meshDestroyDelay);
        }

        yield return new WaitForSeconds(meshRefreshRate);
    }

    IEnumerator AnimateMaterialFloat(Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);

        while (valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
