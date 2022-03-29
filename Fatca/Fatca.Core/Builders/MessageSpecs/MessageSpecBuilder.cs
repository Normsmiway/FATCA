using Fatca.Core.Models;

namespace Fatca.Core.Builders
{

    public class MessageSpecBuilder
    {
        protected MessageSpec MessageSpec { get; set; }

        public MessageSpecBuilder()
        {
            MessageSpec = new MessageSpec();
        }
        public static MessageSpecBuilder Create()
        {
            return new MessageSpecBuilder();
        }
        public MessageSpecInfoBuilder Info => new MessageSpecInfoBuilder(MessageSpec);
        public MessageSpecTransmissionBuilder Transmitting => new MessageSpecTransmissionBuilder(MessageSpec);
        public MessageSpec Build() => MessageSpec;
    }
}
