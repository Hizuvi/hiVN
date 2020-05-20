using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Control/Change variable")]
	public class ChangeVariableNode : BaseNode
	{

		[Input(ShowBackingValue.Never)] public bool prev;
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;

		public string key;
		[Tooltip("Value that the key will be set to")]
		public bool value;

		// Use this for initialization
		protected override void Init()
		{
			base.Init();
		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}

		public override void Continue()
		{
			DialogueGraph dialogueGraph = (DialogueGraph)graph;
			dialogueGraph.GetMaster().SetVar(key, value);

			base.Continue();
		}
	}
}