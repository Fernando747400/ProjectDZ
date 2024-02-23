using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames.Dz.Ai
{
    public enum NodeStates
    {
        Running,
        Success,
        Failure
    }
    public class Node
    {
        protected NodeStates state;
        protected Node parent;
        
        protected List<Node> children = new();

        private Dictionary<string, object> _dataContext = new();
        
        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (var child in children)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public object GetData(string key)
        {
            if (_dataContext.TryGetValue(key, out var value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }

            return false;
        }


        public virtual NodeStates Evaluate( ) => NodeStates.Failure;
    }
}
