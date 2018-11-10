using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask DestructMask;
    public ParticleSystem ExplosionParticles;       
    public AudioSource ExplosionAudio;              
    public float Damage = 25f;                  
    public float ExplosionForce = 1000f;                              
    public float ExplosionRadius = 5f;
    

    private void Start()
    {
 
    }


    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, DestructMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigitbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigitbody)
            {
                DestroyBrick targetBrick = colliders[i].GetComponent<DestroyBrick>();
                targetBrick.TakeDamage(Damage);
            }
            else
            {
                targetRigitbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
                TankHealth targetHealth = colliders[i].GetComponent<TankHealth>();
                if (targetHealth)
                {
                    targetHealth.TakeDamage(Damage);
                }
                else
                {
                    EnemyHealth parentHealth = gameObject.GetComponentInParent<EnemyHealth>();
                    if (!parentHealth)
                    {
                        EnemyHealth enemyHealth = colliders[i].GetComponent<EnemyHealth>();
                        enemyHealth.TakeDamage(Damage);
                    }

                }
                
            }
        }
        ExplosionParticles.transform.parent = null;
        ExplosionParticles.Play();
        //ExplosionAudio.Play();
        //Destroy(ExplosionParticles.gameObject, ExplosionParticles.main.duration);
        Destroy(gameObject);
    }
}