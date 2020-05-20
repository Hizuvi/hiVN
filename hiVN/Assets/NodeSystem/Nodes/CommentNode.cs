using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace hiVN
{
    [CreateNodeMenu("Comment")]
    [NodeTint(200, 200, 30)]
    public class CommentNode : BaseNode
    {
        [TextArea(5, 5)]
        public string comment;
    }
}