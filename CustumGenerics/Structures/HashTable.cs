using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustumGenerics.Structures
{
    public class HashTable<T>
    {
        private T[,] hashTable = new T[10, 10];

        public delegate int Compare(string key, T value);

        private int convertToAscci(string key)
        {
            int ascci = 0;
            char[] charValue = key.ToString().ToCharArray();
            for (int i = 0; i < charValue.Length; i++)
            {
                ascci = ascci + Encoding.ASCII.GetBytes(charValue)[i];
            }
            ascci = (ascci ^ 2) % 10;
            return ascci;
        }
        private int FirstHash(string key)
        {
            return convertToAscci(key) % 10;
        }
        private int SecondHash(string key)
        {
            return (convertToAscci(key) ^ 2) % 10;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></kakkak>
        /// <param name="value"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public bool IncertT(string key, T value, Delegate compare)
        {
            int pos1 = FirstHash(key);
            int pos2 = SecondHash(key);
            if (GetT(key, compare) == null)
            {
                
                if (hashTable[pos1, pos2] == null)
                {
                    hashTable[pos1, pos2] = value;
                    return true;
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (hashTable[pos1, i] == null)
                        {
                            hashTable[pos1, i] = value;
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                if (int.Parse(compare.DynamicInvoke(key, value).ToString()) == 0)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (hashTable[pos1, i] == null)
                        {
                            hashTable[pos1, i] = value;
                            i = 10;
                        }
                    }
                    return true;
                }
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></Hola>
        /// <param name="compare"></param>
        /// <returns></returns>
        public T GetT(string key, Delegate compare)
        {
            int pos1 = FirstHash(key);
            int pos2 = SecondHash(key);
            if (hashTable[pos1, pos2] == null)
            {
                return hashTable[pos1, pos2];
            }
            else
            {
                if (int.Parse(compare.DynamicInvoke(key, hashTable[pos1, pos2]).ToString()) == 0)
                {
                    return hashTable[pos1, pos2];
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (int.Parse(compare.DynamicInvoke(key, hashTable[pos1, i]).ToString()) == 0)
                        {
                            return hashTable[pos1, i];
                        }
                    }
                    return hashTable[pos1, pos2];
                }
            }
        }

    }
}
