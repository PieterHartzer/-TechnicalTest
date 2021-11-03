using System.Text;
using ULaw.ApplicationProcessor.Interfaces;

namespace ULaw.ApplicationProcessor
{
    public class Application
    {
        private readonly Applicant _applicant;
        private readonly IMessageBuilder _messageBuilder;

        public Application(Applicant applicant, IMessageBuilder messageBuilder)
        {
            _messageBuilder = messageBuilder;
            _applicant = applicant;
        }

        public string Process()
        {
            return _messageBuilder.BuildMessage(_applicant);
        }

    }
}

