using UnityEngine;
using UnityEditor;
using com.LazyGames.DZ;

[CustomEditor(typeof(EnemyController))]

public class EnemyControllerEditor : Editor
{
    
    private EnemyController _enemyController;
    private void OnEnable()
    {
        _enemyController = (EnemyController) target;
    }

    public override void OnInspectorGUI()
    {
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("TakeDamage", GUILayout.Height(35)))
            {
                if(!Application.isPlaying)return;
                _enemyController.ReceiveAggression(Vector3.left, 0, 10);
            }
        }
        base.OnInspectorGUI();
    }
}