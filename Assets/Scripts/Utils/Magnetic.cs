using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public float minDistance = .2f;
    public float coinSpeed = 10f;

    void Update()
    {
        if(Vector3.Distance(transform.position, Player.Instance.transform.position) > minDistance)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * coinSpeed);
        }
    }
}
