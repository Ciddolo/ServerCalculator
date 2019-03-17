using System.Net;

namespace ServerCalculator
{
    public interface ITransport
    {
        void Bind(string address, int port);
        bool Send(byte[] data, EndPoint endPoint);
        byte[] Recv(int bufferSize, ref EndPoint sender);
        EndPoint CreateEndPoint();
    }
}
