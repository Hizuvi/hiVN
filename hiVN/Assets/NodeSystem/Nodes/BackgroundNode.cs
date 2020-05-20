using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace hiVN
{
    [CreateNodeMenu("Display background")]
    [NodeWidth(250)]
    public class BackgroundNode : BaseNode
    {

        [Input(ShowBackingValue.Never)] public bool prev;
        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;

        public Sprite background;


        public override object Read()
        {
            return new BackgroundReturn(background);
        }
    }

    public class BackgroundReturn
    {
        public Sprite sprite;

        public BackgroundReturn(Sprite _sprite)
        {
            sprite = _sprite;
        }
    }
}