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

namespace TraceBilling.ControlObjects
{
    public class PDFPrints
    {

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
    }
}