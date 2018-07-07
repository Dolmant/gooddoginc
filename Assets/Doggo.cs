using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : Movement
{
    private Transform leash;
    private Human myHuman;
    private Boolean interrupted;
    private void Start()
    {
        leash = transform.Find("Leash");
    }
	
	// Update is called once per frame
	override protected void FixedUpdate ()
	{
        if (!ani.GetBool("fight") && !ani.GetBool("interact") && !ani.GetBool("bark")) {
            base.FixedUpdate();
        }
    }

    override protected void Update ()
	{
        base.Update();
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(0)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0f;
        }
    }

    public void Fight(Enemy enemy) {
        Debug.Log("Reaaaaddyyyyy... FIGHT");
        Destroy(enemy.gameObject);
        ani.SetBool("fight", true);
        StartCoroutine(FightAni());
    }

    public IEnumerator FightAni() {
        yield return new WaitForSeconds(1);
        ani.SetBool("fight", false);
    }
    public void InteractWithDoggo(PlaceOfInterest POI) {
        Debug.Log("Interact");
        ani.SetBool("interact", true);
        StartCoroutine(InteractWithDoggoAni(POI));
    }

    public IEnumerator InteractWithDoggoAni(PlaceOfInterest POI) {
        yield return new WaitForSeconds(1);
        if (!interrupted) {
            POI.Progress();
            ani.SetBool("interact", false);
        }
        interrupted = false;
    }
    public void DoggoBark() {
        ani.SetBool("bark", true);
        Debug.Log("Bark");
        StartCoroutine(DoggoBarkAni());
    }

    public IEnumerator DoggoBarkAni() {
        yield return new WaitForSeconds(1);
        ani.SetBool("bark", false);
    }
}

// In case we want raycasting later
// if (Input.GetMouseButton(0)) {
//     RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0);
//     if (hit) {
//         if (hit.collider.CompareTag("POI")) {
//                 Debug.Log("This is a POI");
//         }
//     }
//     // doggo.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//     // doggo.target.z = 0f;
// }