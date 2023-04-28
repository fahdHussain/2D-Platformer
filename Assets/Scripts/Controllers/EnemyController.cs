using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected abstract void Start();
    protected abstract void Update();
    protected abstract void FixedUpdate();
    public abstract void SetAggro(bool state);
    protected abstract void changeDirection();
    protected abstract IEnumerator waitIdle();
    public abstract void takeDamage(int damage);

}
