using System;
using System.IO;

namespace ServerCalculator.Test
{
    public class FakePacket
    {
        private MemoryStream stream;
        private BinaryWriter writer;

        public FakePacket()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public FakePacket(byte command, params object[] elements) : this()
        {
            writer.Write(command);
            foreach (object element in elements)
            {
                if (element is int)
                    writer.Write((int)element);
                else if (element is float)
                    writer.Write((float)element);
                else if (element is byte)
                    writer.Write((byte)element);
                else if (element is char)
                    writer.Write((char)element);
                else if (element is uint)
                    writer.Write((uint)element);
                else
                    throw new Exception("unknown type");
            }
        }

        public byte[] GetData()
        {
            return stream.ToArray();
        }
    }
}
