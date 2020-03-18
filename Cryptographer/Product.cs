using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptographer
{
    class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        


        public ICollection<LicenseKey> LicenseKeys { get; set; }


        public Product()
        {
            LicenseKeys = new List<LicenseKey>();
        }
    }
}
