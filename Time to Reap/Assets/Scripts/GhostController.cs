using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
	private Animator anim;
	
	private Vector2 currentGoal;
	private Vector2 startPosition;
	private float moveSpeed = 2f;
		
    // Start is called before the first frame update
    void Start()
    {
			anim = GetComponent<Animator>();
			startPosition = transform.position;
			currentGoal = startPosition;
			GameManager.instance.CoupleEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.TIME_STATE_FROZEN) {
			if (Vector2.Distance(currentGoal, transform.position) < 0.1f) {
					//Get new position
					currentGoal.x = Random.Range(startPosition.x - 2.5f, startPosition.x + 2.5f);
					currentGoal.y = Random.Range(startPosition.y - 2.5f, startPosition.y + 2.5f);

			}
			
			transform.position = Vector2.MoveTowards(transform.position, currentGoal, Time.deltaTime * moveSpeed);
		}
    }
	
	public void notifyAnimChange(bool val) {
		if (anim != null)
			anim.SetBool("timeFrozen", val);
	}
}
