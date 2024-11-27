using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyPatrol : EnemyBase
    {
        [Header("Patrol")]
        public GameObject[] waypoints;
        public float patrolSpeed = 5f;
        public float minDistance = .5f;

        [SerializeField] private int _index = 0;

        public override void Update()
        {
            if (Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance)
            {
                _index++;
                if (_index >= waypoints.Length) _index = 0;
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, patrolSpeed * Time.deltaTime);
            transform.LookAt(waypoints[_index].transform.position);
        }

        private void LookAt()
        {
            Vector3 direction = waypoints[_index].transform.position;
            direction.y = 0f;

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        }
    }
}
