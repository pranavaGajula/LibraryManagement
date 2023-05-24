using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXCTest
{
    public class StudentCount
    {
        public int GetStudentsCount()
        {
          
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            string query = ("select student_details.student_Rollno,student_details.student_Name,books_details.Book_Title,books_details.Author,books_details.Book_Description from issue_book join student_details on issue_book.student_id = student_details.student_Rollno join books_details on issue_book.Book_ID = books_details.Book_ID OR retrun_date=null");
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            int res = adapter.Fill(ds);
            var table = new Table();
            table.AddColumn("Roll Number");
            table.AddColumn("Student Name");
            table.AddColumn("Book Title");
            table.AddColumn("Author");
            table.AddColumn("Description");
            table.Title("[underline rgb(131,111,255)]STUDENTS WHO TAKEN BOOKS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());
            }

            AnsiConsole.Write(table);
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[rgb(124,211,76)] Total students having books : {ds.Tables[0].Rows.Count} [/]");
            Console.WriteLine();

            return res;
        }
    }
}