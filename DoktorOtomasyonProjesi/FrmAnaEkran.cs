﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace DoktorOtomasyonProjesi
{
    public partial class FrmAnaEkran : Form
    {
        public string _idd;
        public FrmAnaEkran(string idd)
        {
            InitializeComponent();
            _idd = idd;
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select tbl_hasta.Hasta_id, tbl_hasta.Hasta_ad, tbl_hasta.Hasta_soyad, tbl_randevu.Randevu_tarih, tbl_randevu.Randevu_saat from tbl_randevu inner join tbl_hasta on tbl_randevu.Randevu_hasta = tbl_hasta.Hasta_id inner join tbl_doktor on tbl_doktor.Doktor_id = tbl_randevu.Randevu_doktor where tbl_randevu.Randevu_doktor  = '" + _idd + "' AND tbl_randevu.Randevu_taburcu = 'False'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmHesap fr = new FrmHesap(_idd);
            fr.Show();
        }

        public string _iddg;
        public string _rd;
        public string _rid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _iddg = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            SqlCommand komut = new SqlCommand("Select tbl_randevu.Randevu_taburcu, tbl_randevu.Randevu_id from tbl_randevu inner join tbl_hasta on tbl_randevu.Randevu_hasta = tbl_hasta.Hasta_id inner join tbl_doktor on tbl_doktor.Doktor_id = tbl_randevu.Randevu_doktor where tbl_randevu.Randevu_doktor  = '" + _idd + "' AND tbl_randevu.Randevu_hasta = '" + _iddg + "'", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                _rd = dr[0].ToString();
                _rid = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_iddg))
            {
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("Bir Hasta Seçiniz");
                }
                else
                {
                    _iddg = dataGridView1.Rows[0].Cells[0].Value.ToString();
                }
            }
            if (!string.IsNullOrEmpty(_iddg))
            {
                FrmMuayene fr = new FrmMuayene(_iddg);
                fr.Show();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_iddg))
            {
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("Bir Hasta Seçiniz");
                }
                
            }
            else
            {
                _iddg = dataGridView1.Rows[0].Cells[0].Value.ToString();
                DialogResult dialogResult = MessageBox.Show("Hastayı Taburcu Etmek İstiyor Musunuz?", "Hasta Taburcu Edilecek?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand komut = new SqlCommand("update tbl_randevu set Randevu_taburcu=@d1 where Randevu_id = '" + _rid + "'", bgl.baglanti());
                    komut.Parameters.AddWithValue("@d1", SqlDbType.Bit).Value = true;
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("Select tbl_hasta.Hasta_id, tbl_hasta.Hasta_ad, tbl_hasta.Hasta_soyad, tbl_randevu.Randevu_tarih, tbl_randevu.Randevu_saat from tbl_randevu inner join tbl_hasta on tbl_randevu.Randevu_hasta = tbl_hasta.Hasta_id inner join tbl_doktor on tbl_doktor.Doktor_id = tbl_randevu.Randevu_doktor where tbl_randevu.Randevu_doktor  = '" + _idd + "' AND tbl_randevu.Randevu_taburcu = 'False'", bgl.baglanti());
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    MessageBox.Show("Hasta Taburcu Edilidi");
                }
                else
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("Select tbl_hasta.Hasta_id, tbl_hasta.Hasta_ad, tbl_hasta.Hasta_soyad, tbl_randevu.Randevu_tarih, tbl_randevu.Randevu_saat from tbl_randevu inner join tbl_hasta on tbl_randevu.Randevu_hasta = tbl_hasta.Hasta_id inner join tbl_doktor on tbl_doktor.Doktor_id = tbl_randevu.Randevu_doktor where tbl_randevu.Randevu_doktor  = '" + _idd + "' AND tbl_randevu.Randevu_taburcu = 'False'", bgl.baglanti());
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(_iddg))
            {
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("Bir Hasta Seçiniz");
                }
                else
                {
                    _iddg = dataGridView1.Rows[0].Cells[0].Value.ToString();
                }
            }
            if (!string.IsNullOrEmpty(_iddg))
            {
                FrmGecmisTahliller fr = new FrmGecmisTahliller(_iddg);
                fr.Show();
            }
        }
    }
}