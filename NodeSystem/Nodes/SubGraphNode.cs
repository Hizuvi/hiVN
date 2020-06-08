using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Control/SubGraph")]
	public class SubGraphNode : BaseNode
	{

		[Input(ShowBackingValue.Never)] public bool prev;
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;
		public SubGraph subGraph;



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

		//when read then send go to the subgraph and run read
		public override object Read()
		{
			if (subGraph.Read() is string)
			{
				if ((string)subGraph.Read() == "__EndNode")
				{
					base.Continue();

					return null;
				}
			}

			if (subGraph == null)
			{
				Debug.LogError("Missing subGraph in : " + graph.name);
				return null;
			}

			return subGraph.Read();
		}

		public override void Continue()
		{
			subGraph.Continue();
		}

		public override void OnAtNode()
		{
			if (graph is MasterGraph)
			{
				subGraph.masterGraph = (MasterGraph)graph;
			}
			else
			{
				DialogueGraph dialogueGraph = (DialogueGraph)graph;
				subGraph.masterGraph = dialogueGraph.masterGraph;
			}
			subGraph.StartGraph();
		}
	}
}