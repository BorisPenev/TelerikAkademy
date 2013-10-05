using System.Data;
using School;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace School.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for SchoolTest and is intended
    ///to contain all SchoolTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SchoolTest
    {

        School school = new School();

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for School Constructor
        ///</summary>
        [TestMethod()]
        public void SchoolConstructorTest()
        {
            Assert.IsNotNull(new School());
        }

        /// <summary>
        ///A test for CreateCourse
        ///</summary>
        [TestMethod()]
        public Course CreateCourseTest()
        {
            string name = "HQC";
            Course actual = school.CreateCourse(name);
            Assert.AreEqual(name, actual.Name);
            return actual;
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateNameException))]
        public void CreateDuplicateNameCourseTest()
        {
            string name = "HQC";
            school.CreateCourse(name);
            school.CreateCourse(name);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateCourseNullOrEmptyTest()
        {
            string name = string.Empty;
            Course actual = school.CreateCourse(name);
        }

        [TestMethod()]
        public void CreateCourseNullTest()
        {
            string name = "HQC";
            school.Courses = null;
            Course actual = school.CreateCourse(name);
            Assert.AreEqual(name, actual.Name);
        }

        /// <summary>
        ///A test for CreateStudent
        ///</summary>
        [TestMethod()]
        public Student CreateStudentTest()
        {
            string name = "Pesho";
            Student actual = school.CreateStudent(name);
            Assert.AreEqual(name, actual.Name);
            return actual;
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateStudentNullOrEmptyTest()
        {
            string name = string.Empty;
            Student actual = school.CreateStudent(name);
        }

        [TestMethod()]
        public void CreateStudentStudentsNullTest()
        {
            string name = "Pesho";
            school.Students = null;
            Student actual = school.CreateStudent(name);
            Assert.AreEqual(name, actual.Name);
        }

        /// <summary>
        ///A test for JoinStudentInCourse
        ///</summary>
        [TestMethod()]
        public void JoinStudentInCourseTest()
        {
            Student student = CreateStudentTest();
            Course course = CreateCourseTest();
            school.JoinStudentInCourse(student, course);
            Assert.IsTrue(course.StudentIds.Any(s => s == student.Id));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void JoinStudentNullInCourseTest()
        {
            Course course = CreateCourseTest();
            school.JoinStudentInCourse(null, course);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void JoinStudentInCourseNullTest()
        {
            Student student = CreateStudentTest();
            school.JoinStudentInCourse(student, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void JoinStudentInCourseOutOfRangeTest()
        {
            Course course = CreateCourseTest();
            for (int i = 0; i < 33; i++)
            {
                Student student = CreateStudentTest();
                school.JoinStudentInCourse(student, course);
            }
        }

        /// <summary>
        ///A test for LeaveStudentFromCourse
        ///</summary>
        [TestMethod()]
        public void LeaveStudentFromCourseTest()
        {
            JoinStudentInCourseTest();
            Student student = school.Students.FirstOrDefault(s => s.Name == "Pesho");
            Course course = school.Courses.FirstOrDefault(c => c.Name == "HQC");
            school.LeaveStudentFromCourse(student, course);
            Assert.IsTrue(course.StudentIds.All(s => s != student.Id));
        }

        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void LeaveUnknownStudentFromCourseTest()
        {
            JoinStudentInCourseTest();
            Student student = school.CreateStudent("Peshev");
            Course course = school.Courses.FirstOrDefault(c => c.Name == "HQC");
            school.LeaveStudentFromCourse(student, course);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LeaveStudentNullFromCourseTest()
        {
            JoinStudentInCourseTest();
            Course course = school.Courses.FirstOrDefault(c => c.Name == "HQC");
            school.LeaveStudentFromCourse(null, course);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LeaveStudentFromCourseNullTest()
        {
            JoinStudentInCourseTest();
            Student student = school.CreateStudent("Peshev");
            school.LeaveStudentFromCourse(student, null);
        }

        /// <summary>
        ///A test for RemoveCourse
        ///</summary>
        [TestMethod()]
        public void RemoveCourseTest()
        {
            CreateCourseTest();
            string name = "HQC";
            school.RemoveCourse(name);
            Assert.IsTrue(school.Courses.All(c => c.Name != name));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveUnknownCourseTest()
        {
            CreateCourseTest();
            string name = string.Empty;
            school.RemoveCourse(name);
        }

        [TestMethod()]
        public void RemoveNullCourseTest()
        {
            CreateCourseTest();
            string name = "HQC";
            school.Courses = null;
            school.RemoveCourse(name);
            Assert.IsTrue(school.Courses.All(c => c.Name != name));
        }

        /// <summary>
        ///A test for RemoveStudent
        ///</summary>
        [TestMethod()]
        public void RemoveStudentTest()
        {
            CreateStudentTest();
            int studentId = school.Students.FirstOrDefault(s => s.Name == "Pesho").Id;
            school.RemoveStudent(studentId);
            Assert.IsTrue(school.Students.All(s => s.Id != studentId));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveUnknownStudentTest()
        {
            CreateStudentTest();
            int studentId = 0;
            school.RemoveStudent(studentId);
        }

        [TestMethod()]
        public void RemoveNullStudentTest()
        {
            CreateStudentTest();
            int studentId = school.Students.FirstOrDefault(s => s.Name == "Pesho").Id;
            school.Students = null;
            school.RemoveStudent(studentId);
            Assert.IsTrue(school.Students.All(s => s.Id != studentId));
        }
    }
}
