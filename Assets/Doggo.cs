using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Loadable {
   public int MaxPee = 100;
   public int Score = 0;
   public int MaxGoodBoy = 1000;
}

public class Doggo : Movement
{
    public UIAnimation uiTrigger;
    public bool Walkies = true;
    private bool interrupted;
    public bool BARK;
    public bool INTERACT;
    public int CurrentPee = 100;
    public Loadable loadable;
    private GameObject[] finishObjects;
    public Human human;
    public Animator loveHuman;
    public Text ScoreText;
    public Text FinishScoreText;
    public Slider PeeMeter;
    public Slider GoodBoyMeter;
    public int CurrentGoodBoy = 1000;
    public Leash leash;

    override protected void Start () {
        base.Start();
        finishObjects = GameObject.FindGameObjectsWithTag("finishMenu");
        hideFinished();
        SetPee(CurrentPee);
        loadable = Serializer.Load<Loadable>("gamedata");
        if (loadable == null) {
            loadable = new Loadable();
        }
        if (loadable.Score > 0) {
            CurrentGoodBoy = loadable.MaxGoodBoy;
            CurrentPee = loadable.MaxPee;
        }

        GoodBoyMeter.value = 1;
    }

    override protected void Update () {
        base.Update();
        HandleInput();
        ScoreText.text = loadable.Score.ToString();
    }

    override protected void FixedUpdate () {
        base.FixedUpdate();
        if (Vector3.Distance(transform.position, human.transform.position) < 1) {
            loveHuman.SetBool("love", true);
            HandleGoodBoy(CurrentGoodBoy + 2);
            uiTrigger.SpawnText("+Good Boy", new Color(0.07226842f, 0.4339623f, 0f, 1f));
        } else {
            loveHuman.SetBool("love", false);
            HandleGoodBoy(CurrentGoodBoy - 1);
        }
    }

    void HandleGoodBoy(int value) {
//        if (value <= 0) {
//            EndWalk();
//        }
        if (!Walkies)
            return;
        
        CurrentGoodBoy = value;
        GoodBoyMeter.value = (float)CurrentGoodBoy / loadable.MaxGoodBoy;
        
        // Set leash length based on good boy meter
        leash.SetLengthPercent(GoodBoyMeter.value);
    }

    public void EndWalk()
    {
        Walkies = false;
        Time.timeScale = 0;
        FinishScoreText.text = ScoreText.text;
        showFinished();
        Serializer.Save<Loadable>("gamedata", loadable);
    }
    
    public void hideFinished(){
		foreach(GameObject g in finishObjects){
			g.SetActive(false);
		}
	}
    
    public void showFinished(){
		foreach(GameObject g in finishObjects){
			g.SetActive(true);
		}
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
                    if (hit.collider.CompareTag("POI")) {
                        hit.GetType();
                        if (!INTERACT && (hit.transform.position - transform.position).magnitude < 0.5) {
                            selfClick = true;
                            InteractWithDoggo(hit);
                        }
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
        ani.SetBool("fight", true);
        StartCoroutine(FightAni(enemy));
    }

    public IEnumerator FightAni(Enemy enemy) {
        goingForTarget = true;
        yield return new WaitForSeconds(2);
        enemy.Die();
        ani.SetBool("fight", false);
        interrupted = false;
    }

    void SetPee(int value) {
        CurrentPee = value;
        PeeMeter.value = (float)CurrentPee / loadable.MaxPee;
    }
    
    public void InteractWithDoggo(RaycastHit2D POIHit) {
        if (CurrentPee >= 20) {
            SetPee(CurrentPee - 20);
            interrupted = false;
            INTERACT = true;
            goingForTarget = false;
            rb.velocity = Vector3.zero;
            Debug.Log("Interact");
            ani.SetBool("interact", true);
            StartCoroutine(InteractWithDoggoAni(POIHit));
            StartCoroutine(InteractionTimer());
        }
    }

    public IEnumerator InteractWithDoggoAni(RaycastHit2D POIHit) {
        yield return new WaitForSeconds(2);
        if (!interrupted) {
            POIHit.collider.gameObject.SendMessage("Progress");
        }
        ani.SetBool("interact", false);
        goingForTarget = true;
        interrupted = false;
    }

    public IEnumerator InteractionTimer() {
        yield return new WaitForSeconds(4);
        INTERACT = false;
    }
    
    public void DoggoBark() {
        ani.SetBool("bark", true);
        human.Grumpy();
        HandleGoodBoy(CurrentGoodBoy - 100);
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
