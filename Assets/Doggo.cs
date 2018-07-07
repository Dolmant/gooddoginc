using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doggo : Movement
{
    private Transform leash;
    private Human myHuman;

    private void Start()
    {
        leash = transform.Find("Leash");
    }
	
	// Update is called once per frame
	override protected void FixedUpdate ()
	{
        if (!ani.GetBool("fight")) {
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
        Destroy(enemy.gameObject);
        ani.SetBool("fight", true);
        StartCoroutine(FightAni());
    }

    public IEnumerator FightAni() {
        yield return new WaitForSeconds(1);
        ani.SetBool("fight", false);
    }
}
