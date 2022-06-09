using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        UnityEditor.Handles.color = Color.red;
        GUIStyle style = new GUIStyle();
        style.fontSize = 50;

        UnityEditor.Handles.Label(transform.position, gameObject.name, style);
        UnityEditor.Handles.color = Color.red;
    }
}
