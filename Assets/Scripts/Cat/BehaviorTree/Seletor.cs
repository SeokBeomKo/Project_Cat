using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        List<Node> childs;

        public Selector(List<Node> childs)
        {
            this.childs = childs;
        }

        public Node.NodeState Evaluate()
        {
            if (this.childs == null) return Node.NodeState.FAILURE;

            foreach(var child in this.childs)
            {
                switch(child.Evaluate())
                {
                    case Node.NodeState.RUNNING: return Node.NodeState.RUNNING;
                    case Node.NodeState.SUCCESS: return Node.NodeState.SUCCESS;
                }
            }
            return Node.NodeState.FAILURE;
        }
    }
}