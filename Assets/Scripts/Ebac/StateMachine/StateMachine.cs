using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StateMachine : MonoBehaviour
{
    public enum States
    {
        NONE,
    }

    public Dictionary<States, StateBase> dictionaryStates;

    private StateBase _currentState;
    public float timeToStartGame = 1f;

    private void Awake()
    {
        dictionaryStates = new Dictionary<States, StateBase>();
        dictionaryStates.Add(States.NONE, new StateBase());

        SwitchState(States.NONE);
        Invoke(nameof(StartGame), timeToStartGame);
    }

    [Button]
    private void StartGame()
    {
        SwitchState(States.NONE);
    }

#if UNITY_EDITOR
    #region Debbug
    [Button]
    private void SwitchStateToNone()
    {
        SwitchState(States.NONE);
    }
    #endregion
#endif

    private void SwitchState(States state)
    {
        if (_currentState != null) _currentState.OnStateExit();

        _currentState = dictionaryStates[state];
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }

}
