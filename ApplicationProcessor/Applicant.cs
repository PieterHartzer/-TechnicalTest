using System;
using ULaw.ApplicationProcessor.Enums;

namespace ULaw.ApplicationProcessor
{
    /// <summary>
    /// Applicant class
    /// </summary>
    public class Applicant
    {
        public Guid ApplicationId { get; private set; }
        public string Faculty { get; private set; }
        public string CourseCode { get; private set; }
        public DateTime StartDate { get; private set; }
        public string Title { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public bool RequiresVisa { get; private set; }
        public DegreeGradeEnum DegreeGrade { get; set; }
        public DegreeSubjectEnum DegreeSubject { get; set; }

        public Applicant(string faculty, string courseCode, DateTime startdate, string title, string firstName, string lastName, DateTime dateOfBirth, bool requiresVisa)
        {
            // TODO: Implement validation (and encoding?)
            ApplicationId = Guid.NewGuid();
            Faculty = faculty;
            CourseCode = courseCode;
            StartDate = startdate;
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            RequiresVisa = requiresVisa;
            DateOfBirth = dateOfBirth;
        }
    }
}

