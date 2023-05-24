using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Spectre.Console;

namespace DXCTest
{
    public class issueBook
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            return con;
        }
        public void issue()
        {
            SqlConnection con = GetConnection();
            string query = "insert into Issue_Book (Book_Id,Student_Id, Issue_Date) values(@Book_Id,@Student_ID,@Issue_Date)";
            SqlCommand cmd = new SqlCommand(query, con);
            Console.WriteLine("Enter Book ID");
            int Book_ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Student ID");
            int Student_ID = Convert.ToInt32(Console.ReadLine());
            DateTime Issue_Date = DateTime.Now.Date;
            try
            {
                cmd.Parameters.AddWithValue("@Book_ID", Book_ID);
                cmd.Parameters.AddWithValue("@Student_ID", Student_ID);
               cmd.Parameters.AddWithValue("@Issue_Date", Issue_Date);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[rgb(124,211,76)]Book Issued to Student Sucessfully [/]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

            int Qty = 0;
            SqlCommand cmd5 = con.CreateCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = $"SELECT * FROM books_details WHERE Book_ID = {Book_ID}";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            da5.Fill(dt5);
            foreach (DataRow dr5 in dt5.Rows)
            {
                Qty = Convert.ToInt32(dr5["Qty"].ToString());
            }
            if (Qty > 0)
            {

                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = $"UPDATE Books_details SET Qty= (Qty - 1) WHERE Book_ID= {Book_ID}";
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;

                AnsiConsole.MarkupLine("[rgb(124,211,76)]Book Issued Sucessfully  [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[rgb(124,211,76)]Book Not Available[/]");

            }
        }
    }




}


         
         
        

    

