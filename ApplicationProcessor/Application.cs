using System;
using System.Text;
using ULaw.ApplicationProcessor.Interfaces;

namespace ULaw.ApplicationProcessor
{
    // Consideration: This could be a class with a single static method taking the two parameters
    //    if more functionality isn't added
    public class Application
    {
        private readonly Applicant _applicant;
        private readonly IMessageBuilder _messageBuilder;

        public Application(Applicant applicant, IMessageBuilder messageBuilder)
        {
            _messageBuilder = messageBuilder ?? throw new ArgumentNullException("messageBuilder");
            _applicant = applicant ?? throw new ArgumentNullException("applicant");
        }

        public string Process()
        {
            return _messageBuilder.BuildMessage(_applicant);
        }

    }
}

