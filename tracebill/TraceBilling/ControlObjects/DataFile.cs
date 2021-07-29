using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;

namespace TraceBilling.ControlObjects
{
    public class DataFile
    {
        ArrayList fileContents;
        public DataFile()
        {

        }
        public void writeToFile(string fileUrl, ArrayList contentToWrite)
        {
            try
            {
                FileStream fs2 = new FileStream(@fileUrl, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs2);
                if (contentToWrite.Count == 0)
                {
                    sw.Close();
                    createFile(fileUrl);
                }
                else
                {
                    for (int i = 0; i < contentToWrite.Count; i++)
                    {
                        sw.WriteLine(contentToWrite[i].ToString());
                    }
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void writeToTarrifFile(string fileUrl, ArrayList contentToWrite)
        {

            FileStream fs2 = new FileStream(@fileUrl, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs2);
            if (contentToWrite.Count == 0)
            {
                sw.Close();
                createFile(fileUrl);
            }
            else
            {
                for (int i = 0; i < contentToWrite.Count; i++)
                {
                    sw.WriteLine(contentToWrite[i].ToString());
                }
                sw.Close();
            }
            //File.SetAttributes(fileUrl, FileAttributes.ReadOnly);
        }

        public ArrayList readFile(string fileUrl)
        {
            fileContents = new ArrayList();
            try
            {

                if (File.Exists(@fileUrl))
                {
                    TextReader tr = new StreamReader(fileUrl);
                    String line = null;
                    fileContents = new ArrayList();
                    while ((line = tr.ReadLine()) != null)
                    {
                        fileContents.Add(line);
                    }
                    tr.Close();
                }
                else
                {
                    createFile(fileUrl);
                    TextReader tr1 = new StreamReader(fileUrl);
                    String line = null;
                    fileContents = new ArrayList();
                    while ((line = tr1.ReadLine()) != null)
                    {
                        fileContents.Add(line);
                    }
                    tr1.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fileContents;
        }
        public void createFile(string fileUrl)
        {
            try
            {
                FileStream fs = new FileStream(@fileUrl, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void renameFile(string sourceUrl, string destinationUrl)
        {
            FileInfo fi = new FileInfo(sourceUrl);
            if (File.Exists(sourceUrl))
            {
                try
                {
                    if (File.Exists(destinationUrl))
                    {
                        File.Delete(destinationUrl);
                        File.Copy(sourceUrl, destinationUrl);
                        File.Delete(sourceUrl);
                    }
                    else
                    {
                        File.Copy(sourceUrl, destinationUrl);
                        File.Delete(sourceUrl);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void deleteFile(string Url)
        {
            FileInfo fi = new FileInfo(Url);
            if (File.Exists(Url))
            {
                try
                {
                    File.Delete(Url);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void writeToFileErrors(string fileUrl, ArrayList contentToWrite)
        {
            FileStream fs2 = new FileStream(@fileUrl, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs2);
            if (contentToWrite.Count == 0)
            {
                sw.Close();
                createFile(fileUrl);
            }
            else
            {
                //modified by Penlope: failure file formatting
                sw.WriteLine("CustRef,Reason");
                for (int i = 0; i < contentToWrite.Count; i += 1)
                {
                    sw.Write(contentToWrite[i].ToString() + ",");
                    //sw.Write(contentToWrite[i + 1].ToString() + ",");

                    sw.WriteLine(",");
                }
                sw.Close();
            }
        }
        public string GetTariffFile(DataTable dt)
        {
            string file = "";
            foreach (DataRow dr in dt.Rows)
            {
                string TariffCode = dr[0].ToString().Trim();
                string TariffName = dr[1].ToString().Trim();
                string Description = dr[2].ToString().Trim();
                if (Description.Length > 40)
                {
                    Description = Description.Substring(0, 37);
                }
                string EffectiveDate = dr[3].ToString().Trim();
                string SlidingScale = dr[4].ToString().Trim();
                string VatCode = dr[5].ToString().Trim();
                string VatRate = dr[6].ToString().Trim();
                string Amount = dr[7].ToString().Trim();
                string Amount1 = dr[8].ToString().Trim();
                string Amount2 = dr[9].ToString().Trim();
                file += WriteTariffFile(TariffCode, TariffName, Description, EffectiveDate, SlidingScale, VatCode, VatRate,
                                        Amount, Amount1, Amount2);
            }
            return file;
        }

        private string WriteTariffFile(string TariffCode, string TariffName, string Description, string EffectiveDate, string SlidingScale, string VatCode, string VatRate, string Amount, string Amount1, string Amount2)
        {
            string output = "";
            int tName = 5; int tDescription = 45; int effDate = 85; int range1_from = 110; int range1_to = 123;
            int range1_rate = 136; int range2_from = 151; int range2_to = 164; int range2_rate = 177;
            int range3_from = 192; int range3_to = 205; int range3_rate = 218;
            string spaces = ""; string spaces1 = ""; string spaces2 = ""; string spaces3 = ""; string spaces4 = "";
            string spaces5 = ""; string spaces6 = ""; string spaces7 = ""; string spaces8 = ""; string spaces9 = "";
            string spaces10 = ""; string spaces11 = "";

            output += '3' + TariffCode;
            for (int i = 0; i < (tName - output.Length); i++)
            {
                spaces += " ";
            }
            output += spaces + TariffName;
            for (int i = 0; i < (tDescription - output.Length); i++)
            {
                spaces1 += " ";
            }
            output += spaces1 + Description;
            for (int i = 0; i < (effDate - output.Length); i++)
            {
                spaces2 += " ";
            }
            output += spaces2 + EffectiveDate + SlidingScale + VatCode + VatRate;
            if (SlidingScale == "0")//plan for industrial tariff...28/12/2108
            {
                for (int i = 0; i < (range1_from - output.Length); i++)
                {
                    spaces3 += " ";
                }
                output += spaces3 + "0.00";
                for (int i = 0; i < (range1_to - output.Length); i++)
                {
                    spaces4 += " ";
                }
                output += spaces4 + "500.00";
                for (int i = 0; i < (range1_rate - output.Length); i++)
                {
                    spaces5 += " ";
                }
                output += spaces5 + Amount;
                for (int i = 0; i < (range2_from - output.Length); i++)
                {
                    spaces6 += " ";
                }
                output += spaces6 + "500.01";
                for (int i = 0; i < (range2_to - output.Length); i++)
                {
                    spaces7 += " ";
                }
                output += spaces7 + "1500.00";
                for (int i = 0; i < (range2_rate - output.Length); i++)
                {
                    spaces8 += " ";
                }
                output += spaces8 + Amount1;
                for (int i = 0; i < (range3_from - output.Length); i++)
                {
                    spaces9 += " ";
                }
                output += spaces9 + "1500.01";
                for (int i = 0; i < (range3_to - output.Length); i++)
                {
                    spaces10 += " ";
                }
                output += spaces10 + "999999999.99";
                for (int i = 0; i < (range3_rate - output.Length); i++)
                {
                    spaces11 += " ";
                }
                output += spaces11 + Amount2 + "\r\n";
            }
            else
            {
                for (int i = 0; i < (range1_from - output.Length); i++)
                {
                    spaces3 += " ";
                }
                output += spaces3 + "0.00";
                for (int i = 0; i < (range1_to - output.Length); i++)
                {
                    spaces4 += " ";
                }
                output += spaces4 + "999999999.99";
                for (int i = 0; i < (range1_rate - output.Length); i++)
                {
                    spaces5 += " ";
                }
                output += spaces5 + Amount + "\r\n";
            }
            return output;
        }
    }
}