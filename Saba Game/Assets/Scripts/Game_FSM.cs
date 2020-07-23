using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

public class Game_FSM : MonoBehaviour
{

    public enum Player_State { IDLE, RUNNING, CHASE, DEAD, GAMEOVER };
    [SerializeField]
    private Player_State currentState;
   
    private Moving mov;
    
    public Player_State CurrentState
    {
        get
        {
            return currentState;

        }
        set
        {

            currentState = value;
            StopAllCoroutines();
            switch (currentState)
            {
                case Player_State.IDLE:
                    StartCoroutine(EnemyIdle(true));
                    break;
                case Player_State.RUNNING:
                    StartCoroutine(EnemyChasing(true));
                    break;
                case Player_State.CHASE:
                    StartCoroutine(EnemyChasing(true));
                    break;
                case Player_State.DEAD:
                    StartCoroutine(EnemyDead());
                    break;
            }

        }

    }

    public IEnumerator EnemyIdle(bool tick)
    {
        while (currentState == Player_State.IDLE)
        {


            while (tick)
            {
                if (mov.run)
                {
                    Debug.Log("in loop");
                    tick = false;
                    CurrentState = Player_State.RUNNING;
                }
            }

            yield return null;
        }
    }

    public IEnumerator EnemyChasing(bool tick)
    {
        while (currentState == Player_State.RUNNING)
        {
            while (tick)
            {
                if (mov.collide)
                {
                    if (mov.health > 0)
                    {
                        tick = false;
                        mov.pos = true;

                        CurrentState = Player_State.DEAD;
                    }

                    else
                    {
                        tick = false;
                        mov.pos = true;

                        CurrentState = Player_State.GAMEOVER;
                    }

                }
            }
        }
        yield return null;
    }

    public IEnumerator EnemyDead()
    {

        yield return new WaitForSeconds(6f);
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = Player_State.IDLE;
        mov = GetComponent<Moving>();

    }

    // Update is called once per frame
    void Update()
    {
     
        
    }
}
