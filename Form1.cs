using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Text;
using System.Windows.Forms;

namespace Safeauto.txt_organizer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		

		private void TxtButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			string filePath;
			dlg.Filter = "PDF Files(*.PDF)|*.PDF|All Files(*.*)|*.*";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				filePath = dlg.FileName.ToString();


				string strText = "";
				try
				{
					PdfReader reader = new PdfReader(filePath);

					for (int page = 1; page <= reader.NumberOfPages; page++)
					{
						ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
						String s = PdfTextExtractor.GetTextFromPage(reader, page, its);

						s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
						strText = strText + s;

						richTextBox1.Text = strText;
						fileName.Text = dlg.SafeFileName;
						fileLocation.Text = dlg.FileName;
					}
					reader.Close();


				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

			}
		}



		private void Reset_Click(object sender, EventArgs e)
		{
			fileName.Text = String.Empty;
			fileLocation.Text = String.Empty;
			richTextBox1.Text = String.Empty;
		}


	}
}
