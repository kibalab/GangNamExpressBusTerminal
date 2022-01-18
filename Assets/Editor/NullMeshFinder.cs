using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NullMeshFinder : EditorWindow
{
    List<MeshRenderer> nullRenderers = new List<MeshRenderer>();
    List<MeshFilter> nullFilter = new List<MeshFilter>();
    

    [MenuItem("K13A_/NullMeshFinder")]
    static void OnOpen()
    {
        NullMeshFinder window = (NullMeshFinder)EditorWindow.GetWindow(typeof(NullMeshFinder));
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Find by MeshRenderer"))
        {
            var roots = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (var obj in roots)
            {
                var renderers = obj.GetComponentsInChildren<MeshRenderer>();
                foreach (var renderer in renderers)
                {
                    if (renderer.materials.Length <= 0)
                    {
                        Debug.Log(renderer.gameObject.name);
                        nullRenderers.Add(renderer);
                    }
                }
            }
        }
        if (GUILayout.Button("Find by MeshFilter"))
        {
            var roots = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (var obj in roots)
            {
                var renderers = obj.GetComponentsInChildren<MeshFilter>();
                foreach (var renderer in renderers)
                {
                    if (renderer.mesh == null)
                    {
                        Debug.Log(renderer.gameObject.name);
                        nullFilter.Add(renderer);
                    }
                }
            }
        }

        if(nullRenderers.Count > 0)
        {
            foreach(var nulls in nullRenderers)
            {
                EditorGUILayout.TextField(nulls.gameObject.name);
                EditorGUILayout.ObjectField("Script Location", nulls.gameObject, typeof(GameObject), false);
            }
        }
        if (nullFilter.Count > 0)
        {
            foreach (var nulls in nullFilter)
            {
                EditorGUILayout.TextField(nulls.gameObject.name);
                EditorGUILayout.ObjectField("Script Location", nulls.gameObject, typeof(GameObject), false);
            }
        }
    }
}
