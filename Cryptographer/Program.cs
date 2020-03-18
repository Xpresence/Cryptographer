using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cryptographer
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitDatabase();
            TestEncryptor();

        }

        static void TestEncryptor()
        {
            var privateKey_RSA = "<?xml version=\"1.0\" encoding=\"utf-16\"?><RSAParameters xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Exponent>AQAB</Exponent><Modulus>sZD3PxRNnawWqgOuOqDqprNny+HQGWb0E4P8NC1rX8JEejkCqVXJUF8wcxN8bv1KrEC+Jc709imo5NprJ4PfGnwzMhCton/DCn/2LDdRNYtMn68stV7jxzk31v7n14Pa/29nmvQGZBTEEvjehGjdXfKEgt5jJ57G6k/fFagIwtDe/otVCtPrafDqtc6zE653OjQM0FbrHKGsIvxP76g9V426Nb5XHBisq7XeBHeGNAd3fUMKZXU/BxcmzGNVEeFV3ZmTGi8r0DEMB6TbQnFt4/UR1eQUwEHeK+guEPK9JBLwMp6LM4rSW8QW0Xy3en8s6HpVWbeCNHHnC4q1kAdszQ==</Modulus><P>0OVmMDhqAVhKwDohWensSBZZSgG9hbpPqTbR7Z2V6t+Nutr8BAFHHqqQlib7fDxAYBibozBAaZWq1cIddpk06DVdNIXbxBjQ+DIYuENiVD8kdAelSeCqltkvlDGWEsx7o/epHlAvWBNkVR2l8C+4IlasFeJyNbPXjpHFFSqiX8c=</P><Q>2ZsKs2CvitbDtoorpV7w7pRo0ui1ya2DeLBDE/iTXhcE5dDt5in/L83fLCtEoi4D4QZFD8lv05SwqF1nMmPt6S0zlA1ZUxbIhoTtbjU8x1nNOs87AaOcDmJM8SAI0fsXJHt32eb25SKYxSAO2hLybLgxGYxn5jFExeXRBCFitss=</Q><DP>lMzDZNfaksisvo7p0N1zRMo+ohvpP4e+VN/K28Kj1qGVmKOCXuv2GB8RTRPJke5tQMZmnvCxAUTuYsOaLy/k4v+YO+CZ8e5ndFRjDWZadX5g3hHQKHbTrQ5Z+Or1ZsCn1d2FZyVhBBEUKBktaZDkTfmFLTWXey92/YRSachrz2M=</DP><DQ>IhYxVOv+U4dU3RFDyQHYR+cjjHY7k74EbDFgMI6ttv1wHPffbECD4t8i6G5Wr/j5TpSG2PQ6+i++hIEtGy2gJPiZrcVxipx1CO1lq+/W93tPtLili5ovZelsGBFmkE3+0A4vNgkB/96tg6OD8BErvHAV8OSXme150m4vkbARIIU=</DQ><InverseQ>Z0/hKz5k0LZjb1EH3WIWHnxkOW+l/pYXgxuomHN0m4hwAiRrRgykT9MFyhQtd6ipYYU9HSuT2b/5CuC2Uny748K0jH6eNpEcVtQuxQKaoScOdoja62t8kZ2LV3Tn0l7XH4kjEa/lMXmbKqXjYKtOBvURuOhkvzLllR3CHBkpKTs=</InverseQ><D>Qh4bonZZ7FMO4WGbd6NBnK8DAEzmIv9N9RJZT8h1yeNZgjVGQ215F59KOB9SXzWbn4ZAB24Hkr2ycPzPl5gn9AG5QnQpY2+1XWyGarJyX8Ct2Gvu9t4NbLpmi3zBMWOcoS6HYpBo7M06sWHP4Xf2ravHIb/TJgaeRNNW2waFtJxZDHemNFXUbeWyjIPiQawnIQKo9G4LJYkeue2IJBT8xDvbp5HcutNqVidN2s98qwkwbnap3Kwv5FRQG4aShurmA7YjS51FHwMvkIx3/wp8sAfjzddIEDicXBGKdSuwTck+g7QrjiWZz8ZHeHOiZf6ahFRasRidpv7TJJqZsU05dQ==</D></RSAParameters>";
            var key_AES = "T3y9J2fWnYhvVuJw+DsiiLgoOGlVMYUsW1qxSuATWGA=";
            var iv_AES = "QWqheHNK08BTYkUZR2/FPQ==";

            var encryptor = new Encryptor(privateKey_RSA, key_AES, iv_AES);

            using (AppContext db = new AppContext())
            {
                var client = db.Clients.FirstOrDefault(c => c.Company == "Company 1");
                var product = db.Products.FirstOrDefault(p => p.Title == "Product 3");

                Console.WriteLine($"{client.Company}");
                Console.WriteLine($"{product.Title}");
                Console.WriteLine();

                var encryptedKey = encryptor.EncryptKey<object>(new { Client = client, Product = product });

                Console.WriteLine($"EncryptedKey:");
                Console.WriteLine(encryptedKey);
                Console.WriteLine();
            }
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
