using System;
using System.Drawing;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

namespace product_entry_tool
{
    public partial class Form1 : Form
    {
        private TextBox productNameTextBox;
        private TextBox versionNumberTextBox;
        private TextBox urlTextBox;
        private TextBox instructionsTextBox;
        private ComboBox shopNumberDropdown;
        private Button submitButton;

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Product Entry Tool";
            this.Size = new Size(960, 540);
            this.MinimumSize = new Size(960, 540);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.Resize += Form_Resize;

            Label productNameLabel = new Label() { Text = "Product Name:", Top = 20, Left = 20 };
            productNameTextBox = new TextBox() { Top = 50, Left = 20, Width = this.ClientSize.Width - 40 };

            Label versionNumberLabel = new Label() { Text = "Version Number:", Top = 90, Left = 20 };
            versionNumberTextBox = new TextBox() { Top = 120, Left = 20, Width = this.ClientSize.Width - 40 };

            Label urlLabel = new Label() { Text = "URL:", Top = 160, Left = 20 };
            urlTextBox = new TextBox() { Top = 190, Left = 20, Width = this.ClientSize.Width - 40 };

            Label instructionsLabel = new Label() { Text = "Instructions:", Top = 230, Left = 20 };
            instructionsTextBox = new TextBox() { Top = 260, Left = 20, Width = this.ClientSize.Width - 40, Height = 100, Multiline = true };

            Label shopNumberLabel = new Label() { Text = "Shop Number:", Top = 380, Left = 20 };
            shopNumberDropdown = new ComboBox() { Top = 410, Left = 20, Width = 100 };
            for (int i = 1; i <= 10; i++)
            {
                shopNumberDropdown.Items.Add(i);
            }
            shopNumberDropdown.SelectedIndex = 0;

            submitButton = new Button() { Text = "Submit", Top = 410, Left = 140, Width = 100 };
            submitButton.Click += SubmitButton_Click;

            this.Controls.Add(productNameLabel);
            this.Controls.Add(productNameTextBox);
            this.Controls.Add(versionNumberLabel);
            this.Controls.Add(versionNumberTextBox);
            this.Controls.Add(urlLabel);
            this.Controls.Add(urlTextBox);
            this.Controls.Add(instructionsLabel);
            this.Controls.Add(instructionsTextBox);
            this.Controls.Add(shopNumberLabel);
            this.Controls.Add(shopNumberDropdown);
            this.Controls.Add(submitButton);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            int newWidth = this.ClientSize.Width - 40;
            productNameTextBox.Width = newWidth;
            versionNumberTextBox.Width = newWidth;
            urlTextBox.Width = newWidth;
            instructionsTextBox.Width = newWidth;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string productName = productNameTextBox.Text;
            string versionNumber = versionNumberTextBox.Text;
            string url = urlTextBox.Text;
            string instructions = instructionsTextBox.Text;
            int shopNumber = (int)shopNumberDropdown.SelectedItem;

            string filePath = @"C:\Users\adriel magalona\Desktop\example.xlsx";

            if (File.Exists(filePath))
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    int row = worksheet.Dimension.End.Row + 1;

                    worksheet.Cells[row, 1].Value = productName;
                    worksheet.Cells[row, 2].Value = versionNumber;
                    worksheet.Cells[row, 3].Value = url;
                    worksheet.Cells[row, 4].Value = instructions;
                    worksheet.Cells[row, 5].Value = shopNumber;

                    package.Save();
                }

                MessageBox.Show("Data added to Excel file successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Excel file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
