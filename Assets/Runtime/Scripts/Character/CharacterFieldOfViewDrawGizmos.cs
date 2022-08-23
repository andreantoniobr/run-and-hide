using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(CharacterFieldOfView))]
public class CharacterFieldOfViewDrawGizmos : MonoBehaviour
{
    private CharacterFieldOfView characterFieldOfView;
    private void Awake()
    {
        characterFieldOfView = GetComponent<CharacterFieldOfView>();
    }

    private void OnDrawGizmos()
    {
        if (characterFieldOfView)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, characterFieldOfView.ViewRadius);

            Vector3 viewAngleA = characterFieldOfView.DirFromAngle(-characterFieldOfView.ViewAngle / 2, false);
            Vector3 viewAngleB = characterFieldOfView.DirFromAngle(characterFieldOfView.ViewAngle / 2, false);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * characterFieldOfView.ViewRadius);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * characterFieldOfView.ViewRadius);
        }
    }    
}
