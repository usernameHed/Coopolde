﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEntity : MonoBehaviour
{
    [SerializeField]
    private int hurtAmount = 1;
    [SerializeField]
    private bool hurtOnlyPlayer = false;
    [SerializeField]
    private bool isPlayer = false;

    private bool isSomethingInside = false;

    private bool playingAttackSong = false;

    [SerializeField]
    private FrequencyCoolDown coolDown;
    [SerializeField]
    private FrequencyCoolDown timeAfterStopAttacking;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        coolDown.Reset();
        isSomethingInside = false;
        playingAttackSong = false;
    }

    private void Hurt(GameObject other)
    {
        LifeEntity life = other.GetComponent<LifeEntity>();
        if (life)
        {

            isSomethingInside = true;

            timeAfterStopAttacking.StartCoolDown();

            if (!life.isPlayer && hurtOnlyPlayer)
                return;


            //ici on a déja tapé...
            if (!coolDown.IsReady())
                return;

            //ici on tape
            coolDown.StartCoolDown();
            life.GetHit(hurtAmount);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Hurt.ToString()))
        {
            Hurt(other.gameObject);
        }
    }

    private void PlayerAttack()
    {
        if (!isPlayer)
            return;

        if (!playingAttackSong && isSomethingInside)
        {
            Debug.Log("play sound attack");
            playingAttackSong = true;
        }
        else if (!isSomethingInside && playingAttackSong)
        {
            Debug.Log("stop sound attack");
            playingAttackSong = false;
        }
    }

    private void Update()
    {
        PlayerAttack();

        if (timeAfterStopAttacking.IsStartedAndOver())
        {
            isSomethingInside = false;
        }
    }

}
