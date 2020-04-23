using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustumGenerics.Structures
{
    public class Heep<T>
    {
        private static Stack<string> Travel = new Stack<string>();
        private static Stack<string> TravelOut = new Stack<string>();
        private static Node<T> root { get; set; }
        private static int count { get; set; }//muestra la cantidad de nodos
        private static T Last { get; set; }

        #region Principal Functions 
        public bool isEmpty()
        {
            if (root == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Insert(T value, Delegate compare)
        {
            var newNode = createNode(value);
            count++;
            if (isEmpty())
                root = newNode;
            else
            {
                CreateTravel();
                InvariantFormAndORder(root, newNode, compare);
                Travel.Clear();
                TravelOut.Clear();
            }
        }
        public T Delete(Delegate compare)
        {
            if (root.right == null && root.left == null)
            {
                count--;
                var value = root.Value;
                root = null;
                return value;
            }
            else
            {
                var value = root.Value;
                ChangeLastForFirst(root);
                DeleteInvariantOrder(root, compare);
                Travel.Clear();
                count--;
                return value;
            }
        }
        #endregion

        #region Aux Functions
        private static bool EsPar(int x)
        {
            if (x % 2 == 0)
                return true;
            else
            {
                if (x == 2)
                    return true;
                else
                    return false;
            }
        }
        private Node<T> createNode(T value)
        {
            Node<T> newNode = new Node<T>();
            newNode.right = null;
            newNode.left = null;
            newNode.Value = value;
            return newNode;
        }
        private static Stack<string> CreateTravel()
        {
            var div = count;
            while (div != 1)
            {
                if (EsPar(div))
                    Travel.Push("Izquierda");
                else
                    Travel.Push("Derecha");
                div = div / 2;
            }
            return Travel;
        }//Genera la ruta de acceso y salida para el ultimo nodo.
        private static string GetNexWay()
        {
            string nexWay = "";
            if (Travel.Count() > 0)
            {
                nexWay = Travel.Pop();
                TravelOut.Push(nexWay);
            }
            return nexWay;
        }//Obtine el siguiente camino de la ruta de aaceso para el ultimo nodo.
        private static void Change(Node<T> father, Node<T> song)
        {
            var temp = father.Value;
            father.Value = song.Value;
            song.Value = temp;
        }//Reliza el cambio necesario para cumplir el orden invariate.
        private static void OrderIn(Node<T> father, Delegate compare, string travel)
        {
            if (travel == "Izquierda")
            {
                if (int.Parse(compare.DynamicInvoke(father.Value, father.left.Value).ToString()) == -1)
                {
                    Change(father, father.left);
                }
            }
            else
            {
                if (int.Parse(compare.DynamicInvoke(father.Value, father.right.Value).ToString()) == -1)
                {
                    Change(father, father.right);
                }
            }
        }//Cumple el orden invariante durante la incercion.
        private static void SearchLast(ref Node<T> father, int contAux)
        {
            if (GetNexWay() == "Izquierda")
            {
                if (father.left == null && father.right == null)
                {
                    Last = father.Value;
                    father = null;
                    return;
                }
                else
                {
                    contAux++;
                    SearchLast(ref father.left, contAux);
                }
            }
            else
            {
                if (father.right == null && father.left == null)
                {
                    Last = father.Value;
                    father = null;
                    return;
                }
                else
                {
                    contAux++;
                    SearchLast(ref father.right, contAux);
                }
            }
        }
        private static T ChangeLastForFirst(Node<T> father)
        {
            CreateTravel();
            SearchLast(ref father, 0);
            var first = root.Value;
            root.Value = Last;
            return first;
        }

        #endregion

        #region condiciones 
        private static void InvariantFormAndORder(Node<T> father, Node<T> newNode, Delegate compare)
        {
            if (Travel.Count() != 0)
            {
                if (GetNexWay() == "Izquierda")
                {
                    if (father.left == null)
                    {
                        father.left = newNode;
                        Travel.Clear();
                        if (int.Parse(compare.DynamicInvoke(father.Value, father.left.Value).ToString()) == -1)
                        {
                            Change(father, father.left);
                            TravelOut.Pop();
                        }
                        return;
                    }
                    else
                    {
                        InvariantFormAndORder(father.left, newNode, compare);
                        OrderIn(father, compare, TravelOut.Pop());
                    }
                }
                else
                {
                    if (father.right == null)
                    {
                        father.right = newNode;
                        Travel.Clear();
                        if (int.Parse(compare.DynamicInvoke(father.Value, father.right.Value).ToString()) == -1)
                        {
                            Change(father, father.right);
                            TravelOut.Pop();
                        }
                        return;
                    }
                    else
                    {
                        InvariantFormAndORder(father.right, newNode, compare);
                        OrderIn(father, compare, TravelOut.Pop());
                    }
                }
            }
        }
        private static void OrderOut(Node<T> father, Delegate compare)
        {
            if (father.left != null && father.right != null)
            {
                if (int.Parse(compare.DynamicInvoke(father.Value, father.left.Value).ToString()) == -1 &&
                    int.Parse(compare.DynamicInvoke(father.Value, father.right.Value).ToString()) == -1)
                {
                    if (int.Parse(compare.DynamicInvoke(father.right.Value, father.left.Value).ToString()) == -1)
                    {
                        Change(father, father.left);
                        Travel.Push("Izquierda");
                    }
                    else if (int.Parse(compare.DynamicInvoke(father.left.Value, father.right.Value).ToString()) == -1)
                    {
                        Change(father, father.right);
                        Travel.Push("Derecha");
                    }
                    else if (int.Parse(compare.DynamicInvoke(father.left.Value, father.right.Value).ToString()) == 0)
                    {
                        Change(father, father.left);
                        Travel.Push("Izquierda");
                    }

                }
                else if (int.Parse(compare.DynamicInvoke(father.Value, father.left.Value).ToString()) == -1 || int.Parse(compare.DynamicInvoke(father.Value, father.right.Value).ToString()) == -1)
                {
                    if (int.Parse(compare.DynamicInvoke(father.Value, father.left.Value).ToString()) == -1)
                    {
                        Change(father, father.left);
                        Travel.Push("Izquierda");
                    }
                    else
                    {
                        Change(father, father.right);
                        Travel.Push("Derecha");
                    }
                }
                else
                {
                    if (int.Parse(compare.DynamicInvoke(father.Value, father.left.Value).ToString()) == 0)
                    {
                        Change(father, father.left);
                        Travel.Push("Izquierda");
                    }
                    else if (int.Parse(compare.DynamicInvoke(father.Value, father.right.Value).ToString()) == 0)
                    {
                        Change(father, father.right);
                        Travel.Push("Izquierda");
                    }
                    else
                    {
                        Travel.Push("Exit");
                        return;
                    }
                }
            }
            else
            {
                if (father.right == null && father.left == null)
                {
                    return;
                }
                else
                {
                    if (father.right == null)
                    {
                        if (int.Parse(compare.DynamicInvoke(father.Value, father.left.Value).ToString()) == -1)
                        {
                            Change(father, father.left);
                            Travel.Push("Izquierda");
                        }
                        else
                        {
                            Travel.Push("Exit");
                        }
                    }
                    else if (father.left == null)
                    {
                        if (int.Parse(compare.DynamicInvoke(father.Value, father.right.Value).ToString()) == -1)
                        {
                            Change(father, father.left);
                            Travel.Push("Izquierda");
                        }
                        else
                        {
                            Travel.Push("Exit");
                        }
                    }
                }
            }
        }
        private static void DeleteInvariantOrder(Node<T> father, Delegate compare)
        {
            if (father.left == null && father.right == null)
            {
                return;
            }
            else
            {
                OrderOut(father, compare);
                var route = Travel.Pop();
                if (route != "Exit")
                {
                    if (route == "Izquierda")
                    {
                        DeleteInvariantOrder(father.left, compare);
                    }
                    else if (route == "Derecha")
                    {
                        DeleteInvariantOrder(father.right, compare);
                    }
                }
                else
                {
                    Travel.Clear();
                    return;
                }
            }
        }
        #endregion
    }
}
