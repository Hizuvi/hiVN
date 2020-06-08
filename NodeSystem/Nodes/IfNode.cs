using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Control/If")]
	public class IfNode : BaseNode
	{

		[Input(ShowBackingValue.Never)] public bool prev;
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool ifTrue;
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool ifFalse;

		public CheckData[] variables;

		public override void Continue()
		{
			DialogueGraph dialogueGraph = (DialogueGraph)graph;
			bool condition = true;
			foreach (CheckData check in variables)
			{
				if (check.not == dialogueGraph.GetMaster().GetVar(check.key))
				{
					condition = false;
				}//loop through all of the check and if any one of them are false then set condition to false. If condition is set to false then the it will continue to the ifFalse connection
			}

			if (GetOutputPort("ifTrue").Connection == null || GetOutputPort("ifFalse").Connection == null)
			{
				Debug.LogError("Untied end in : " + graph.name);
			}
			if (GetOutputPort("ifTrue").ConnectionCount > 1 || GetOutputPort("ifFalse").ConnectionCount > 1)
			{
				Debug.LogWarning("Multiple next's in : " + graph.name);
			}

			if (condition)
			{
				dialogueGraph.atNode = (BaseNode)GetOutputPort("ifTrue").Connection.node;
			}
			else
			{
				dialogueGraph.atNode = (BaseNode)GetOutputPort("ifFalse").Connection.node;
			}
		}


		[System.Serializable]
		public class CheckData
		{
			public string key;
			public bool not;
		}
	}
}