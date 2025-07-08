using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.Views
{
    public partial class StudentCoursesWindow : Window
    {
        private readonly Prn1Context _context;
        private readonly int _studentId;

        // Tạo class để binding tránh lỗi dynamic
        public class CourseViewModel
        {
            public int Index { get; set; }
            public int CourseId { get; set; }
            public string? CourseName { get; set; }
            public string? TeacherName { get; set; }
            public DateOnly? StartDate { get; set; }
            public DateOnly? EndDate { get; set; }
            public string? Description { get; set; }
        }

        public StudentCoursesWindow(int studentId)
        {
            InitializeComponent();
            _context = new Prn1Context();
            _studentId = studentId;
            LoadStudentCourses();
        }

        private void LoadStudentCourses()
        {
            var courses = (from reg in _context.Registrations
                           where reg.UserId == _studentId
                           join c in _context.Courses on reg.CourseId equals c.CourseId
                           join t in _context.Users on c.TeacherId equals t.UserId
                           select new
                           {
                               c.CourseId,
                               c.CourseName,
                               TeacherName = t.FullName,
                               c.StartDate,
                               c.EndDate,
                               Description = c.ContentCourse
                           })
                          .Distinct()
                          .ToList()
                          .Select((x, i) => new CourseViewModel
                          {
                              Index = i + 1,
                              CourseId = x.CourseId,
                              CourseName = x.CourseName,
                              TeacherName = x.TeacherName,
                              StartDate = x.StartDate,
                              EndDate = x.EndDate,
                              Description = x.Description
                          }).ToList();

            lstCourses.ItemsSource = courses;
        }

        private void lstCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = lstCourses.SelectedItem as CourseViewModel;
            if (selected == null)
            {
                tbTeacher.Text = "";
                tbStartDate.Text = "";
                tbEndDate.Text = "";
                tbDescription.Text = "";
                return;
            }
            tbTeacher.Text = selected.TeacherName ?? "";
            tbStartDate.Text = selected.StartDate.HasValue ? selected.StartDate.Value.ToShortDateString() : "";
            tbEndDate.Text = selected.EndDate.HasValue ? selected.EndDate.Value.ToShortDateString() : "";
            tbDescription.Text = selected.Description ?? "";
        }
    }
}