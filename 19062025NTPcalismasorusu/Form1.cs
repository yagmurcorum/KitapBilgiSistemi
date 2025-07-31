using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19062025NTPcalismasorusu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DGYukle();
        }
        OleDbCommand cmd;
        OleDbConnection conn;
        string query, conStr = "Provider= Microsoft.ACE.OleDb.12.0;Data Source=..\\..\\..\\veritabani.accdb";
        Random rnd = new Random();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            for (int i = 0; i < dataGridView1.Rows.Count - 1;i++)
            {
                int isbn = Convert.ToInt32(dataGridView1[0, i].Value);
                string query = "Update Kitaplar set SayfaSayısı=" + rnd.Next(50,1001) + " Where ISBN=" +isbn;
                cmd = new OleDbCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            DGYukle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            for(int i=0;i<dataGridView1.Rows.Count-1;i++) 
            {
                int isbn = Convert.ToInt32(dataGridView1[0, i].Value);
                string query = "Update Kitaplar set Puan =" + rnd.Next(1, 101) + " Where ISBN=" + isbn;
                cmd = new OleDbCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            DGYukle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string query = "Update Kitaplar set KitapAdı=@kitad, YazarAdı=@yazad, SayfaSayısı=@sayfa, Puan=@puan where ISBN= " + textBox1.Text;
            cmd = new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@kitad", textBox2.Text);
            cmd.Parameters.AddWithValue("yazad", textBox3.Text);
            cmd.Parameters.AddWithValue("@sayfa", textBox4.Text);
            cmd.Parameters.AddWithValue("@puan", textBox5.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            DGYukle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            string query = "Insert into Kitaplar(ISBN,KitapAdı,YazarAdı,SayfaSayısı,Puan) Values (@isbn,@kitad,@yazad,@sayfa,@puan)";
            cmd= new OleDbCommand(query, conn);
            cmd.Parameters.AddWithValue("@isbn", textBox1.Text);
            cmd.Parameters.AddWithValue("@kitad", textBox2.Text);
            cmd.Parameters.AddWithValue("yazad", textBox3.Text);
            cmd.Parameters.AddWithValue("@sayfa", textBox4.Text);
            cmd.Parameters.AddWithValue("@puan", textBox5.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            DGYukle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dr= MessageBox.Show("Kayıt Silme İşlemini Onaylıyor musunuz? ID:"+ textBox1.Text,"Kayıt Silinecek",MessageBoxButtons.YesNoCancel,MessageBoxIcon.
                Question,MessageBoxDefaultButton.Button3);
            if (dr == DialogResult.Yes)
            {
                conn.Open();
                string query = "Delete from Kitaplar where ISBN=" + textBox1.Text;
                cmd = new OleDbCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                DGYukle();
            }
            else MessageBox.Show("Silme İşlemi İptal Edildi.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int top = 0, count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                top += Convert.ToInt32(dataGridView1[4, i].Value);
                count++;
            }
            textBox6.Text += " SQL'siz Ortalama :" + (1.0 * top / count) + "\r\n";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conn.Open();
            string query = "select AVG(Puan) from Kitaplar";
            cmd = new OleDbCommand(query, conn);
            double ort= (double)cmd.ExecuteScalar();
            conn.Close();
            textBox6.Text += "SQL ile Ortalama :" + ort.ToString() + "\r\n";

        }

        private void DGYukle() 
        {
            string query = "SELECT*FROM Kitaplar";
            conn = new OleDbConnection(conStr);
            OleDbDataAdapter da = new OleDbDataAdapter(query,conn);
            DataSet ds = new DataSet();
            conn.Open();
            da.Fill(ds, "Kitaplar");
            dataGridView1.DataSource = ds.Tables["Kitaplar"];
            conn.Close();
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
        }
    }
}
