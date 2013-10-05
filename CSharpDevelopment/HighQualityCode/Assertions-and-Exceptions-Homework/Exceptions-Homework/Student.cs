using System;
using System.Linq;
using System.Collections.Generic;

public class Student
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public IList<Exam> Exams { get; set; }

    public Student(string firstName, string lastName, IList<Exam> exams = null)
    {
        if (firstName == null)
        {
            throw new ArgumentNullException("'firstName' cannot be null");
        }

        if (lastName == null)
        {
            throw new ArgumentNullException("'lastName' cannot be null");
        }

        this.FirstName = firstName;
        this.LastName = lastName;
        this.Exams = exams;
    }

    public IList<ExamResult> CheckExams()
    {
        if (this.Exams == null)
        {
            throw new ArgumentNullException("'Exams' cannot be null");
        }

        if (this.Exams.Count == 0)
        {
            throw new ArgumentOutOfRangeException("No Exams for student" + this.FirstName + " " + this.LastName);
        }

        IList<ExamResult> results = new List<ExamResult>();
        for (int i = 0; i < this.Exams.Count; i++)
        {
            results.Add(this.Exams[i].Check());
        }

        return results;
    }

    public double CalcAverageExamResultInPercents()
    {
        if (this.Exams == null)
        {
            throw new ArgumentNullException("'Exams' cannot be null");
        }

        if (this.Exams.Count == 0)
        {
            throw new ArgumentOutOfRangeException("Cannot calculate average on missing exams for student" + this.FirstName + " " + this.LastName);
        }

        double[] examScore = new double[this.Exams.Count];
        IList<ExamResult> examResults = CheckExams();

        for (int i = 0; i < examResults.Count; i++)
        {
            examScore[i] = 
                ((double)examResults[i].Grade - examResults[i].MinGrade) / 
                (examResults[i].MaxGrade - examResults[i].MinGrade);
        }

        return examScore.Average();
    }
}
