using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public float Speed = 8f;
    public float DirectionSwitcherTime = 3;
    public float DirectionSwitcherTimer = 3;
    public float MoveSwitcherTime = 1;
    public float MoveSwitcherTimer = 1;

    private Rigidbody Rigidbody;
    private TankShooting Shooting;
    private Quaternion InitialRot;
    private Transform Transform;
    private int IsMove;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Shooting = GetComponent<TankShooting>();
        Transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        Rigidbody.isKinematic = false;
    }


    private void OnDisable()
    {
        Rigidbody.isKinematic = true;
    }

    // Use this for initialization
    void Start () {
        Shooting.LastAttackTime = 0;
        InitialRot = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if ((Time.time - Shooting.LastAttackTime) >= Random.Range(Shooting.AttackSpeed, (Shooting.AttackSpeed + 0.1f)))
        {
            Shooting.Fire(Transform);
        }

        MoveLogic();
    }

    public void Move()
    {
           Vector3 movement = transform.forward * Speed * Time.deltaTime;
           Rigidbody.MovePosition(Rigidbody.position + movement);
    }

    public void Turn()
    {
        transform.rotation = InitialRot;
        int turnValue = Random.Range(0, 361);
        transform.Rotate(new Vector3(0, turnValue, 0));
    }

    public void MoveLogic()
    {
        MoveSwitcherTimer -= Time.deltaTime;

        if (MoveSwitcherTimer <= 0)
        {
            MoveSwitcherTimer = Random.Range((MoveSwitcherTime-1) ,MoveSwitcherTime);
            IsMove = Random.Range(0, 2);
        }

        if (IsMove == 1)
        {
            Move();
        }

        DirectionSwitcherTimer -= Time.deltaTime;

        if (DirectionSwitcherTimer <= 0)
        {
            DirectionSwitcherTimer = Random.Range((DirectionSwitcherTime-1) ,DirectionSwitcherTime);
            Turn();
        }
    }
}
