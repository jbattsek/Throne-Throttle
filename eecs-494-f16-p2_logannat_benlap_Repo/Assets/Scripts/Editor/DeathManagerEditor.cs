using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DeathManager))]
public class DeathManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		DeathManager myScript = (DeathManager)target;
		if(GUILayout.Button("Kill Player 0"))
		{
			myScript.KillPlayer(0, false);
		}
		if(GUILayout.Button("Kill Player 1"))
		{
			myScript.KillPlayer(1, false);
		}
		if(GUILayout.Button("Kill Player 2"))
		{
			myScript.KillPlayer(2, false);
		}
		if(GUILayout.Button("Kill Player 3"))
		{
			myScript.KillPlayer(3, false);
		}
	}
}
