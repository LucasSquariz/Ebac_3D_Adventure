using Enemy;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        [SerializeField, BoxGroup("References")] public List<Transform> waypoints;

        [SerializeField, BoxGroup("Walk setup")] public float minDistance = 1f;
        [SerializeField, BoxGroup("Walk setup")] public float speed = 1f;

        [ShowNonSerializedField] private int _index = 0;

        private void Update()
        {
            if (Vector3.Distance(this.transform.position, waypoints[_index].position) < minDistance)
            {
                _index++;
                if (_index >= waypoints.Count)
                {
                    _index = 0;
                }
            }

            this.transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].position, Time.deltaTime * speed);
            this.transform.LookAt(waypoints[_index].position);
        }

    }
}

