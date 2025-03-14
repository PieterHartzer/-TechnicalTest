﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ULaw.ApplicationProcessor.Enums;
using ULaw.ApplicationProcessor.Interfaces;

namespace ULaw.ApplicationProcessor.Tests
{
    [TestClass]
    public class ApplicationSubmissionTests
    {
        private const string OfferEmailForFirstLawDegreeResultEncoded = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear &lt;script&gt;alert(&#39;xss&#39;);&lt;/script&gt;, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law degree at grade: 1st.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";

        private const string OfferEmailForFirstLawDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law degree at grade: 1st.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";
        private const string OfferEmailForTwoOneLawDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law degree at grade: 2:1.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";
        private const string OfferEmailForFirstLawAndBusinessDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law and Business degree at grade: 1st.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";
        private const string OfferEmailForTwoOneLawAndBusinessDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are delighted to offer you a place on our course reference: ABC123 starting on 22 September 2019.<br/> This offer will be subject to evidence of your qualifying Law and Business degree at grade: 2:1.<br/> Please contact us as soon as possible to confirm your acceptance of your place and arrange payment of the £350.00 deposit fee to secure your place.<br/> We look forward to welcoming you to the University,<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";

        private const string FurtherInfoEmailResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application for our course reference: ABC123 starting on 22 September 2019, we are writing to inform you that we are currently assessing your information and will be in touch shortly.<br/> If you wish to discuss any aspect of your application, please contact us at AdmissionsTeam@Ulaw.co.uk.<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";
        private const string RejectionEmailForAnyThirdDegreeResult = @"<html><body><h1>Your Recent Application from the University of Law</h1><p> Dear Test, </p><p/> Further to your recent application, we are sorry to inform you that you have not been successful on this occasion.<br/> If you wish to discuss the decision further, or discuss the possibility of applying for an alternative course with us, please contact us at AdmissionsTeam@Ulaw.co.uk.<br/> Yours sincerely,<p/> The Admissions Team,</body></html>";

        private readonly Applicant _applicant = new Applicant("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "Test", "Tester", new DateTime(1991, 08, 14), false);
        // TODO: Replace with IOC/DI
        private readonly IMessageBuilder _messageBuilder = new MessageBuilder();

        //! NOTE: The instructions state that the coverage must retain the same coverage and the same equal assertions
        //           - code coverage was not checked.  None included and no time to add any.
        //           - not certain if the same equal assertions means all the existing or the same number.  Presumed the same existing ones so new one(s) added.
        //! TODO: Change the tests to test the MessageBuilder separately as it's no longer a unit test but integration

        [TestMethod]
        public void NullParameterApplicationSubmission()
        {
            // Test all parameters here - could be split but it's such a small test
            Assert.ThrowsException<ArgumentNullException>(() => new Application(null, _messageBuilder));
            Assert.ThrowsException<ArgumentNullException>(() => new Application(_applicant, null));
        }

        [TestMethod]
        public void ApplicationSubmissionWithFirstLawDegreeHtmlEncoded()
        {
            // Create an evil applicant
            var applicant = new Applicant("Law", "ABC123", new DateTime(2019, 9, 22), "Mr", "<script>alert('xss');</script>", "Tester", new DateTime(1991, 08, 14), false)
            {
                DegreeGrade = DegreeGradeEnum.first,
                DegreeSubject = DegreeSubjectEnum.law
            };

            Application thisSubmission = new Application(applicant, _messageBuilder);

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, OfferEmailForFirstLawDegreeResultEncoded);
        }

        [TestMethod]
        public void ApplicationSubmissionWithFirstLawDegree()
        {
            _applicant.DegreeGrade = DegreeGradeEnum.first;
            _applicant.DegreeSubject = DegreeSubjectEnum.law;

            Application thisSubmission = new Application(_applicant, _messageBuilder);

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, OfferEmailForFirstLawDegreeResult);
        }

        [TestMethod]
        public void ApplicationSubmissionWithFirstLawAndBusinessDegree()
        {
            Application thisSubmission = new Application(_applicant, _messageBuilder);

            _applicant.DegreeGrade = DegreeGradeEnum.first;
            _applicant.DegreeSubject = DegreeSubjectEnum.lawAndBusiness;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, OfferEmailForFirstLawAndBusinessDegreeResult);
        }

        [TestMethod]
        public void ApplicationSubmissionWithFirstEnglishDegree()
        {
            Application thisSubmission = new Application(_applicant, _messageBuilder);

            _applicant.DegreeGrade = DegreeGradeEnum.first;
            _applicant.DegreeSubject = DegreeSubjectEnum.English;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, FurtherInfoEmailResult);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoOneLawDegree()
        {
            Application thisSubmission = new Application(_applicant, _messageBuilder);

            _applicant.DegreeGrade = DegreeGradeEnum.twoOne;
            _applicant.DegreeSubject = DegreeSubjectEnum.law;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, OfferEmailForTwoOneLawDegreeResult);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoOneMathsDegree()
        {

            Application thisSubmission = new Application(_applicant, _messageBuilder);

            _applicant.DegreeGrade = DegreeGradeEnum.twoOne;
            _applicant.DegreeSubject = DegreeSubjectEnum.maths;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, FurtherInfoEmailResult);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoOneLawAndBusinessDegree()
        {
            Application thisSubmission = new Application(_applicant, _messageBuilder);

            _applicant.DegreeGrade = DegreeGradeEnum.twoOne;
            _applicant.DegreeSubject = DegreeSubjectEnum.lawAndBusiness;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, OfferEmailForTwoOneLawAndBusinessDegreeResult);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoTwoEnglishDegree()
        {
            Application thisSubmission = new Application(_applicant, _messageBuilder);

            _applicant.DegreeGrade = DegreeGradeEnum.twoTwo;
            _applicant.DegreeSubject = DegreeSubjectEnum.English;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, FurtherInfoEmailResult);
        }

        [TestMethod]
        public void ApplicationSubmissionWithTwoTwoLawDegree()
        {
            Application thisSubmission = new Application(_applicant, _messageBuilder);

            _applicant.DegreeGrade = DegreeGradeEnum.twoTwo;
            _applicant.DegreeSubject = DegreeSubjectEnum.law;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, FurtherInfoEmailResult);
        }

        [TestMethod]
        public void ApplicationSubmissionWithThirdDegree()
        {
            Application thisSubmission = new Application(_applicant, _messageBuilder);

            _applicant.DegreeGrade = DegreeGradeEnum.third;
            _applicant.DegreeSubject = DegreeSubjectEnum.maths;

            string emailHtml = thisSubmission.Process();
            Assert.AreEqual(emailHtml, RejectionEmailForAnyThirdDegreeResult);
        }
    }
  
}
