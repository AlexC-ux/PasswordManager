using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace PasswordManager
{
    class Des
    {

        
        string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PasswordManager");

        DES DesEx = DES.Create();
        byte[] key;
        byte[] IV;
        
        public Des()
        {
            if (!(File.Exists(Path.Combine(dir,"key"))&&File.Exists(Path.Combine(dir,"IV"))))
            {
                CreateKeys();
            }
            else
            {

                DesEx.Key = File.ReadAllBytes(Path.Combine(dir, "key"));
                DesEx.Key[Properties.Settings.Default.pos] = Properties.Settings.Default.oldbyte;
                DesEx.IV = File.ReadAllBytes(Path.Combine(dir, "IV"));
            }
        }
        
        private void CreateKeys()
        {
            DesEx.GenerateIV();
            byte[] IV = DesEx.IV;
            DesEx.GenerateKey();
            byte[] key = DesEx.Key;

            //Properties.Settings.Default.pos = new Random().Next(0,key.Length-1);

            byte[] nb = new byte[1];
            new Random().NextBytes(nb);

            Properties.Settings.Default.newbyte = nb[0];
            Properties.Settings.Default.oldbyte = key[Properties.Settings.Default.pos];
            Properties.Settings.Default.Save();

            //key[Properties.Settings.Default.pos] = Properties.Settings.Default.newbyte;


            File.WriteAllBytes(Path.Combine(dir,"key"),key);
            File.WriteAllBytes(Path.Combine(dir, "IV"), IV);

        }

        public void EncrypDataFile()
        {
            key = DesEx.Key;
            IV = DesEx.IV;


            string Data = File.ReadAllText(Path.Combine(dir, "data"));
            FileStream fStream = File.Open(Path.Combine(dir,"data"), FileMode.OpenOrCreate);

            // Create a new DES object.
            DES DESalg = DES.Create();

            // Create a CryptoStream using the FileStream
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(fStream,
                DESalg.CreateEncryptor(key, IV),
                CryptoStreamMode.Write);

            // Create a StreamWriter using the CryptoStream.
            StreamWriter sWriter = new StreamWriter(cStream);

            // Write the data to the stream
            // to encrypt it.
            sWriter.WriteLine(Data);

            // Close the streams and
            // close the file.
            sWriter.Close();
            cStream.Close();
            fStream.Close();




            //File.WriteAllBytes(Path.Combine(dir,"data"),File.ReadAllBytes(Path.Combine(dir,"crypted")));
            //File.Delete(Path.Combine(dir, "crypted"));
            Properties.Settings.Default.crypted = true;
            Properties.Settings.Default.Save();
        }

        public void DecryptDataFile()
        {
            key = DesEx.Key;
            IV = DesEx.IV;

            FileStream fStream = File.Open(Path.Combine(dir, "data"), FileMode.OpenOrCreate);

                CryptoStream cStream = new CryptoStream(fStream,
                    DesEx.CreateDecryptor(key, IV),
                    CryptoStreamMode.Read);

                StreamReader sReader = new StreamReader(cStream);
                string val = sReader.ReadToEnd();
                sReader.Close();
                cStream.Close();
                fStream.Close();
            File.WriteAllText(Path.Combine(dir, "data"),val);
            CreateKeys();
            Properties.Settings.Default.crypted = false;
            Properties.Settings.Default.Save();


        }
    }
}
