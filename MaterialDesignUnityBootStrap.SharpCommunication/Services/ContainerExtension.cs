using Communication.Codec;
using Prism.Ioc;
using SharpCommunication.Base.Channels;
using SharpCommunication.Base.Codec;
using SharpCommunication.Base.Transport;
using SharpCommunication.Base.Transport.SerialPort;

namespace Communication
{
    public static class ContainerExtension
    {
        public static IContainerRegistry UseCommunication(this IContainerRegistry containerRegistry)
        {
            return containerRegistry.Register<ICodec<DevicePacket>, DevicePacketCodec>()
            .Register<IChannelFactory<DevicePacket>, ChannelFactory<DevicePacket>>()
            .Register<DataTransport<DevicePacket>, SerialPortDataTransport<DevicePacket>>()
            .RegisterInstance(new SerialPortDataTransportOption("com6", 115200))
            .RegisterSingleton<DataTransportFacade>();
        }
        public static IContainerRegistry UseCodec(this IContainerRegistry containerRegistry)
        {
            return containerRegistry
                 .RegisterInstance(BatteryConfigurationEncoding.CreateBuilder())
                 .RegisterInstance(BatteryOutputEncoding.CreateBuilder())
                 .RegisterInstance(CoreConfigurationEncoding.CreateBuilder())
                 .RegisterInstance(CoreSituationEncoding.CreateBuilder())
                 .RegisterInstance(CruiseCommandEncoding.CreateBuilder())
                 .RegisterInstance(FaultEncoding.CreateBuilder())
                 .RegisterInstance(LightCommandEncoding.CreateBuilder())
                 .RegisterInstance(LightSettingPacketEncoding.CreateBuilder())
                 .RegisterInstance(LightStatePacetEncoding.CreateBuilder())
                 .RegisterInstance(PedalConfigurationEncoding.CreateBuilder())
                 .RegisterInstance(PedalSettingEncoding.CreateBuilder())
                 .RegisterInstance(ServoInputEncoding.CreateBuilder())
                 .RegisterInstance(ServoOutputEncoding.CreateBuilder())
                 .RegisterInstance(ThrottleConfigurationEncoding.CreateBuilder())
                 ;
        }
    }
}
