using System;
using System.IO;
using System.Windows.Forms;

namespace ProductEntryTool
{
    public partial class Form1 : Form
    {
        private string filePath = "data.csv"; // File to store the data

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private DataGridView dataGridView;

        private void SetupForm()
        {
            this.Text = "Excel-Like Table with Auto-Save";
            this.Width = 800;
            this.Height = 600;

            // Create the DataGridView
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 500,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            };

            // Add columns
            dataGridView.Columns.Add("Column1", "Column 1");
            dataGridView.Columns.Add("Column2", "Column 2");
            dataGridView.Columns.Add("Column3", "Column 3");
            dataGridView.Columns.Add("Column4", "Column 4");

            // Load saved data if available
            LoadData();

            // Create Add and Delete buttons
            Button addButton = new Button
            {
                Text = "Add Row",
                Dock = DockStyle.Bottom
            };
            addButton.Click += (s, args) =>
            {
                dataGridView.Rows.Add("", "", "", "");
            };

            Button deleteButton = new Button
            {
                Text = "Delete Row",
                Dock = DockStyle.Bottom
            };
            deleteButton.Click += (s, args) =>
            {
                if (dataGridView.CurrentRow != null)
                {
                    dataGridView.Rows.Remove(dataGridView.CurrentRow);
                }
            };

            // Add controls to the form
            this.Controls.Add(dataGridView);
            this.Controls.Add(addButton);
            this.Controls.Add(deleteButton);

            // Save data on form closing
            this.FormClosing += (s, args) => SaveData();
        }

        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] cells = line.Split(',');
                    dataGridView.Rows.Add(cells);
                }
            }
        }

        private void SaveData()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string[] cells = new string[dataGridView.Columns.Count];
                        for (int i = 0; i < dataGridView.Columns.Count; i++)
                        {
                            cells[i] = row.Cells[i].Value?.ToString() ?? "";
                        }
                        writer.WriteLine(string.Join(",", cells));
                    }
                }
            }
        }
    }
}
