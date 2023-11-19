using ChanceryStore.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;

namespace ChanceryStore.Forms
{
    /// <summary>
    /// Логика взаимодействия для DocumentWindow.xaml
    /// </summary>
    public partial class DocumentWindow : Window
    {

        double summa = 0;
        public DocumentWindow()
        {
            InitializeComponent();

            dgData.ItemsSource = Purchase.ProductsForPurchaseObsColToPur;

            var providers = Provider.GetProviders();
            ProviderComboBox.ItemsSource = providers;
            ProviderComboBox.DisplayMemberPath = "Name";
            ProviderComboBox.SelectedValuePath = "Id";
           

            for (int i = 0; i < Purchase.ProductsForPurchaseObsColToPur.Count ; i++)
            {
                summa += Purchase.ProductsForPurchaseObsColToPur[i].Summa;
                
            }

            Total.Content = summa;
        }

        private void Button_Click_Open(object sender, RoutedEventArgs e)
        {

        }



        private void Button_Click_Excel(object sender, RoutedEventArgs e)
        {

        }

   

        private void AddRowBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Purchase.ProductsForPurchaseObsColToPur.Clear();
        }

        private void WordBtn_Click(object sender, RoutedEventArgs e)
        {
            // создаем приложение ворд
            Word.Application winword = new Word.Application();
            //winword.Visible = true;

            // добавляем документ
            Word.Document document = winword.Documents.Add();

            // добавляем параграф с номером документа и выбранной датой
            Word.Paragraph invoicePar = document.Content.Paragraphs.Add();
            DateTime? selectDate = invoiceDate.SelectedDate;
            if (selectDate != null)
                invoicePar.Range.Text = string.Concat("Документ № ", invoiceNumber.Text, " от ", selectDate.Value.ToString("dd.MM.yyyy"));
            else
                invoicePar.Range.Text = string.Concat("Документ № ", invoiceNumber.Text);
            invoicePar.Range.Font.Name = "Times new roman";
            invoicePar.Range.Font.Size = 14;
            invoicePar.Range.InsertParagraphAfter();

            // добавляем параграф с поставщиком
            Word.Paragraph providerPar = document.Content.Paragraphs.Add();
            providerPar.Range.Text = string.Concat("Поставщик: ", Provider.GetProvider(Convert.ToInt32(ProviderComboBox.SelectedValue)).Name);
            providerPar.Range.Font.Name = "Times new roman";
            providerPar.Range.Font.Size = 14;
            providerPar.Range.InsertParagraphAfter();

            // формируем таблицу
            // количество колонок - 4
            // количество строк - nRows
            int nRows = dgData.Items.Count;
            Word.Table myTable = document.Tables.Add(providerPar.Range, nRows, 5);
            myTable.Borders.Enable = 1;

            // устанавливаем названия колонок
            var headerRow = myTable.Rows[1].Cells;
            headerRow[1].Range.Text = "Код";
            headerRow[2].Range.Text = "Продукт";
            headerRow[4].Range.Text = "Количество";
            headerRow[3].Range.Text = "Цена";
            headerRow[5].Range.Text = "Сумма";
            // добавляем данные из таблицы в ворд
            for (int i = 2; i < Purchase.ProductsForPurchaseObsColToPur.Count + 2; i++)
            {
                var dataRow = myTable.Rows[i].Cells;
                dataRow[1].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Product.Code.ToString();
                dataRow[2].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Product.Name;
                dataRow[3].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Amount.ToString();
                dataRow[4].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Product.Price.ToString();
                dataRow[5].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Summa.ToString();
            }

            // добавляем параграф с 
            Word.Paragraph totalrPar = document.Content.Paragraphs.Add();
            totalrPar.Range.Text = string.Concat("Итого: ", Total.Content);
            totalrPar.Range.Font.Name = "Times new roman";
            totalrPar.Range.Font.Size = 14;
            totalrPar.Range.InsertParagraphAfter();
            // указываем в какой файл сохранить
            // TODO - добавьте возможность выбора названия файла
            // и места где его нужно сохранить
            object filename = $@"D:\ChanceryStore\Закупки\№ {invoiceNumber.Text} -{selectDate.Value.ToString("dd.MM.yyyy")}.doc";
            document.SaveAs(filename);
            document.Close();
            winword.Quit();

            MessageBox.Show("Документ сформирован Word");
        }

        private void PdfBtn_Click(object sender, RoutedEventArgs e)
        {

            // создаем приложение ворд
            Word.Application winword = new Word.Application();
            //winword.Visible = true;

            // добавляем документ
            Word.Document document = winword.Documents.Add();

            // добавляем параграф с номером документа и выбранной датой
            Word.Paragraph invoicePar = document.Content.Paragraphs.Add();
            DateTime? selectDate = invoiceDate.SelectedDate;
            if (selectDate != null)
                invoicePar.Range.Text = string.Concat("Документ № ", invoiceNumber.Text, " от ", selectDate.Value.ToString("dd.MM.yyyy"));
            else
                invoicePar.Range.Text = string.Concat("Документ № ", invoiceNumber.Text);
            invoicePar.Range.Font.Name = "Times new roman";
            invoicePar.Range.Font.Size = 14;
            invoicePar.Range.InsertParagraphAfter();

            // добавляем параграф с поставщиком
            Word.Paragraph providerPar = document.Content.Paragraphs.Add();
            providerPar.Range.Text = string.Concat("Поставщик: ", Provider.GetProvider(Convert.ToInt32(ProviderComboBox.SelectedValue)).Name);
            providerPar.Range.Font.Name = "Times new roman";
            providerPar.Range.Font.Size = 14;
            providerPar.Range.InsertParagraphAfter();

            // формируем таблицу
            // количество колонок - 4
            // количество строк - nRows
            int nRows = dgData.Items.Count;
            Word.Table myTable = document.Tables.Add(providerPar.Range, nRows, 5);
            myTable.Borders.Enable = 1;

            // устанавливаем названия колонок
            var headerRow = myTable.Rows[1].Cells;
            headerRow[1].Range.Text = "Код";
            headerRow[2].Range.Text = "Продукт";
            headerRow[4].Range.Text = "Количество";
            headerRow[3].Range.Text = "Цена";
            headerRow[5].Range.Text = "Сумма";
            // добавляем данные из таблицы в ворд
            for (int i = 2; i < Purchase.ProductsForPurchaseObsColToPur.Count + 2; i++)
            {
                var dataRow = myTable.Rows[i].Cells;
                dataRow[1].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Product.Code.ToString();
                dataRow[2].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Product.Name;
                dataRow[3].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Amount.ToString();
                dataRow[4].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Product.Price.ToString();
                dataRow[5].Range.Text = Purchase.ProductsForPurchaseObsColToPur[i - 2].Summa.ToString();
            }

            // добавляем параграф с 
            Word.Paragraph totalrPar = document.Content.Paragraphs.Add();
            totalrPar.Range.Text = string.Concat("Итого: ", Total.Content);
            totalrPar.Range.Font.Name = "Times new roman";
            totalrPar.Range.Font.Size = 14;
            totalrPar.Range.InsertParagraphAfter();
            // указываем в какой файл сохранить
            // TODO - добавьте возможность выбора названия файла
            // и места где его нужно сохранить
            object filename = $@"D:\ChanceryStore\Закупки\№ {invoiceNumber.Text} -{selectDate.Value.ToString("dd.MM.yyyy")}.pdf";
            document.SaveAs(filename);
            document.Close();
            winword.Quit();


            // Document document = new Document(PageSize.A4);
            // try
            // {


            //     string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            //     BaseFont baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //     Font font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);

            //     PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream( $@"D:\ChanceryStore\Закупки\№ {invoiceNumber.Text} - {((DateTime)invoiceDate.SelectedDate).ToString("dd.MM.yyyy") }.pdf"
            //, FileMode.Create));

            //     document.Open();
            //     document.Add(new iTextSharp.text.Paragraph("период: " + ((DateTime)invoiceDate.SelectedDate).ToString("dd.MM.yyyy") , font));
            //     document.Add(new iTextSharp.text.Paragraph("поставщик: " + Provider.GetProvider(Convert.ToInt32(ProviderComboBox.SelectedValue)).Name, font));

            //     DataTable dataTable = Purchase.ProductsForPurchaseObsColToPur;
            //     //Создаем объект таблицы и передаем в нее число столбцов таблицы из нашего датасета
            //     PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
            //     // добавление в таблицу общего заголовка
            //     PdfPCell pdfCell = new PdfPCell(new Phrase("ФИО пациентов с оказанными им услугами,со стоимостью каждой услуги", font));
            //     pdfCell.Colspan = dataTable.Columns.Count;
            //     pdfCell.HorizontalAlignment = 1;

            //     pdfCell.Border = 0;
            //     pdfTable.AddCell(pdfCell);

            //     // сначала добавляем заголовки таблицы
            //     for (int j = 0; j < dataTable.Columns.Count; j++)
            //     {
            //         pdfCell = new PdfPCell(new Phrase(new Phrase(dataTable.Columns[j].ColumnName, font)));
            //         // Фоновый цвет (необязательно, просто сделаем по красивее)
            //         pdfCell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            //         pdfTable.AddCell(pdfCell);
            //     }

            //     // Добавляем все остальные ячейки
            //     for (int row = 0; row < dataTable.Rows.Count; row++)
            //     {
            //         for (int col = 0; col < dataTable.Rows.Count; col++)
            //         {
            //             pdfTable.AddCell(new Phrase(dataTable.Rows[row][col].ToString(), font));
            //         }
            //     }
            //     // Добавляем таблицу в документ
            //     document.Add(pdfTable);

            //     document.Add(new iTextSharp.text.Paragraph("Стоимость услуг по каждому пациенту:", font));

            //     string[] arr = GetPriceServicesPatients();
            //     for (int i = 0; i < arr.Length; i++)
            //     {
            //         document.Add(new Paragraph(arr[i], font));
            //     }
            //     document.Add(new Paragraph("Итоговая стоимость по всем пациентам за период: " + TotalCost() + " руб.", font));
            //     //pdfWriter.Close();
            // }
            // catch (DocumentException Ex)
            // { throw new Exception(Ex.Message); }

            // catch (IOException Ex)
            // { throw new Exception(Ex.Message); }
            // document.Close();

            MessageBox.Show("Документ сформирован Pdf");
        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e)
        {

            Excel.Application app = new Excel.Application
            {
                //Отобразить Excel
                Visible = false,
                //Количество листов в рабочей книге
                SheetsInNewWorkbook = 2
            };
            //Добавить рабочую книгу
            Excel.Workbook workBook = app.Workbooks.Add();
            //Отключить отображение окон с сообщениями
            app.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
            //Название листа (вкладки снизу)
            sheet.Name = "Накладная";

            //Пример заполнения ячеек №1
            int count = Purchase.ProductsForPurchaseObsColToPur.Count;
            for (int i = 6; i < count + 6; i++)
            {

                sheet.Range["A" + i].Value = Purchase.ProductsForPurchaseObsColToPur[i - 6].Product.Code.ToString();
                sheet.Range["B" + i].Value = Purchase.ProductsForPurchaseObsColToPur[i - 6].Product.Name;
                sheet.Range["C" + i].Value = Purchase.ProductsForPurchaseObsColToPur[i - 6].Amount.ToString();
                sheet.Range["D" + i].Value = Purchase.ProductsForPurchaseObsColToPur[i - 6].Product.Price.ToString();
                sheet.Range["E" + i].Value = Purchase.ProductsForPurchaseObsColToPur[i - 6].Summa.ToString();
            }




            sheet.Range["A1"].Value = "№ документа: " + invoiceNumber.Text + " от " + invoiceDate.Text;
            sheet.Range["A2"].Value = "Поставщик " + Provider.GetProvider(Convert.ToInt32(ProviderComboBox.SelectedValue)).Name;

            sheet.Range["A5"].Value = "Код";
            sheet.Range["B5"].Value = "Продукт";
            sheet.Range["C5"].Value = "Количество";
            sheet.Range["D5"].Value = "Цена";
            sheet.Range["E5"].Value = "Сумма";

            sheet.Range["A" + (count + 7)].Value = "Итого: " + Total.Content;
            //workBook.Sa
            app.Application.ActiveWorkbook.SaveAs($@"D:\ChanceryStore\Закупки\№ {invoiceNumber.Text} - {invoiceDate.SelectedDate}.xlsx", 
               Excel.XlSaveAsAccessMode.xlNoChange);
            workBook.Close();
            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            MessageBox.Show("Документ сформирован Excel");
        }

        private void dgData_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            summa = 0;

            for (int i = 0; i < Purchase.ProductsForPurchaseObsColToPur.Count; i++)
            {
                summa += Purchase.ProductsForPurchaseObsColToPur[i].Summa;

            }

            Total.Content = summa;
        }

        private void dgData_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            summa = 0;

            for (int i = 0; i < Purchase.ProductsForPurchaseObsColToPur.Count; i++)
            {
                summa += Purchase.ProductsForPurchaseObsColToPur[i].Summa;

            }

            Total.Content = summa;
        }

        private void dgData_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            summa = 0;

            for (int i = 0; i < Purchase.ProductsForPurchaseObsColToPur.Count; i++)
            {
                summa += Purchase.ProductsForPurchaseObsColToPur[i].Summa;

            }

            Total.Content = summa;
        }
    }


}
