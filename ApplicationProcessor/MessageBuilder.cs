using System.Text;
using System.Web;
using ULaw.ApplicationProcessor.Enums;
using ULaw.ApplicationProcessor.Interfaces;

namespace ULaw.ApplicationProcessor
{
    // This is not generic because we only deal with a single type - Applicant
    public class MessageBuilder : IMessageBuilder
    {
        public string BuildMessage(Applicant applicant)
        {
            // HACK: These string templates are awful.  Use a real templating engine
            var result = new StringBuilder("<html><body><h1>Your Recent Application from the University of Law</h1>");
            result.Append(string.Format("<p> Dear {0}, </p>", HttpUtility.HtmlEncode(applicant.FirstName)));

            switch (applicant.DegreeGrade)
            {
                case DegreeGradeEnum.twoTwo:
                    result.Append(string.Format("<p/> Further to your recent application for our course reference: {0} starting on {1}, we are writing to inform you that we are currently assessing your information and will be in touch shortly.",
                        applicant.CourseCode, applicant.StartDate.ToLongDateString()));
                    result.Append("<br/> If you wish to discuss any aspect of your application, please contact us at AdmissionsTeam@Ulaw.co.uk.");
                    break;
                case DegreeGradeEnum.third:
                    result.Append("<p/> Further to your recent application, we are sorry to inform you that you have not been successful on this occasion.");
                    result.Append("<br/> If you wish to discuss the decision further, or discuss the possibility of applying for an alternative course with us, please contact us at AdmissionsTeam@Ulaw.co.uk.");
                    break;
                default:
                    // Not TwoTwo or Third
                    if (applicant.DegreeSubject == DegreeSubjectEnum.law || applicant.DegreeSubject == DegreeSubjectEnum.lawAndBusiness)
                    {
                        // Will this be dynamic and change?  If not, embed it in the text
                        decimal depositAmount = 350.00M;

                        result.Append(string.Format("<p/> Further to your recent application, we are delighted to offer you a place on our course reference: {0} starting on {1}.",
                            applicant.CourseCode, applicant.StartDate.ToLongDateString()));
                        result.Append(string.Format("<br/> This offer will be subject to evidence of your qualifying {0} degree at grade: {1}.", applicant.DegreeSubject.ToDescription(),
                            applicant.DegreeGrade.ToDescription()));
                        result.Append(string.Format("<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £{0} deposit fee to secure your place.",
                            depositAmount.ToString()));
                        result.Append("<br/> We look forward to welcoming you to the University,");
                    }
                    else
                    {
                        // Not Law or LawAndBusiness
                        result.Append(string.Format("<p/> Further to your recent application for our course reference: {0} starting on {1}, we are writing to inform you that we are currently assessing your information and will be in touch shortly.",
                            applicant.CourseCode, applicant.StartDate.ToLongDateString()));
                        result.Append("<br/> If you wish to discuss any aspect of your application, please contact us at AdmissionsTeam@Ulaw.co.uk.");
                    }
                    break;
            }

            result.Append("<br/> Yours sincerely,");
            result.Append("<p/> The Admissions Team,");
            result.Append("</body></html>");

            return result.ToString();
        }
    }
}