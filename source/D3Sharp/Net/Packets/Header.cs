﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.ProtocolBuffers;

namespace D3Sharp.Net.Packets
{
    public class Header
    {
        public byte[] Data { get; private set; }

        public byte Service { get; set; }
        public uint Method { get; set; }
        public int RequestID { get; set; }
        public ulong Unknown { get; set; }
        public uint PayloadLenght { get; set; }

        public Header()
        {            
            this.Unknown = 0x0;
            this.PayloadLenght = 0x0;
        }

        public Header(byte[] data)
        {
            this.Data = data;

            var stream = CodedInputStream.CreateInstance(data);
            this.Service = stream.ReadRawByte();
            this.Method = stream.ReadRawVarint32();
            this.RequestID = stream.ReadRawByte() | (stream.ReadRawByte() << 8);
            if (Service != 0xfe) this.Unknown = stream.ReadRawVarint64();
            this.PayloadLenght = stream.ReadRawVarint32();
        }

        public Header(IEnumerable<byte> data)
            : this(data.ToArray())
        {
        }

        public void Build()
        {            
            //var stream = CodedOutputStream.CreateInstance(this.Data);
            //stream.WriteRawByte(this.Service);
            //stream.WriteRawVarint32(this.Method);
            //stream.WriteRawByte((byte) this.RequestID);
            //stream.WriteRawVarint64(this.Unknown);
            //stream.WriteRawVarint32(this.PayloadLenght);
        }

        public override string ToString()
        {
            return string.Format("Service: {0}, Method: {1}, RequestID: {2}, Unknown: {3}, Payload-Length: {4}", this.Service, this.Method, this.RequestID, this.Unknown, this.PayloadLenght);
        }
    }
}