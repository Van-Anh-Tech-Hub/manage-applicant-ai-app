using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args) // Sửa void thành Task
        {
            UserBLL u = new UserBLL();
            var a = await u.GetUsers();

            Console.WriteLine(a);
        }
    }
}
