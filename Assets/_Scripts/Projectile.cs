using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] float speed = 10f;
    [SerializeField] float timer = .5f;
    [SerializeField] GameObject spark;
    [SerializeField] GameObject visual;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        spark.SetActive(false);
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamege(damage);
            StartCoroutine(MakeSparksAndOff());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator MakeSparksAndOff()
    {
        visual.SetActive(false);
        spark.SetActive(true);
        yield return new WaitForSeconds(timer);
        visual.SetActive(true);
        spark.SetActive(false);
        gameObject.SetActive(false);
    }
}
