using DXCTest;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXC.TEST
{
    public class StudentTest
    {
        public void Addstudent_WhenCalled_ReturnsValues()
        {
            var add = new Mock<IStudentInfo>();
            add.Setup(x => x.Add_Student()).Returns(1);
            var res = add.Object.Add_Student();
            res.Equals(1);
        }
        [Test]
        public void Updatestudent_WhenCalled_ReturnsValues()
        {
            var add = new Mock<IStudentInfo>();
            add.Setup(x => x.Update_Student()).Returns(1);
            var res = add.Object.Update_Student();
            res.Equals(1);
        }
        [Test]
        public void Deletestudent_WhenCalled_ReturnsValues()
        {
            var add = new Mock<IStudentInfo>();
            add.Setup(x => x.Delete_Student()).Returns(1);
            var res = add.Object.Delete_Student();
            res.Equals(1);
        }
    }
}

