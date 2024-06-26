using iText.Kernel.Font;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using WebApplicationn.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Html2pdf.Resolver.Font;
using iText.Layout.Font;
using iText.IO.Font.Constants;
using iText.IO.Font;
using System.Collections.Generic;
using WebApplicationn.Database.Dreamy;
using iText.Layout.Borders;

namespace WebApplicationn.Controllers
{
    public class PdfController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GeneratePDF(List<Cart> cartList)
        {
            // Define your memory stream which will temporarily hold the PDF
            using (MemoryStream stream = new MemoryStream())
            {
                // Initialize PDF writer
                PdfWriter writer = new PdfWriter(stream);

                // Initialize PDF document
                PdfDocument pdf = new PdfDocument(writer);

                // Initialize document
                Document document = new Document(pdf);

                // Load the Courier font and Courier Bold font
                PdfFont courierFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.COURIER);
                PdfFont courierBoldFont = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.COURIER_BOLD);

                // Set the default font for the document to Courier
                document.SetFont(courierFont);

                // Add content to the document
                // Header
                document.Add(new Paragraph("INVOICE\n")
                    .SetFont(courierBoldFont)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(30)
                    .SetBold());

                document.Add(new Paragraph("dreamy\n")
                    .SetFont(courierBoldFont)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(25)
                    );
                document.Add(new Paragraph("Mumbai - 400078 ")
                   .SetFont(courierBoldFont)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(15)
                  );
                document.Add(new Paragraph("------------------------------------------------------------ \n\n")
                   .SetFont(courierBoldFont)
                   .SetTextAlignment(TextAlignment.CENTER)
                  );

                // Invoice data
                DateTime CurrentDate = DateTime.Now;
                document.Add(new Paragraph($"Date & Time: {CurrentDate}\n\n\n"));


                // Table for invoice items
                Table table = new Table(new float[] { 3, 1, 1, 1, 1 });
                table.SetWidth(UnitValue.CreatePercentValue(100));

                table.AddHeaderCell("PRODUCT").SetFont(courierBoldFont);
                table.AddHeaderCell("CATEGORY").SetFont(courierBoldFont);
                table.AddHeaderCell("QUANTITY").SetFont(courierBoldFont);
                table.AddHeaderCell("PRODUCT PRICE").SetFont(courierBoldFont);
                table.AddHeaderCell("FINAL PRICE").SetFont(courierBoldFont);


                // Add each cart item to the table
                long subtotal = 0;

                foreach (var item in cartList)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.productName.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.category.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.quantity.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.price.ToString())));
                    table.AddCell(new Cell().Add(new Paragraph(item.finalPrice.ToString())));

                    subtotal += item.finalPrice;
                }

                // Add the Table to the PDF Document
                document.Add(table);
                document.Add(new Paragraph("\n\n"));


                // Create a table with two columns, adjusting the widths for better alignment

                Table summaryTable = new Table(new float[] { 1, 1.5f });                      //adjusted the column widths to 1 and 1.5f to reduce the gap between the text and values
                summaryTable.SetWidth(UnitValue.CreatePercentValue(50));                      //set the table width to 50% of the document width to make the table narrower
                summaryTable.SetHorizontalAlignment(HorizontalAlignment.RIGHT);               //aligned the table to the right

                // SUBTOTAL
                summaryTable.AddCell(new Cell().Add(new Paragraph("SUBTOTAL:").SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT).SetFont(courierBoldFont)).SetBorder(Border.NO_BORDER));
                summaryTable.AddCell(new Cell().Add(new Paragraph($"₹ {subtotal}").SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT).SetFont(courierBoldFont)).SetBorder(Border.NO_BORDER));

                // SHIPPING
                long shippingCharges = 99;
                summaryTable.AddCell(new Cell().Add(new Paragraph("SHIPPING:").SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT).SetFont(courierBoldFont)).SetBorder(Border.NO_BORDER));
                summaryTable.AddCell(new Cell().Add(new Paragraph($"₹ {shippingCharges}").SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT).SetFont(courierBoldFont)).SetBorder(Border.NO_BORDER));

                // LINE
                summaryTable.AddCell(new Cell(1, 2).Add(new Paragraph("------------------------------").SetTextAlignment(TextAlignment.RIGHT).SetFont(courierBoldFont)).SetBorder(Border.NO_BORDER)); //cell that spans one row and two columns - makes the line span across both columns

                // TOTAL
                long total = subtotal + shippingCharges;
                summaryTable.AddCell(new Cell().Add(new Paragraph("TOTAL:").SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT).SetFont(courierBoldFont)).SetBorder(Border.NO_BORDER));
                summaryTable.AddCell(new Cell().Add(new Paragraph($"Rs. {total}").SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT).SetFont(courierBoldFont)).SetBorder(Border.NO_BORDER));

                // Add the summary table to the document
                document.Add(summaryTable);


                // Close the Document
                document.Close();

                // Convert the stream to a byte array
                byte[] pdfBytes = stream.ToArray();

                // Return the PDF file as a downloadable file
                return File(pdfBytes, "application/pdf", "Invoice.pdf");
            }
        }

        public IActionResult DownloadInvoice()
        {
            List<Cart> cartList = Dreamy.GetProducts();

            if (cartList == null || cartList.Count == 0)
            {
                return NotFound("No products found in the cart.");
            }

            //to generate PDF for the first item in the list--
            //Cart cart = cartList[0];

            return GeneratePDF(cartList);
        }
    }
}




