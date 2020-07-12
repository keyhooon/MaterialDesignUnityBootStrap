
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Communication.Codec;
using Prism.Commands;
using Prism.Ioc;
using Prism.Unity;
using Prism.Unity.Ioc;
using SharpCommunication.Base.Channels;
using SharpCommunication.Base.Codec.Packets;
using SharpCommunication.Base.Transport;
using SharpCommunication.Base.Transport.SerialPort;
using Unity;

namespace Communication
{
    public class DataTransportService
    {
        private DataTransport<DevicePacket> _dataTransport;
        private UnityContainerExtension _container;
        private readonly SerialPortDataTransportOption serialPortDataTransportOption;

        public event EventHandler<PacketReceivedEventArg> DataReceived;
        public event EventHandler<PacketReceivedEventArg> CommandReceived;
        public event EventHandler IsOpenChanged;
        public event EventHandler CanCloseChanged;
        public event EventHandler CanOpenChanged;

        public DataTransportService(UnityContainerExtension container, DataTransportOption serialPortDataTransportOption)
        {
            _container = container;
            this.serialPortDataTransportOption = serialPortDataTransportOption;

        }

        private void OnChannel_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in e.NewItems)
                    ((IChannel<DevicePacket>)item).DataReceived += OnChannel_DataReceived;
            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (var item in e.OldItems)
                    ((IChannel<DevicePacket>)item).DataReceived -= OnChannel_DataReceived;
        }

        private void OnChannel_DataReceived(object sender, DataReceivedEventArg<DevicePacket> e)
        {
            switch (e.Data.DescendantPacket)
            {
                case DataPacket dataPacket:
                    DataReceived?.Invoke(sender, new PacketReceivedEventArg(dataPacket.DescendantPacket));
                    break;
                case CommandPacket commandPacket:
                    CommandReceived?.Invoke(sender, new PacketReceivedEventArg(commandPacket.DescendantPacket));
                    break;
                default:
                    break;
            }
        }

        public void DataTransmit(IAncestorPacket dataPacket)
        {
            foreach (var channel in _dataTransport.Channels)
            {
                channel.Transmit(DevicePacket.CreateDataPacket(dataPacket));
            }
        }

        public void CommandTransmit(IFunctionPacket commandPacket)
        {
            foreach (var channel in _dataTransport.Channels)
            {
                channel.Transmit(DevicePacket.CreateCommandPacket(commandPacket));
            }
        }

        public void ReadCommandTransmit(byte dataId)
        {
            foreach (var channel in _dataTransport.Channels)
            {
                channel.Transmit(DevicePacket.CreateReadCommand(dataId));
            }
        }
        public void ReadCommandsTransmitAll(IEnumerable<byte> dataIds)
        {
            foreach (var dataId in dataIds)
            {
                ReadCommandTransmit(dataId);
            }
        }


        public bool IsOpen => _dataTransport?.IsOpen ?? false;

        private DelegateCommand _startCommand;
        public DelegateCommand StartCommand =>
            _startCommand ?? (_startCommand = new DelegateCommand(() =>
            {
                _dataTransport = _container.Resolve<DataTransport<DevicePacket>>();
                _dataTransport.IsOpenChanged += IsOpenChanged;
                _dataTransport.CanCloseChanged += CanCloseChanged;
                _dataTransport.CanOpenChanged += CanOpenChanged;
                ((INotifyCollectionChanged)_dataTransport.Channels).CollectionChanged += OnChannel_CollectionChanged;
                _dataTransport.Open();
            }, () => _dataTransport.CanOpen).ObservesProperty(() => IsOpen));

        private DelegateCommand _stopCommand;
        public DelegateCommand StopCommand =>
            _stopCommand ?? (_stopCommand = new DelegateCommand(() => _dataTransport.Close(), () => _dataTransport.CanClose).ObservesProperty(() => IsOpen));

        private DelegateCommand<byte?> _refreshDataCommand;
        public DelegateCommand<byte?> RefreshDataCommand =>
            _refreshDataCommand ?? (_refreshDataCommand = new DelegateCommand<byte?>(
                i =>
                {
                    _dataTransport.Channels[0].Transmit(DevicePacket.CreateReadCommand(i.Value));
                },
                i => _dataTransport.IsOpen).ObservesProperty(() => IsOpen));

    }
}
