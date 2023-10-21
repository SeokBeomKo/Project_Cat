using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public interface Node
    {
        public enum NodeState
        {
            RUNNING,
            SUCCESS,
            FAILURE
        }

        public NodeState Evaluate();
    }
}
