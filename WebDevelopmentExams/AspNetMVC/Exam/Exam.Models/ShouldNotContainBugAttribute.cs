using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class ShouldNotContainBugAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (valueAsString == null)
            {
                return false;
            }

            return !valueAsString.ToLower().Contains("bug");
        }
    }
}