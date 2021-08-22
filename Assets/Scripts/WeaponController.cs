using System;
using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask layerMask;
    [Header("Shooting Stuff")]
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private float bulletSpeed;


    private Transform leftGun;
    private Transform rightGun;
    private GameObject target = null;
    private bool canShoot=true;

    private void Start()
    {
        leftGun = transform.GetChild(1);
        rightGun = transform.GetChild(2);
        InvokeRepeating(nameof(FindClosestEnemy), 0f, 0.1f);
    }

    private void Update()
    {
        if (target == null) return;
        Vector3 dirn = target.transform.position - transform.position;
        Vector3 rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirn), Time.deltaTime*10f).eulerAngles;
        transform.rotation=Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        Shoot();
    }

    private void Shoot()
    {
        if (!canShoot) return;
        GameObject leftBullet=Instantiate(bullet, leftGun.position, Quaternion.identity, bulletContainer);
        GameObject rightBullet=Instantiate(bullet, rightGun.position, Quaternion.identity, bulletContainer);
        Vector3 forward = transform.forward;
        leftBullet.GetComponent<Rigidbody>().AddForce(bulletSpeed*forward);
        rightBullet.GetComponent<Rigidbody>().AddForce(bulletSpeed*forward);
        StartCoroutine(DestroyBullets(leftBullet, rightBullet));
        canShoot = false;
        StartCoroutine(ShootCoolDown());
    }

    private IEnumerator DestroyBullets(GameObject leftBullet, GameObject rightBullet)
    {
        LeanTween.alpha(leftBullet, 0, 0.25f);
        LeanTween.alpha(rightBullet, 0, 0.25f);
        yield return new WaitForSeconds(0.25f);
        Destroy(leftBullet);
        Destroy(rightBullet);
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

    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
