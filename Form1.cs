using System;
using System.Windows.Forms;

namespace ProductEntryTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupForm(); // Initialize the UI components programmatically
        }

        private void SetupForm()
        {
            // Set form properties
            this.Text = "Excel-Like Table";
            this.Width = 800;
            this.Height = 600;

            // Create a DataGridView
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 500,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            };

            // Add columns to the DataGridView
            dataGridView.Columns.Add("Column1", "Column 1");
            dataGridView.Columns.Add("Column2", "Column 2");
            dataGridView.Columns.Add("Column3", "Column 3");
            dataGridView.Columns.Add("Column4", "Column 4");

            // Add some sample data
            dataGridView.Rows.Add("Row 1 Col 1", "Row 1 Col 2", "Row 1 Col 3", "Row 1 Col 4");

            // Create buttons for adding and deleting rows
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
        }
    }
}
