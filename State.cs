using System;
using System.Collections.Generic;

namespace Bengkel
{
    public enum StatusPerbaikan
    {
        Ada,
        Bermasalah,
        SedangDiperbaiki,
        Batal,
        Selesai
    }

    public enum Pemicu
    {
        MotorBermasalah,
        BatalkanPesan,
        MulaiPerbaikan,
        SelesaiPerbaikan
    }

    public class BengkelState
    {
        public class Transisi
        {
            public StatusPerbaikan StatusAwal;
            public StatusPerbaikan StatusAkhir;
            public Pemicu Pemicu;

            public Transisi(StatusPerbaikan statusAwal, StatusPerbaikan statusAkhir, Pemicu pemicu)
            {
                StatusAwal = statusAwal;
                StatusAkhir = statusAkhir;
                Pemicu = pemicu;
            }
        }

        Transisi[] transisi =
        {
            new Transisi(StatusPerbaikan.Ada, StatusPerbaikan.SedangDiperbaiki, Pemicu.MulaiPerbaikan),
            new Transisi(StatusPerbaikan.SedangDiperbaiki, StatusPerbaikan.Bermasalah, Pemicu.MotorBermasalah),
            new Transisi(StatusPerbaikan.Bermasalah, StatusPerbaikan.SedangDiperbaiki, Pemicu.SelesaiPerbaikan),
            new Transisi(StatusPerbaikan.SedangDiperbaiki, StatusPerbaikan.Selesai, Pemicu.SelesaiPerbaikan),
            new Transisi(StatusPerbaikan.Ada, StatusPerbaikan.Batal, Pemicu.BatalkanPesan),
            new Transisi(StatusPerbaikan.SedangDiperbaiki, StatusPerbaikan.Batal, Pemicu.BatalkanPesan)
        };

        public StatusPerbaikan StatusSaatIni = StatusPerbaikan.Ada;
        public Dictionary<string, StatusPerbaikan> MotorList = new Dictionary<string, StatusPerbaikan>();

        public StatusPerbaikan GetNextState(StatusPerbaikan statusAwal, Pemicu pemicu)
        {
            foreach (Transisi transisi in transisi)
            {
                if (statusAwal == transisi.StatusAwal && pemicu == transisi.Pemicu)
                {
                    return transisi.StatusAkhir;
                }
            }
            return statusAwal;
        }

        public void AktifkanPemicu(Pemicu pemicu)
        {
            StatusSaatIni = GetNextState(StatusSaatIni, pemicu);
            Console.WriteLine("Status saat ini: " + StatusSaatIni);
        }

        public void TambahMotor()
        {
            Console.Write("Masukkan jumlah motor yang ingin ditambahkan: ");
            if (int.TryParse(Console.ReadLine(), out int jumlahMotor) && jumlahMotor >= 0)
            {
                for (int i = 0; i < jumlahMotor; i++)
                {
                    Console.Write("Masukkan nama motor ke-" + (i + 1) + ": ");
                    string namaMotor = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(namaMotor))
                    {
                        ProsesMotorBaru(namaMotor, StatusPerbaikan.Ada);
                    }
                    else
                    {
                        Console.WriteLine("Nama motor tidak boleh kosong");
                    }
                }

                TampilkanMotor();
            }
            else
            {
                Console.WriteLine("Jumlah motor tidak valid.");
            }
        }

        public void MotorBermasalah(string motor)
        {
            UbahStatusMotor(motor, StatusPerbaikan.Bermasalah);
        }

        public void BatalkanPesan(string motor)
        {
            UbahStatusMotor(motor, StatusPerbaikan.Batal);
        }

        public void ProsesMotor(string motor)
        {
            UbahStatusMotor(motor, StatusPerbaikan.SedangDiperbaiki);
        }

        public void SelesaikanPerbaikan(string motor)
        {
            UbahStatusMotor(motor, StatusPerbaikan.Selesai);
        }

        public void ProsesMotorBaru(string motor, StatusPerbaikan status)
        {
            MotorList.Add(motor, status);
            Console.WriteLine("Motor baru '" + motor + "' berhasil ditambahkan dengan status: " + status);
        }

        public void TampilkanMotor()
        {
            Console.WriteLine("Daftar motor:");
            foreach (var motor in MotorList)
            {
                Console.WriteLine("- " + motor.Key + " (Status: " + motor.Value + ")");
            }
        }

        public void UbahStatusMotor(string motor, StatusPerbaikan statusBaru)
        {
            if (MotorList.ContainsKey(motor))
            {
                MotorList[motor] = statusBaru;
                Console.WriteLine("Status motor '" + motor + "' berhasil diubah menjadi: " + statusBaru);
            }
            else
            {
                Console.WriteLine("Motor '" + motor + "' tidak ditemukan.");
            }
        }
    }
}
