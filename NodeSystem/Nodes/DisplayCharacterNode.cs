using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{

	[CreateNodeMenu("Character/Show character")]
	public class DisplayCharacterNode : BaseNode
	{

		[Input(ShowBackingValue.Never)] public bool prev;
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;
		public Character character;
		public Sprite sprite;
		public ImageReturn.ImagePositions imagePosition;

		public override object Read()
		{
			if(sprite == null)
			{
				Debug.LogError("Node missing sprite in : " + graph.name + ". If you want to hide a characters sprite use the hide character node");
			}
			return new ImageReturn(character, sprite, imagePosition);
		}
	}

	public class ImageReturn
	{
		public Sprite sprite;
		public Character character;
		public bool remove;
		public bool removeAll;
		public ImagePositions pos;

		public ImageReturn(Character _character, Sprite _sprite, ImagePositions _pos)
		{
			sprite = _sprite;
			character = _character;
			pos = _pos;
		}

		public enum ImagePositions
		{
			prev, //uses the same position as previous. If there is none then place it in center
			left,
			right,
			center
		}
	}
}
