﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuhasebeOtomasyon.Modul_Cek
{
    public partial class frmKendiCekimiz : DevExpress.XtraEditors.XtraForm
    {

        Fonksiyonlar.DatabaseDataContext DB = new Fonksiyonlar.DatabaseDataContext();
        Fonksiyonlar.Formlar Formlar = new Fonksiyonlar.Formlar();
        Fonksiyonlar.Mesajlar Mesajlar = new Fonksiyonlar.Mesajlar();

        int CekID = -1;
        int BankaID = -1;
        bool Edit = false;


        public frmKendiCekimiz()
        {
            InitializeComponent();
        }

    

        private void frmKendiCekimiz_Load(object sender, EventArgs e)
        {
            txtVadeTarihi.Text = DateTime.Now.ToShortDateString();
        }



        void Temizle()
        {
            txtAciklama.Text = "";
            txtBanka.Text = "";
            txtBelgeNo.Text = "";
            txtCekNo.Text = "";
            txtHesapNo.Text = "";
            txtSube.Text = "";
            txtTutar.Text = "";
            txtVadeTarihi.Text = DateTime.Now.ToShortDateString();
            CekID = -1;
            BankaID = -1;
            Edit = false;
            AnaForm.Aktarma = -1;
        }

        public void Ac(int CekinIDsi)
        {
            try
            {
                CekID = CekinIDsi;
                Fonksiyonlar.TBL_CEKLER Cek = DB.TBL_CEKLER.First(s => s.ID == CekID);
                BankaAc(DB.TBL_BANKALAR.First(s => s.BANKAADI == Cek.BANKA && s.HESAPNO == Cek.HESAPNO).ID);
                txtAciklama.Text = Cek.ACIKLAMA;
                txtBelgeNo.Text = Cek.BELGENO;
                txtCekNo.Text = Cek.CEKNO;
                txtTutar.Text = Cek.TUTAR.Value.ToString();
                txtVadeTarihi.Text = Cek.VADETARIHI.Value.ToShortDateString();
                Edit = true;
            }
            catch (Exception e)
            {
                Mesajlar.Hata(e);
            }
        }
        
        void BankaAc(int BankaninIDsi)
        {
            try
            {
                BankaID = BankaninIDsi;
                Fonksiyonlar.TBL_BANKALAR Banka = DB.TBL_BANKALAR.First(s => s.ID == BankaID);
                txtBanka.Text = Banka.BANKAADI;
                txtHesapNo.Text = Banka.HESAPNO;
                txtSube.Text = Banka.SUBE;
            }
            catch (Exception e)
            {
                Mesajlar.Hata(e);
            }
        }


        void YeniKaydet()
        {
            try
            {
                Fonksiyonlar.TBL_CEKLER Cek = new Fonksiyonlar.TBL_CEKLER();
                Cek.ACIKLAMA = txtAciklama.Text;
                Cek.ACKODU = 'A';
                Cek.BANKA = txtBanka.Text;
                Cek.BELGENO = txtBelgeNo.Text;
                Cek.CEKNO = txtCekNo.Text;
                Cek.DURUMU = "Portföy";
                Cek.HESAPNO = txtHesapNo.Text;
                Cek.SUBE = txtSube.Text;
                Cek.TAHSIL = "Hayır";
                Cek.TARIH = DateTime.Now;
                Cek.TIPI = "Kendi Çekimiz";
                Cek.TUTAR = decimal.Parse(txtTutar.Text);
                Cek.VADETARIHI = DateTime.Parse(txtVadeTarihi.Text);
                Cek.SAVEDATE = DateTime.Now;
                Cek.SAVEUSER = AnaForm.UserID;
                DB.TBL_CEKLER.InsertOnSubmit(Cek);
                DB.SubmitChanges();
                Mesajlar.YeniKayit(txtCekNo.Text + " No'lu kendi çekimizin kaydı yapılmıştır");

                Fonksiyonlar.TBL_BANKAHAREKETLERI BankaHareketi = new Fonksiyonlar.TBL_BANKAHAREKETLERI();
                BankaHareketi.ACIKLAMA = txtCekNo.Text + " no'lu ve " + txtVadeTarihi.Text + " vadeli kendi çekimiz";
                BankaHareketi.BANKAID = BankaID;
                BankaHareketi.BELGENO = txtBelgeNo.Text;
                BankaHareketi.EVRAKID = Cek.ID;
                BankaHareketi.EVRAKTURU = "Kendi Çekimiz";
                BankaHareketi.GCKODU = 'C';
                BankaHareketi.TARIH = DateTime.Now;
                BankaHareketi.TUTAR = 0;
                BankaHareketi.SAVEDATE = DateTime.Now;
                BankaHareketi.SAVEUSER = AnaForm.UserID;
                DB.TBL_BANKAHAREKETLERI.InsertOnSubmit(BankaHareketi);
                DB.SubmitChanges();
                Mesajlar.YeniKayit(txtCekNo.Text + " No'lu kendi çekimizin kaydı yapılmıştır");
                Temizle();
            }
            catch (Exception e)
            {
                Mesajlar.Hata(e);
            }

        }

        void Guncelle()
        {
            try
            {
                Fonksiyonlar.TBL_CEKLER Cek = DB.TBL_CEKLER.First(s => s.ID == CekID);
                Cek.ACIKLAMA = txtAciklama.Text;
                Cek.ACKODU = 'A';
                Cek.BANKA = txtBanka.Text;
                Cek.BELGENO = txtBelgeNo.Text;
                Cek.CEKNO = txtCekNo.Text;
                Cek.DURUMU = "Portföy";
                Cek.HESAPNO = txtHesapNo.Text;
                Cek.SUBE = txtSube.Text;
                Cek.TAHSIL = "Hayır";
                Cek.TARIH = DateTime.Now;
                Cek.TIPI = "Kendi Çekimiz";
                Cek.TUTAR = decimal.Parse(txtTutar.Text);
                Cek.VADETARIHI = DateTime.Parse(txtVadeTarihi.Text);
                Cek.EDITDATE = DateTime.Now;
                Cek.EDITUSER = AnaForm.UserID;
                DB.SubmitChanges();

                Fonksiyonlar.TBL_BANKAHAREKETLERI BankaHareketi = DB.TBL_BANKAHAREKETLERI.First(s => s.EVRAKID == CekID && s.EVRAKTURU == "Kendi Çekimiz"); 
                BankaHareketi.ACIKLAMA = txtCekNo.Text + " no'lu ve " + txtVadeTarihi.Text + " vadeli kendi çekimiz";
                BankaHareketi.BANKAID = BankaID;
                BankaHareketi.BELGENO = txtBelgeNo.Text;
                BankaHareketi.EVRAKID = Cek.ID;
                BankaHareketi.EVRAKTURU = "Kendi Çekimiz";
                BankaHareketi.GCKODU = 'C';
                BankaHareketi.TARIH = DateTime.Now;
                BankaHareketi.TUTAR = 0;
                BankaHareketi.EDITDATE = DateTime.Now;
                BankaHareketi.EDITUSER = AnaForm.UserID;
                DB.SubmitChanges();
                Mesajlar.Guncelle(true);
                Temizle();
            }
            catch (Exception e)
            {
                Mesajlar.Hata(e);
            }

        }


        void Sil()
        {
            try
            {
                DB.TBL_BANKAHAREKETLERI.DeleteOnSubmit(DB.TBL_BANKAHAREKETLERI.First(s => s.EVRAKID == CekID && s.EVRAKTURU == "Kendi Çekimiz"));
                DB.TBL_CEKLER.DeleteOnSubmit(DB.TBL_CEKLER.First(s => s.ID == CekID));
                DB.SubmitChanges();
            }
            catch (Exception e)
            {
                Mesajlar.Hata(e);
            }
        }

        private void txtHesapNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int id = Formlar.BankaListesi(true);
            if (id > 0)
            {
                BankaAc(id);
            }
            AnaForm.Aktarma = -1;

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (Edit && CekID > 0 && Mesajlar.Guncelle() == DialogResult.Yes) Guncelle();
            else
                YeniKaydet();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (Edit && CekID > 0 && Mesajlar.Sil() == DialogResult.Yes) Sil();

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBelgeNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}