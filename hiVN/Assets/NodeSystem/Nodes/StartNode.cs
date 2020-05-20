using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Basic/Start")]
	public class StartNode : BaseNode
	{
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;

	}
}