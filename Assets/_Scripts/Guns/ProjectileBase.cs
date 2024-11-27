using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 50f;
    public int damage;
    public float timeToDestroy = 2f;

    public List<string> hitTags;

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
        foreach (var t in hitTags)
        {
            if (other.transform.tag == t)
            {
                var damageable = other.transform.GetComponent<IDamageable>();
                
                if (damageable != null) 
                {
                    Vector3 dir = other.transform.position - transform.position;
                    dir.y = 0;
                    dir = -dir.normalized;

                    damageable.Damage(damage, dir);
                }
                
                Destroy(gameObject);
                break;
            }
        }         

    }
}
