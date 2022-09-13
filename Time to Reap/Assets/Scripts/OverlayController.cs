using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
	private bool lastState;
	private SpriteRenderer spriteRenderer;
	
	private static float red = 44f/255f;
	private static float green = 231f/255f;
	private static float blue = 205f/255f;
	private static float alphaShow = 50f/255f;
	private static float alphaHide = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        lastState = true;
		spriteRenderer = GetComponent<SpriteRenderer>();
		//44, 231, 205, 111 - MAGIC NUMBERS, NOT GOOD
		spriteRenderer.color = new Color(red, green, blue, alphaShow);

    }

    // Update is called once per frame
    void Update()
    {
		if (GameManager.TIME_STATE_FROZEN) {
			//if lastState is false then we just switched time state, show overlay
			if (lastState != true) {
				lastState = GameManager.TIME_STATE_FROZEN;
				//44, 231, 205, 111 - MAGIC NUMBERS, NOT GOOD
				spriteRenderer.color = new Color(red, green, blue, alphaShow);
			}
		}
		else {
			//if lastState is true, we switched time state, hide overlay
			if (lastState == true) {
				lastState = GameManager.TIME_STATE_FROZEN;
				spriteRenderer.color = new Color(red, green, blue, alphaHide);
			}
		}
    }
}
