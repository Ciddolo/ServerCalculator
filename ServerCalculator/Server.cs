using System;
using System.Collections.Generic;
using System.Net;

namespace ServerCalculator
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }

    public class Server
    {
        private delegate void GameCommand(byte[] data, EndPoint sender);

        private Dictionary<byte, GameCommand> commandsTable;

        private ITransport transport;

        public Server(ITransport transport)
        {
            this.transport = transport;
            commandsTable = new Dictionary<byte, GameCommand>();
            commandsTable[0] = Sum;
            commandsTable[1] = Sub;
            commandsTable[2] = Mul;
            commandsTable[3] = Div;
        }

        private void Sum(byte[] data, EndPoint sender)
        {
            float a = BitConverter.ToSingle(data, 1);
            float b = BitConverter.ToSingle(data, 5);
            if ((a + b) > float.MaxValue)
                throw new ServerException("Result too big, max value reached");
            byte[] c = BitConverter.GetBytes(a + b);

            transport.Send(c, sender);
        }

        private void Sub(byte[] data, EndPoint sender)
        {
            float a = BitConverter.ToSingle(data, 1);
            float b = BitConverter.ToSingle(data, 5);
            byte[] c = BitConverter.GetBytes(a - b);

            transport.Send(c, sender);
        }

        private void Mul(byte[] data, EndPoint sender)
        {
            float a = BitConverter.ToSingle(data, 1);
            float b = BitConverter.ToSingle(data, 5);
            if ((a * b) > float.MaxValue)
                throw new ServerException("Result too big, max value reached");
            byte[] c = BitConverter.GetBytes(a * b);

            transport.Send(c, sender);
        }

        private void Div(byte[] data, EndPoint sender)
        {
            float a = BitConverter.ToSingle(data, 1);
            float b = BitConverter.ToSingle(data, 5);
            if (b == 0.0f)
                throw new ServerException("Can't divide by zero");
            byte[] c = BitConverter.GetBytes(a / b);

            transport.Send(c, sender);
        }

        public void Run()
        {
            while (true)
            {
                SingleStep();
            }
        }

        public void SingleStep()
        {
            EndPoint sender = transport.CreateEndPoint();
            byte[] data = transport.Recv(68, ref sender);

            if (data != null)
            {
                byte gameCommand = data[0];
                if (commandsTable.ContainsKey(gameCommand))
                    commandsTable[gameCommand](data, sender);
            }
        }
    }
}
