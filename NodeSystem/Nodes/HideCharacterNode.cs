using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Character/Hide character")]
	public class HideCharacterNode : BaseNode
	{

		[Input(ShowBackingValue.Never)] public bool prev;
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;

		public Character character;

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

		public override object Read()
		{
			ImageReturn imageReturn = new ImageReturn(character, null, ImageReturn.ImagePositions.prev);
			imageReturn.remove = true;
			return imageReturn;
		}
	}
}