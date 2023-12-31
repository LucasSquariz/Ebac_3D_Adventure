using System;
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
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [SerializeField, BoxGroup("References")] public StateMachine<BossAction> stateMachine;
        [SerializeField, BoxGroup("References")] public List<Transform> waypoints;
        [SerializeField, BoxGroup("References")] public HealthBase healthBase;        

        [SerializeField, BoxGroup("Boss animation config")] public float startAnimationDuration = .5f;
        [SerializeField, BoxGroup("Boss animation config")] public Ease startAnimationEase = Ease.OutBack;

        [SerializeField, BoxGroup("Boss Attack config")] public int attackAmount = 5;
        [SerializeField, BoxGroup("Boss Attack config")] public float timeBetweenAttacks = .5f;

        [SerializeField, BoxGroup("Boss config")] public float speed = 1f;

        private void Start()
        {
            Init();
            OnValidate();
            healthBase.OnKill += OnBossKill;
        }

        private void OnValidate()
        {
            if (healthBase == null) GetComponent<HealthBase>();
        }

        public void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());

            SwitchState(BossAction.ATTACK);
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }

        #region ATTACK

        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(StartAttackCoroutine(endCallback));
        }

        IEnumerator StartAttackCoroutine(Action endCallback)
        {
            int attacks = 0;

            while(attacks < attackAmount)
            {
                attacks++;
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }
        #endregion

        #region WALK
        public void GoToRandomPosition(Action onArrive = null)
        {
            StartCoroutine(GoToPositionCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPositionCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }

        #endregion

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

        [Button]
        public void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
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

