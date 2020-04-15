using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustumGenerics.Structures
{
    /// <summary>
    /// FUNCIONES PRINCIPALES DEL ARBOL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public delegate int CompareTo<T>(T value, T value2);
    public interface TAVL<T>
    {
        Node<T> Delete(T value);
        Node<T> search(T value, Comparison<T> comparison);
        void Insert(T value, Comparison<T> comparison);
        List<T> InOrder();
        List<T> PreOrder();
        List<T> PostOrder();

    }
}
