using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;


namespace hiVN
{
    [CustomEditor(typeof(GraphReader))]
    public class GraphReaderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GraphReader graphReader = (GraphReader)target;

            /*
             * These are commented out to avoid confusion when trying to use events or choices in the editor mode.
             * hiVN is made to make shure you don't have to playtest constantly, so you can focus on making the game as interesting as possible
             * 
             */
            
            /*if (GUILayout.Button("Start"))
            {
                graphReader.StartGraph();
            }

            if (GUILayout.Button("Next"))
            {
                graphReader.Next();
            }
            */
            
        }
    }
}