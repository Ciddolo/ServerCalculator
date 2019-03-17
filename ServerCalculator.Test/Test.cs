using System;
using System.Text;
using System.Net;
using NUnit.Framework;
using System.Collections.Generic;

namespace ServerCalculator.Test
{
    class Test
    {
        private Server server;
        private FakeTransport transport;
        private FakeEndPoint client;

        [SetUp]
        public void SetUpTest()
        {
            transport = new FakeTransport();
            server = new Server(transport);
            client = new FakeEndPoint("client", 0);
        }

        [Test]
        public void TestSumPositivePositive()
        {
            FakePacket packet = new FakePacket(0, 2.0f, 3.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(5.0f));
        }

        [Test]
        public void TestSumNegativeNegative()
        {
            FakePacket packet = new FakePacket(0, -2.0f, -3.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(-5.0f));
        }

        [Test]
        public void TestSumPositiveNegative()
        {
            FakePacket packet = new FakePacket(0, 2.0f, -3.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(-1.0f));
        }

        [Test]
        public void TestSumNegativePositive()
        {
            FakePacket packet = new FakePacket(0, -2.0f, 3.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(1.0f));
        }

        [Test]
        public void TestSumMaxValue()
        {
            FakePacket packet = new FakePacket(0, float.MaxValue, float.MaxValue);

            transport.ClientEnqueue(packet, "tester", 0);

            Assert.That(() => server.SingleStep(), Throws.InstanceOf<ServerException>());
        }

        [Test]
        public void TestSubPositivePositive()
        {
            FakePacket packet = new FakePacket(1, 7.0f, 2.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(5.0f));
        }

        [Test]
        public void TestSubNegativeNegative()
        {
            FakePacket packet = new FakePacket(1, -7.0f, -2.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(-5.0f));
        }

        [Test]
        public void TestSubPositiveNegative()
        {
            FakePacket packet = new FakePacket(1, 7.0f, -2.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(9.0f));
        }

        [Test]
        public void TestSubNegativePositive()
        {
            FakePacket packet = new FakePacket(1, -7.0f, 2.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(-9.0f));
        }

        [Test]
        public void TestMulPositivePositive()
        {
            FakePacket packet = new FakePacket(2, 5.0f, 4.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(20.0f));
        }

        [Test]
        public void TestMulNegativeNegative()
        {
            FakePacket packet = new FakePacket(2, -5.0f, -4.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(20.0f));
        }

        [Test]
        public void TestMulPositiveNegative()
        {
            FakePacket packet = new FakePacket(2, 5.0f, -4.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(-20.0f));
        }

        [Test]
        public void TestMulNegativePositive()
        {
            FakePacket packet = new FakePacket(2, -5.0f, 4.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(-20.0f));
        }

        [Test]
        public void TestMulMaxValue()
        {
            FakePacket packet = new FakePacket(2, float.MaxValue, float.MaxValue);

            transport.ClientEnqueue(packet, "tester", 0);

            Assert.That(() => server.SingleStep(), Throws.InstanceOf<ServerException>());
        }

        [Test]
        public void TestDivPositivePositive()
        {
            FakePacket packet = new FakePacket(3, 30.0f, 3.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(10.0f));
        }

        [Test]
        public void TestDivNegativeNegative()
        {
            FakePacket packet = new FakePacket(3, -30.0f, -3.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(10.0f));
        }

        [Test]
        public void TestDivPositiveNegative()
        {
            FakePacket packet = new FakePacket(3, 30.0f, -3.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(-10.0f));
        }

        [Test]
        public void TestDivNegativePositive()
        {
            FakePacket packet = new FakePacket(3, -30.0f, -3.0f);

            transport.ClientEnqueue(packet, "tester", 0);
            server.SingleStep();

            float output = BitConverter.ToSingle(transport.ClientDequeue().data, 0);

            Assert.That(output, Is.EqualTo(10.0f));
        }

        [Test]
        public void TestDivByZero()
        {
            FakePacket packet = new FakePacket(3, 10.0f, 0.0f);

            transport.ClientEnqueue(packet, "tester", 0);            

            Assert.That(() => server.SingleStep(), Throws.InstanceOf<ServerException>());
        }
    }
}
