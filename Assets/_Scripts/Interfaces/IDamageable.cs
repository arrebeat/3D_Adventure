using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float dmg);
    void Damage(float dmg, Vector3 dir);
}
