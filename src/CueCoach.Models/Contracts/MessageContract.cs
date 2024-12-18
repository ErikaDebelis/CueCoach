namespace CueCoach.Models.Contracts
{
    /// <summary>
    ///
    /// </summary>
    public class MessageContract
    {
        /// <summary>
        ///
        /// </summary>
        public Dictionary<string, string> IncomingHeaders { get; set; }
        /// <summary>
        /// username basically
        /// </summary>
        public string Identifier { get; set; }
        
        /// <summary>
        /// The TransactionId of the request.
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// The body of the request. The actual Message text sent to the endpoint.
        /// </summary>
        public string Body { get; set; }
    }
}