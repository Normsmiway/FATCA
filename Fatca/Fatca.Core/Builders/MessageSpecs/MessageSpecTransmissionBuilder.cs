using Fatca.Core.Models;

namespace Fatca.Core.Builders
{
    public class MessageSpecTransmissionBuilder: MessageSpecBuilder
    {
        public MessageSpecTransmissionBuilder(MessageSpec messageSpec)
        {
            MessageSpec = messageSpec;
        }
        public MessageSpecTransmissionBuilder From(string transmittingCountry)
        {
            MessageSpec.TransmittingCountry = transmittingCountry;
            return this;
        }
        public MessageSpecTransmissionBuilder To(string receivingCountry)
        {
            MessageSpec.ReceivingCountry = receivingCountry;
            return this;
        }
    }
}
