using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public GameObject ExplosionPrefab;
    public float CurrentHealth;
    public static int EnemyCount;

    private AudioSource ExplosionAudio;
    private ParticleSystem ExplosionParticles;
    private bool Dead;
    private float StartingHealth;
    

    private void Awake()
    {
        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        ExplosionAudio = ExplosionParticles.GetComponent<AudioSource>();
        StartingHealth = GetComponent<Stats>().Health;
        ExplosionParticles.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CurrentHealth = StartingHealth;
        Dead = false;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0 && !Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        EnemyCount--;
        Dead = true;
        Destroy(gameObject);
        ExplosionParticles.transform.position = transform.position;
        ExplosionParticles.gameObject.SetActive(true);
        ExplosionParticles.Play();
        ExplosionAudio.Play();

    }
}
