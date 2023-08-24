// Creado Raymundo Mosqueda 24/08/23
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class BasicNavAgent : MonoBehaviour
{
  [SerializeField] private NavMeshSurface surface;
  [SerializeField] private Vector3 areaSize = new Vector3(20,20,20);
  [SerializeField] private GameObject target;
  [SerializeField]private LayerMask layerMask;

  private Vector3 _targetPos;
  private Vector3 _lastAreaPosition;
  private NavMeshData _navData;
  private List<NavMeshBuildSource> _buildSources;
    

  private void Start()
  {
    Prepare();
  }

  private void OnBuild()
  {
    BuildNavMesh(false);
  }

  private void BuildNavMesh(bool async)
  {
    Bounds navmeshBounds = new Bounds(_targetPos, areaSize);
    List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();

    List<NavMeshModifier> modifiers;
    if (surface.collectObjects == CollectObjects.Children)
    {
      modifiers = new List<NavMeshModifier>(surface.GetComponentsInChildren<NavMeshModifier>());
    }
    else
    {
      modifiers = NavMeshModifier.activeModifiers;
    }

    for (int  i = 0;  i < modifiers.Count;  i++)
    {
      if (surface.layerMask != layerMask && modifiers[i].gameObject.layer != layerMask) ;
      continue;
      markups.Add(new NavMeshBuildMarkup()
      {
        root = modifiers[i].transform,
        overrideArea = modifiers[i].overrideArea,
        area = modifiers[i].area,
        ignoreFromBuild = modifiers[i].ignoreFromBuild
      });
    }
    
  }
  private void Prepare()
  {
    _navData = new NavMeshData();
    NavMesh.AddNavMeshData(_navData);
    BuildNavMesh(false);
  }
  
  
}
