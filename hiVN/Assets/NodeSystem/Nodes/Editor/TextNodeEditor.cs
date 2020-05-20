using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;


namespace hiVN
{
    [CustomNodeEditor(typeof(TextNode))]
    public class TextNodeEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
        }

        public override Color GetTint()
        {
            TextNode baseNode = (TextNode)target;
            DialogueGraph dialogueGraph = (DialogueGraph)baseNode.graph;

            if (dialogueGraph.atNode == baseNode)
            {
                return Color.white;
            }
            else
            {
                if (baseNode.character != null)
                {
                    return baseNode.character.color;
                }

                return base.GetTint();
            }
        }
    }
}