// MainForm.Designer.cs - Updated event handlers
namespace BookCatalogingSystemGUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReportGenre;
        private System.Windows.Forms.Button btnReportAuthor;
        private System.Windows.Forms.ListBox lstBooks;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReportGenre = new System.Windows.Forms.Button();
            this.btnReportAuthor = new System.Windows.Forms.Button();
            this.lstBooks = new System.Windows.Forms.ListBox();

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.Text = "Book Catalog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;

            // Sidebar Panel
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Width = 200;
            this.sidebarPanel.Padding = new System.Windows.Forms.Padding(10);

            // Styling Template for Buttons
            void StyleButton(System.Windows.Forms.Button btn, string text, int top)
            {
                btn.Text = text;
                btn.Width = 180;
                btn.Height = 40;
                btn.Top = top;
                btn.Left = 10;
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ForeColor = System.Drawing.Color.White;
                btn.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
                btn.Font = new System.Drawing.Font("Segoe UI", 10F);
            }

            StyleButton(this.btnViewAll, "View All", 10);
            StyleButton(this.btnAdd, "Add Book", 60);
            StyleButton(this.btnSearch, "Search", 110);
            StyleButton(this.btnReportGenre, "Report by Genre", 160);
            StyleButton(this.btnReportAuthor, "Report by Author", 210);

            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearchBooks_Click);
            this.btnReportGenre.Click += new System.EventHandler(this.btnReportGenre_Click);
            this.btnReportAuthor.Click += new System.EventHandler(this.btnReportAuthor_Click);

            this.sidebarPanel.Controls.Add(this.btnViewAll);
            this.sidebarPanel.Controls.Add(this.btnAdd);
            this.sidebarPanel.Controls.Add(this.btnSearch);
            this.sidebarPanel.Controls.Add(this.btnReportGenre);
            this.sidebarPanel.Controls.Add(this.btnReportAuthor);

            // ListBox for Book Display
            this.lstBooks.Left = 220;
            this.lstBooks.Top = 50;
            this.lstBooks.Width = 740;
            this.lstBooks.Height = 500;
            this.lstBooks.Font = new System.Drawing.Font("Segoe UI", 10);
            this.lstBooks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Add controls to MainForm
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.lstBooks);

            this.ResumeLayout(false);
        }
    }
}
