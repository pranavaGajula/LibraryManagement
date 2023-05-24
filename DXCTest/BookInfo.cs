using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Policy;

namespace DXCTest
{
    public class BookInfo 
    {
       public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            return con;
        }
        public void Add_Book()
        {
            SqlConnection con = GetConnection();
            string query = $"insert into Books_Details values(@Book_Title,@Author,@Book_Description,@Qty)";
            SqlCommand cmd = new SqlCommand(query, con);

            string book_Title = AnsiConsole.Ask<string>("[yellow]Enter Name:[/]");
            string author = AnsiConsole.Ask<string>("[yellow]Enter Author:[/]");
            string book_Description = AnsiConsole.Ask<string>("[yellow]Enter Description:[/]");
            byte Qty = AnsiConsole.Ask<byte>("[yellow]Enter Quantity:[/]");


            cmd.Parameters.AddWithValue("@Book_Title", book_Title);
            cmd.Parameters.AddWithValue("@Author", author);
            cmd.Parameters.AddWithValue("@Book_Description", book_Description);
            cmd.Parameters.AddWithValue("@Qty", Qty);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[rgb(124,211,76)] Book Added Sucessfully [/]");

            con.Close();
        }
        public void Book()
        {
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Books_Details", con);
            SqlDataReader reader1 = cmd.ExecuteReader();
            
            var table = new Table();
            table.AddColumn("Book_ID");
            table.AddColumn("Book_Title");
            table.AddColumn("Author");
            table.AddColumn("Book_Description");
            table.AddColumn("Qty");
            table.Title("[underline rgb(131,111,255)]BOOK DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);

            while (reader1.Read())
            {
                table.AddRow(reader1["Book_ID"].ToString(), reader1["Book_Title"].ToString(), reader1["Author"].ToString(), reader1["Book_Description"].ToString(), reader1["Qty"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();

        }
        public void view_Book_ByAuthor() {


            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            string author = AnsiConsole.Ask<string>("Enter the Book ID you want to update:"); 
            string sQuery = $"select * from Books_Details where  Author='{author}' ";
            SqlCommand cmd = new SqlCommand(sQuery, con);
            SqlDataReader dr1 = cmd.ExecuteReader();
                var table = new Table();
                table.AddColumn("Book_ID");
                table.AddColumn("Book_Title");
                table.AddColumn("Author");
                table.AddColumn("Book_Description");
                table.AddColumn("Qty");
                table.Title("[underline rgb(131,111,255)]BOOK DETAILS[/]");
                table.BorderColor(Color.LightSlateGrey);
                try
                {
                    while (dr1.Read())
                    {
                        table.AddRow(dr1["Book_ID"].ToString(), dr1["Book_Title"].ToString(), dr1["Author"].ToString(), dr1["Book_Description"].ToString(), dr1["Qty"].ToString());
                    }
                    AnsiConsole.Write(table);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                con.Close();
        }
       

        public void update_Book()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("Enter the Book ID you want to update:");
            string query = $"update Books_Details set Book_Title=@Book_Title,Author=@Author,Book_Description=@Book_Description, Qty=@Qty where Book_ID = {id}";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                string book_Title = AnsiConsole.Ask<string>("[yellow]Enter Name:[/]");
                string author = AnsiConsole.Ask<string>("[yellow]Enter Author:[/]");
                string book_Description = AnsiConsole.Ask<string>("[yellow]Enter Description:[/]");
                byte Qty = AnsiConsole.Ask<byte>("[yellow]Enter Quantity:[/]");
                cmd.Parameters.AddWithValue("@Book_Title", book_Title);
                cmd.Parameters.AddWithValue("@Author", author);
                cmd.Parameters.AddWithValue("@Book_Description", book_Description);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[rgb(124,211,76)] Book updated Sucessfully [/]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();
        } 
        public void Delete_Book()
        {
            SqlConnection con = GetConnection();
            byte id = AnsiConsole.Ask<byte>("[yellow]Enter the Book id you want to Delete:[/]");
            try
            {
                string query = $"delete from Books_Details where Book_ID = {id}";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();

                AnsiConsole.MarkupLine("[rgb(124,211,76)]Book Deleted Sucessfully [/]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();


        }
    }
}
