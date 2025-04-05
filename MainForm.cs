// MainForm.cs - Full functionality implementation
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BookCatalogingSystemGUI
{
    public partial class MainForm : Form
    {
        private List<Book> books;
        private const string StorageFile = "books.json";

        public MainForm()
        {
            InitializeComponent();
            LoadBooks();
            RefreshBookList();

            lstBooks.DoubleClick += LstBooks_DoubleClick;
        }

        private void LstBooks_DoubleClick(object sender, EventArgs e)
        {
            if (lstBooks.SelectedItem is Book selectedBook)
            {
                var confirm = MessageBox.Show($"Do you want to delete '{selectedBook.Title}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    books.Remove(selectedBook);
                    SaveBooks();
                    RefreshBookList();
                }
            }
        }

        private void LoadBooks()
        {
            if (File.Exists(StorageFile))
            {
                var json = File.ReadAllText(StorageFile);
                books = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();
            }
            else
            {
                books = new List<Book>();
            }
        }

        private void SaveBooks()
        {
            File.WriteAllText(StorageFile, JsonConvert.SerializeObject(books, Formatting.Indented));
        }

        private void RefreshBookList()
        {
            lstBooks.Items.Clear();
            if (books.Count == 0)
            {
                lstBooks.Items.Add("No books available.");
            }
            else
            {
                foreach (var book in books)
                {
                    lstBooks.Items.Add(book);
                }
            }
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            RefreshBookList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddBookForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                books.Add(form.NewBook);
                SaveBooks();
                RefreshBookList();
            }
        }

        private void btnSearchBooks_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter search keyword:", "Search Books", "");
            if (!string.IsNullOrWhiteSpace(input))
            {
                var filtered = books.Where(b => b.Title.Contains(input, StringComparison.OrdinalIgnoreCase)
                    || b.Author.Contains(input, StringComparison.OrdinalIgnoreCase)
                    || b.Genre.Contains(input, StringComparison.OrdinalIgnoreCase)).ToList();

                lstBooks.Items.Clear();
                foreach (var book in filtered)
                    lstBooks.Items.Add(book);

                if (filtered.Count == 0)
                    lstBooks.Items.Add("No matches found.");
            }
        }

        private void btnReportGenre_Click(object sender, EventArgs e)
        {
            var grouped = books.GroupBy(b => b.Genre);
            lstBooks.Items.Clear();
            foreach (var group in grouped)
            {
                lstBooks.Items.Add($"-- {group.Key} --");
                foreach (var book in group)
                    lstBooks.Items.Add(book);
            }
        }

        private void btnReportAuthor_Click(object sender, EventArgs e)
        {
            var grouped = books.GroupBy(b => b.Author);
            lstBooks.Items.Clear();
            foreach (var group in grouped)
            {
                lstBooks.Items.Add($"-- {group.Key} --");
                foreach (var book in group)
                    lstBooks.Items.Add(book);
            }
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{Title} by {Author}, Genre: {Genre}, Year: {Year}";
        }
    }

    public class AddBookForm : Form
    {
        public Book NewBook { get; private set; }
        private TextBox txtTitle, txtAuthor, txtGenre, txtYear;
        private Button btnSubmit;

        public AddBookForm()
        {
            this.Text = "Add Book";
            this.Width = 350;
            this.Height = 250;

            txtTitle = new TextBox { PlaceholderText = "Title", Top = 20, Left = 20, Width = 280 };
            txtAuthor = new TextBox { PlaceholderText = "Author", Top = 60, Left = 20, Width = 280 };
            txtGenre = new TextBox { PlaceholderText = "Genre", Top = 100, Left = 20, Width = 280 };
            txtYear = new TextBox { PlaceholderText = "Year", Top = 140, Left = 20, Width = 280 };

            btnSubmit = new Button { Text = "Add Book", Top = 180, Left = 20, Width = 280 };
            btnSubmit.Click += BtnSubmit_Click;

            this.Controls.Add(txtTitle);
            this.Controls.Add(txtAuthor);
            this.Controls.Add(txtGenre);
            this.Controls.Add(txtYear);
            this.Controls.Add(btnSubmit);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("Please enter a valid year.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NewBook = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Genre = txtGenre.Text,
                Year = year
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
