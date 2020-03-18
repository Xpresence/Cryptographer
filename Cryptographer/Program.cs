using System;
using System.Collections.Generic;

namespace Cryptographer
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitDatabase();


        }

        static void InitDatabase()
        {
            using (AppContext db = new AppContext())
            {
                Client c1 = new Client() { Company = "Company 1", Phone = "89997773311" };
                Client c2 = new Client() { Company = "Company 2", Phone = "89990001122" };

                db.Clients.AddRange(new List<Client>() { c1, c2 });

                Product p1 = new Product() { Title = "Product 1" };
                Product p2 = new Product() { Title = "Product 2" };
                Product p3 = new Product() { Title = "Product 3" };

                db.Products.AddRange(new List<Product>() { p1, p2, p3 });

                db.SaveChanges();




                c1.LicenseKeys.Add(new LicenseKey() { ClientId = c1.Id, ProductId = p1.Id, Key = "C1P1-XXXX-AAAA" });
                c2.LicenseKeys.Add(new LicenseKey() { ClientId = c2.Id, ProductId = p1.Id, Key = "C2P1-XXXX-AAAA" });
                c2.LicenseKeys.Add(new LicenseKey() { ClientId = c2.Id, ProductId = p2.Id, Key = "C2P2-XXXX-AAAA" });
                c2.LicenseKeys.Add(new LicenseKey() { ClientId = c2.Id, ProductId = p3.Id, Key = "C2P3-XXXX-AAAA" });

                db.SaveChanges();

            }
        }
    }
}
