using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptographer
{
    class Client
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }



        public ICollection<LicenseKey> LicenseKeys { get; set; }


        public Client()
        {
            LicenseKeys = new List<LicenseKey>();
        }
    }
}
