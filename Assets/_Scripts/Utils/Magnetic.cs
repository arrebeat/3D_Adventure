using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public PlayerController_Astronaut player { get; private set; }
    public float coinSpeed = 3;

    private float _dist = .2f;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController_Astronaut>();
        coinSpeed = 3;
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > _dist)
        {
            coinSpeed += _dist; 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * coinSpeed);
        }
    }
}
