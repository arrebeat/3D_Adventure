using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(int dmg);
    void Damage(int dmg, Vector3 dir);
}
