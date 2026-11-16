using System;
using System.Collections.Generic;

namespace ingyah
{
    class Sekolah
    {
        public string idSekolah;
        public string namaSekolah;
        public int jumlahPaketDiterima;
        public Sekolah(string idSekolah, string namaSekolah, int jumlahPaketDiterima)
        {
            this.idSekolah = idSekolah;
            this.namaSekolah = namaSekolah;
            this.jumlahPaketDiterima = jumlahPaketDiterima;
        }
        public void TampilInfo()
        {
            Console.WriteLine("ID Sekolah                  : " + idSekolah);
            Console.WriteLine("Nama Sekolah                : " + namaSekolah);
            Console.WriteLine("Jumlah Paket Yang Diterima  : " + jumlahPaketDiterima);
        }
        public void TambahPaketDiterima(int jumlah)
        {
            if (jumlah > 0)
            {
                jumlahPaketDiterima += jumlah;
            }
        }
    }
    abstract class PaketMBG
    {
        private string idPaket;
        private string namaPaket;
        private int stokDapur;
        public string Idpaket
        {
            get { return idPaket; }
            set { idPaket = value; }
        }
        public string NamaPaket
        {
            get { return namaPaket; }
            set { namaPaket = value; }
        }
        public int StokDapur
        {
            get { return stokDapur; }
            private set
            {
                if (value >= 0)
                {
                    stokDapur = value;
                }
                else
                {
                    throw new ArgumentException("stok tidak boleh negatif ya kaka!");
                }
            }
        }
        public PaketMBG(string idPaket, string namaPaket, int stokDapur)
        {
            if (stokDapur <= 0)
            {
                throw new ArgumentException("stok tidak boleh negatif ya kaka!");
            }
            this.idPaket = idPaket;
            this.namaPaket = namaPaket;
            this.StokDapur = stokDapur;
        }

        public bool KurangiStok(int jumlah)
        {
            if (jumlah <= 0)
            {
                return false;
            }
            if (jumlah > stokDapur)
            {
                return false;
            }
            stokDapur -= jumlah;
            return true;
        }
        public void TampilInfo()
        {

        }
        public abstract decimal HitungBiaya(int jumlah);
    }
    class PaketReguler : PaketMBG
    {
        public PaketReguler(string idPaket, string namaPaket, int stokDapur) : base(idPaket, namaPaket, stokDapur)
        {

        }
        public override decimal HitungBiaya(int jumlah)
        {
            return jumlah * 1500m;
        }
        public void TampilPaket()
        {
            Console.WriteLine("Paket Reguler");
            Console.WriteLine("Isi: nasi, ayam, sayur, buah");
            Console.WriteLine("stok: " + StokDapur);
        }
    }
    class PaketKhusus : PaketMBG
    {
        public string JenisDiet;
        public PaketKhusus(string idPaket, string namaPaket, int stokDapur, string jenisDiet) : base(idPaket, namaPaket, stokDapur)
        {
            JenisDiet = jenisDiet;
        }
        public override decimal HitungBiaya(int jumlah)
        {
            return jumlah * 20000m;
        }
        public void TampilPaket()
        {
            Console.WriteLine("Paket Khusus");
            Console.WriteLine("Jenis Diet: " + JenisDiet);
            Console.WriteLine("stok: " + StokDapur);
        }
    }
    interface DapatDidistribusikan
    {
        void KirimPaket();
    }
    class DistribusiMBG : DapatDidistribusikan
    {
        private Sekolah sekolahTujuan;
        private PaketMBG paket;
        private int Jumlah;
        public DistribusiMBG(Sekolah sekolah, PaketMBG paketMakanan, int jumlahKirim)
        {
            sekolahTujuan = sekolah;
            paket = paketMakanan;
            Jumlah = jumlahKirim;
        }
        public void KirimPaket()
        {
            if (Jumlah <= 0)
            {
                Console.WriteLine("Distribusi gagal! Jumlah Paket harus lebih dari 0");
                return;
            }
            if (Jumlah > paket.StokDapur)
            {
                Console.WriteLine("Distribusi gagal! Jumlah Paket harus lebih dari 0");
                Console.WriteLine("Stok tersedia: " + paket.StokDapur);
                return;
            }
            bool berhasil = paket.KurangiStok(Jumlah);
            if (berhasil)
            {
                sekolahTujuan.TambahPaketDiterima(Jumlah);
                Console.WriteLine("Distribusi MBG berhasi! Sebanyak " + Jumlah + "Paket makanan telah dikirimkan ke sekolah " + sekolahTujuan.namaSekolah);
            }
            else
            {
                Console.WriteLine("Distribusi gagal! Jumlah paket harus lebih dari 0");
            }

        }
    }
    class PengelolaMBG
    {
        private List<Sekolah> daftarSekolah = new List<Sekolah>();
        private List<PaketMBG> daftarPaket = new List<PaketMBG>();
        public void TambahSekolah(Sekolah sekolah)
        {
            daftarSekolah.Add(sekolah);
        }
        public void TambahPaket(PaketMBG paket)
        {
            daftarPaket.Add(paket);
        }
        public void TampilkanDataMBG()
        {
            Console.WriteLine("\n===Data Sekolah===");
            foreach (Sekolah sekolah in daftarSekolah)
            {
                sekolah.TampilInfo();
                Console.WriteLine();
            }
            Console.WriteLine("===Data Paket MBG===");
            foreach (PaketMBG paket in daftarPaket)
            {
                paket.TampilInfo();
                Console.WriteLine();
            }
        }
    }

    class program
    {
        static void Main(String[] args)
        {
            //Sekolah Smasa = new Sekolah("3007", "SMA 1 Jember", 50);
            //Sekolah Skaga = new Sekolah("9002", "SMK 3 Jember", 100);
            Console.WriteLine("---Sistem Manajemen MBG---");

            Sekolah Smasa = new Sekolah("3007", "SMA 1 Jember", 50);
            Sekolah Skaga = new Sekolah("9002", "SMK 3 Jember", 100);
            Console.WriteLine();

            Console.WriteLine("---Informasi Sekolah---");
            Smasa.TampilInfo();
            Console.WriteLine();

            Skaga.TampilInfo();
            Console.WriteLine();

            PaketReguler reguler = new PaketReguler("P001", "Paket Reguler", 199);
            PaketKhusus khusus = new PaketKhusus("P002", "Paket Khusus", 200, "Rendah Gula");
            Console.WriteLine();

            Console.WriteLine("\n---Perhitungan Biaya---");
            PaketMBG paket1 = reguler;
            PaketMBG paket2 = khusus;
            Console.WriteLine("Biaya 10 paket reguler : RP" + paket1.HitungBiaya(10));
            Console.WriteLine("Biaya 10 paket khusus : RP" + paket2.HitungBiaya(10));
            Console.WriteLine();

            Console.WriteLine("\n----Proses Distribusi---");
            DistribusiMBG dis1 = new DistribusiMBG(Smasa, reguler, 30);
            dis1.KirimPaket();

            DistribusiMBG dis2 = new DistribusiMBG(Skaga, khusus, 20);
            dis2.KirimPaket();

            Console.WriteLine();

            Console.WriteLine("\n----Uji Distribusi Gagal----");
            DistribusiMBG dis3 = new DistribusiMBG(Smasa, reguler, 200);
            dis3.KirimPaket();

            Console.WriteLine("\n===== PENGELOLA MBG =====");

            PengelolaMBG pengelola = new PengelolaMBG();

            pengelola.TambahSekolah(Smasa);
            pengelola.TambahSekolah(Skaga);

            pengelola.TambahPaket(reguler);
            pengelola.TambahPaket(khusus);

            pengelola.TampilkanDataMBG();


            Console.WriteLine("\n===== PROGRAM SELESAI =====");

            Console.ReadKey();

        }
    }
}

