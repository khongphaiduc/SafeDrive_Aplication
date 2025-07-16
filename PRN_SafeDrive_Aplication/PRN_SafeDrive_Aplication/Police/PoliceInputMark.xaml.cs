
﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PRN_SafeDrive_Aplication.Models;
using PRN_SafeDrive_Aplication.MyModels;
using PRN_SafeDrive_Aplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PRN_SafeDrive_Aplication.Police
{
    /// <summary>
    /// Interaction logic for PoliceInputMark.xaml
    /// </summary>
    public partial class PoliceInputMark : Window
    {
        private int _IDCourse;
        private int _IDExam;

        public PoliceInputMark(int IDCourse, int IDExam)
        {
            InitializeComponent();
            _IDCourse = IDCourse;
            _IDExam = IDExam;
            StudentsDataGrid.ItemsSource = GetListStudentOfCourse(_IDCourse);
        }




        // lấy danh sách của 1 khóa học trong bài thi đấy 
        public List<Students> GetListStudentOfCourse(int courseId)
        {
            using (var dbcontext = new Prn1Context())
            {
                var students = (
                    from a in dbcontext.Courses
                    join b in dbcontext.Registrations on a.CourseId equals b.CourseId
                    join c in dbcontext.Users on b.UserId equals c.UserId
                    join d in dbcontext.Results on c.UserId equals d.UserId into gj
                    from d in gj.DefaultIfEmpty()
                    where a.CourseId == courseId
                    select new Students
                    {
                        Id = c.UserId,
                        Name = c.FullName,
                        Email = c.Email,
                        Class = c.Class,
                        School = c.School,
                        Phone = c.Phone,
                        Mark = d != null ? d.Score : 0  // nếu null thì cho thằng này 0 điểm  không nó nhiều  ok 
                    }
                ).ToList();

                return students;
            }
        }



        // khi sửa vào dataGrid thì sự kiện này tự động sẽ được gọi 
        private void myDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // Lấy item vừa sửa
            var EditMark = e.Row.Item as Students;

            if (EditMark != null)
            {
                try
                {
                    using (var dbcontext = new Prn1Context())
                    {
                        dbcontext.Results.Add(new Result
                        {
                            UserId = EditMark.Id,  // id thằng học sinh 
                            ExamId = _IDExam,
                            Score = EditMark.Mark,
                            PassStatus = EditMark.Mark >= 5 // nếu điểm >= 5 thì pass ok 
                        });
                        dbcontext.SaveChanges();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task SendResultExam(int IDExam)
        {
            try
            {
                using (var dbcontext = new Prn1Context())
                {
                    var results = (from a in dbcontext.Exams
                                   join b in dbcontext.Results on a.ExamId equals b.ExamId
                                   join c in dbcontext.Users on b.UserId equals c.UserId
                                   where a.ExamId == IDExam
                                   select new NotificationResultExam
                                   {
                                       Email = c.Email,
                                       FullName = c.FullName,
                                       Score = b.Score.ToString(),
                                       Statue = b.PassStatus ? "Passed" : "Failed"
                                   }).ToList();

                    foreach (var item in results)
                    {
                        string subject;
                        string body;

                        if (item.Statue == "Passed")
                        {
                            subject = "Congratulation to You";
                            body = $"Bạn đã Pass Chứng Chỉ với điểm số là {item.Score}";
                        }
                        else
                        {
                            subject = "Result of You";
                            body = $"We regret to inform you that you did not pass the exam this time. Your current score is {item.Score}, and we encourage you to try again in the future.";
                        }

                        await MyResendEmail.SendGmailAsyncs(item.Email, subject, body);
                        await Task.Delay(1000); 
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        private void EndExam(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var dbcontext = new Prn1Context())
                {

                    var s = (from a in dbcontext.Exams
                             where a.ExamId == _IDExam
                             select a).FirstOrDefault();


                    if (s != null)
                    {
                        s.Status = "Ended"; // cập nhật trạng thái kỳ thi là đã kết thúc 
                        dbcontext.SaveChanges();
                        MessageBox.Show("Kỳ thi đã kết thúc thành công");
                        _=SendResultExam(_IDExam); // gọi hàm gửi kết quả thi cho học sinh
                        this.Close(); 
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kỳ thi với ID đã cho.");
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        private void Button_Click(object sender, RoutedEventArgs e) => this.Close(); 

    }
}

