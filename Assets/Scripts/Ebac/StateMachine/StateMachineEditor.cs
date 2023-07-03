using System.Linq;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(FSM))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FSM fsm = (FSM)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Availabel states");

        if (showFoldout)
        {
            if(fsm.stateMachine.dictionaryStates != null)
            {
                var keys = fsm.stateMachine.dictionaryStates.Keys.ToArray();
                var values = fsm.stateMachine.dictionaryStates.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], values[i]));
                }
            }            
        }
    }
}

#endif