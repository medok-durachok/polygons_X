using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _Многоугольники_
{
    class UndoRedo<T>
    {
        public List<T> history = new List<T>();

        public T Record(T before)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, before);
                formatter.Serialize(stream, Shape.radius);
                formatter.Serialize(stream, Shape.Color);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public void Copy(T step)
        {
            T before = Record(step);
            history.Add(before);
        }

        public T Undo(int count)
        {
            if (count < history.Count && count >= 0)
            {
                return history[history.Count - (count + 1)];
            }
            else
            {
                if (count >= history.Count)
                {
                    return history[0];
                }
                else
                {
                    return history[history.Count - 1];
                }
            }
        }
    }
}