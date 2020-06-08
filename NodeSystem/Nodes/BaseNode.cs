using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;



namespace hiVN
{
	[CreateNodeMenu("")]
	public class BaseNode : Node
	{

		//When it should proceed to the next node
		public virtual void Continue()
		{
			//set atNode to the next node
			DialogueGraph dialogueGraph = (DialogueGraph)graph;
			dialogueGraph.atNode = GetNext();
		}

		//Read the data in the node
		public virtual object Read()
		{
			return null;
		}

		//Runs when the node gets set as the atNode 
		public virtual void OnAtNode()
		{

		}

		//get the node connected to the output "next" port
		public BaseNode GetNext()
		{
			if (GetOutputPort("next").Connection == null)
			{
				Debug.LogError("Untied end in : " + graph.name);

				DialogueGraph dialogueGraph = (DialogueGraph)graph;
				return dialogueGraph.atNode;
			}
			if (GetOutputPort("next").ConnectionCount > 1)
			{
				Debug.LogWarning("Multiple next's in : " + graph.name);
			}

			return (BaseNode)GetOutputPort("next").Connection.node;
		}
	}
}