using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptographer
{
    class LicenseKey
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }


        public int ProductId { get; set;}
        public Product Product { get; set; }


        public string Key { get; set; }
    }
}
