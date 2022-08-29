using System.Collections;
using UnityEngine;

namespace Pathfinding.Examples {
	/// <summary>Example script used in the example scenes</summary>
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_door_controller.php")]
	public class DoorController : MonoBehaviour {
		[SerializeField] private bool open = false;

		public int opentag = 1;
		public int closedtag = 1;
		public bool updateGraphsWithGUO = true;
		public float yOffset = 5;

		Bounds bounds;

		public void Start () {
			// Capture the bounds of the collider while it is closed
			bounds = GetComponent<Collider>().bounds;

			// Initially open the door
			SetState(open);
		}

		void OnGUI () {
			// Show a UI button for opening and closing the door
			if (GUI.Button(new Rect(5, yOffset, 100, 22), "Toggle Door")) {
				SetState(!open);
			}
		}

		public void SetState (bool open) {
			this.open = open;

			if (updateGraphsWithGUO) {
				// Update the graph below the door
				// Set the tag of the nodes below the door
				// To something indicating that the door is open or closed
				GraphUpdateObject guo = new GraphUpdateObject(bounds);
				guo.updatePhysics = false;

				//AstarPath.active.UpdateGraphs(guo);
				
				//AstarPath.active.Scan();

			}

			// Play door animations
			if (open) {
				GetComponent<Animation>().Play("Open");
			} else {
				GetComponent<Animation>().Play("Close");
			}
			StartCoroutine(OpenDoor());
			//AstarPath.active.UpdateGraphs(bounds);
		}
		private IEnumerator OpenDoor()
		{
			yield return new WaitForSeconds(0.5f);
			AstarPath.active.UpdateGraphs(bounds);
		}
	}
}
