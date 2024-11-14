using NavMeshPlus.Components; // Убедитесь, что у вас установлен NavMeshPlus
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshSurfeces : MonoBehaviour {
    private NavMeshSurface _navMeshSurface;

    private void Awake() {
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void NavMeshSurfaceBake() {
          _navMeshSurface.BuildNavMesh();
    }
}