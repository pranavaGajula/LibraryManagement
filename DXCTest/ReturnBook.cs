using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using System.Collections;
using Microsoft.VisualBasic;

namespace DXCTest
{
    public class ReturnBook
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            return con;
        }
        public void bookreturn()
        {
            SqlConnection con = GetConnection();
            Console.WriteLine("Enter Student ID");
            int Student_ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Book ID");
            int Book_ID = Convert.ToInt32(Console.ReadLine());
            string query = $"SELECT COUNT(*) FROM IssuedBooks WHERE ={Student_ID},Book_ID={Book_ID})";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                cmd.Parameters.AddWithValue("@Student_ID", Student_ID);
                cmd.Parameters.AddWithValue("@Book_ID", Book_ID);
                Console.WriteLine("returned succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            DateTime Retrun_Date = DateTime.Now.Date;
        string q1 = $"UPDATE Issue_Book SET Retrun_Date =@Retrun_Date  WHERE Book_Id = {Book_ID} AND Student_Id = {Student_ID} ";
            SqlCommand cmd2 = new SqlCommand(q1, con);
            cmd2.Parameters.AddWithValue("@Student_ID", Student_ID);
            cmd2.Parameters.AddWithValue("@Book_ID", Book_ID);
            cmd2.Parameters.AddWithValue("@Retrun_Date", Retrun_Date);
            int rows=cmd2.ExecuteNonQuery();
            if (rows > 0)
            {        
                string q3 = $"UPDATE Books_Details SET Qty = Qty + 1 WHERE Book_ID = {Book_ID}";
                SqlCommand cmd1 = new SqlCommand(q3, con);

                cmd1.ExecuteNonQuery();

                Console.WriteLine("Books Returned Succesfully.");
            }
            else
            {
                Console.WriteLine("Books  Not Returned ");

            }
            con.Close();
        }
    }
    
}
