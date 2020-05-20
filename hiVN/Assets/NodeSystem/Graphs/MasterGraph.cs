using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
	[CreateAssetMenu]
	public class MasterGraph : DialogueGraph
	{
		public List<Variable> variables; //Variables that can be used for branching

		public List<TextVariable> textVariables; //Variables that can be inserted into text

		public void Reset()
		{
			if(variables.Count != 0)
			{
				foreach (Variable var in variables)
				{
					var.value = false;
				}
			}
		}

		public void SetVar(string key, bool value)
		{
			foreach (Variable var in variables)
			{
				if (var.key == key)
				{
					var.value = value;
				}
			}
		}

		public bool GetVar(string key)
		{
			foreach (Variable var in variables)
			{
				if (var.key == key)
				{
					return var.value;
				}
			}

			Debug.LogError("Key does not exist");
			return false;
		}

		public override MasterGraph GetMaster()
		{
			return (MasterGraph)this;
		}

		public override void StartGraph()
		{
			Reset();

			base.StartGraph();
		}
	}



	[System.Serializable]
	public class Variable
	{
		public string key;
		[Tooltip("DEFAULT IS ALWAYS FALSE, CHANGE ONLY WHEN PLAYTESTING OR DEBUGGING")]
		public bool value; //Standard value is ALWAYS false
	}

	[System.Serializable]
	public class TextVariable
	{
		public string key;
		public string value;
	}
}