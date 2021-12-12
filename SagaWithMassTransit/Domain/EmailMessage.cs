namespace SagaWithMassTransit.Domain
{
    public class EmailMessage
    {
        public string CorrelationId { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string AppointmentNumber { get; set; }
    }
}