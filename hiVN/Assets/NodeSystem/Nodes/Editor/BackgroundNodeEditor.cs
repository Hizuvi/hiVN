using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using XNodeEditor;
using XNode;

namespace hiVN
{
    [CustomNodeEditor(typeof(BackgroundNode))]
    public class BackgroundNodeEditor : NodeEditor
    {

        public override void OnBodyGUI()
        {
            BackgroundNode node = (BackgroundNode)target;

            float divide = 10;

            if (node.background != null)
            {
                GUI.DrawTexture(new Rect(new Vector2(30, 130), new Vector2(1920 / divide, 1080 / divide)), node.background.texture);
            }

            base.OnBodyGUI();

            if (node.background != null)
            {
                GUILayout.Space(150);
            }
        }
    }
}