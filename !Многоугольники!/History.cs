using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _Многоугольники_
{
    class History<T>
    {
        public List<T> before = new List<T>();

        public void Record(T a)
        {
            T b = DeepCopy(a);
            before.Add(b);
        }

        public T DeepCopy(T a)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, a);
                formatter.Serialize(memoryStream, Shape.radius);
                formatter.Serialize(memoryStream, Shape.Color);
                memoryStream.Position = 0;

                return (T)formatter.Deserialize(memoryStream);
            }
        }

        public T Undo(int undoCount)
        {
            if(undoCount < before.Count && undoCount > 0)
            {
                return before[before.Count - undoCount - 1];
            }
            else
            {
                if(undoCount >= before.Count)
                {
                    return before[0];
                }
                else
                {
                    return before[before.Count - 1];
                }
            }
        }
    }
}
