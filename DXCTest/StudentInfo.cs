using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXCTest
{
    internal class StudentInfo
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            return con;
        }
        public void Add_Student()
        {
            SqlConnection con = GetConnection();
            string query = $"insert into Student_Details values(@Student_Name,@Gender,@EMail,@contact)";
            SqlCommand cmd = new SqlCommand(query, con);

            string Student_Name = AnsiConsole.Ask<string>("[yellow]Enter Student Name:[/]");
            string Gender = AnsiConsole.Ask<string>("[yellow]Enter Gender (M/F):[/]");
            string EMail = AnsiConsole.Ask<string>("[yellow]Enter EMail:[/]");
            Console.WriteLine("enter contact");
            int contact=Convert.ToInt32(Console.ReadLine());
            cmd.Parameters.AddWithValue("@Student_Name", Student_Name);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@EMail", EMail);
            cmd.Parameters.AddWithValue("@contact", contact);

            cmd.ExecuteNonQuery();
            AnsiConsole.MarkupLine("[rgb(124,211,76)]Student Added Sucessfully [/]");
            con.Close();
        }
        public void View_Student()
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Student_Details", con);
            SqlDataReader reader1 = cmd.ExecuteReader();
            var table = new Table();
            table.AddColumn("Student_RollNo");
            table.AddColumn("Student_Name");
            table.AddColumn("Gender");
            table.AddColumn("EMail");
            table.AddColumn("contact");
            table.Title("[underline rgb(131,111,255)]STUDENT DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            while (reader1.Read())
            {
                table.AddRow(reader1["Student_RollNo"].ToString(),  reader1["Student_Name"].ToString(), reader1["Gender"].ToString(), reader1["EMail"].ToString(), reader1["contact"].ToString());
            }
            AnsiConsole.Write(table);
            con.Close();

        }
        public void View_Student_ByNo()
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            Console.WriteLine("Enter Student Roll Number");
           int Student_RollNo=Convert.ToInt32(Console.ReadLine());
            try
            {
                SqlCommand cmd = new SqlCommand($"select * from Student_Details where Student_RollNo={Student_RollNo}", con);
                SqlDataReader reader1 = cmd.ExecuteReader();
                var table = new Table();
                table.AddColumn("Student_RollNo");
                table.AddColumn("Student_Name");
                table.AddColumn("Gender");
                table.AddColumn("EMail");
                table.AddColumn("contact");
                table.Title("[underline rgb(131,111,255)]STUDENT DETAILS[/]");
                table.BorderColor(Color.LightSlateGrey);
                while (reader1.Read())
                {
                    table.AddRow(reader1["Student_RollNo"].ToString(), reader1["Student_Name"].ToString(), reader1["Gender"].ToString(), reader1["EMail"].ToString(), reader1["contact"].ToString());
                }
                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();

        }
        public void update_student()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("Enter the Student Roll Number you want to update:");
            string query = $"update Student_Details set Student_Name=@Student_Name,Gender=@Gender, EMail=@EMail,contact=@contact where Student_RollNo = {id}";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                string Student_Name = AnsiConsole.Ask<string>("[yellow]Enter Updated Student Name:[/]");
                string Gender = AnsiConsole.Ask<string>("[yellow]Enter Updated  Gender:[/]");
                string EMail = AnsiConsole.Ask<string>("[yellow]Enter Updated EMail:[/]");
                Console.WriteLine("Enter Updated Contact:");
                int contact=Convert.ToInt32(Console.ReadLine());
                //byte contact = AnsiConsole.Ask<byte>("[yellow]Enter Updated Contact:[/]");

                cmd.Parameters.AddWithValue("@Student_Name", Student_Name);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@EMail", EMail);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[rgb(124,211,76)]Student Updated Sucessfully [/]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();
        }
        public void Delete_Student()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Book id you want to Delete:[/]");
            try
            {
                string query = $"delete from Student_Details where Student_RollNo = {id}";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();

                AnsiConsole.MarkupLine("[rgb(124,211,76)]Student  Deleted Sucessfully [/]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();
        }
    }
}
