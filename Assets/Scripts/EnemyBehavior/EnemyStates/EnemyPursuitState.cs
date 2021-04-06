using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyPursuitState : State
{
    
    public Transform player;

    public float pursuitDistance = 1.5f;
    public float shootingDistance = 0.5f;

    public Vector2 followPoint;
    public Vector2 xCathetus;
    public Vector2 yCathetus;

    public bool followCompleted = false;
    public bool isCoroutineExist = false;

    public EnemyPursuitState(GameObject entity, StateMachine stateMachine) : base(entity, stateMachine)
    {

    }

  /*  public override void Enter()
    {
        *//*base.Enter();*/
        /*enemy = entity.GetComponent<Enemy>();
        player = GameManager.instance.player.transform;
        ChangePointPosition();
        //TODO: REMOVE ON RELEASE(ONLY FOR DEBUG)
        enemy.position = new Vector3(followPoint.x, followPoint.y, 5);
        enemy.xCathetus = xCathetus;
        enemy.yCathetus = yCathetus;*//*
    }*/

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        FollowOnEachAxis();
    }

    public virtual void ChangePointPosition()
    {
        /*if (Vector2.Distance(enemy.transform.position, player.position) <= pursuitDistance *//*&& *//*Vector2.Distance(enemy.transform.position, player.position) > shootingDistance*//*)
        {
            followPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        }
        else if (Vector2.Distance(enemy.transform.position, player.position) > pursuitDistance)
        {
            stateMachine.ChangeState(enemy.enemyPatrollingState);
        }
        followCompleted = false;*/





        /* else if (Vector2.Distance(enemy.transform.position, player.position) <= shootingDistance)
         {
             followPoint = new Vector2(player.transform.position.x, player.transform.position.y);
             stateMachine.ChangeState(enemy.enemyShootingState);
             *//* isPatroll = false;
              isPursuit = false;*//*

         }*/
        /*xCathetus = new Vector2(followPoint.x, enemy.transform.position.y);
        yCathetus = new Vector2(xCathetus.x, followPoint.y);*/

        //isXCathetusBigger = Vector2.Distance(enemy.position, xCathetus) > Vector2.Distance(enemy.position, yCathetus) ? true : false;
        //TODO: Only for DEBUG, REMOVE ON RELEASE





        /*enemy.position = new Vector3(followPoint.x, followPoint.y, 5);
        enemy.xCathetus = xCathetus;
        enemy.yCathetus = yCathetus;*/
    }

   

    public IEnumerator ChangeFollow()
    {
        if (isCoroutineExist) yield break;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        ChangePointPosition();
        isCoroutineExist = false;
    }

    public virtual void FollowOnEachAxis()
    {
         /*   if (!followCompleted && Vector2.Distance(enemy.transform.position, player.position) <= pursuitDistance)
            {
                followPoint = new Vector2(player.transform.position.x, player.transform.position.y);
                enemy.Move(followPoint, out followCompleted);
                return;
            }
            else if (Vector2.Distance(enemy.transform.position, player.position) < pursuitDistance)
            {
                ChangePointPosition();
            }
            else
            {
                enemy.StartCoroutine(ChangeFollow());
                isCoroutineExist = true;
            }*/





      /*  if (isShoot)
        {
            if (!followCompleted && Vector2.Distance(enemy.transform.position, player.position) <= shootingDistance)
            {
                followPoint = new Vector2(player.transform.position.x, player.transform.position.y);
                enemy.Move(followPoint, out followCompleted);
                return;
            }
            else
            {
                enemy.StartCoroutine(ChangeFollow());
                isCoroutineExist = true;
            }
        }*/
    }
}