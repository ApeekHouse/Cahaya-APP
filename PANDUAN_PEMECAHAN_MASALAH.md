# Panduan Pemecahan Masalah AluGlass Estimator

Dokumen ini berisi langkah-langkah untuk mengatasi masalah umum yang mungkin terjadi saat menggunakan aplikasi AluGlass Estimator.

## Masalah Startup dan Instalasi

### Aplikasi Tidak Dapat Dijalankan

**Masalah**: Aplikasi tidak mau terbuka atau langsung tertutup setelah dibuka.

**Solusi**:
1. Pastikan Anda memiliki .NET Runtime 7.0 terinstal:
   - Unduh dari [situs Microsoft](https://dotnet.microsoft.com/download/dotnet/7.0)
   - Instal versi ".NET Runtime" (bukan SDK)
   - Restart komputer setelah instalasi

2. Periksa file DLL yang diperlukan:
   - Pastikan semua file berikut ada di folder aplikasi:
     * av_libglesv2.dll
     * e_sqlite3.dll
     * libHarfBuzzSharp.dll
     * libSkiaSharp.dll
   - Jika ada yang hilang, ekstrak ulang semua file dari ZIP ke folder kosong

3. Cek Visual C++ Redistributable:
   - Pastikan Visual C++ Redistributable 2019 atau yang lebih baru terinstal
   - Unduh dari [situs Microsoft](https://aka.ms/vs/17/release/vc_redist.x64.exe)

### Error "File DLL yang diperlukan tidak ditemukan"

**Masalah**: Pesan error tentang file DLL yang hilang.

**Solusi**:
1. Ekstrak ulang semua file dari paket ZIP ke folder kosong
2. Periksa apakah antivirus memblokir/mengkarantina file DLL
3. Pastikan Windows tidak memblokir file (klik kanan file -> Properties -> Unblock)
4. Jika perlu, unduh ulang paket ZIP dari sumber resmi

### Error "Gagal menginisialisasi komponen grafis (SkiaSharp)"

**Masalah**: Aplikasi tidak dapat menginisialisasi library grafis.

**Solusi**:
1. Pastikan Visual C++ Redistributable 2019+ terinstal
2. Periksa apakah kartu grafis Anda memiliki driver terbaru
3. Pastikan Windows Anda diperbarui ke versi terbaru
4. Jika menggunakan Windows 7, aplikasi ini tidak didukung (upgrade ke Windows 10)

### Error Akses atau Permission

**Masalah**: Aplikasi meminta akses tulis ke folder.

**Solusi**:
1. Jalankan aplikasi sebagai Administrator (klik kanan -> Run as administrator)
2. Pindahkan aplikasi ke folder yang memiliki akses penuh (misal: Documents)
3. Pastikan folder aplikasi tidak dalam mode read-only
4. Nonaktifkan sementara antivirus atau tambahkan aplikasi ke daftar pengecualian

## Masalah Operasional

### Aplikasi Lambat atau Tidak Responsif

**Masalah**: Aplikasi berjalan lambat atau hang.

**Solusi**:
1. Restart aplikasi dan komputer
2. Kurangi jumlah program lain yang berjalan bersamaan
3. Pastikan komputer memenuhi persyaratan minimum (RAM 4GB)
4. Jika menggunakan file proyek besar, coba pecah menjadi beberapa file yang lebih kecil

### Error Database atau Penyimpanan Data

**Masalah**: Error saat menyimpan atau membuka file.

**Solusi**:
1. Periksa apakah folder data aplikasi ada dan dapat diakses:
   - `%LOCALAPPDATA%\AluGlassEstimator`
2. Pastikan disk tidak penuh
3. Jika file rusak, coba gunakan file backup dari folder Backup

### Hasil Perhitungan atau Optimasi Tidak Tepat

**Masalah**: Hasil perhitungan estimasi material atau optimasi tidak sesuai harapan.

**Solusi**:
1. Periksa kembali semua input data (ukuran, harga, dll.)
2. Pastikan Anda menggunakan satuan yang benar (mm, cm, atau m)
3. Verifikasi jenis material dan konfigurasi yang dipilih
4. Coba gunakan metode optimasi yang berbeda untuk perbandingan

### Error Cetak atau Ekspor

**Masalah**: Tidak bisa mencetak atau mengekspor laporan.

**Solusi**:
1. Pastikan printer diinstal dan tersedia di Windows
2. Periksa akses folder `%LOCALAPPDATA%\AluGlassEstimator\Exports`
3. Jika mengekspor PDF, pastikan tidak ada file PDF yang sedang terbuka
4. Coba restart aplikasi dan komputer

## Masalah Spesifik

### Error "SKImageInfo Initialization Error"

**Masalah**: Error saat menginisialisasi komponen SkiaSharp.

**Solusi**:
1. Install Visual C++ Redistributable 2019+
2. Pastikan driver grafis terbaru terinstal
3. Jika menggunakan OS lain selain Windows, aplikasi ini tidak didukung

### Error "Kalkulasi material tidak dapat diselesaikan"

**Masalah**: Sistem tidak dapat menyelesaikan kalkulasi material.

**Solusi**:
1. Periksa apakah semua dimensi material masuk akal (tidak terlalu besar/kecil)
2. Pastikan menggunakan jenis kaca dan aluminium yang kompatibel
3. Coba dengan metode optimasi yang berbeda
4. Pecah pekerjaan besar menjadi beberapa bagian yang lebih kecil

### Error "Algoritma Optimasi Tidak Konvergen"

**Masalah**: Algoritma optimasi pemotongan tidak dapat menemukan solusi.

**Solusi**:
1. Periksa dan sesuaikan dimensi stock material (terlalu kecil?)
2. Kurangi kompleksitas dengan membagi menjadi beberapa pekerjaan
3. Coba metode optimasi alternatif (First-Fit atau Guillotine)
4. Tambahkan margin yang lebih besar untuk pemotongan

## Pemulihan Data dan Backup

### Memulihkan Data dari Backup

Aplikasi secara otomatis membuat backup data di folder:
`%LOCALAPPDATA%\AluGlassEstimator\Backup`

Untuk memulihkan:
1. Tutup aplikasi
2. Salin file backup ke folder `%LOCALAPPDATA%\AluGlassEstimator\Data`
3. Ganti file yang rusak
4. Buka kembali aplikasi

### Memulai Ulang dengan Data Kosong

Jika ingin memulai dari awal:
1. Tutup aplikasi
2. Hapus atau ganti nama folder `%LOCALAPPDATA%\AluGlassEstimator`
3. Buka kembali aplikasi (folder baru akan dibuat secara otomatis)

## Menghubungi Support

Jika masalah Anda tidak dapat diselesaikan dengan langkah-langkah di atas, silakan hubungi tim dukungan di:

- Email: support@aluglass-estimator.example.com
- WhatsApp: +62-xxx-xxxx-xxxx
- Situs web: https://www.aluglass-estimator.example.com/support

Siapkan informasi berikut saat menghubungi dukungan:
1. Versi aplikasi (dapat dilihat di About)
2. OS Windows yang digunakan
3. Deskripsi lengkap masalah
4. Screenshot error (jika ada)
5. Log error dari folder `%LOCALAPPDATA%\AluGlassEstimator\Logs`