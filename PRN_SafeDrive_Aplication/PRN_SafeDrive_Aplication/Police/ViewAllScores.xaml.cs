using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.Police
{
    public partial class ViewAllScores : UserControl
    {
        private readonly Prn1Context _context;
        public ViewAllScores()
    {
        InitializeComponent();
        _context = new Prn1Context();
        LoadScores();
    }

    private void LoadScores()
    {
        var scores = (from result in _context.Results
                      join exam in _context.Exams on result.ExamId equals exam.ExamId
                      join course in _context.Courses on exam.CourseId equals course.CourseId
                      join user in _context.Users on result.UserId equals user.UserId
                      select new StudentScoreDTO
                      {
                          StudentName = user.FullName,
                          CourseName = course.CourseName,
                          ExamDate = exam.Date.ToDateTime(TimeOnly.MinValue),
                          Score = (double)result.Score,
                          PassStatus = result.PassStatus ? "Đạt" : "Không đạt"
                      }).ToList();

        dgScores.ItemsSource = scores;
    }
    }
}
