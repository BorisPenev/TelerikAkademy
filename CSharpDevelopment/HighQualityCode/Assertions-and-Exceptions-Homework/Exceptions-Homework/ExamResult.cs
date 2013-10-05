using System;

public class ExamResult
{
    public int Grade { get; private set; }
    public int MinGrade { get; private set; }
    public int MaxGrade { get; private set; }
    public string Comments { get; private set; }

    public ExamResult(int grade, int minGrade, int maxGrade, string comments)
    {
        if (grade < 0)
        {
            throw new ArgumentOutOfRangeException("'grade' cannot be negative");
        }
        if (minGrade < 0)
        {
            throw new ArgumentOutOfRangeException("'minGrade' cannot be negative");
        }
        if (maxGrade <= minGrade)
        {
            throw new ArgumentOutOfRangeException("'maxGrade' cannot be smaller than 'minGrade'");
        }
        if (comments == null || comments == "")
        {
            throw new ArgumentNullException("comments cannot be null or empty");
        }

        this.Grade = grade;
        this.MinGrade = minGrade;
        this.MaxGrade = maxGrade;
        this.Comments = comments;
    }
}
