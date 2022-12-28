using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : StateMachineBehaviour
{
    public float reloadTime = 0.7f;
    private bool reloaded = false;





	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (reloaded)
			return;
		if (stateInfo.normalizedTime >= reloadTime)
		{
			animator.GetComponent<Weapon>().Reload();
			reloaded = true;
		}
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		reloaded = false;
	}




}
