using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pansiyon_Kayıt
{
    public partial class FrmKyt : Form
    {
        public FrmKyt()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True");

        private void showdatas()
        {
            listView1.Items.Clear();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From musteriler order by id ASC", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = rdr["id"].ToString();
                add.SubItems.Add(rdr["Ad"].ToString());
                add.SubItems.Add(rdr["Soyad"].ToString());
                add.SubItems.Add(rdr["OdaNo"].ToString());
                add.SubItems.Add(rdr["GTarih"].ToString());
                add.SubItems.Add(rdr["Telefon"].ToString());
                add.SubItems.Add(rdr["Hesap"].ToString());
                add.SubItems.Add(rdr["CTarih"].ToString());

                listView1.Items.Add(add);

               

            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showdatas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into musteriler (id,Ad,Soyad,OdaNo,GTarih,Telefon,Hesap,CTarih) values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + comboBox1.Text.ToString() + "','" + dateTimePicker1.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + dateTimePicker2.Text.ToString() + "')", conn);

            cmd.ExecuteNonQuery();

            cmd.CommandText = "insert into doluyerler(doluyerler) VALUES ('" + comboBox1.Text + "')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = ("Delete from bosoda where bosyerler = '" + comboBox1.Text + "'");
            cmd.ExecuteNonQuery();
            



            conn.Close( );
            comboBox1.Refresh();
            showdatas();
            allclear();
            





        }

        private void allclear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            
            textBox5.Clear();
            textBox6.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }
        int id = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Delete from musteriler where id = (" + id + ")", conn);
            cmd.ExecuteNonQuery();
            cmd.CommandText = "insert into bosoda(bosyerler) values ('" + comboBox1.Text + "')";
            cmd.ExecuteNonQuery( );
            cmd.CommandText = ("Delete from doluyerler where doluyerler = '" + comboBox1.Text + "'");
            cmd.ExecuteNonQuery();
            



            conn.Close( );
            showdatas( );
            allclear();
            
            
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[3].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[6].Text;
            dateTimePicker2.Text = listView1.SelectedItems[0].SubItems[7].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update musteriler set id='" + textBox1.Text.ToString() + "',Ad='" + textBox2.Text.ToString() + "',Soyad='" + textBox3.Text.ToString() + "',OdaNo='" + comboBox1.Text.ToString() + "',GTarih='" + dateTimePicker1.Text.ToString() + "',Telefon='" + textBox5.Text.ToString() + "',Hesap='" + textBox6.Text.ToString() + "',CTarih='" + dateTimePicker2.Text.ToString()+"'where id="+id+"",conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            showdatas();
            allclear();
        }

        private void FrmKyt_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from bosoda order by bosyerler ASC", conn);
            SqlDataReader oda = cmd.ExecuteReader();
            while (oda.Read())
            {
                comboBox1.Items.Add(oda[0].ToString());
            }
            conn.Close ();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * From musteriler where Ad like '%"+textBox7.Text+"%'", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = rdr["id"].ToString();
                add.SubItems.Add(rdr["Ad"].ToString());
                add.SubItems.Add(rdr["Soyad"].ToString());
                add.SubItems.Add(rdr["OdaNo"].ToString());
                add.SubItems.Add(rdr["GTarih"].ToString());
                add.SubItems.Add(rdr["Telefon"].ToString());
                add.SubItems.Add(rdr["Hesap"].ToString());
                add.SubItems.Add(rdr["CTarih"].ToString());

                listView1.Items.Add(add);



            }
            conn.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox8.Text = textBox8.Text.Substring(1) + textBox8.Text.Substring(0, 1);
        }
    }
}
