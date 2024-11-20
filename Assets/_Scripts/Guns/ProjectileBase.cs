using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 50f;
    public int damage;
    public float timeToDestroy = 2f;

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);    
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);    
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
