using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Class : MonoBehaviour
{

    public enum Player_State { IDLE, RUNNING, DEAD, GAMEOVER };
    [SerializeField]
    private Player_State currentState;
    private Moving mov;
    void Start()
    {
        mov = GetComponent<Moving>();
        CurrentState = Player_State.IDLE;
    }

    public Player_State CurrentState
    {
        get
        { return currentState; }
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
                    StartCoroutine(EnemyRunning(true));
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
        { while (tick)
            {
                if (mov.run)
                {
                    Debug.Log("in loop");
                    tick = false;
                    break;
                    CurrentState = Player_State.RUNNING;
                }
            }

            yield return null;
        }
    }
    public IEnumerator EnemyRunning(bool tick)
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
                        break;
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
        if (mov.health <= 0)
        {
            
            mov.pos = true;
            CurrentState = Player_State.GAMEOVER;
        }

        yield return new WaitForSeconds(6f);
    }

    public IEnumerator EnemyGameOver()
    {
        Destroy(gameObject);
        yield return null;
    }
    // Start is called before the first frame update

    void Update()
    {


    }
    // Update is called once per frame
   
}