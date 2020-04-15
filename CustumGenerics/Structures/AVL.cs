using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustumGenerics.Structures
{
    public class AVL<T> : TAVL<T> where T: IComparable
    {
        public Node<T> root;
        public List<T> list;
        CompareTo<T> compare;
        #region CONSTRUCTOR
        public AVL()
        {
            root = null;
        }
        public AVL(CompareTo<T> compare)
        {
            root = null;
            this.compare = compare;
        }
        #endregion

        #region RECORRIDOS
        private void inOrder(Node<T> node)
        {
            if (node != null)
            {
                inOrder(node.left);
                list.Add(node.Value);
                inOrder(node.right);
            }
        }
        public List<T> InOrder()
        {
            list = new List<T>();
            inOrder(root);
            return list;
        }
        private void preOrder(Node<T> node)
        {
            if (node != null)
            {
                list.Add(node.Value);
                preOrder(node.left);
                preOrder(node.right);
            }
        }
        public List<T>PreOrder()
        {
            list = new List<T>();
            preOrder(root);
            return list;
        }
        private void postOrder(Node<T> node)
        {
            if (node != null)
            {
                postOrder(node.left);
                postOrder(node.right);
                list.Add(node.Value);

            }
        }
        public List<T>PostOrder()
        {
            list = new List<T>();
            postOrder(root);
            return list;
        }
        #endregion
        #region ROTACIONES
        protected void RotacionSimpleDerecha(Node<T> node)
        {
            var NewParent = node as Node<T>;
            var NewLeft = NewParent.left as Node<T>;
            var NewRight = NewLeft.right as Node<T>;
            var rootParent = node.Parent;

            if (rootParent !=null)
            {
                if (rootParent.right == NewParent)
                {
                    rootParent.right = NewLeft;
                }
                else
                {
                    rootParent.left = NewLeft;
                }
            }
            else
            {
                root = NewLeft as Node<T>;
                root.Parent = null;
            }

            NewParent.left = NewRight;
            NewLeft.right = NewParent;
            NewParent.Parent = NewLeft;

            if (NewRight != null)
            {
                NewRight.Parent = NewParent;
                NewLeft.Parent = rootParent;
            }
            if (NewLeft.Number == 0)
            {
                NewParent.Number = -1;
                NewLeft.Number = 1;
            }
            else
            {
                NewParent.Number = 0;
                NewLeft.Number = 0;
            }
        }
        protected void RotacionDobleDerecha(Node<T> node)
        {
            var ChangeNode = node as Node<T>;//P,
            var Change1 = ChangeNode.left;//Q,
            var Change2 = Change1.right;//R
            var Change3 = Change2.left;//B,
            var Change4 = Change2.right;//C
            var rootParent = node.Parent;
            if (rootParent != null)
            {
                if (rootParent.right == ChangeNode)
                {
                    rootParent.right = Change2;
                }
                else
                {
                    rootParent.left = Change2;
                }
            }
            else
            {
                root = Change2;
                root.Parent = null;
            }
            
            Change1.right = Change3;
            ChangeNode.left = Change4;
            Change2.left = Change1;
            Change2.right = ChangeNode;
            Change2.Parent = rootParent;
            ChangeNode.Parent = Change1.Parent = Change2;

            if (Change3 != null)
                Change3.Parent = Change1;
            if (Change4 != null)

            {
                Change4.Parent = ChangeNode;
            }
            switch (Change2.Number)
            {
                case -1:
                    {
                        Change1.Number = 0;
                        ChangeNode.Number = 1;
                    }
                    break;
                case 0:
                    {
                        Change1.Number = 0;
                        ChangeNode.Number = 0;

                    }
                    break;
                case 1:
                    {
                        Change1.Number = -1;
                        ChangeNode.Number = 0;
                    }
                    break;
            }
            Change2.Number = 0;
        }
        protected void RotacionSimpleIzquierda(Node<T> node)
        {
            var NewLeft = node;
            var NewParent = NewLeft.right;
            var newRight = NewParent.left;
            var rootParent = node.Parent;
            if (rootParent != null)
            {
                if (rootParent.right == NewLeft)
                {
                    rootParent.right = NewParent;
                }
                else
                {
                    rootParent.left = NewParent;
                }
            }
            else
            {
                root = NewParent;
                root.Parent = null;
            }
            NewLeft.right = NewParent.left;
            NewParent.left = NewLeft;
            NewLeft.Parent = NewParent;
            if (newRight!= null)
            {
                newRight.Parent = NewLeft;
                NewParent.Parent = rootParent;
            }
            if (NewParent.Number == 0)
            {
                NewLeft.Number = 1;
                NewParent.Number = -1;
            }
            else
            {
                NewLeft.Number = 0;
                NewParent.Number = 0;
            }
        }
        protected void RotacionDobleIzquierda(Node<T> node)
        {
            var ChangeNode = node;//p
            var Change1 = node.right;//q
            var Change2 = Change1.left;//r
            var Change3 = Change2.left;//b
            var Change4 = Change2.right;//C
            var rootParent = node.Parent;
            if (rootParent != null)
            {
                if (rootParent.right == ChangeNode)
                {
                    rootParent.right = Change2;
                }
                else
                {
                    rootParent.left = Change2;
                }
            }
            ChangeNode.right = Change3;
            Change1.left = Change4;
            Change2.left = ChangeNode;
            Change2.right = Change1;

            Change2.Parent = rootParent;
            ChangeNode.Parent = Change1.Parent = Change2;
            if (Change3 != null)
            {
                Change3.Parent = ChangeNode;
            }
            if (Change4 != null)
                Change4.Parent = Change1;
            switch (Change2.Number)
            {
                case -1:
                    {
                        ChangeNode.Number = 0;
                        Change1.Number = 1;
                    }
                    break;

                case 0:
                    {
                        ChangeNode.Number = 0;
                        Change1.Number = 0;
                    }
                    break;

                case 1:
                    {
                        ChangeNode.Number = -1;
                        Change1.Number = 0;
                    }
                    break;
            }
        }
        #endregion
        #region BALANCEO
        private void Balanced(Node<T> node, bool New, bool left)
        {
            var exit = false;
            while (node != null && !exit)
            {
                var rotate = false;
                if (New)
                {
                    if (left)
                    {
                        node.Number--;
                    }
                    else
                    {
                        node.Number++;
                    }
                }
                else
                {
                    if (node.Number == 0)
                        exit = true;
                    if (left)
                        node.Number++;
                    else
                        node.Number--;
                }
                if(node.Number ==0)
                {
                    exit = true;
                }
                else if (node.Number == -2)
                {
                    if (node.left.Number == 1)
                    {
                        RotacionDobleDerecha(node);
                        rotate = true;
                    }
                    else
                    {
                        RotacionSimpleDerecha(node);
                        rotate = true;
                    }
                    exit = true;
                }
                else if (node.Number == 2)
                {
                    if (node.right.Number == -1)
                    {
                        RotacionDobleIzquierda(node);
                        rotate = true;
                    }
                    else
                    {
                        RotacionSimpleIzquierda(node);
                        rotate = true;
                    }
                    exit = true;
                }
                if (rotate && node.Parent != null && !New)
                {
                    node = node.Parent;
                }
                if (node.Parent != null)
                {
                    if (node.Parent.right == node)
                    {
                        left = false;
                    }
                    else
                    {
                        left = true;
                    }
                    if (!New && node.Number == 0)
                    {
                        exit = false;
                    }
                }
                node = node.Parent;
            }
        }
        #endregion
        #region FUNCIONES PRINCIPALES
        public Node<T> search(T value, Comparison<T> comparison)
        {
            var auxiliar = root;
            while (comparison.Invoke(auxiliar.Value, value) != 0)
            {
                auxiliar = comparison.Invoke(value, auxiliar.Value) < 0 ? auxiliar.left : auxiliar.right;
                if (auxiliar == null)
                {
                    return null;
                }
            }
            return auxiliar;
        }
        public void Insert(T value, Comparison<T> comparison)
        {
            var newNode = new Node<T>(value);
            if (root == null)
            {
                root = newNode;
            }
            else
            {
                InsertInto(newNode, root, comparison);
            }
        }
        public void InsertInto(Node<T> newNode, Node<T> parent, Comparison<T> comparison)
        {
            if (parent != null)
            {
                if (comparison.Invoke(newNode.Value, parent.Value) < 0)
                {
                    if (parent.left == null)
                    {
                        parent.left = newNode;
                        parent.left.Parent = parent;
                        Balanced(parent, true, true);
                    }
                    else
                    {
                        InsertInto(newNode, parent.left, comparison);
                    }
                }
                else
                {
                    
                    if (comparison.Invoke(newNode.Value, parent.Value) > 0)
                    {
                        if(parent.right == null)
                        {
                            parent.right = newNode;
                            parent.right.Parent = parent;
                            Balanced(parent, true, false);
                        }
                        else
                        {
                            InsertInto(newNode, parent.right, comparison);
                        }
                    }
                }
            }
        }
        private static Node<T> Replace(Node<T> nodeDelete)
        {
            var replaceParent = nodeDelete;
            var replace = nodeDelete;
            var auxiliar = nodeDelete.right;
            while (auxiliar != null)
            {
                replaceParent = replace;
                replace = auxiliar;
                auxiliar = auxiliar.left;
            }
            if (replace != nodeDelete.right)
            {
                replaceParent.left = replace.right;
                replace.right = nodeDelete.right;
            }
            return replace;
        }
        public Node<T> Delete(T value)
        {
            var auxiliar = root;
            var parent = null as Node<T>;
            var childLeft = true;
            while (compare(auxiliar.Value, value) != 0)
            {
                parent = auxiliar;
                if (compare(value, auxiliar.Value) < 0)
                {
                    childLeft = true;
                    auxiliar = auxiliar.left;
                }
                else
                {
                    childLeft = false;
                    auxiliar = auxiliar.right;
                }
                if(auxiliar == null)
                {
                    return null;
                }
            }
            if (auxiliar.left == null && auxiliar.right == null )
            {
                if (parent != null)
                {
                    if (childLeft)
                    {
                        parent.left = null;
                    }
                    else
                    {
                        parent.right = null;
                    }
                    Balanced(parent, false, childLeft);
                }
                else
                {
                    root = null; 
                }
            }
            else if (auxiliar.right == null)
            {
                if (parent != null)
                {
                    if (childLeft)
                    {
                        parent.left = auxiliar.left;
                        auxiliar.left.Parent = parent;
                    }
                    else
                    {
                        parent.right = auxiliar.left;
                        auxiliar.left.Parent = parent;
                    }
                    Balanced(parent, false, childLeft);
                }
                else
                {
                    auxiliar.left.Parent = null;
                    root = auxiliar.left;
                }
            }
            else if(auxiliar.left == null)
            {
                if (parent != null)
                {
                    if(childLeft)
                    {
                        parent.left = auxiliar.right;
                        auxiliar.right.Parent = parent;
                    }
                    else
                    {
                        parent.right = auxiliar.right;
                        auxiliar.right.Parent = parent;
                    }
                    Balanced(parent, false, childLeft);
                }
                else
                {
                    auxiliar.right.Parent = null;
                    root = auxiliar.right;
                }
            }
            else
            {
                var replace = Replace(auxiliar);
                if (root == auxiliar)
                {
                    root = replace;
                    root.Parent = null;
                    Balanced(root, false, childLeft);
                }
                else if(childLeft)
                {
                    parent.left = replace;
                    parent.left.Parent = parent;
                    Balanced(parent, false, childLeft);
                }
                else
                {
                    parent.right = replace;
                    Balanced(parent, false, childLeft);
                }
                replace.left = auxiliar.left;

            }
            return auxiliar;
        }
        #endregion
    }

}
