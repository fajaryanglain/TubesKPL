using System;

namespace Bengkel
{
    class Program
    {
        static void Main(string[] args)
        {
            BengkelState bengkel = new BengkelState();
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Tambah Motor");
                Console.WriteLine("2. Tandai Motor Bermasalah");
                Console.WriteLine("3. Batalkan Pemesanan Motor");
                Console.WriteLine("4. Proses Perbaikan Motor");
                Console.WriteLine("5. Selesaikan Perbaikan Motor");
                Console.WriteLine("6. Tampilkan Daftar Motor");
                Console.WriteLine("7. Keluar");
                Console.Write("Pilihan: ");
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        bengkel.TambahMotor();
                        break;
                    case "2":
                        Console.Write("Masukkan nama motor yang bermasalah: ");
                        string motorBermasalah = Console.ReadLine();
                        bengkel.MotorBermasalah(motorBermasalah);
                        break;
                    case "3":
                        Console.Write("Masukkan nama motor yang ingin dibatalkan pemesanannya: ");
                        string motorBatal = Console.ReadLine();
                        bengkel.BatalkanPesan(motorBatal);
                        break;
                    case "4":
                        Console.Write("Masukkan nama motor yang ingin diperbaiki: ");
                        string motorProses = Console.ReadLine();
                        bengkel.ProsesMotor(motorProses);
                        break;
                    case "5":
                        Console.Write("Masukkan nama motor yang telah selesai diperbaiki: ");
                        string motorSelesai = Console.ReadLine();
                        bengkel.SelesaikanPerbaikan(motorSelesai);
                        break;
                    case "6":
                        bengkel.TampilkanMotor();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }
    }
}
