using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACentipede : MonoBehaviour
{
    [SerializeField]
    private CentipedeController cuca;

    [SerializeField, ReadOnly]
    private int state = 0;

    [SerializeField]
    private FrequencyCoolDown timeGettingAngry;
    [SerializeField]
    private FrequencyTimer frequency;

    private bool isPasive = true;
    public bool isGettingAngry = false;

    /// <summary>
    /// test if centipede have to get
    /// </summary>
    private void TestToSetAttacking()
    {
        if (isPasive && cuca.isAttacking == true)
        {
            //ici on passe en état ANGRY
            isPasive = false;
            isGettingAngry = true;
            timeGettingAngry.StartCoolDown();
        }
    }

    private void TestToAttack()
    {
        if (!isPasive && isGettingAngry && timeGettingAngry.IsStartedAndOver())
        {
            isGettingAngry = false;
            //ICI passe en état ATTACK
        }
    }

    public void Init()
    {
        state = 0;
        isPasive = true;
        isGettingAngry = false;
        timeGettingAngry.Reset();
    }

    /// <summary>
    /// STATE MACHINE FOR CUCA AI
    /// </summary>
    public void Machine()
    {
        Vector3 vectDir;
        Vector2 stopVect;

        TestToSetAttacking();
        TestToAttack();

        float x, y;

        int randInt = 0;

        if (cuca.IsDying)
        {
            return;
        }
           
        switch (state)
        {

            // IDLE
            //===---
            case 0:         //Don't move
                {
                    stopVect = new Vector2(cuca.rb.transform.forward.x, cuca.rb.transform.forward.z);
                    cuca.ChangeDirectionIA(new Vector2(stopVect.x*0.0001f, stopVect.y * 0.0001f));

                    if (cuca.isAttacking)
                    {
                        state = 2;
                        break;
                    }
                    else
                    {
                        randInt = Random.Range(0,5);

                        if (randInt == 1)
                        {
                            state = 1;
                            break;
                        }
                    }
                    break;
                }

            case 1:         //Crawl
                {                        
                    stopVect = new Vector2(cuca.rb.transform.forward.x, cuca.rb.transform.forward.z);
                    x = ExtRandom.GenerateNormalRandom(0.0f, 0.5f);
                    y = ExtRandom.GenerateNormalRandom(0.0f, 0.5f);


                    if (Random.Range(0, 100) == 1)
                    {
                        cuca.ChangeDirectionIA(new Vector2(stopVect.x + x + 5.0f, stopVect.y + y + 5.0f));
                    }
                    else
                    {
                        cuca.ChangeDirectionIA(new Vector2(stopVect.x + x, stopVect.y + y));
                    }

                    if (cuca.isAttacking)
                    {
                        state = 2;
                        break;
                    }

                    state = 0;
                    break;
                        
                }

            case 2: // Forward food
                {

                    GameObject target = cuca.refToFollow;
                    if (!target)
                    {
                        state = 0;
                        break;
                    }

                        
                    vectDir = -(cuca.rb.transform.position - target.transform.position); // Create vector for direction
                    vectDir.Normalize();

                    cuca.ChangeDirectionIA(new Vector2(vectDir.x * 1, vectDir.z * 1)); // change dir

                    if (!cuca.isAttacking)
                    {
                        state = 0;
                        break;
                    }
                    else
                    {
                        randInt = Random.Range(0, 2);
                        if (randInt == 1)
                        {
                            state = 3;
                            break;
                        }
                    }
                    break;
                }

            case 3: // Forward food with variations in direction
                {

                    GameObject target = cuca.refToFollow;
                    if (!target)
                    {
                        state = 0;
                        break;
                    }

                    vectDir = -(cuca.rb.transform.position - target.transform.position);
                    vectDir.Normalize();
                    x = ExtRandom.GenerateNormalRandom(0.0f, 0.3f);
                    y = ExtRandom.GenerateNormalRandom(0.0f, 0.3f);

                    cuca.ChangeDirectionIA(new Vector2((vectDir.x + x) * 1, (vectDir.y + y) * 1));

                    if (!cuca.isAttacking)
                    {
                        state = 0;
                        break;
                    }
                    else
                    {                        
                        state = 2;
                        break;
                    }

                }
        }
    }

    /// <summary>
    /// change IA at every frame
    /// </summary>
    private void Update()
    {
        if (frequency.Ready())
        {
            Machine();
        }
    }
}


