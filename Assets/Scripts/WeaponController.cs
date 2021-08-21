using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask layerMask;

    private GameObject target = null;

    private void Start()
    {   
        InvokeRepeating(nameof(FindClosestEnemy), 0f, 0.1f);
    }

    private void Update()
    {
        if (target == null) return;
        Vector3 dirn = target.transform.position - transform.position;
        Vector3 rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirn), Time.deltaTime*10f).eulerAngles;
        transform.rotation=Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void FindClosestEnemy()
    {
        if (target != null)
        {
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) > range * range)
            {
                target = null;
            }
            else return;
        }
        float shortestDistance=Mathf.Infinity;
        GameObject closestTarget = null;
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, layerMask);
        foreach (Collider enemy in enemies)
        {
            float distanceFromWeapon = Vector3.Magnitude(transform.position - enemy.transform.position);
            if (distanceFromWeapon < shortestDistance) shortestDistance = distanceFromWeapon;
            closestTarget = enemy.gameObject;
        }
        target = shortestDistance <= range ? closestTarget : null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
