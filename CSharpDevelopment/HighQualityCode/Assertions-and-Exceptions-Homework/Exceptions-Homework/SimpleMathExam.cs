using System;

public class SimpleMathExam : Exam
{
    public int ProblemsSolved { get; private set; }

    public SimpleMathExam(int problemsSolved)
    {
        if (problemsSolved < 0)
        {
            throw new ArgumentOutOfRangeException("'problemsSolved' cannot be negative");
        }

        if (problemsSolved > 10)
        {
            throw new ArgumentOutOfRangeException("'problemsSolved' cannot be more than 10!");
        }

        this.ProblemsSolved = problemsSolved;
    }

    public override ExamResult Check()
    {
        switch (ProblemsSolved)
        {
            case 0:
                return new ExamResult(2, 2, 6, "Bad result: nothing done.");
            case 1:
                return new ExamResult(4, 2, 6, "Average result: nothing done.");
            case 2:
                return new ExamResult(6, 2, 6, "Average result: nothing done.");
            default:
                throw new ArgumentOutOfRangeException("'ProblemsSolved' is invalid");
        }
    }
}
