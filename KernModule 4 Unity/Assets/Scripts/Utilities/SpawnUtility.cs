using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class SpawnUtility {

    public static Vector3 GetRandomLocation() {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick the first indice of a random triangle in the nav mesh
        int rndIndex = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on it
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[rndIndex]], navMeshData.vertices[navMeshData.indices[rndIndex + 1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[rndIndex + 2]], Random.value);

        return point;
    }
}
