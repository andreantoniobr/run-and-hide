using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimatorController : MonoBehaviour
{    
    [SerializeField] private Animator animator;

    private PlayerController playerController;

    private void Awake()
    {
        if (!playerController)
        {
            playerController = GetComponent<PlayerController>();
        }        
    }

    private void Update()
    {
        if (playerController)
        {
            OnRun(playerController.IsRunning);
        }
    }

    private void OnRun(bool isRunning)
    {
        if (animator && playerController)
        {
            animator.SetBool(PlayerAnimatorConstants.IsRunning, isRunning);
        }
    }
}
