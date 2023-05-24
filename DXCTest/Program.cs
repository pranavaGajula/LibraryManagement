using Microsoft.VisualBasic;
using Spectre.Console;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using Unity;
using static System.Reflection.Metadata.BlobBuilder;

namespace DXCTest
{
    public class Program

    {
        static void Main(string[] args)
        {
            string res;
            
            SqlConnection con = new SqlConnection("Server=IN-5YC79S3; database=LibraryRecords; Integrated Security=true");
            con.Open();
            Console.WriteLine("Enter userId");
            int username=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter password");
            int password = Convert.ToInt32(Console.ReadLine());
            string sQuery = $"select * from LOGIN where  Userid={username} and  Password={password}" ;
            SqlCommand cmd = new SqlCommand(sQuery, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                {
                    AnsiConsole.MarkupLine("[rgb(124,211,76)]WELCOME TO LIBRARY MANAGEMENT  [/]");

                    AnsiConsole.Write(new FigletText("LIBRARY").Centered().Color(Color.Aqua));
                    do
                    {
                        BookInfo bookInfo = new BookInfo();
                        issueBook Issue = new issueBook();
                        StudentInfo studentInfo = new StudentInfo();
                        ReturnBook returnBook = new ReturnBook();
                        StudentCount studentCount = new StudentCount();
                        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("What's your [yellow]choice[/]")
                            .AddChoices(new[] { "ADD BOOKS", "VIEW BOOKS", "UPDATE BOOKS", "DELETE BOOKS",
                    "ADD STUDENTS", "VIEW STUDENTS","VIEW BOOK BY AUTHOR", "UPDATE STUDENTS", "DELETE STUDENTS",
                    "VIEW STUDENT BY ROLLNUMBER","Issue Book","Return Book","STUDENT COUNT"
                        }));
                        switch (choice)
                        {
                            case "ADD BOOKS":
                                {
                                    bookInfo.Add_Book();
                                    break;
                                }
                            case "VIEW BOOKS":
                                {
                                    bookInfo.Book();
                                    break;
                                }
                            case "VIEW BOOK BY AUTHOR":
                                {
                                    bookInfo.view_Book_ByAuthor();
                                    break;
                                }

                            case "UPDATE BOOKS":
                                {
                                    bookInfo.update_Book();
                                    
                                    break;
                                }
                            case "DELETE BOOKS":
                                {
                                    bookInfo.Delete_Book();
                                    break;
                                }
                            case "ADD STUDENTS":
                                {
                                    studentInfo.Add_Student();
                                    break;
                                }
                            case "VIEW STUDENTS":
                                {
                                    studentInfo.View_Student();
                                    break;
                                }
                            case "UPDATE STUDENTS":
                                {
                                    studentInfo.update_student();
                                    break;
                                }
                            case "DELETE STUDENTS":
                                {
                                    studentInfo.Delete_Student();
                                    break;
                                }
                            case "VIEW STUDENT BY ROLLNUMBER":
                                {
                                    studentInfo.View_Student_ByNo();
                                    break;
                                }
                            case "Issue Book":
                                {
                                    Issue.issue();
                                    //Issue.avail();
                                    break;
                                }
                            case "Return Book":
                                {
                                    returnBook.bookreturn();
                                    //returnBook.returnbook();
                                    break;
                                }
                            case "STUDENT COUNT":
                                {
                                    studentCount.GetStudentsCount();
                                    break;
                                }

                        }
                        res = AnsiConsole.Ask<string>("Do you wish to [green] continue y/n? [/] ");

                    } while (res.ToLower() == "y");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[rgb(124,211,76)]Wrong UserName and Password [/]");

            }
            con.Close();
        }
    } 
}



