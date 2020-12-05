using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PerlinNoise : MonoBehaviour
{   // Runtime noise

    public float noiseScale;
    public float waveHeight;
    public float waveSpeed;



    // Update is called once per frame
    void FixedUpdate()
    {
        Noise();
    }

    void Noise()
    {

        MeshCollider meshCol = GetComponent<MeshCollider>();
        MeshFilter meshFilt = GetComponent<MeshFilter>();

        Vector3[] verticesCol = meshCol.sharedMesh.vertices; // Gets EVERY collider vertices
        Vector3[] verticesFilt = meshFilt.mesh.vertices; // Same for this but for renders

        for (int i = 0; i < verticesCol.Length; i++) // either col or filt for length is fine
        {
            float pZCol = (verticesCol[i].z * noiseScale) + (Time.time * waveSpeed);
            float pXCol = (verticesCol[i].x * noiseScale) + (Time.time * waveSpeed);

            float pZFilt = (verticesFilt[i].z * noiseScale) + (Time.time * waveSpeed);
            float pXFilt = (verticesFilt[i].x * noiseScale) + (Time.time * waveSpeed);

            verticesCol[i].y = Mathf.PerlinNoise(pXCol,pZCol) * waveHeight;
            verticesFilt[i].y = Mathf.PerlinNoise(pXFilt, pZFilt) * waveHeight;

        }

        meshCol.sharedMesh.vertices = verticesFilt;
        meshCol.sharedMesh = meshCol.sharedMesh;
        meshCol.sharedMesh.RecalculateNormals();
        meshCol.sharedMesh.RecalculateBounds();
        meshCol.sharedMesh.RecalculateTangents();

        meshFilt.mesh.vertices = verticesFilt;
        meshFilt.mesh.RecalculateNormals();
        meshFilt.mesh.RecalculateBounds();

    }

}
