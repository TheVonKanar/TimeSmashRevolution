using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PoolManager))]
public class PoolManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PoolManager myTarget = (PoolManager)target;

        if (Application.isEditor)
        {
            if (myTarget.poolItems != null)
            {
                for (int i = 0; i < myTarget.poolItems.Length; i++)
                {
                    if (string.IsNullOrEmpty(myTarget.poolItems[i].name) && myTarget.poolItems[i].prefab != null)
                    {
                        myTarget.poolItems[i].name = myTarget.poolItems[i].prefab.name;
                    }
                }
            }
        }
    }
}
