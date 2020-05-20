using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Choice")]
	public class ChoiceNode : BaseNode
	{
		[Input(ShowBackingValue.Never)] public bool prev;

		[TextArea(5, 5)]
		[Output(dynamicPortList = true, connectionType = ConnectionType.Override)] public string[] choices;

		[HideInInspector]
		public int answer;

		public override object Read()
		{
			if(choices.Length <= 0)
			{
				Debug.LogError("No choices");
			}

			return new ChoiceReturn(choices, this);
		}

		public override void Continue()
		{
			DialogueGraph dialogueGraph = (DialogueGraph)graph;

			Debug.Log("choices " + answer);


			if (GetPort("choices " + answer).Connection == null)
			{
				Debug.LogError("Untied end in : " + graph.name);
			}
			if (GetPort("choices " + answer).ConnectionCount > 1)
			{
				Debug.LogWarning("Multiple out connections in : " + graph.name);
			}

			dialogueGraph.atNode = (BaseNode)GetOutputPort("choices " + answer).Connection.node;
		}
	}

	public class ChoiceReturn
	{
		public string[] strings;
		public ChoiceNode node;

		public ChoiceReturn(string[] _strings, ChoiceNode _node)
		{
			strings = _strings;
			node = _node;
		}
	}
}