using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorControllerConstants
{
	public const string IsRunning = "IsRunning";
	public const string IsAttacking = "IsAttacking";
}

public class CharacterAnimatorControllerDancingConstants
{
	public const string IsDancingRumba = "IsDancingRumba";
	public const string IsDancingSilly = "IsDancingSilly";
}

[RequireComponent(typeof(CharacterMovementController))]
public class CharacterAnimatorController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	
	private CharacterMovementController characterMovementController;
	
	private List<string> dancingAnimations = new List<string>()
		{
			CharacterAnimatorControllerDancingConstants.IsDancingRumba,
			CharacterAnimatorControllerDancingConstants.IsDancingSilly,
		};

	private string dancingAnimationConstant;

	private void Awake()
	{
		characterMovementController = GetComponent<CharacterMovementController>();
	}

    private void Start()
    {
		GetRandomDancingConstant(out dancingAnimationConstant);
	}

    private void LateUpdate()
	{
        if (animator)
        {
			if (characterMovementController)
			{
				animator.SetBool(CharacterAnimatorControllerConstants.IsRunning, characterMovementController.IsRunning);
				//animator.SetBool(dancingAnimationConstant, characterMovementController.IsDancing);
			}
		}		
	}

	private bool GetRandomDancingConstant(out string dancingAnimationConstant)
    {
		bool canGetRandomDancingAnimationConstant = false;
		dancingAnimationConstant = CharacterAnimatorControllerDancingConstants.IsDancingRumba;
		int dancingAnimationsAmount = dancingAnimations.Count;
		if (dancingAnimationsAmount > 0)
        {
			int dancingAnimationRandomIndex = Random.Range(0, dancingAnimationsAmount);
			if (dancingAnimationRandomIndex >= 0 && dancingAnimationRandomIndex <= dancingAnimationsAmount - 1)
			{
				canGetRandomDancingAnimationConstant = true;
				dancingAnimationConstant = dancingAnimations[dancingAnimationRandomIndex];
			}
		}		
		return canGetRandomDancingAnimationConstant;
	}
}
