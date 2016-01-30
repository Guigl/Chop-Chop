using UnityEngine;
using System.Collections;

public class TreeLoad : MonoBehaviour {
	public TerrainData terr;
	public GameObject[] treeTypes;
	// replace the terraintrees with prefab trees
	void Start () {

		// generate new trees
		foreach (TreeInstance tree in terr.treeInstances) {
			int treetype = tree.prototypeIndex;
			Vector3 pos = new Vector3 (tree.position.x, tree.position.y, tree.position.z);
			Instantiate ((GameObject)treeTypes[treetype], Vector3.Scale(pos, terr.size), Quaternion.identity);
		}

		// remove the terraintrees
		TreeInstance[] temp = new TreeInstance[0];
		terr.treeInstances = temp;
	}

}
