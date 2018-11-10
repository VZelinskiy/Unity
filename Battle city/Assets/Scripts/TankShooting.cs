using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{       
    public Rigidbody Shell;            
    public Transform FireTransform;               
    public AudioSource ShootingAudio;      
    public AudioClip FireClip;         
    public float LaunchForce = 30;
    public float AttackSpeed = 0.5f;
    public float LastAttackTime;                                 

    public void Fire(Transform parent)
    {
        Rigidbody shallInstance = Instantiate(Shell, FireTransform.position, FireTransform.rotation, parent) as Rigidbody;
        shallInstance.velocity = LaunchForce * FireTransform.forward;

        ShootingAudio.clip = FireClip;
        ShootingAudio.Play();

        LastAttackTime = Time.time;
    }
}