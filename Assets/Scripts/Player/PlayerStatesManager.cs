using Ebac.Core.Singleton;
using Ebac.StateMachine;

public class PlayerStatesManager : Singleton<PlayerStatesManager>
{
    public enum PlayerStates
    {
        IDLE,
        WALKING,
        JUMPING
    }

    public StateMachine<PlayerStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerStateIdle());
        stateMachine.RegisterStates(PlayerStates.WALKING, new PlayerStateWalking());
        stateMachine.RegisterStates(PlayerStates.JUMPING, new PlayerStateJumping());        

        stateMachine.SwitchState(PlayerStates.IDLE);
    }
}
