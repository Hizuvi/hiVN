using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Basic/End")]
	public class EndNode : BaseNode
	{
		[Input(ShowBackingValue.Never)] public bool prev;

		//makes shure continue doesn't do anything
		public override void Continue()
		{

		}

		public override object Read()
		{
			DialogueGraph dialogueGraph = (DialogueGraph)graph;
			dialogueGraph.End();

			return "__EndNode"; //used so there is no unnecessary looping
		}
	}
}