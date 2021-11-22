using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

namespace TraceBilling.ControlObjects
{
    public class PDFPrints
    {
        BusinessLogic bll = new BusinessLogic();
        DatabaseHandler dh = new DatabaseHandler();
        public string GetPDFForm(DataTable dt, DataTable dtprofile, string user)
        {
            string output = "";
            try
            {
                string pdf = "";
                string printedby = user;
                string filename = "ApplicationForm_" + dt.Rows[0]["applicationNumber"].ToString().Replace("/", "") + "_" + DateTime.Now.ToString("yyMMdd HH:mm").Replace(":", "") + ".pdf";
                //bool IsMonthly = false;//Convert.ToBoolean(dt.Rows[0]["Monthly"].ToString());

                string targetdirectory = @"C:\Data\Files\";
                if (!Directory.Exists("targetdirectory"))
                {
                    Directory.CreateDirectory(targetdirectory);
                }
                //pdf = targetdirectory + filename;
                pdf = Path.Combine(targetdirectory, filename);
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, -5f, -5f, 15f, -5f);

                FileStream fs = new FileStream(pdf, FileMode.Create);
                PdfWriter.GetInstance(document, fs);

                iTextSharp.text.BaseColor fontcolor = new iTextSharp.text.BaseColor(0, 0, 0);
                var titleFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 12, iTextSharp.text.Font.BOLD, fontcolor);
                var subTitleFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 10, iTextSharp.text.Font.NORMAL, fontcolor);
                var boldTitleFont = iTextSharp.text.FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, fontcolor);
                var boldTextFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.BOLD, fontcolor);
                var endingMessageFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.ITALIC, fontcolor);
                var bodyFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.NORMAL, fontcolor);
                var bodyFontUnderlined = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE, fontcolor);

                PdfPTable content_table = new PdfPTable(1);
                PdfPCell content_cell = new PdfPCell();

                float[] columnWidths = { 0f, 2f, 8f };

                PdfPTable pdfTable = new PdfPTable(columnWidths);
                PdfPCell cell = new PdfPCell();
                cell.Border = 0;
                pdfTable.AddCell(cell);

                //format the logo
                String path = AppDomain.CurrentDomain.BaseDirectory;
                //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);                        
                //string logo = Path.Combine(path, @"icons/logoimg2.png");//tracebilllogo
                string logo = Path.Combine(path, @"icons/tracebilllogo.png");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(logo);
                image.ScaleAbsolute(80f, 60f);
                image.Alignment = iTextSharp.text.Image.ALIGN_JUSTIFIED_ALL;
                cell = new PdfPCell(image);
                cell.Border = 0;
                pdfTable.AddCell(cell); //add logo to Header table

                PdfPTable nestedTable = new PdfPTable(2);
                PdfPCell nestedCell = new PdfPCell();
                ///company setup
                string companyname = "", email = "", address = "", tollcontact = "", website = "", othercontact = "", combinedaddress = "", combinedcontact = "";
                if (dtprofile.Rows.Count > 0)
                {
                    companyname = dtprofile.Rows[0]["companyName"].ToString().ToUpper();
                    address = dtprofile.Rows[0]["physicalAddress"].ToString();
                    email = dtprofile.Rows[0]["emailAddress"].ToString();
                    website = dtprofile.Rows[0]["webAddress"].ToString();
                    tollcontact = dtprofile.Rows[0]["tollContact"].ToString();
                    othercontact = dtprofile.Rows[0]["otherContact"].ToString();
                    combinedaddress = "Email:" + email + " " + "website:" + website;
                    combinedcontact = "TollFree:" + tollcontact + " " + "other contact:" + othercontact;
                }
                ///format header text
                ///company name
                iTextSharp.text.Paragraph header_title = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(companyname, titleFont));
                header_title.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedCell = new PdfPCell(header_title);
                nestedCell.Border = 0;
                nestedCell.Colspan = 2;
                nestedCell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedTable.AddCell(nestedCell);

                //company address
                iTextSharp.text.Paragraph company_addr = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(address, subTitleFont));
                company_addr.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedCell = new PdfPCell(company_addr);
                nestedCell.Border = 0;
                nestedCell.Colspan = 2;
                nestedCell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedTable.AddCell(nestedCell);

                //company contacts
                iTextSharp.text.Paragraph company_addr1 = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(combinedcontact, subTitleFont));
                company_addr1.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedCell = new PdfPCell(company_addr1);
                nestedCell.Border = 0;
                nestedCell.Colspan = 2;
                nestedCell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedTable.AddCell(nestedCell);

                //company emails
                iTextSharp.text.Paragraph company_addr2 = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(combinedaddress, subTitleFont));
                company_addr2.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedCell = new PdfPCell(company_addr2);
                nestedCell.Border = 0;
                nestedCell.Colspan = 2;
                nestedCell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedTable.AddCell(nestedCell);

                cell = new PdfPCell(nestedTable);
                cell.Colspan = 2;
                cell.Border = 0;
                cell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                pdfTable.AddCell(cell);

                iTextSharp.text.Paragraph form_title = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("NEW CONNECTION APPLICATION FOAM", boldTitleFont));
                form_title.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                //customer details
                float[] columnWidths1 = { 1.5f, 1.5f, 1.5f, 2f, 3f };
                PdfPTable customerdetails = new PdfPTable(columnWidths1);
                iTextSharp.text.Paragraph content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Serial No:", boldTextFont));
                PdfPCell details = new PdfPCell(content);
                details.Border = 0;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("SN" + dt.Rows[0]["ApplicationID"].ToString(), boldTextFont));
                details = new PdfPCell(content);
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 0;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Print Date:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("I/We Hereby request for Service Connection of: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["typeName"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);
                //connection type
                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("For Connection type: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["className"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);


                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Block Number:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("N/A", bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Application Reference:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["applicationNumber"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Border = 15;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 1;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Customer Name ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["fullName"].ToString().ToUpper(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 1;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Of (Customer Address) ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["address"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                //country and state
                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Located within: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);
                string location = dt.Rows[0]["countryName"].ToString() + "/" + dt.Rows[0]["areaName"].ToString();
                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(location, bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);
                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Office Phone Number: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(tollcontact, bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Border = 15;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Customer Phone Number: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["contact"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Border = 15;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                //details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                //details.Border = 0;
                //details.Colspan = 5;
                //details.Rowspan = 2;
                //customerdetails.AddCell(details);



                string condition = GetTORS();
                string consent = GetConsent();

                string declaration = condition;
                string warning = consent;

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(declaration, bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 5;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_JUSTIFIED;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(warning, boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 5;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_JUSTIFIED;
                customerdetails.AddCell(details);


                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Authorised Customer Signatory:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Date:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Utility Authorised Signatory:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Date:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("PRINTED BY:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(printedby.ToUpper(), bodyFont));
                details = new PdfPCell(content);
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                //paper margins
                content_cell = new PdfPCell(pdfTable);
                content_cell.Border = 1;
                content_cell.BorderWidth = 1f;
                content_cell.BorderWidthRight = 1f;
                content_cell.BorderWidthLeft = 1f;
                content_cell.PaddingTop = 10f;
                content_cell.PaddingLeft = 10f;
                content_cell.PaddingRight = 10f;
                content_table.AddCell(content_cell);

                content_cell = new PdfPCell(form_title);
                content_cell.Border = 0;
                content_cell.BorderWidthRight = 1f;
                content_cell.BorderWidthLeft = 1f;
                content_cell.PaddingLeft = 10f;
                content_cell.PaddingRight = 10f;
                content_cell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                content_table.AddCell(content_cell);

                content_cell = new PdfPCell(new iTextSharp.text.Paragraph("\r\n"));
                content_cell.Border = 0;
                content_cell.PaddingLeft = 10f;
                content_cell.PaddingRight = 10f;
                content_cell.BorderWidthRight = 1f;
                content_cell.BorderWidthLeft = 1f;
                content_table.AddCell(content_cell);

                content_cell = new PdfPCell(customerdetails);
                content_cell.Border = 2;
                content_cell.PaddingBottom = 10f;
                content_cell.PaddingLeft = 10f;
                content_cell.PaddingRight = 10f;
                content_cell.BorderWidth = 1f;
                content_cell.BorderWidthRight = 1f;
                content_cell.BorderWidthLeft = 1f;
                content_table.AddCell(content_cell);

                //content_table.DefaultCell.PaddingLeft = 5;



                try
                {
                    if (document.IsOpen() == false)
                    {
                        document.Open();
                        //document.Add(pdfTable);
                        //document.Add(form_title);
                        //document.Add(new iTextSharp.text.Paragraph("\r\n"));
                        //document.Add(customerdetails);
                        document.Add(content_table);

                    }
                }
                catch (Exception ex)
                {
                    //ShowMessage(ex.Message);
                    throw ex;

                }
                finally
                {
                    document.Close();
                    fs.Close();



                    //  Clears all content output from Buffer Stream
                    HttpContext.Current.Response.ClearContent();

                    //Clears all headers from Buffer Stream
                    HttpContext.Current.Response.ClearHeaders();

                    //Adds an HTTP header to the output stream
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(pdf));

                    //Gets or Sets the HTTP MIME type of the output stream
                    HttpContext.Current.Response.ContentType = "application/pdf";



                    //Writes the content of the specified file directory to an HTTP response output stream as a file block
                    HttpContext.Current.Response.WriteFile(pdf);

                    //sends all currently buffered output to the client
                    HttpContext.Current.Response.Flush();
                }
            }
            catch (Exception ex)
            {
                //ShowMessage("Generate Agreement: " + ex.Message);
                //throw ex;
                string err = ex.Message;
                output = err;
            }
            return output;
        }

        private string GetConsent()
        {
            string output = "";
            output = @"Declaration: I/We hereby give notice to the above Utility that I/We wish to have and agree that water supply and/or sewer service be connected to the above premises and further agree to be bound by general conditions attached and to pay for all water and sewerage services in accordance with the tariff in force from time to time.";
            return output;
        }

        private string GetTORS()
        {
            string output = "";
            output = @"CONDITIONS OF CONNECTION AND SUPPLY
1.SCOPE OF WORK
a)	Work on connection of supply which has to be undertaken by the customer will not start until permission is given by the Area Manager.
b)	All materials for the works must be certified by the Area Manager.
2.DETERMINATION OF CHARGES AND SUPPLY
Matters arising over provision, connection or associated charges shall be determined by the Utility statute and General condition of supply and the prevailing tariffs.
3.METER EQUIPMENT
The reading registered on a meter shall be conclusive evidence in the absence of fraud, of the value of consumption.Where a dispute arises as to the correctness of the meter readings, that dispute shall be determined on the application of either party to the dispute by the Area Manager and the decision of the Manager shall be final and binding on both parties.
4.CONNECTION LINES
Consumers are required to procure pipes and their accessories for connections of supply to their premises.Though the property belongs to consumer, the lines may be used to supply other customers.The Corporation shall fix a meter within this line.The meter remains the property of the Corporation.The consumer shall desist from tampering with the meter.If the meter is damaged a fine and / or the cost of its replacement will be imposed on the landlord.
5.MAINTENANCE OF CONNECTION LINES
It is the responsibility of the landlord to fix leaks and replace connection lines.Any excessive consumption arising from leaks after the meter has to be borne by the landlord.The Corporation reserves the right to disconnect supply if such leaks are not fixed.
6.WATER QUALITY
To sustain acceptable quality of water at the premises, the consumer is expected to regularly clean overhead tanks and all the other drawn off points within the house.
7.ILLEGAL CONNECTION
A customer who has defaulted and is disconnected from supply, is liable for a fine which is enforceable in courts of law if found illegally connected to the services.
8.TEMPORARY INTERRUPTION OF WATER SUPPLY
The Corporation may temporarily disconnect supply of water for purposes of maintenance and other works connected with the distribution network.
9.ACCESS TO CONSUMER’S PREMISES
Authorized staff with Corporation Identity Cards are entitled at all reasonable times to enter the premises for the purposes of inspection, meter reading, bill distribution and any other issues related to water and sewerage supply.If permission to enter is not granted, NWSC may apply to courts of law for a warrant.
10.DEPOSITS
NWSC may require the customer to make deposits equivalent to six months estimated consumption.Notwithstanding any such deposits, payment for water and sewerage services are due on demand.
11.DISCONNECTION
In the event of non-payment of charges, the Corporation shall be entitled to disconnect supply upon giving appropriate notice.Legal Action will then follow if payment is not effected two weeks after disconnection.";
            return output;
        }
        public string GetPDFBillFile(int AreaID, string CustRefx, string periodx, int number)
        {
            string output = "";
            try
            {
                string pdf = "";
                DatabaseHandler dh = new DatabaseHandler();

                string Period = periodx; string CustomerRef = CustRefx;
                int print_count = 0; int fail_count = 0; int total_count = 0;
                int BillNo = 0; string error = "";

                //Added on 15/12/2015
                DataTable dtprint = dh.GetBillDetailsByCustRef(AreaID, CustomerRef);
                if (dtprint.Rows.Count > 0)
                {
                    // Library.WriteErrorLog("Monthly Bill Available");
                    string CustRef = dtprint.Rows[0]["custref"].ToString();
                    string Propref = dtprint.Rows[0]["propertyref"].ToString();
                    string MeterRef = dtprint.Rows[0]["meterref"].ToString();
                    string MeterSize = dtprint.Rows[0]["metersize"].ToString();
                    string CustTarrif = dtprint.Rows[0]["custtarrif"].ToString();
                    int CustClass = Convert.ToInt16(dtprint.Rows[0]["custclass"].ToString());
                    int areaID = Convert.ToInt16(dtprint.Rows[0]["areaId"].ToString());
                    int BranchID = Convert.ToInt16(dtprint.Rows[0]["branchId"].ToString());
                    //get bill nos

                    DataTable dtbill = dh.GetCustBillNoByPeriod(CustRef, Period);
                    if (dtbill.Rows.Count > 0)
                    {

                        //Library.WriteErrorLog("second image");
                        //BillNo = 12;
                        foreach (DataRow dr in dtbill.Rows)
                        {
                            BillNo = int.Parse(dr["billNumber"].ToString());
                            if (BillNo == number)
                            {
                                try
                                {
                                    string[] Date = new string[5];
                                    string[] Location = new string[5];
                                    string[] ReceiptNo = new string[5];
                                    string[] Amount = new string[5];
                                    //Relationship manager details
                                    //DataTable rshpmngrs = dh.GetRelationshipManagerDetails(CustRef);
                                    //Customer and charging details
                                    DataTable custBillTable = dh.GetCustBill(CustRef, BillNo, areaID, Period);
                                    //Bill Readings
                                    DataTable custReadingsTable = dh.GetCustBillDetails(CustRef, BillNo, 2);
                                    //Payment Details
                                    DataTable PaymentsTable = dh.GetCustBillDetails(CustRef, BillNo, 1);
                                    int count = 0;
                                    while (PaymentsTable.Rows.Count > count)
                                    //for(int i = 0;i <)
                                    {
                                        Date[count] = PaymentsTable.Rows[count]["postdate"].ToString();
                                        Location[count] = PaymentsTable.Rows[count]["transname"].ToString();
                                        ReceiptNo[count] = PaymentsTable.Rows[count]["documentno"].ToString();
                                        double pay_amount = (Math.Round((double.Parse(PaymentsTable.Rows[count]["total"].ToString())), 0, MidpointRounding.AwayFromZero));
                                        Amount[count] = pay_amount.ToString("#,##0");
                                        count++;
                                    }


                                    string invoice_date = custBillTable.Rows[0]["date1"].ToString();
                                    string invoicenumber = custBillTable.Rows[0]["InvoiceNumber"].ToString();
                                    string area = custBillTable.Rows[0]["name3"].ToString();
                                    string custName = custBillTable.Rows[0]["name4"].ToString();
                                    string address = custBillTable.Rows[0]["name5"].ToString();
                                    string meterSerial = custBillTable.Rows[0]["name7"].ToString();
                                    string meterName = custBillTable.Rows[0]["name8"].ToString();
                                    string CustomerTarrif = custBillTable.Rows[0]["name6"].ToString();
                                    string branch = custBillTable.Rows[0]["namex"].ToString();
                                    //Capture the value as a double,Round it off and convert it to a string with commas
                                    /*double bal_bfwd = Math.Round((double.Parse(custBillTable.Rows[0]["number9"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string bal_bfwd1 = bal_bfwd.ToString("#,0.00");//changed from "#,##0" to "#,0.00" on 30/5/2021
                                    double pre_payments = Math.Round((double.Parse(custBillTable.Rows[0]["number10"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string pre_payments1 = pre_payments.ToString("#,##0");
                                    double adjustments = Math.Round((double.Parse(custBillTable.Rows[0]["number11"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string adjustments1 = adjustments.ToString("#,##0");
                                    double bal_as_at = Math.Round((double.Parse(custBillTable.Rows[0]["number12"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string bal_as_at1 = bal_as_at.ToString("#,0.00");//changed from "#,##0" to "#,0.00" on 30/5/2021
                                    double unit_water_charge = Math.Round((double.Parse(custBillTable.Rows[0]["number14"].ToString())), MidpointRounding.AwayFromZero);
                                    string unit_water_charge1 = unit_water_charge.ToString("#,##0");
                                    double water_charge = Math.Round((double.Parse(custBillTable.Rows[0]["number15"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string water_charge1 = water_charge.ToString("#,##0");
                                    string sewerpercentage = custBillTable.Rows[0]["number6"].ToString();
                                    string sewer_charge1 = "";
                                    double sewer = 0;
                                    string sewer_charge = custBillTable.Rows[0]["number18"].ToString();
                                    if (sewer_charge != "")
                                    {
                                        sewer = Math.Round((double.Parse(sewer_charge)), 0, MidpointRounding.AwayFromZero);
                                        sewer_charge1 = sewer.ToString("#,##0");
                                    }
                                    double service_charge = Math.Round((double.Parse(custBillTable.Rows[0]["number16"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string service_charge1 = service_charge.ToString("#,##0");
                                    double VAT = Math.Round((double.Parse(custBillTable.Rows[0]["number17"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string VAT1 = VAT.ToString("#,##0");
                                    double subtotal = Math.Round((double.Parse(custBillTable.Rows[0]["number19"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string subtotal1 = subtotal.ToString("#,##0");
                                    double BillTotal = Math.Round((double.Parse(custBillTable.Rows[0]["number8"].ToString())), 0, MidpointRounding.AwayFromZero);
                                    string BillTotal1 = BillTotal.ToString("#,0.00");//changed from "#,##0" to "#,0.00" on 30/5/2021;
                                    */
                                    //revised data 2 dps without round off
                                    double bal_bfwd = double.Parse(custBillTable.Rows[0]["number9"].ToString());
                                    string bal_bfwd1 = bal_bfwd.ToString("#,0.00");//changed from "#,##0" to "#,0.00" on 30/5/2021
                                    double pre_payments = double.Parse(custBillTable.Rows[0]["number10"].ToString());
                                    string pre_payments1 = pre_payments.ToString("#,0.00");
                                    double adjustments = double.Parse(custBillTable.Rows[0]["number11"].ToString());
                                    string adjustments1 = adjustments.ToString("#,0.00");
                                    double bal_as_at = double.Parse(custBillTable.Rows[0]["number12"].ToString());
                                    string bal_as_at1 = bal_as_at.ToString("#,0.00");//changed from "#,##0" to "#,0.00" on 30/5/2021
                                    double unit_water_charge = double.Parse(custBillTable.Rows[0]["number14"].ToString());
                                    string unit_water_charge1 = unit_water_charge.ToString("#,0.00");
                                    double water_charge = double.Parse(custBillTable.Rows[0]["number15"].ToString());
                                    string water_charge1 = water_charge.ToString("#,0.00");
                                    string sewerpercentage = custBillTable.Rows[0]["number6"].ToString();
                                    string sewer_charge1 = "";
                                    double sewer = 0;
                                    string sewer_charge = custBillTable.Rows[0]["number18"].ToString();
                                    if (sewer_charge != "")
                                    {
                                        sewer = double.Parse(sewer_charge);
                                        sewer_charge1 = sewer.ToString("#,0.00");
                                    }
                                    double service_charge = double.Parse(custBillTable.Rows[0]["number16"].ToString());
                                    string service_charge1 = service_charge.ToString("#,0.00");
                                    double VAT = double.Parse(custBillTable.Rows[0]["number17"].ToString());
                                    string VAT1 = VAT.ToString("#,0.00");
                                    double subtotal = double.Parse(custBillTable.Rows[0]["number19"].ToString());
                                    string subtotal1 = subtotal.ToString("#,0.00");
                                    double BillTotal = double.Parse(custBillTable.Rows[0]["number8"].ToString());
                                    string BillTotal1 = BillTotal.ToString("#,0.00");
                                    //end revised data                                   

                                    string curDate = custReadingsTable.Rows[0]["currdgdate"].ToString();
                                    string curRdg = custReadingsTable.Rows[0]["curreading"].ToString();
                                    string preDate = custReadingsTable.Rows[0]["prerdgdate"].ToString();
                                    string preRdg = custReadingsTable.Rows[0]["prereading"].ToString();
                                    string cons = custReadingsTable.Rows[0]["consumption"].ToString();
                                    string readstatus = custReadingsTable.Rows[0]["readstatus"].ToString();
                                    //new r/ship details
                                    string rmanager = custBillTable.Rows[0]["namey"].ToString();
                                    string mngrname = "", mngrphone = "", mngremail = "";



                                    string tel = ""; string contact1 = ""; string email = ""; string rship = ""; string Email = ""; string info = "";
                                    if (!rmanager.Equals("0"))
                                    {
                                        string[] param = Regex.Split(rmanager, ",,");
                                        tel = param[0].Trim();
                                        contact1 = param[2].Trim();
                                        email = param[1].Trim();

                                        // add relationship manager
                                        //info = file.GetContactPersonInfo();
                                        //rship = file.GetRelationShipManagerDetails(tel, contact1);
                                        // Email = file.GetManagerEmail(email);
                                        mngrname = contact1;
                                        mngrphone = tel;
                                        mngremail = email;
                                    }

                                    //contact details
                                    DataTable dtprofile = dh.GetCompanyProfile(AreaID.ToString());
                                    string companyname = "", email1 = "", address1 = "", tollcontact = "", website = "", othercontact = "", combinedaddress = "", combinedcontact = "", logofile = "";

                                    if (dtprofile.Rows.Count > 0)
                                    {
                                        companyname = dtprofile.Rows[0]["companyName"].ToString().ToUpper();
                                        address = dtprofile.Rows[0]["physicalAddress"].ToString();
                                        email = dtprofile.Rows[0]["emailAddress"].ToString();
                                        website = dtprofile.Rows[0]["webAddress"].ToString();
                                        tollcontact = dtprofile.Rows[0]["tollContact"].ToString();
                                        othercontact = dtprofile.Rows[0]["otherContact"].ToString();
                                        logofile = dtprofile.Rows[0]["logoPath"].ToString();
                                        combinedaddress = "Email:" + email + " " + "website:" + website;
                                        combinedcontact = "TollFree:" + tollcontact + " " + "other contact:" + othercontact;
                                    }
                                    //mngrname = "";
                                    //mngrphone = combinedcontact;
                                    //mngremail = combinedaddress;

                                    //Populate Methods in the File Formater Class....write to pdf logic
                                    //D:\TestFiles\billimages
                                    // string targetdir = @"\\10.0.1.10\\Application\\PDFBillImages\\billimages\\";
                                    string targetdir = @"C:\\Data\\Files\\PDFBillFiles\\";
                                    if (Directory.Exists(targetdir))
                                    {

                                    }
                                    else
                                    {
                                        Directory.CreateDirectory(targetdir);
                                    }

                                    pdf = targetdir + periodx + CustomerRef + BillNo + ".pdf";
                                    output = pdf;
                                    PdfPTable pdfTable = new PdfPTable(3);
                                    PdfPTable pdftablecustname = new PdfPTable(2);

                                    FileStream fs = new FileStream(pdf, FileMode.Create);
                                    Document pdfdocument = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                                    PdfWriter.GetInstance(pdfdocument, fs);
                                    pdfdocument.Open();

                                    pdfTable.DefaultCell.Padding = 3;
                                    pdfTable.WidthPercentage = 100;
                                    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                                    pdfTable.DefaultCell.BorderWidth = 0;
                                    pdfTable.DefaultCell.Border = 0;

                                    pdftablecustname.DefaultCell.Padding = 3;
                                    pdftablecustname.WidthPercentage = 100;
                                    pdftablecustname.HorizontalAlignment = Element.ALIGN_CENTER;
                                    pdftablecustname.DefaultCell.BorderWidth = 0;
                                    pdftablecustname.DefaultCell.Border = 0;

                                    BaseColor headercolor = new BaseColor(87, 145, 189);
                                    BaseColor white = new BaseColor(255, 255, 255);
                                    BaseColor fontcolor = new BaseColor(0, 0, 0);
                                    iTextSharp.text.Font headertextcolor = iTextSharp.text.FontFactory.GetFont("Times New Roman", 13, white);
                                    iTextSharp.text.Font textcolor = iTextSharp.text.FontFactory.GetFont("Times New Roman", 13, fontcolor);
                                    iTextSharp.text.Font bluetext = iTextSharp.text.FontFactory.GetFont("Times New Roman", 13, headercolor);


                                    PdfPCell to = new PdfPCell(new Phrase(custName, textcolor));
                                    to.Colspan = 2;
                                    to.PaddingLeft = 200;
                                    to.BackgroundColor = headercolor;
                                    pdftablecustname.AddCell(to);
                                    // to.BorderColor = iTextSharp.text.BaseColor.CYAN;



                                    to.BackgroundColor = headercolor;

                                    PdfPCell toheader = new PdfPCell(new Phrase("TO", headertextcolor));
                                    PdfPCell invoiceheader = new PdfPCell(new Phrase("INVOICE DATE", headertextcolor));
                                    PdfPCell custrefheader = new PdfPCell(new Phrase("CUSTOMER REFERENCE", headertextcolor));
                                    PdfPCell propertyheader = new PdfPCell(new Phrase("PROPERTY REFERENCE", headertextcolor));
                                    PdfPCell branchheader = new PdfPCell(new Phrase("IN RESPECT OF SERVICES AT", headertextcolor));
                                    PdfPCell meterdetalsheader = new PdfPCell(new Phrase("METER DETAILS", headertextcolor));
                                    PdfPCell basisheader = new PdfPCell(new Phrase("BASIS OF CHARGE", headertextcolor));
                                    PdfPCell toheadervalue = new PdfPCell(new Phrase(custName, textcolor));
                                    PdfPCell branchspan = new PdfPCell(new Phrase(address + " / " + branch, textcolor));


                                    invoiceheader.BackgroundColor = headercolor;
                                    toheader.BackgroundColor = headercolor;
                                    custrefheader.BackgroundColor = headercolor;
                                    propertyheader.BackgroundColor = headercolor;
                                    branchheader.BackgroundColor = headercolor;
                                    meterdetalsheader.BackgroundColor = headercolor;
                                    basisheader.BackgroundColor = headercolor;

                                    ///adding spans
                                    ///
                                    toheader.Colspan = 2;
                                    toheadervalue.Colspan = 2;
                                    toheadervalue.Rowspan = 3;

                                    branchspan.Colspan = 2;
                                    branchspan.Rowspan = 2;
                                    branchheader.Colspan = 2;



                                    /////////////
                                    invoiceheader.BackgroundColor = headercolor;
                                    ///adding cells
                                    pdfTable.AddCell(toheader);
                                    pdfTable.AddCell(invoiceheader);
                                    //second row
                                    pdfTable.AddCell(toheadervalue);
                                    pdfTable.AddCell(new PdfPCell(new Phrase(invoice_date)));

                                    ////
                                    pdfTable.AddCell(custrefheader);
                                    //custref
                                    pdfTable.AddCell(new PdfPCell(new Phrase(CustomerRef)));


                                    pdfTable.AddCell(branchheader);
                                    pdfTable.AddCell(new Paragraph(""));
                                    pdfTable.AddCell(branchspan);

                                    //property
                                    pdfTable.AddCell(propertyheader);
                                    //property value
                                    pdfTable.AddCell(new PdfPCell(new Phrase(Propref)));
                                    /////////////////////
                                    //Basis of charge
                                    pdfTable.AddCell(new Paragraph(""));
                                    pdfTable.AddCell(new Paragraph(""));
                                    pdfTable.AddCell(new Paragraph(""));

                                    basisheader.Colspan = 1;
                                    pdfTable.AddCell(basisheader);
                                    pdfTable.AddCell(new PdfPCell(new Phrase("")));
                                    pdfTable.AddCell(new PdfPCell(new Phrase("")));
                                    PdfPCell basiscell = new PdfPCell(new Phrase(CustomerTarrif, textcolor));
                                    basiscell.Colspan = 3;
                                    pdfTable.AddCell(basiscell);

                                    //////////
                                    ///////////////////////basis of charge and meter details different table
                                    meterdetalsheader.Colspan = 2;
                                    pdfTable.AddCell(meterdetalsheader);
                                    //
                                    string meter = meterName + "/" + meterSerial;
                                    //pdfTable.AddCell(new PdfPCell(new Phrase(meterSerial)));
                                    pdfTable.AddCell(new PdfPCell(new Phrase(meter)));

                                    //////////////////////next table with three columns

                                    PdfPTable readingstable = new PdfPTable(3);
                                    readingstable.DefaultCell.Padding = 3;
                                    readingstable.WidthPercentage = 100;
                                    readingstable.HorizontalAlignment = Element.ALIGN_CENTER;
                                    readingstable.DefaultCell.BorderWidth = 0;
                                    readingstable.DefaultCell.Border = 0;

                                    ////building readings table
                                    PdfPCell readblank = new PdfPCell(new Phrase("", headertextcolor));
                                    PdfPCell readdate = new PdfPCell(new Phrase("DATE READ", headertextcolor));
                                    PdfPCell readreading = new PdfPCell(new Phrase("READING", headertextcolor));

                                    //setting color to header of reading table
                                    readblank.BackgroundColor = headercolor;
                                    readdate.BackgroundColor = headercolor;
                                    readreading.BackgroundColor = headercolor;
                                    ///////add to table
                                    readingstable.AddCell(readblank);
                                    readingstable.AddCell(readdate);
                                    readingstable.AddCell(readreading);

                                    //done with current
                                    readingstable.AddCell(new PdfPCell(new Phrase("CURRENT", textcolor)));
                                    readingstable.AddCell(new PdfPCell(new Phrase(curDate, textcolor)));
                                    readingstable.AddCell(new PdfPCell(new Phrase(curRdg, textcolor)));

                                    //done with previous
                                    readingstable.AddCell(new PdfPCell(new Phrase("PREVIOUS", textcolor)));
                                    readingstable.AddCell(new PdfPCell(new Phrase(preDate, textcolor)));
                                    readingstable.AddCell(new PdfPCell(new Phrase(preRdg, textcolor)));

                                    //done with Consumption
                                    /*string mycons = "CONSUMPTION" + "       " + readstatus;
                                    PdfPCell conscell = new PdfPCell(new Phrase(mycons, textcolor));
                                    conscell.Colspan = 2;
                                    readingstable.AddCell(conscell);
                                    readingstable.AddCell(new PdfPCell(new Phrase(cons, textcolor)));*/
                                    //consumption modified
                                    readingstable.AddCell(new PdfPCell(new Phrase("CONSUMPTION", textcolor)));
                                    readingstable.AddCell(new PdfPCell(new Phrase(readstatus, textcolor)));
                                    readingstable.AddCell(new PdfPCell(new Phrase(cons, textcolor)));


                                    /////////charge table and the second last
                                    PdfPTable chargetable = new PdfPTable(2);
                                    chargetable.DefaultCell.Padding = 3;
                                    chargetable.WidthPercentage = 100;
                                    chargetable.HorizontalAlignment = Element.ALIGN_CENTER;
                                    chargetable.DefaultCell.BorderWidth = 0;
                                    chargetable.DefaultCell.Border = 0;

                                    ////building readings table
                                    PdfPCell chargedetail = new PdfPCell(new Phrase("CHARGING DETAILS", headertextcolor));
                                    PdfPCell chargeamount = new PdfPCell(new Phrase("AMOUNT USHS", headertextcolor));

                                    //setting color to header of reading table
                                    chargedetail.BackgroundColor = headercolor;
                                    chargeamount.BackgroundColor = headercolor;
                                    chargetable.AddCell(chargedetail);
                                    chargetable.AddCell(chargeamount);
                                    //finished adding headers

                                    //adding values
                                    chargetable.AddCell(new PdfPCell(new Phrase("Balance B/Fwd from previous Invoice", textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(bal_bfwd1, textcolor)));
                                    //
                                    chargetable.AddCell(new PdfPCell(new Phrase("Payments since Prev Invoice", textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(pre_payments1, textcolor)));

                                    //
                                    chargetable.AddCell(new PdfPCell(new Phrase("Adjustments since Prev Invoice", textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(adjustments1, textcolor)));

                                    //
                                    chargetable.AddCell(new PdfPCell(new Phrase("B/Fwd as at " + invoice_date, textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(bal_as_at1, textcolor)));

                                    //
                                    PdfPCell emptytwocolumn = new PdfPCell(new Phrase("", textcolor));
                                    emptytwocolumn.Colspan = 2;
                                    chargetable.AddCell(emptytwocolumn);

                                    //
                                    chargetable.AddCell(new PdfPCell(new Phrase("WATER " + cons + "m3 at " + unit_water_charge1 + "/=", textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(water_charge1, textcolor)));


                                    //

                                    //
                                    //

                                    if ((sewerpercentage.Equals("")) && (CustClass == 2))
                                    {

                                        sewerpercentage = "75%";
                                    }

                                    else if ((sewerpercentage.Equals("")) && (CustClass != 2))
                                    {

                                        sewerpercentage = "100%";
                                    }

                                    chargetable.AddCell(new PdfPCell(new Phrase("SEWERAGE at " + sewerpercentage + " of WATER CHARGES", textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(sewer_charge1, textcolor)));



                                    //

                                    //
                                    chargetable.AddCell(new PdfPCell(new Phrase("Service Charge", textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(service_charge1, textcolor)));

                                    //
                                    chargetable.AddCell(new PdfPCell(new Phrase("VAT", textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(VAT1, textcolor)));

                                    //
                                    chargetable.AddCell(new PdfPCell(new Phrase("Sub Total", textcolor)));
                                    chargetable.AddCell(new PdfPCell(new Phrase(subtotal1, textcolor)));

                                    PdfPCell totalamountcell = new PdfPCell(new Phrase("Total Amount Due", headertextcolor));

                                    //setting color to header of reading table
                                    totalamountcell.BackgroundColor = headercolor;
                                    chargetable.AddCell(totalamountcell);
                                    chargetable.AddCell(new PdfPCell(new Phrase(BillTotal1, textcolor)));

                                    //add payments
                                    /////////charge table and the second last
                                    PdfPTable paymenttable = new PdfPTable(1);
                                    paymenttable.DefaultCell.Padding = 3;
                                    paymenttable.WidthPercentage = 100;
                                    paymenttable.HorizontalAlignment = Element.ALIGN_CENTER;
                                    paymenttable.DefaultCell.BorderWidth = 0;
                                    paymenttable.DefaultCell.Border = 0;

                                    ////building readings table
                                    PdfPCell paymentdetail = new PdfPCell(new Phrase("PAYMENT DETAILS", headertextcolor));


                                    //setting color to header of payment table
                                    paymentdetail.BackgroundColor = headercolor;
                                    paymenttable.AddCell(paymentdetail);
                                    //add values
                                    //Space For getpayments 
                                    string payments = GetPayments(Date, Location, ReceiptNo, Amount);
                                    paymenttable.AddCell(new PdfPCell(new Phrase(payments, textcolor)));

                                    //finished adding headers
                                    //end payments
                                    PdfPTable noticetable = new PdfPTable(1);
                                    noticetable.DefaultCell.Padding = 3;
                                    noticetable.WidthPercentage = 100;
                                    noticetable.HorizontalAlignment = Element.ALIGN_CENTER;
                                    noticetable.DefaultCell.BorderWidth = 0;
                                    noticetable.DefaultCell.Border = 0;


                                    PdfPCell notice1 = new PdfPCell(new Phrase("Please pay your bill to avoid interruption of services.", headertextcolor));

                                    PdfPCell notice2 = new PdfPCell(new Phrase("Clean your tank every month ", headertextcolor));

                                    PdfPCell notice3 = new PdfPCell(new Phrase("Report any suspected unauthorized usage of water to our confidential line", headertextcolor));

                                    notice1.Border = Rectangle.NO_BORDER;
                                    notice1.BackgroundColor = headercolor;

                                    notice2.Border = Rectangle.NO_BORDER;
                                    notice2.BackgroundColor = headercolor;

                                    notice3.Border = Rectangle.NO_BORDER;
                                    notice3.BackgroundColor = headercolor;

                                    noticetable.AddCell(notice1);
                                    noticetable.AddCell(notice2);
                                    noticetable.AddCell(notice3);
                                    //ADD VALUES ADD NEW KLINE


                                    iTextSharp.text.Font bigtext = iTextSharp.text.FontFactory.GetFont("Times New Roman", 18, fontcolor);
                                    Paragraph header = new Paragraph(new Phrase(companyname, bigtext));
                                    Paragraph add = new Paragraph(new Phrase(combinedaddress, textcolor));
                                    Paragraph contact = new Paragraph(new Phrase(combinedcontact, textcolor));
                                    Paragraph invoicedetails = new Paragraph(new Phrase("TAX INVOICE FOR WATER & SEWERAGE SERVICES", textcolor));

                                    header.Alignment = Element.ALIGN_CENTER;

                                    add.Alignment = Element.ALIGN_CENTER;

                                    contact.Alignment = Element.ALIGN_CENTER;
                                    invoicedetails.Alignment = Element.ALIGN_CENTER;

                                    //pdfdocument.Add(new Paragraph("\r\n"));

                                    /////////////adding image and 
                                    PdfPTable headtable = new PdfPTable(2);

                                    float[] widths = new float[] { 100f, 400f };
                                    headtable.SetWidths(widths);
                                    headtable.DefaultCell.Padding = 3;
                                    headtable.WidthPercentage = 100;
                                    headtable.HorizontalAlignment = Element.ALIGN_CENTER;
                                    headtable.DefaultCell.BorderWidth = 0;
                                    headtable.DefaultCell.Border = Rectangle.BOX;

                                    PdfPCell imagecell = new PdfPCell(new Phrase("", headertextcolor));

                                    imagecell.Rowspan = 3;
                                    imagecell.Border = Rectangle.NO_BORDER;
                                    //String path1 = Path.Combine(Environment.CurrentDirectory, @"icons\logoimg2.png");
                                    //String path=System.Reflection.Assembly.GetEntryAssembly().Location;
                                    //String path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                                    String path = AppDomain.CurrentDomain.BaseDirectory;
                                    //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);    
                                    //string mylogo = @"icons\"+ logofile;
                                    //string headlogo = Path.Combine(path, @"icons/tracebilllogo.png");
                                    //string headlogo = Path.Combine(path, mylogo);
                                    string headlogo = Path.Combine(path, @"icons\" + logofile);
                                    //String logo = HttpContext.Current.Server.MapPath("~/Images/") + "nswc_logo.png";
                                    iTextSharp.text.Image headimage = iTextSharp.text.Image.GetInstance(headlogo);
                                    headimage.ScaleAbsolute(50f, 50f);

                                    headimage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;


                                    // imagecell.AddElement(headimage);

                                    Paragraph managername = new Paragraph("Name: " + mngrname);
                                    Paragraph managerphone = new Paragraph("Tel: " + mngrphone);
                                    Paragraph manageremail = new Paragraph("Email: " + mngremail);
                                    //add fiscal details
                                    string invoicecombination = "";

                                    string str = "";
                                    // str = "Test FDN";
                                    str = invoicecombination;
                                    //Paragraph fiscal = new Paragraph(str);
                                    PdfPCell tinbox = new PdfPCell(new Phrase("eTAX TIN: 1000023440 ", bluetext));
                                    tinbox.Border = Rectangle.NO_BORDER;
                                    //PdfPCell fiscalbox = new PdfPCell(new Phrase(str, bluetext));
                                    //fiscalbox.Border = Rectangle.NO_BORDER;
                                    //headtable.AddCell(imagecell);
                                    //headtable.AddCell(managername);
                                    // headtable.AddCell(managerphone);
                                    // headtable.AddCell(manageremail);
                                    // headtable.AddCell(fiscalbox);
                                    // headtable.AddCell(tinbox);

                                    //Paragraph tinboxval = new Paragraph(new Phrase("TAX INVOICE FOR WATER & SEWERAGE SERVICES", bluetext));
                                    Paragraph tinboxfiscal = new Paragraph(new Phrase(str, bluetext));
                                    PdfPTable tintable = new PdfPTable(2);
                                    float[] widthtin = new float[] { 90f, 500f };
                                    tintable.SetWidths(widths);
                                    tintable.DefaultCell.Padding = 3;
                                    tintable.WidthPercentage = 100;
                                    tintable.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tintable.DefaultCell.BorderWidth = 0;
                                    tintable.DefaultCell.Border = Rectangle.BOX;
                                    tintable.AddCell("");
                                    //tintable.AddCell(fiscal);//commented out
                                    //tintable.AddCell(tinboxval);
                                    //tintable.AddCell(tinboxfiscal);
                                    pdfdocument.Add(headimage);
                                    pdfdocument.Add(header);
                                    pdfdocument.Add(add);
                                    pdfdocument.Add(contact);
                                    pdfdocument.Add(invoicedetails);
                                    pdfdocument.Add(new Paragraph("\r\n"));
                                    //pdfdocument.Add(new Paragraph("Relationship Contact Person"));
                                    // pdfdocument.Add(new Paragraph("\r\n"));
                                    pdfdocument.Add(headtable);
                                    // pdfdocument.Add(new Paragraph("\r\n"));
                                    // pdfdocument.Add(tintable);

                                    /////
                                    //  String path=Path.Combine(Environment.CurrentDirectory, @"icons\nwsc.png");

                                    String logo = Path.Combine(path, @"icons\tracebilllogo.png");

                                    // String logo = path;
                                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(logo);
                                    image.ScaleToFit(300, 300);


                                    image.Alignment = iTextSharp.text.Image.UNDERLYING;
                                    image.SetAbsolutePosition(100, 280);

                                    pdfdocument.Add(image);
                                    //pdfdocument.Add(pdftablecustname);
                                    pdfdocument.Add(new Paragraph("\r\n"));
                                    pdfdocument.Add(pdfTable);
                                    pdfdocument.Add(new Paragraph("\r\n"));
                                    pdfdocument.Add(readingstable);
                                    pdfdocument.Add(new Paragraph("\r\n"));
                                    pdfdocument.Add(chargetable);
                                    pdfdocument.Add(new Paragraph("\r\n"));
                                    pdfdocument.Add(paymenttable);
                                    pdfdocument.Add(new Paragraph("\r\n"));
                                    pdfdocument.Add(noticetable);
                                    //
                                    if (Directory.Exists(targetdir))
                                    {

                                    }
                                    else
                                    {
                                        Directory.CreateDirectory(targetdir);
                                    }
                                    pdfdocument.Close();
                                    fs.Close();
                                    //  Clears all content output from Buffer Stream
                                     HttpContext.Current.Response.ClearContent();

                                     //Clears all headers from Buffer Stream
                                     HttpContext.Current.Response.ClearHeaders();

                                     //Adds an HTTP header to the output stream
                                     HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(pdf));

                                     //Gets or Sets the HTTP MIME type of the output stream
                                     HttpContext.Current.Response.ContentType = "application/pdf";



                                     //Writes the content of the specified file directory to an HTTP response output stream as a file block
                                     HttpContext.Current.Response.WriteFile(pdf);

                                     //sends all currently buffered output to the client
                                     HttpContext.Current.Response.Flush();
                                     
                                }
                                catch (Exception ex)
                                {
                                    throw ex;

                                    // Library.WriteErrorLog("error" + ex.ToString());
                                }
                                //finally
                                //{
                                //    pdfdocument.Close();
                                //    fs.Close();



                                //}
                            }//end if billno
                        }//end for loop
                    }

                }
                else
                {
                    Console.WriteLine("No Bills Available");
                    //Library.WriteErrorLog("No Bills Available");
                }
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            return output;
        }
        public string GetPayments(string[] date, string[] _location, string[] receiptNo, string[] amount)
        {
            string payments = "";
            string location = "";
            string receiptnumber = "";

            int count = 0;
            while (count < date.Length)
            {
                //if (!(count > 4))
                //{
                if (date[count] != null)
                {
                    int intspaces = 11 - date[count].Length;
                    string spaces = "";
                    for (int i = 0; i < intspaces; i++)
                    {
                        spaces += " ";
                    }
                    string spaces1 = "";
                    if (_location[count].Length > 16)
                        location = _location[count].Substring(0, 16);
                    else
                        location = _location[count];
                    int intspaces1 = 28 - (location.Length + intspaces + date[count].Length);
                    for (int i = 0; i < intspaces1; i++)
                    {
                        spaces1 += " ";
                    }
                    if (receiptNo[count].Length > 8)
                        receiptnumber = receiptNo[count].Substring(0, 8);
                    else
                        receiptnumber = receiptNo[count];
                    int intspaces2 = (21 - amount[count].Length) - receiptnumber.Length;
                    string spaces2 = "";
                    for (int i = 0; i < intspaces2; i++)
                    {
                        spaces2 += " ";
                    }


                    string date_loc = date[count] + spaces + location + spaces1 + receiptnumber + spaces2 + amount[count] + "\r\n";
                    payments += date_loc;
                    //count++;
                }
                else
                    payments += "." + "\r\n";
                //}
                count += 1;
            }
            //payments += "\r\n";
        
            return payments;

        }


        public string GetInvoiceForm(DataTable dt, DataTable dtprofile, string user)
        {
            string output = "";
            try
            {
                string pdf = "";
                string printedby = user;
                string filename = "Invoice_" + dt.Rows[0]["paymentRef"].ToString().Replace("/", "") + "_" + DateTime.Now.ToString("yyMMdd HH:mm").Replace(":", "") + ".pdf";
                //bool IsMonthly = false;//Convert.ToBoolean(dt.Rows[0]["Monthly"].ToString());

                string targetdirectory = @"C:\Data\Files\";
                if (!Directory.Exists("targetdirectory"))
                {
                    Directory.CreateDirectory(targetdirectory);
                }
                //pdf = targetdirectory + filename;
                pdf = Path.Combine(targetdirectory, filename);
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, -5f, -5f, 15f, -5f);

                FileStream fs = new FileStream(pdf, FileMode.Create);
                PdfWriter.GetInstance(document, fs);

                iTextSharp.text.BaseColor fontcolor = new iTextSharp.text.BaseColor(0, 0, 0);
                var titleFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 12, iTextSharp.text.Font.BOLD, fontcolor);
                var subTitleFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 10, iTextSharp.text.Font.NORMAL, fontcolor);
                var boldTitleFont = iTextSharp.text.FontFactory.GetFont("Verdana", 10, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, fontcolor);
                var boldTextFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.BOLD, fontcolor);
                var endingMessageFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.ITALIC, fontcolor);
                var bodyFont = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.NORMAL, fontcolor);
                var bodyFontUnderlined = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, iTextSharp.text.Font.NORMAL | iTextSharp.text.Font.UNDERLINE, fontcolor);

                PdfPTable content_table = new PdfPTable(1);
                PdfPCell content_cell = new PdfPCell();

                float[] columnWidths = { 0f, 2f, 8f };

                PdfPTable pdfTable = new PdfPTable(columnWidths);
                PdfPCell cell = new PdfPCell();
                cell.Border = 0;
                pdfTable.AddCell(cell);

                //format the logo
                String path = AppDomain.CurrentDomain.BaseDirectory;
                //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);                        
                //string logo = Path.Combine(path, @"icons/logoimg2.png");//tracebilllogo
                string logo = Path.Combine(path, @"icons/tracebilllogo.png");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(logo);
                image.ScaleAbsolute(80f, 60f);
                image.Alignment = iTextSharp.text.Image.ALIGN_JUSTIFIED_ALL;
                cell = new PdfPCell(image);
                cell.Border = 0;
                pdfTable.AddCell(cell); //add logo to Header table

                PdfPTable nestedTable = new PdfPTable(2);
                PdfPCell nestedCell = new PdfPCell();
                ///company setup
                string companyname = "", email = "", address = "", tollcontact = "", website = "", othercontact = "", combinedaddress = "", combinedcontact = "";
                if (dtprofile.Rows.Count > 0)
                {
                    companyname = dtprofile.Rows[0]["companyName"].ToString().ToUpper();
                    address = dtprofile.Rows[0]["physicalAddress"].ToString();
                    email = dtprofile.Rows[0]["emailAddress"].ToString();
                    website = dtprofile.Rows[0]["webAddress"].ToString();
                    tollcontact = dtprofile.Rows[0]["tollContact"].ToString();
                    othercontact = dtprofile.Rows[0]["otherContact"].ToString();
                    combinedaddress = "Email:" + email + " " + "website:" + website;
                    combinedcontact = "TollFree:" + tollcontact + " " + "other contact:" + othercontact;
                }
                ///format header text
                ///company name
                iTextSharp.text.Paragraph header_title = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(companyname, titleFont));
                header_title.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedCell = new PdfPCell(header_title);
                nestedCell.Border = 0;
                nestedCell.Colspan = 2;
                nestedCell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedTable.AddCell(nestedCell);

                //company address
                iTextSharp.text.Paragraph company_addr = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(address, subTitleFont));
                company_addr.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedCell = new PdfPCell(company_addr);
                nestedCell.Border = 0;
                nestedCell.Colspan = 2;
                nestedCell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedTable.AddCell(nestedCell);

                //company contacts
                iTextSharp.text.Paragraph company_addr1 = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(combinedcontact, subTitleFont));
                company_addr1.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedCell = new PdfPCell(company_addr1);
                nestedCell.Border = 0;
                nestedCell.Colspan = 2;
                nestedCell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedTable.AddCell(nestedCell);

                //company emails
                iTextSharp.text.Paragraph company_addr2 = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(combinedaddress, subTitleFont));
                company_addr2.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedCell = new PdfPCell(company_addr2);
                nestedCell.Border = 0;
                nestedCell.Colspan = 2;
                nestedCell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                nestedTable.AddCell(nestedCell);

                cell = new PdfPCell(nestedTable);
                cell.Colspan = 2;
                cell.Border = 0;
                cell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                pdfTable.AddCell(cell);

                iTextSharp.text.Paragraph form_title = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("NEW CONNECTION APPLICATION FOAM", boldTitleFont));
                form_title.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                //customer details
                float[] columnWidths1 = { 1.5f, 1.5f, 1.5f, 2f, 3f };
                PdfPTable customerdetails = new PdfPTable(columnWidths1);
                iTextSharp.text.Paragraph content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Serial No:", boldTextFont));
                PdfPCell details = new PdfPCell(content);
                details.Border = 0;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("SN" + dt.Rows[0]["ApplicationID"].ToString(), boldTextFont));
                details = new PdfPCell(content);
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 0;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Print Date:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("I/We Hereby request for Service Connection of: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["typeName"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);
                //connection type
                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("For Connection type: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["className"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);


                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Block Number:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("N/A", bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Application Reference:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["paymentRef"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Border = 15;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 1;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Customer Name ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["fullName"].ToString().ToUpper(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 1;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Of (Customer Address) ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["address"].ToString(), bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                //country and state
                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Located within: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);
                string location = dt.Rows[0]["countryName"].ToString() + "/" + dt.Rows[0]["areaName"].ToString();
                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(location, bodyFont));
                details = new PdfPCell(content);
                details.Border = 15;
                details.Colspan = 3;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);
                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Office Phone Number: ", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(tollcontact, bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Border = 15;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                //content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Customer Phone Number: ", boldTextFont));
                //details = new PdfPCell(content);
                //details.Border = 0;
                //details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                //customerdetails.AddCell(details);

                //content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(dt.Rows[0]["contact"].ToString(), bodyFont));
                //details = new PdfPCell(content);
                //details.Border = 0;
                //details.Border = 15;
                //details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                //customerdetails.AddCell(details);

                //details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                //details.Border = 0;
                //details.Colspan = 5;
                //details.Rowspan = 2;
                //customerdetails.AddCell(details);



                string condition = "";//GetTORS();
                string consent = "";//GetConsent();

                string declaration = condition;
                string warning = consent;

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(declaration, bodyFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 5;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_JUSTIFIED;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(warning, boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 5;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_JUSTIFIED;
                customerdetails.AddCell(details);


                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Authorised Customer Signatory:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Date:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Utility Authorised Signatory:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("Date:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_RIGHT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase("PRINTED BY:", boldTextFont));
                details = new PdfPCell(content);
                details.Border = 0;
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                content = new iTextSharp.text.Paragraph(new iTextSharp.text.Phrase(printedby.ToUpper(), bodyFont));
                details = new PdfPCell(content);
                details.Colspan = 2;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell();
                details.Border = 0;
                details.HorizontalAlignment = iTextSharp.text.Image.ALIGN_LEFT;
                customerdetails.AddCell(details);

                details = new PdfPCell(new iTextSharp.text.Paragraph("\n"));
                details.Border = 0;
                details.Colspan = 5;
                details.Rowspan = 2;
                customerdetails.AddCell(details);

                //paper margins
                content_cell = new PdfPCell(pdfTable);
                content_cell.Border = 1;
                content_cell.BorderWidth = 1f;
                content_cell.BorderWidthRight = 1f;
                content_cell.BorderWidthLeft = 1f;
                content_cell.PaddingTop = 10f;
                content_cell.PaddingLeft = 10f;
                content_cell.PaddingRight = 10f;
                content_table.AddCell(content_cell);

                content_cell = new PdfPCell(form_title);
                content_cell.Border = 0;
                content_cell.BorderWidthRight = 1f;
                content_cell.BorderWidthLeft = 1f;
                content_cell.PaddingLeft = 10f;
                content_cell.PaddingRight = 10f;
                content_cell.HorizontalAlignment = iTextSharp.text.Image.ALIGN_CENTER;
                content_table.AddCell(content_cell);

                content_cell = new PdfPCell(new iTextSharp.text.Paragraph("\r\n"));
                content_cell.Border = 0;
                content_cell.PaddingLeft = 10f;
                content_cell.PaddingRight = 10f;
                content_cell.BorderWidthRight = 1f;
                content_cell.BorderWidthLeft = 1f;
                content_table.AddCell(content_cell);

                content_cell = new PdfPCell(customerdetails);
                content_cell.Border = 2;
                content_cell.PaddingBottom = 10f;
                content_cell.PaddingLeft = 10f;
                content_cell.PaddingRight = 10f;
                content_cell.BorderWidth = 1f;
                content_cell.BorderWidthRight = 1f;
                content_cell.BorderWidthLeft = 1f;
                content_table.AddCell(content_cell);

                //content_table.DefaultCell.PaddingLeft = 5;



                try
                {
                    if (document.IsOpen() == false)
                    {
                        document.Open();
                        //document.Add(pdfTable);
                        //document.Add(form_title);
                        //document.Add(new iTextSharp.text.Paragraph("\r\n"));
                        //document.Add(customerdetails);
                        document.Add(content_table);

                    }
                }
                catch (Exception ex)
                {
                    //ShowMessage(ex.Message);
                    throw ex;

                }
                finally
                {
                    document.Close();
                    fs.Close();



                    //  Clears all content output from Buffer Stream
                    HttpContext.Current.Response.ClearContent();

                    //Clears all headers from Buffer Stream
                    HttpContext.Current.Response.ClearHeaders();

                    //Adds an HTTP header to the output stream
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(pdf));

                    //Gets or Sets the HTTP MIME type of the output stream
                    HttpContext.Current.Response.ContentType = "application/pdf";



                    //Writes the content of the specified file directory to an HTTP response output stream as a file block
                    HttpContext.Current.Response.WriteFile(pdf);

                    //sends all currently buffered output to the client
                    HttpContext.Current.Response.Flush();
                }
            }
            catch (Exception ex)
            {
                //ShowMessage("Generate Agreement: " + ex.Message);
                //throw ex;
                string err = ex.Message;
                output = err;
            }
            return output;
        }
        //get statement
        public string GetStatementPrint(string custref, string fromdate, string todate, string user,DataTable dtstmt)
        {
            string output = "";
            try
            {
                string pdf = "";
               
               // DateTime startdate = Convert.ToDateTime(fromdate);
                //DateTime enddate = Convert.ToDateTime(todate);
                string AreaID = "10";
                //DataTable dtprint = dh.GetStatement(custref, startdate, enddate);
                DataTable dtprint = dtstmt;
                if (dtprint.Rows.Count > 0)
                {
                    //first details
                    string CustRef = dtprint.Rows[0]["customerRef"].ToString();
                    string Propref = dtprint.Rows[0]["PropertyRef"].ToString();
                    string custname = dtprint.Rows[0]["customerName"].ToString();
                    string custaddress = dtprint.Rows[0]["address"].ToString();
                    string stdate = dtprint.Rows[0]["startDate"].ToString();
                    string eddate = dtprint.Rows[0]["endDate"].ToString();
                    string openbal = dtprint.Rows[0]["OpenBal"].ToString();
                    //contact details
                    DataTable dtprofile = dh.GetCompanyProfile(AreaID.ToString());
                    string companyname = "", email = "", address = "", tollcontact = "", website = "", othercontact = "", combinedaddress = "", combinedcontact = "", logofile = "";

                    if (dtprofile.Rows.Count > 0)
                    {
                        companyname = dtprofile.Rows[0]["companyName"].ToString().ToUpper();
                        address = dtprofile.Rows[0]["physicalAddress"].ToString();
                        email = dtprofile.Rows[0]["emailAddress"].ToString();
                        website = dtprofile.Rows[0]["webAddress"].ToString();
                        tollcontact = dtprofile.Rows[0]["tollContact"].ToString();
                        othercontact = dtprofile.Rows[0]["otherContact"].ToString();
                        logofile = dtprofile.Rows[0]["logoPath"].ToString();
                        combinedaddress = "Email:" + email + " " + "website:" + website;
                        combinedcontact = "TollFree:" + tollcontact + " " + "other contact:" + othercontact;
                    }
                    //mngrname = "";
                    //mngrphone = combinedcontact;
                    //mngremail = combinedaddress;

                    //Populate Methods in the File Formater Class....write to pdf logic
                    //D:\TestFiles\billimages
                    // string targetdir = @"\\10.0.1.10\\Application\\PDFBillImages\\billimages\\";
                    string targetdir = @"D:\\Data\\Files\\PDFStatements\\";
                    if (Directory.Exists(targetdir))
                    {

                    }
                    else
                    {
                        Directory.CreateDirectory(targetdir);
                    }
                    string fileName = CustRef + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    pdf = targetdir + fileName + ".pdf";
                    output = pdf;
                    PdfPTable pdfTable = new PdfPTable(3);
                    PdfPTable pdftablecustname = new PdfPTable(2);

                    FileStream fs = new FileStream(pdf, FileMode.Create);
                    Document pdfdocument = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdocument, fs);
                    pdfdocument.Open();

                    pdfTable.DefaultCell.Padding = 3;
                    pdfTable.WidthPercentage = 100;
                    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable.DefaultCell.BorderWidth = 0;
                    pdfTable.DefaultCell.Border = 0;

                    pdftablecustname.DefaultCell.Padding = 3;
                    pdftablecustname.WidthPercentage = 100;
                    pdftablecustname.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdftablecustname.DefaultCell.BorderWidth = 0;
                    pdftablecustname.DefaultCell.Border = 0;

                    BaseColor headercolor = new BaseColor(87, 145, 189);
                    BaseColor white = new BaseColor(255, 255, 255);
                    BaseColor fontcolor = new BaseColor(0, 0, 0);
                    iTextSharp.text.Font headertextcolor = iTextSharp.text.FontFactory.GetFont("Times New Roman", 13, white);
                    iTextSharp.text.Font textcolor = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, fontcolor);
                    iTextSharp.text.Font bluetext = iTextSharp.text.FontFactory.GetFont("Times New Roman", 13, headercolor);

                    //customer table
                    PdfPTable customertable = new PdfPTable(1);
                    customertable.DefaultCell.Padding = 3;
                    customertable.WidthPercentage = 100;
                    customertable.HorizontalAlignment = Element.ALIGN_CENTER;
                    customertable.DefaultCell.BorderWidth = 0;
                    customertable.DefaultCell.Border = 0;

                    ////building readings table
                    PdfPCell custdetail = new PdfPCell(new Phrase("CUSTOMER DETAILS", headertextcolor));


                    //setting color to header of payment table
                    custdetail.BackgroundColor = headercolor;
                    customertable.AddCell(custdetail);
                    //add values
                    //Space For getpayments 
                    string custstr = "CUSTOMER NAME:" + custname + "                      " + "CUSTREF:" + CustRef + "\r\n" +
                        "PROPERTY REF:" + Propref + "\r\n" +
                        "ADDRESS:" + custaddress;
                    customertable.AddCell(new PdfPCell(new Phrase(custstr, textcolor)));
                    //issueing officer
                    PdfPTable officertable = new PdfPTable(1);
                    officertable.DefaultCell.Padding = 3;
                    officertable.WidthPercentage = 100;
                    officertable.HorizontalAlignment = Element.ALIGN_CENTER;
                    officertable.DefaultCell.BorderWidth = 0;
                    officertable.DefaultCell.Border = 0;

                    ////building readings table
                    PdfPCell officerdetail = new PdfPCell(new Phrase("OFFICER DETAILS", headertextcolor));


                    //setting color to header of payment table
                    officerdetail.BackgroundColor = headercolor;
                    officertable.AddCell(officerdetail);
                    //add values
                    //Space For getpayments 
                    DateTime printdate = DateTime.Now;
                    string officerstr = "PRINTED BY:" + user + "                       " + "PRINT DATE:" + printdate.ToString() + "\r\n" +
                        "SIGN:" + "...........................";
                    officertable.AddCell(new PdfPCell(new Phrase(officerstr, textcolor)));


                    //add payments
                    /////////charge table and the second last
                    PdfPTable paymenttable = new PdfPTable(1);
                    paymenttable.DefaultCell.Padding = 3;
                    paymenttable.WidthPercentage = 100;
                    paymenttable.HorizontalAlignment = Element.ALIGN_CENTER;
                    paymenttable.DefaultCell.BorderWidth = 0;
                    paymenttable.DefaultCell.Border = 0;

                    ////building readings table
                    PdfPCell paymentdetail = new PdfPCell(new Phrase("STATEMENT DETAILS", headertextcolor));


                    //setting color to header of payment table
                    paymentdetail.BackgroundColor = headercolor;
                    paymenttable.AddCell(paymentdetail);
                  
                    //add data table
                    //document.Open();
                    // iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);
                    // iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);
                    iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont("Times New Roman", 8, fontcolor);
                    // DataTable dt = dtprint;
                    DataTable dt = bll.GetStatementData(dtprint);
                    string outstandingbal = dh.GetOutstandingBalance(dt);

                    //Space For getpayments 
                    string payments = "FROM:" + stdate + "                      " + "TO:" + eddate + "\r\n" +
                        "Opening Balance:  " + double.Parse(openbal).ToString("#,##0") + "\r\n" +
                        "Outstanding Balance:   " + outstandingbal;
                    paymenttable.AddCell(new PdfPCell(new Phrase(payments, textcolor)));
                    PdfPTable table = new PdfPTable(dt.Columns.Count);
                    PdfPRow row = null;
                    float[] widths = new float[] { 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f };

                    table.SetWidths(widths);

                    table.WidthPercentage = 100;
                    int iCol = 0;
                    string colname = "";
                    PdfPCell cell = new PdfPCell(new Phrase("Products"));

                    cell.Colspan = dt.Columns.Count;

                    foreach (DataColumn c in dt.Columns)
                    {

                        table.AddCell(new Phrase(c.ColumnName, font5));
                    }

                    foreach (DataRow r in dt.Rows)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            table.AddCell(new Phrase(r[0].ToString(), font5));
                            table.AddCell(new Phrase(r[1].ToString(), font5));
                            table.AddCell(new Phrase(r[2].ToString(), font5));
                            table.AddCell(new Phrase(r[3].ToString(), font5));
                            table.AddCell(new Phrase(r[4].ToString(), font5));
                            table.AddCell(new Phrase(r[5].ToString(), font5));
                            table.AddCell(new Phrase(r[6].ToString(), font5));
                            table.AddCell(new Phrase(r[7].ToString(), font5));
                            table.AddCell(new Phrase(r[8].ToString(), font5));
                            table.AddCell(new Phrase(r[9].ToString(), font5));
                            //table.AddCell(new Phrase(r[10].ToString(), font5));
                            //table.AddCell(new Phrase(r[11].ToString(), font5));
                            //table.AddCell(new Phrase(r[12].ToString(), font5));
                            //table.AddCell(new Phrase(r[13].ToString(), font5));
                            //table.AddCell(new Phrase(r[14].ToString(), font5));
                            //table.AddCell(new Phrase(r[15].ToString(), font5));

                        }
                    }

                    // document.Close();
                    //finished adding headers
                    //end payments


                    iTextSharp.text.Font bigtext = iTextSharp.text.FontFactory.GetFont("Times New Roman", 18, fontcolor);
                    Paragraph header = new Paragraph(new Phrase(companyname, bigtext));
                    Paragraph add = new Paragraph(new Phrase(combinedaddress, textcolor));
                    Paragraph contact = new Paragraph(new Phrase(combinedcontact, textcolor));
                    Paragraph invoicedetails = new Paragraph(new Phrase("STATEMENT OF ACCOUNT", textcolor));

                    header.Alignment = Element.ALIGN_CENTER;

                    add.Alignment = Element.ALIGN_CENTER;

                    contact.Alignment = Element.ALIGN_CENTER;
                    invoicedetails.Alignment = Element.ALIGN_CENTER;

                    //pdfdocument.Add(new Paragraph("\r\n"));

                    /////////////adding image and 
                    PdfPTable headtable = new PdfPTable(2);

                    float[] widthsx = new float[] { 100f, 400f };
                    headtable.SetWidths(widthsx);
                    headtable.DefaultCell.Padding = 3;
                    headtable.WidthPercentage = 100;
                    headtable.HorizontalAlignment = Element.ALIGN_CENTER;
                    headtable.DefaultCell.BorderWidth = 0;
                    headtable.DefaultCell.Border = Rectangle.BOX;

                    PdfPCell imagecell = new PdfPCell(new Phrase("", headertextcolor));

                    imagecell.Rowspan = 3;
                    imagecell.Border = Rectangle.NO_BORDER;
                    //String path1 = Path.Combine(Environment.CurrentDirectory, @"icons\logoimg2.png");
                    //String path=System.Reflection.Assembly.GetEntryAssembly().Location;
                    //String path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                    String path = AppDomain.CurrentDomain.BaseDirectory;
                    //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);    
                    //string mylogo = @"icons\"+ logofile;
                    //string headlogo = Path.Combine(path, @"icons/tracebilllogo.png");
                    //string headlogo = Path.Combine(path, mylogo);
                    string headlogo = Path.Combine(path, @"icons\" + logofile);
                    //String logo = HttpContext.Current.Server.MapPath("~/Images/") + "nswc_logo.png";
                    iTextSharp.text.Image headimage = iTextSharp.text.Image.GetInstance(headlogo);
                    headimage.ScaleAbsolute(50f, 50f);

                    headimage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;



                    pdfdocument.Add(headimage);
                    pdfdocument.Add(header);
                    pdfdocument.Add(add);
                    pdfdocument.Add(contact);
                    pdfdocument.Add(invoicedetails);
                    pdfdocument.Add(new Paragraph("\r\n"));
                    //pdfdocument.Add(new Paragraph("Relationship Contact Person"));
                    // pdfdocument.Add(new Paragraph("\r\n"));
                    pdfdocument.Add(headtable);
                    // pdfdocument.Add(new Paragraph("\r\n"));
                    // pdfdocument.Add(tintable);

                    /////
                    //  String path=Path.Combine(Environment.CurrentDirectory, @"icons\nwsc.png");

                    String logo = Path.Combine(path, @"icons\tracebilllogo.png");

                    // String logo = path;
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(logo);
                    image.ScaleToFit(300, 300);


                    image.Alignment = iTextSharp.text.Image.UNDERLYING;
                    image.SetAbsolutePosition(100, 280);

                    //pdfdocument.Add(image);
                    //pdfdocument.Add(pdftablecustname);
                    pdfdocument.Add(new Paragraph("\r\n"));
                    pdfdocument.Add(pdfTable);
                    //pdfdocument.Add(new Paragraph("\r\n"));

                    // pdfdocument.Add(new Paragraph("\r\n"));
                    pdfdocument.Add(customertable);
                    pdfdocument.Add(new Paragraph("\r\n"));
                    pdfdocument.Add(paymenttable);
                    pdfdocument.Add(new Paragraph("\r\n"));
                    pdfdocument.Add(table);
                    pdfdocument.Add(new Paragraph("\r\n"));
                    pdfdocument.Add(officertable);
                    //
                    if (!Directory.Exists(targetdir))
                    {
                        Directory.CreateDirectory(targetdir);
                    }
                    //else
                    //{
                    //    Directory.CreateDirectory(targetdir);
                    //}
                    pdfdocument.Close();
                    fs.Close();
                    //  Clears all content output from Buffer Stream
                     HttpContext.Current.Response.ClearContent();

                     //Clears all headers from Buffer Stream
                     HttpContext.Current.Response.ClearHeaders();

                     //Adds an HTTP header to the output stream
                     HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(pdf));

                     //Gets or Sets the HTTP MIME type of the output stream
                     HttpContext.Current.Response.ContentType = "application/pdf";



                     //Writes the content of the specified file directory to an HTTP response output stream as a file block
                     HttpContext.Current.Response.WriteFile(pdf);

                     //sends all currently buffered output to the client
                     HttpContext.Current.Response.Flush();                   


                }
                else
                {
                    Console.WriteLine("No Statement Available");
                    //Library.WriteErrorLog("No Bills Available");
                }
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            return output;
        }

    }
}