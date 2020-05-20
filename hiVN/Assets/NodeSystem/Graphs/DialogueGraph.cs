using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	public class DialogueGraph : NodeGraph
	{

		[HideInInspector]
		public MasterGraph masterGraph;
		[HideInInspector]
		public BaseNode atNode; //Node which we are at

		//Start the graph by setting the atNode to the startNode
		public virtual void StartGraph()
		{
			int startNodes = 0; //Make shure there is not more than one startNode
			foreach (BaseNode node in nodes)
			{
				if (node.name == "Start")
				{
					atNode = node;
					startNodes++;
				}
			}
			if (startNodes > 1)
			{
				Debug.LogError("More than one StartNode in : " + name);
			}

			if (atNode == null) { Debug.LogError("Missing start node in : " + name); }

			Debug.Log("Start dialogue : " + name);
		}

		//activate the continue function in the block
		public void Continue()
		{
			BaseNode node = (BaseNode)atNode;
			BaseNode prevNode = atNode; //used to check if the atNode variable has changed
			node.Continue();
			if (prevNode != atNode)
			{
				atNode.OnAtNode();
			}
		}

		//returns data from the current node
		public object Read()
		{
			return atNode.Read();
		}


		//used when reaching an end node
		public void End()
		{
			Debug.Log("End dialogue : " + name);
		}

		public virtual MasterGraph GetMaster()
		{
			return masterGraph;
		}
	}
}