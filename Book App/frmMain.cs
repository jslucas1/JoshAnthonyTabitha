using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Book_App
{
    public partial class frmMain : Form
    {
        string cwid;
        List<Book> myBooks;
        public frmMain(string tempCwid)
        {
            this.cwid = tempCwid;
            InitializeComponent();
            pbCover.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadList()
        {
            myBooks = BookFile.GetAllBooks(cwid);
            lstBooks.DataSource = myBooks;
        }

        private void LstBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            Book myBook = (Book)lstBooks.SelectedItem;

            txtTitleData.Text = myBook.title;
            txtAuthorData.Text = myBook.author;
            txtGenreData.Text = myBook.genre;
            txtCopiesData.Text = myBook.copies.ToString();
            txtLenghtData.Text = myBook.length.ToString();
            txtISBNData.Text = myBook.isbn;

            try
            {
                pbCover.Load(myBook.cover);
            }
            catch
            {

            }

        }

        private void BtnRent_Click(object sender, EventArgs e)
        {
            Book myBook = (Book)lstBooks.SelectedItem;

            myBook.copies--;

            BookFile.SaveBook(myBook, cwid, "edit");
            LoadList();
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            Book myBook = (Book)lstBooks.SelectedItem;

            myBook.copies++;

            BookFile.SaveBook(myBook, cwid, "edit");
            LoadList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Book myBook = (Book)lstBooks.SelectedItem;

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                BookFile.DeleteBook(myBook, cwid);
                LoadList();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            Book myBook = (Book)lstBooks.SelectedItem;

            frmEdit myForm = new frmEdit(myBook, "edit", cwid);
            if(myForm.ShowDialog() == DialogResult.OK)
            {

            }
            else
            {
                LoadList();
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            Book myBook = new Book();

            frmEdit myForm = new frmEdit(myBook, "new", cwid);
            if (myForm.ShowDialog() == DialogResult.OK)
            {

            }
            else
            {
                LoadList();
            }
        }
    }
}
