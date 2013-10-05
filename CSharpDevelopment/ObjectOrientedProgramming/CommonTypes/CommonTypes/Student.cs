using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    class Student : ICloneable, IComparable
    {
        private string firstName = string.Empty;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string middleName = string.Empty;
        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        private string lastName = string.Empty;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private int ssn = 0;
        public int Ssn
        {
            get { return ssn; }
            set { ssn = value; }
        }

        private string permanentAddress = string.Empty;
        public string PermanentAddress
        {
            get { return permanentAddress; }
            set { permanentAddress = value; }
        }

        private int mobilePhone = 0;
        public int MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }

        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string course = string.Empty;
        public string Course
        {
            get { return course; }
            set { course = value; }
        }

        private SpecialtyType specialty = SpecialtyType.Default;
        public SpecialtyType Specialty
        {
            get { return specialty; }
            set { specialty = value; }
        }

        private UniversityType university = UniversityType.Default;
        public UniversityType University
        {
            get { return university; }
            set { university = value; }
        }

        private FacultyType faculty = FacultyType.Default;
        public FacultyType Faculty
        {
            get { return faculty; }
            set { faculty = value; }
        }

        public override bool Equals(object obj)
        {
            Student source = obj as Student;
            if (source == null)
                return false;
            if (this.FirstName != source.FirstName)
                return false;
            if (this.MiddleName != source.MiddleName)
                return false;
            if (this.LastName != source.LastName)
                return false;
            if (this.Ssn != source.Ssn)
                return false;
            if (this.PermanentAddress != source.PermanentAddress)
                return false;
            if (this.MobilePhone != source.MobilePhone)
                return false;
            if (this.Email != source.Email)
                return false;
            if (this.Course != source.Course)
                return false;
            if (this.Specialty != source.Specialty)
                return false;
            if (this.University != source.University)
                return false;
            if (this.Faculty != source.Faculty)
                return false;
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.FirstName);
            sb.AppendLine(this.MiddleName);
            sb.AppendLine(this.LastName);
            sb.AppendLine(this.PermanentAddress);
            sb.AppendLine(this.MobilePhone.ToString());
            sb.AppendLine(this.Course.ToString());
            sb.AppendLine(this.Ssn.ToString());
            sb.AppendLine(this.Specialty.ToString());
            sb.AppendLine(this.University.ToString());
            sb.AppendLine(this.Faculty.ToString());
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + firstName.GetHashCode();
            hash = (hash * 7) + middleName.GetHashCode();
            hash = (hash * 7) + lastName.GetHashCode();
            hash = (hash * 7) + permanentAddress.GetHashCode();
            hash = (hash * 7) + mobilePhone.GetHashCode();
            hash = (hash * 7) + course.GetHashCode();
            hash = (hash * 7) + ssn.GetHashCode();
            hash = (hash * 7) + specialty.GetHashCode();
            hash = (hash * 7) + university.GetHashCode();
            hash = (hash * 7) + faculty.GetHashCode();
            return hash;
        }

        public static bool operator ==(Student source, Student obj)
        {
            return Student.Equals(source, obj);
        }

        public static bool operator !=(Student source, Student obj)
        {
            return !(Student.Equals(source, obj));
        }

        public object Clone()
        {
            Student s = new Student();
            s.FirstName = this.FirstName;
            s.MiddleName = this.MiddleName;
            s.LastName = this.LastName;
            s.PermanentAddress = this.PermanentAddress;
            s.MobilePhone = this.MobilePhone;
            s.Course = this.Course;
            s.Ssn = this.Ssn;
            s.Specialty = this.Specialty;
            s.University = this.University;
            s.Faculty = this.Faculty;
            return s;
        }

        public int CompareTo(object obj)
        {
            Student s = obj as Student;
            if (s == null)
                return -1;
            if (this.LastName == s.LastName)
                return this.LastName.CompareTo(s.LastName);
            if (this.FirstName == s.FirstName)
                return this.FirstName.CompareTo(s.FirstName);
            return this.Ssn.CompareTo(s.Ssn);
        }
    }
}
