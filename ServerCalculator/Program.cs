namespace ServerCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            TransportIPv4 transport = new TransportIPv4();
            transport.Bind("192.168.23.28", 9999);

            Server server = new Server(transport);

            server.Run();
        }
    }
}
