using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{	
	public PathNode previousNode;
	public List<PathNode> nextNodes;
	
	void Start()
	{
		// Set up empty next/previous node
		if (nextNodes.Count == 0)
		{
			int i = transform.GetSiblingIndex();
			if (i < transform.parent.childCount - 1)
			{
				Transform nextT = transform.parent.GetChild(transform.GetSiblingIndex() + 1);
				if (nextT)
				{
					nextNodes.Add(nextT.GetComponent<PathNode>());
					nextNodes[0].previousNode = this;
				}
			}
		}

		if (!previousNode)
		{
			int i = transform.GetSiblingIndex();
			if (i > 0)
			{
				Transform prevT = transform.parent.GetChild(i - 1);
				if (prevT)
				{
					previousNode = prevT.GetComponent<PathNode>();
				}
			}
		}
	}

	public PathNode GetNextNode()
	{
		if (nextNodes.Count == 0)
			return null;
		
		if (nextNodes.Count == 1)
			return nextNodes[0];

		return nextNodes[UnityEngine.Random.Range(0, nextNodes.Count)];
	}
}
