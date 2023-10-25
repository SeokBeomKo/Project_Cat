using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree
{
    public class ActionNode : Node
    {
        Func<Node.NodeState> updateEvent = null;

        public ActionNode(Func<Node.NodeState> updateEvent)
        {
            this.updateEvent = updateEvent;
        }

        public Node.NodeState Evaluate() => this.updateEvent ?. Invoke() ?? Node.NodeState.FAILURE;
    }
}
