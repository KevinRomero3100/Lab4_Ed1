using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CustumGenerics.Structures
{
    public class Node<T>
    {
        public Node<T> left;

        public Node<T> right;

        Node<T> parent;
        public T Value;

        int number;
        /// <summary>
        /// Nodo del arbol avl
        /// </summary>
        /// <param name="value"></param> este parametro es para almacenamiento
        /// <param name="left"></param> define al nodo izquierdo
        /// <param name="right"></param> define al nodo derecho
        /// <param name="parent"></param> define al nodo padre
        /// <param name="number"></param> define la altura

        #region Constructor
        public Node()
        {

        }
        public Node(T value, Node<T> left, Node<T> right)
        {
            this.Value = value;
            this.left = left;
            this.right = right;
            this.parent = null;
            this.number = 0;
        }
        public Node(T value) : this (value, null, null) { }

        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public Node <T> Parent
        {
            get
            {
                return parent;
            }
            set
            {
                this.parent = value;
            }
        }
        #endregion
    }
}
