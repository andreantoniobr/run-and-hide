using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFieldOfView : MonoBehaviour
{
	[SerializeField] private float viewRadius;
	[Range(0, 360)]
	[SerializeField] private float viewAngle;
	[SerializeField] private float delay = .2f;

	[SerializeField] private LayerMask targetMask;
	[SerializeField] private LayerMask obstacleMask;

	[SerializeField] private List<Transform> visibleTargets = new List<Transform>();

	public float ViewRadius => viewRadius;
	public float ViewAngle => viewAngle;
	public List<Transform> VisibleTargets => visibleTargets;

	private void Start()
	{
		StartCoroutine(FindTargetsWithDelay());
	}

	private IEnumerator FindTargetsWithDelay()
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	private void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float distanceToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
				{
					visibleTargets.Add(target);
				}
			}
		}
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
