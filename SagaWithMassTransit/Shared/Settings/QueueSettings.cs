using SagaWithMassTransit.Shared.Submissions;

namespace SagaWithMassTransit.Shared.Settings
{
    public class QueueSettings
    {
        public string HostName { get; set; }
        public ushort Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
        public ClaimSubmission ClaimSubmission { get; set; }
    }
}