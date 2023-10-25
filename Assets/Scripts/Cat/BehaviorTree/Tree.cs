using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Tree
    {
        Node rootNode;

        public Tree(Node rootNode)
        {
            this.rootNode = rootNode;
        }

        public void Operate()
        {
            this.rootNode.Evaluate();
        }
    }
}