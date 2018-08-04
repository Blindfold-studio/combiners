﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Boss3ShortRangeAttack : MonoBehaviour {

	private bool isPlayerInRange;
    private Boss3Movement boss3Movement;
    private GameObject shortRangeWeapon;
    [SerializeField]
    private float animWaitTime = 1f;
    [SerializeField]
    private float waitAfterAttackTime = 0.5f;

    private void Start() {
        isPlayerInRange = false;
        boss3Movement = GetComponentInParent<Boss3Movement>();
        shortRangeWeapon = this.gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isPlayerInRange = false;
        }
    }

    private void Update() {
        
        if(isPlayerInRange && boss3Movement.CurrentState == Boss3Movement.State.Moving) {
            StartCoroutine("SlashPlayer");
        }
    }

    IEnumerator SlashPlayer() {
        //do something
        Animation anim = GetComponentInParent<Animation>();
        boss3Movement.CurrentState = Boss3Movement.State.IsShortRangeAttacking;
        Debug.Log(boss3Movement.TargetPlayer.name + " was attacked by sword!");
        yield return new WaitForSeconds(animWaitTime);
        //shortRangeWeapon.SetActive(true);
        GetComponentInParent<Animation>().Play("ShortAttack");
        Debug.Log("Sword attack clip length: " + anim["ShortAttack"].length);
        yield return new WaitForSeconds(anim["ShortAttack"].length);
        //shortRangeWeapon.SetActive(false);
        GetComponentInParent<Animation>().Stop("ShortAttack");
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
        Debug.Log("The sword attack stops!");
    }

    void StopAttack() {
        isPlayerInRange = false;
        StopCoroutine("SlashPlayer");
    }

    private void OnEnable()
    {
        Boss3Movement.StopCoroutineEvent += StopAttack;
    }

    private void OnDisable()
    {
        Boss3Movement.StopCoroutineEvent -= StopAttack;
    }
}
