using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateNodeMenu("Dialogue")]
	[NodeWidth(300)]
	public class TextNode : BaseNode
	{

		[Input(ShowBackingValue.Never)] public bool prev;
		[Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;
		public Character character;
		[TextArea(10, 10)]
		public string text;

		//used if text should be displayed on multiple "screens"
		private int maxLength;
		private int atTextIndex;

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

		//used for reading the text
		public override object Read()
		{
			if(text == null)
			{
				return null;
			}
			if (text.Length - atTextIndex > maxLength)
			{
				//cut the string where the last space is and then return the new string
				string cutString = text.Substring(atTextIndex, maxLength);
				int lastSpaceIndex = cutString.LastIndexOf(" ") + 1; //Returns the index of the last space
				atTextIndex += lastSpaceIndex;

				return new TextReturn(character, text.Substring(atTextIndex - lastSpaceIndex, lastSpaceIndex - 1) + "...");
			}
			else
			{
				if (text.Length < maxLength)
				{
					atTextIndex = text.Length;
					return new TextReturn(character, text);
				}
				else
				{
					int saveATI = atTextIndex; //has to be used becuase attextindex will be changed
					atTextIndex = text.Length;
					return new TextReturn(character, text.Substring(saveATI, text.Length - saveATI));
				}
			}//if the text is too long, send it incrementally
		}

		public override void Continue()
		{
			//if the text is too long to be displayed at one "screen" wait with continuing
			if (text.Length - atTextIndex + maxLength <= maxLength)
			{
				base.Continue();
			}
		}

		public override void OnAtNode()
		{
			maxLength = 150;
			atTextIndex = 0;
		}
	}

	public class TextReturn
	{
		public Character character;
		public string text;

		public TextReturn(Character _character, string _text)
		{
			character = _character;
			text = _text;
		}
	}
}