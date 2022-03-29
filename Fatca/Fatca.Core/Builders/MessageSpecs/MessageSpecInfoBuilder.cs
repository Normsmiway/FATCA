using Fatca.Core.Models;
using System;

namespace Fatca.Core.Builders
{
    public class MessageSpecInfoBuilder: MessageSpecBuilder
    {
        public MessageSpecInfoBuilder(MessageSpec messageSpec)
        {
            MessageSpec = messageSpec;
        }
        public MessageSpecInfoBuilder From(string sendingCompanyIN)
        {
            MessageSpec.SendingCompanyIN = sendingCompanyIN;
            return this;
        }
        public MessageSpecInfoBuilder WithMessageType(string messageType)
        {
            MessageSpec.MessageType = messageType;
            return this;
        }
        public MessageSpecInfoBuilder WithMesageRefId(string messageRefId)
        {
            MessageSpec.MessageRefId = messageRefId;
            return this;
        }
        public MessageSpecInfoBuilder WithPeriod(DateTime period)
        {
            MessageSpec.ReportingPeriod = period.ToString("yyyy-MM-dd");
            return this;
        }
        public MessageSpecInfoBuilder At(DateTime time)
        {
            MessageSpec.Timestamp = time.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            return this;
        }

     
    }
}
