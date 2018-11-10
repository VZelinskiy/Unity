using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{          
    public Slider Slider;                        
    public Image FillImage;                      
    public Color FullHealthColor = Color.green;  
    public Color ZeroHealthColor = Color.red;    
    public GameObject ExplosionPrefab;
    public static int LifeCount = 1;
    public float CurrentHealth = 10;
    public static bool Dead;

    private AudioSource ExplosionAudio;          
    private ParticleSystem ExplosionParticles;            

    private void Awake()
    {
        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        ExplosionAudio = ExplosionParticles.GetComponent<AudioSource>();
        CurrentHealth = GetComponent<Stats>().Health;
        ExplosionParticles.gameObject.SetActive(false);

    }


    private void OnEnable()
    {
        
        Dead = false;

        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        SetHealthUI();

        if (CurrentHealth <= 0 && !Dead)
        {
            OnDeath();
        }
    }


    public void SetHealthUI()
    {
        Slider.value = CurrentHealth;
        float fullHealth = GetComponent<Stats>().Health;

        FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, CurrentHealth / fullHealth);
    }


    private void OnDeath()
    {
        Dead = true;
        Destroy(gameObject);
        LifeCount--;

        ExplosionParticles.transform.position = transform.position;
        ExplosionParticles.gameObject.SetActive(true);
        ExplosionParticles.Play();
        ExplosionAudio.Play();

    }
}