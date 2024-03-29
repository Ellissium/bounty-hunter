﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public StateMachine state;
    public GroundedState grounding;
    public ShootingState shooting;

    [SerializeField] private FirePointPosition firePoint;
    [SerializeField] private GameObject bullet;
    private Rigidbody2D rbody;
    private Animator animator;
    

    Vector2 lastInputVector;
    private bool isDiagonal = false;
    private bool noDelayStarted = false;
    private float delay = 0.05f;

    public Animator CharacterAnimator { get { return animator; } }

    public void Move(Vector2 inputVector, float speed)
    {
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * speed;
        rbody.velocity = new Vector2(movement.x, movement.y);
        //rbody.MovePosition(rbody.position + movement * Time.fixedDeltaTime);
        if (inputVector.magnitude > 0.1f)
        {
            animator.Play("Run");
            if (inputVector.x != 0 && inputVector.y != 0)
            {
                isDiagonal = true;
                lastInputVector = new Vector2(inputVector.x, inputVector.y);
                animator.SetFloat("Horizontal", inputVector.x);
                animator.SetFloat("Vertical", inputVector.y);
                animator.SetFloat("Magnitude", inputVector.magnitude);
            }
            else
            {
                if (isDiagonal && !noDelayStarted)
                {
                    StartCoroutine(NoMoreDiagonal());
                    noDelayStarted = true;
                }
                else
                {
                    lastInputVector = new Vector2(inputVector.x, inputVector.y);
                    animator.SetFloat("Horizontal", inputVector.x);
                    animator.SetFloat("Vertical", inputVector.y);
                    animator.SetFloat("Magnitude", inputVector.magnitude);
                }
            }
        }
        else
        {
            animator.Play("Idle");
            animator.SetFloat("Horizontal", lastInputVector.x);
            animator.SetFloat("Vertical", lastInputVector.y);
            animator.SetFloat("Magnitude", 0);
        }
    }

    public void Shoot() 
    {
        ReceiveDamage(1);
        rbody.velocity = Vector2.zero;
        animator.Play("Shoot");
    }

    public void CreateBullet()
    {
        CharacterStats.instance.onShoot();
        firePoint.SetCurrentPosition(lastInputVector);
        Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Bullet>().SetBulletDirection(lastInputVector);
    }

    public void ReceiveDamage(int damageAmount)
    {
        CharacterStats.instance.HealthPoint = CharacterStats.instance.HealthPoint - damageAmount;
        CharacterStats.instance.onHeatlhChanged();
    }

    private IEnumerator NoMoreDiagonal()
    {
        yield return new WaitForSeconds(delay);
        isDiagonal = false;
        noDelayStarted = false;
    }

    private void Start()
    {
        state = new StateMachine();

        grounding = new GroundedState(gameObject, state);
        shooting = new ShootingState(gameObject, state);
        state.Initialize(grounding);
    }
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        state.CurrentState.HandleInput();

        state.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        state.CurrentState.PhysicsUpdate();
    }
}
