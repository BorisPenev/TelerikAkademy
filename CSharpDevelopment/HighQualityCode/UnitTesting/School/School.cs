using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace School
{
    public class School
    {
        public List<Student> Students { internal set; get; }
        public List<Course> Courses { internal set; get; }

        public School()
        {
            if (this.Students == null)
            {
                this.Students = new List<Student>();
            }

            if (this.Courses == null)
            {
                this.Courses = new List<Course>();
            }
        }

        public Student CreateStudent(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name cannot be null or empty");
            }

            if (this.Students == null)
            {
                this.Students = new List<Student>();
            }

            Student student = new Student(name);
            this.Students.Add(student);
            return student;
        }

        public void RemoveStudent(int studentId)
        {
            if (studentId <= 0)
            {
                throw new ArgumentOutOfRangeException("studentId cannot be negative or equal to zero");
            }

            if (this.Students == null)
            {
                this.Students = new List<Student>();
            }
            this.Students.RemoveAll(s => s.Id == studentId);
        }

        public Course CreateCourse(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name cannot be null or empty");
            }

            if (this.Courses == null)
            {
                this.Courses = new List<Course>();
            }

            if (this.Courses.Any(c => c.Name == name))
            {
                throw new DuplicateNameException("Error: course with same name is found");
            }

            var cours = new Course(name);
            this.Courses.Add(cours);
            return cours;
        }

        public void RemoveCourse(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name cannot be null or empty");
            }

            if (this.Courses == null)
            {
                this.Courses = new List<Course>();
            }
            this.Courses.RemoveAll(c => c.Name == name);
        }

        public void JoinStudentInCourse(Student student, Course course)
        {
            if (student == null)
            {
                throw new ArgumentNullException("student cannot be null");
            }

            if (course == null)
            {
                throw new ArgumentNullException("course cannot be null");
            }
            course.Join(student);
        }

        public void LeaveStudentFromCourse(Student student, Course course)
        {
            if (student == null)
            {
                throw new ArgumentNullException("student cannot be null");
            }

            if (course == null)
            {
                throw new ArgumentNullException("course cannot be null");
            }
            course.Leave(student);
        }
    }
}
