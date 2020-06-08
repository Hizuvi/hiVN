using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Control/Reset values")]
	public class ResetVariablesNode : BaseNode
	{

		[Input(ShowBackingValue.Never)] public bool prev;
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;

		public override void Continue()
		{
			DialogueGraph dialogueGraph = (DialogueGraph)graph;
			dialogueGraph.GetMaster().Reset();

			base.Continue();
		}
	}
}