using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

using XNode;
using XNodeEditor;


namespace hiVN
{
    [CustomNodeEditor(typeof(BaseNode))]
    public class BaseNodeEditor : NodeEditor
    {
        public override Color GetTint()
        {
            BaseNode baseNode = (BaseNode)target;
            DialogueGraph dialogueGraph = (DialogueGraph)baseNode.graph;

            if (dialogueGraph.atNode == baseNode)
            {
                return Color.white;
            }
            else
            {
                return base.GetTint();
            }
        }
    }
}