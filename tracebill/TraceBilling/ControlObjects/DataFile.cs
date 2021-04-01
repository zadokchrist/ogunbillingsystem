﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
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
    }
}