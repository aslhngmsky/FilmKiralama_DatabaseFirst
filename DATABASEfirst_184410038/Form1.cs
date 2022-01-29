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

namespace DATABASEfirst_184410038
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        filmDBEntities db = new filmDBEntities();
        private void btnkiralamalisteleme_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-37V5P3S;Initial Catalog=filmDB;Integrated Security=True");
            SqlCommand komut = new SqlCommand("select * from kiralik",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void btnmusterilistele_Click(object sender, EventArgs e)
        {
            filmDBEntities db = new filmDBEntities();
            dataGridView1.DataSource = db.musteris.ToList();
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnfilmlisteleme_Click(object sender, EventArgs e)
        {
            var sorgu = from x in db.films
                        select new { x.filmID, x.tur, x.yonetmen , x.filmadi};
            dataGridView1.DataSource = sorgu.ToList();
               
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            musteri m = new musteri();
            m.isim = tbisim.Text;
            m.soyisim = tbsoyisim.Text;
            m.telefon = tbtelefon.Text;
            db.musteris.Add(m);
            db.SaveChanges();
            MessageBox.Show("Müşteri Eklendi");

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbID.Text);
            var x = db.musteris.Find(id);
            db.musteris.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci sistemden silindi");
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbID.Text);
            var x = db.musteris.Find(id);
            x.isim = tbisim.Text;
            x.soyisim = tbsoyisim.Text;
            x.telefon = tbtelefon.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci bilgileri başarıyla güncellendi!!");
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.musteris.Where(x => x.isim == tbisim.Text && x.soyisim==tbsoyisim.Text).ToList();
        }

        private void tbisim_TextChanged(object sender, EventArgs e)
        {//ad tboxa veri girildiğinde o isme ait kişileri listeleme
            string aranan = tbisim.Text;
            var sorgu=from s in db.musteris
                      where s.isim.Contains(aranan)
                      select s;
            dataGridView1.DataSource=sorgu.ToList();
        }

        private void btnent_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                List<musteri> Liste1 = db.musteris.OrderBy(p => p.isim).ToList();
                dataGridView1.DataSource = Liste1;
            }
            if(radioButton2.Checked==true)           
            {
                List<musteri> liste2 = db.musteris.OrderByDescending(p => p.isim).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked == true)
            {
                List<musteri> liste3 = db.musteris.OrderBy(p => p.isim).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            
        }
    }
}
