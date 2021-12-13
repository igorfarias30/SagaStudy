namespace SagaWithMassTransit.Shared.InputModels
{
    public class SmtpProviderInputModel
    {
        public string SmtpClientHost { get; set; }
        public ushort Port { get; set; }
        public string From { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}