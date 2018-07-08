using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doggo : Movement
{
    private bool interrupted;
    public bool BARK;
    public bool INTERACT;
    public int MaxPee = 100;
    public int CurrentPee = 100;

    public Slider PeeMeter;
    public Int64 MaxGoodBoy = 10000;
    public Int64 CurrentGoodBoy = 10000;

    public Slider GoodBoyMeter;

    override protected void Start () {
        base.Start();
        SetPee(CurrentPee);
    }
    override protected void Update () {
        base.Update();
        HandleInput();
    }

    override protected void FixedUpdate () {
        base.FixedUpdate();
        HandleGoodBoy(CurrentGoodBoy - 1);
    }

    void HandleGoodBoy(Int64 value) {
        if (value <= 0) {
            // Game End
        }
        CurrentGoodBoy = value;
        GoodBoyMeter.value = (float)CurrentGoodBoy / MaxGoodBoy;
    }

    void HandleInput() {
        if (!ani.GetBool("fight") && !ani.GetBool("interact") && !ani.GetBool("bark") && Input.GetMouseButton(0)) {
            goingForTarget = true;
            var selfClick = false;
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit2D hit = Physics2D.Raycast(
                    new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y), 
                    Vector2.zero, 
                    0);
                if (hit) {
                    Debug.Log("hit");
                    if (hit.collider.CompareTag("Player")) {
                        selfClick = true;
                        DoggoBark();
                    }
                }
            }
            if (!selfClick) {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0f;
            }
        }
    }

    public void Fight(Enemy enemy) {
        Debug.Log("Reaaaaddyyyyy... FIGHT");
        interrupted = true;
        goingForTarget = false;
        rb.velocity = Vector3.zero;
        target = transform.position;
        Destroy(enemy.gameObject);
        ani.SetBool("fight", true);
        StartCoroutine(FightAni());
    }

    public IEnumerator FightAni() {
        goingForTarget = true;
        yield return new WaitForSeconds(2);
        ani.SetBool("fight", false);
        interrupted = false;
    }

    void SetPee(int value) {
        CurrentPee = value;
        PeeMeter.value = (float)CurrentPee / MaxPee;
    }
    
    public void InteractWithDoggo(PlaceOfInterest POI) {
        if (CurrentPee >= 20) {
            SetPee(CurrentPee - 20);
            interrupted = false;
            INTERACT = true;
            goingForTarget = false;
            rb.velocity = Vector3.zero;
            Debug.Log("Interact");
            ani.SetBool("interact", true);
            StartCoroutine(InteractWithDoggoAni(POI));
            StartCoroutine(InteractionTimer());
        }
    }

    public IEnumerator InteractWithDoggoAni(PlaceOfInterest POI) {
        yield return new WaitForSeconds(2);
        if (!interrupted) {
            POI.Progress();
            ani.SetBool("interact", false);
        }
        goingForTarget = true;
        interrupted = false;
    }

    public IEnumerator InteractionTimer() {
        yield return new WaitForSeconds(4);
        INTERACT = false;
    }
    
    public void DoggoBark() {
        ani.SetBool("bark", true);
        rb.velocity = Vector3.zero;
        BARK = true;
        goingForTarget = false;
        Debug.Log("Bark");
        StartCoroutine(DoggoBarkAni());
    }

    public IEnumerator DoggoBarkAni() {
        yield return new WaitForSeconds(2);
        ani.SetBool("bark", false);
        goingForTarget = true;
        BARK = false;
    }
}
