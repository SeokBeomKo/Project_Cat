using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        List<Node> childs;

        public Sequence(List<Node> childs)
        {
            this.childs = childs;
        }

        public Node.NodeState Evaluate()
        {
            if (this.childs == null || this.childs.Count == 0) return Node.NodeState.FAILURE;
            
            foreach(var child in this.childs)
            {
                switch(child.Evaluate())
                {
                    case Node.NodeState.RUNNING: return Node.NodeState.RUNNING;
                    case Node.NodeState.SUCCESS: continue;
                    case Node.NodeState.FAILURE: return Node.NodeState.FAILURE;
                }
            }

            return Node.NodeState.SUCCESS;
        }
    }
}