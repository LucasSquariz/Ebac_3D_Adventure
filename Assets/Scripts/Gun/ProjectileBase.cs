using UnityEngine;

public class ProjectileBase : MonoBehaviour
{    
    public float timeToDestroy = 1f;
    public float speed = 50f;
    public int damageAmount = 1;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime );
    }

    private void OnCollisionEnter(Collision collision)
    {        
        var damageable = collision.transform.GetComponent<IDamageable>();

        if (damageable != null) 
        { 
            Vector3 direction = collision.transform.position - transform.position;
            direction = -direction.normalized;
            direction.y = 0f;

            damageable.Damage(damageAmount, direction);
            Destroy(this.gameObject);
        }
        
    }
}