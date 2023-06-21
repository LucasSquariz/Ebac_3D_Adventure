using UnityEngine;
using Ebac.StateMachine;
using NaughtyAttributes;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK
    }

    public class BossBase : MonoBehaviour
    {
        [SerializeField, BoxGroup("References")] public StateMachine<BossAction> stateMachine;
        [SerializeField, BoxGroup("References")] public List<Transform> waypoints;

        [SerializeField, BoxGroup("Boss animation config")] public float startAnimationDuration = .5f;
        [SerializeField, BoxGroup("Boss animation config")] public Ease startAnimationEase = Ease.OutBack;

        [SerializeField, BoxGroup("Boss config")] public float speed = 1f;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        }

        public void GoToRandomPosition()
        {
            StartCoroutine(GoToPositionCoroutine(waypoints[Random.Range(0, waypoints.Count)]));
        }

        IEnumerator GoToPositionCoroutine(Transform t)
        {
            while(Vector3.Distance(transform.position, t.position) >1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
        }

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion

        #region DEBUG

        [Button]
        public void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }
        [Button]
        public void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }


        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}

