using DXCTest;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Data.SqlClient;
using Moq;

namespace DXC.TEST
{
    public class BookTest
    {
            [Test]

            public void AddBook_WhenCalled_ReturnsValues()
            {
            var add = new Mock<IBookInfo>();
            add.Setup(x => x.Add_Book()).Returns(1);
            var res=add.Object.Add_Book();
            res.Equals(1);
            }
        [Test]
        public void UpdateBook_WhenCalled_ReturnsValues()
        {
            var add = new Mock<IBookInfo>();
            add.Setup(x => x.update_Book()).Returns(1);
            var res = add.Object.update_Book();
            res.Equals(1);
        }
        [Test]
        public void DeleteBook_WhenCalled_ReturnsValues()
        {
            var add = new Mock<IBookInfo>();
            add.Setup(x => x.delete_Book()).Returns(1);
            var res = add.Object.delete_Book();
            res.Equals(1);
        }
    }
}