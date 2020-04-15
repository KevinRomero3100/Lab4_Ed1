using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustumGenerics.Structures
{
    public class BinaryTree<T>
    {
        #region FUNCIONES PRINCIPALES
        public static Node<T> Root { get; set; }
        public static Node<T> Wanted { get; set; }

        public Node<T> createNode(T value)
        {
            Node<T> newNode = new Node<T>();
            newNode.right = null;
            newNode.left = null;
            newNode.Value = value;
            return newNode;
        }

        public void Insert(T value, Comparison<T> comparison)
        {
            Node<T> newNode = createNode(value);
            
            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                InsertNode(Root, newNode, comparison);
            }
        }

        public void  InsertNode(Node<T> actulay, Node<T> newNode,Comparison<T> comparison)
        {
            if (comparison.Invoke(actulay.Value, newNode.Value) == 1)
            {
                if (actulay.right == null)
                    actulay.right = newNode;
                else
                    InsertNode(actulay.right, newNode, comparison);
            }
            else if (comparison.Invoke(actulay.Value, newNode.Value) == -1)
            {
                if (actulay.left == null)
                    actulay.left = newNode;
                else
                    InsertNode(actulay.left, newNode, comparison);
            }
        }

        public Node<T> search(T value, Comparison<T> comparison)
        {
            Wanted = null;
            Preorden(Root, value, comparison);
            if (Wanted != null)
            {
                return Wanted;
            }
            else
                return Wanted;
        }
        #endregion
        public static void Preorden(Node<T> actualy, T value, Comparison<T> comparison)
        {
            if (comparison.Invoke(value, actualy.Value) == 0)
                Wanted = actualy;
            if (Wanted == null)
            {
                if (actualy.left != null)
                    Preorden(actualy.left, value, comparison);
                if (actualy.right != null)
                    Preorden(actualy.right, value, comparison);
            }
        }
    }
}
