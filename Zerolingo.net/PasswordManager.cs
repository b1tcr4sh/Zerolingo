using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zerolingo
{
    class PasswordManager
    {
        public string[] CollectCredentials(string service)
        {
            string[] credentials = new string[2];


            Console.Write($"{service} Username > ");
            credentials[0] = Console.ReadLine();

            Console.Write($"{service} Password > ");
            credentials[1] = Console.ReadLine();


            return credentials;
        }
    }
}
