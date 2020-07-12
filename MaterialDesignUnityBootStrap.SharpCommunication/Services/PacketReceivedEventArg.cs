using SharpCommunication.Base.Codec.Packets;
using System;

namespace Communication
{
    public class PacketReceivedEventArg : EventArgs
    {
        public PacketReceivedEventArg(IAncestorPacket packet)
        {
            Packet = packet;
        }

        public IAncestorPacket Packet { get; }
    }
}
