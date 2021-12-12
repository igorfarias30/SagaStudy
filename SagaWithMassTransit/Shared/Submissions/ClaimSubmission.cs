namespace SagaWithMassTransit.Shared.Submissions
{
    public class ClaimSubmission
    {
        public int RetryCount { get; set; }
        public int Interval { get; set; }
    }
}