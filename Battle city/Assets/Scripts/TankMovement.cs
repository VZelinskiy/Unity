using UnityEngine;

public class TankMovement : MonoBehaviour
{        
    public float Speed = 8f;            
    public float TurnSpeed = 180f;       
    public AudioSource MovementAudio;    
    public AudioClip EngineIdling;       
    public AudioClip EngineDriving;      
    public float PitchRange = 0.2f;

    
    private string MovementAxisName;     
    private string TurnAxisName;         
    private Rigidbody Rigidbody;         
    private float MovementInputValue;    
    private float TurnInputValue;        
    private float OriginalPitch;
    private TankShooting Shooting;
    private string FireButton;
    private Transform Transform;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Shooting = GetComponent<TankShooting>();
        Transform = GetComponent<Transform>();
    }


    private void OnEnable ()
    {
        Rigidbody.isKinematic = false;
        MovementInputValue = 0f;
        TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        MovementAxisName = "Vertical";
        TurnAxisName = "Horizontal";
        FireButton = "Fire1";
        Shooting.LastAttackTime = 0;
        
        OriginalPitch = MovementAudio.pitch;
    }
    

    private void Update()
    {
        MovementInputValue = Input.GetAxis(MovementAxisName);
        TurnInputValue = Input.GetAxis(TurnAxisName);
        EngineAudio();

        if (Input.GetButton(FireButton) && ((Time.time - Shooting.LastAttackTime) >= Shooting.AttackSpeed))
        {
            Shooting.Fire(Transform);
        }
    }


    private void EngineAudio()
    {
        if (Mathf.Abs(MovementInputValue) < 0.1f && Mathf.Abs(TurnInputValue) < 0.1f)
        {
            if (MovementAudio.clip == EngineDriving)
            {
                MovementAudio.clip = EngineIdling;
                MovementAudio.pitch = Random.Range(OriginalPitch - PitchRange, OriginalPitch + PitchRange);
                MovementAudio.Play();
            }
        }
        else
        {
            if (MovementAudio.clip == EngineIdling)
            {
                MovementAudio.clip = EngineDriving;
                MovementAudio.pitch = Random.Range(OriginalPitch - PitchRange, OriginalPitch + PitchRange);
                MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        Move();
        Turn();
    }


    private void Move()
    {
        Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + movement);
    }


    private void Turn()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float turn = 360 - (TurnInputValue * TurnSpeed * Time.deltaTime);
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
        }
        else
        {
            float turn = TurnInputValue * TurnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);    
        }
        
    }
}