using System.Linq;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(PlayerStatesManager))]
public class PlayerStatesEditor : Editor
{
    public bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerStatesManager playerStatesManager = (PlayerStatesManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (playerStatesManager.stateMachine == null) return;

        if (playerStatesManager.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", playerStatesManager.stateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Availabel states");

        if (showFoldout)
        {
            if (playerStatesManager.stateMachine.dictionaryStates != null)
            {
                var keys = playerStatesManager.stateMachine.dictionaryStates.Keys.ToArray();
                var values = playerStatesManager.stateMachine.dictionaryStates.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], values[i]));
                }
            }
        }
    }
}

#endif