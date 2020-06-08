using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;


namespace hiVN
{
    [CustomNodeEditor(typeof(DisplayCharacterNode))]
    public class DisplayCharacterNodeEditor : NodeEditor
    {

        public override void OnBodyGUI()
        {
            DisplayCharacterNode node = (DisplayCharacterNode)target;

            float divide = 4;

            if (node.sprite != null)
            {
                GUI.DrawTexture(new Rect(new Vector2(104 - node.sprite.rect.width / (divide * 2), 150), new Vector2(node.sprite.rect.width / divide, node.sprite.rect.height / divide)), node.sprite.texture);
            }

            base.OnBodyGUI();

            if (node.sprite != null)
            {
                GUILayout.Space(20 + node.sprite.rect.height / divide);
            }
        }
    }
}