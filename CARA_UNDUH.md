# Cara Mengunduh dan Menginstal AluGlass Estimator

## Persyaratan Sistem
- Windows 10 atau Windows 11 (64-bit)
- .NET Runtime 7.0 atau yang lebih baru
- Visual C++ Redistributable 2019 atau yang lebih baru
- Minimal 4GB RAM
- 100MB ruang disk kosong
- Resolusi layar minimal 1280x720

## Langkah-langkah Pengunduhan dan Instalasi

### 1. Mengunduh Aplikasi
1. Kunjungi halaman [Release](https://github.com/yourusername/AluGlassEstimator/releases) dari repositori ini
2. Unduh file `AluGlassEstimator_v3.zip` dari daftar rilis terbaru
3. Alternatif: Unduh langsung dari link [ini](https://example.com/AluGlassEstimator_v3.zip)

### 2. Persiapan Komputer
1. Pastikan Anda memiliki .NET Runtime 7.0 terinstal:
   - Unduh dari [situs Microsoft](https://dotnet.microsoft.com/download/dotnet/7.0) jika belum terinstal
   - Pilih ".NET Runtime 7.0" (bukan SDK)

2. Pastikan Visual C++ Redistributable 2019 terinstal:
   - Unduh dari [situs Microsoft](https://aka.ms/vs/17/release/vc_redist.x64.exe)
   - Jalankan installer dan ikuti petunjuk yang muncul

### 3. Instalasi
1. Buat folder baru di lokasi yang mudah diakses (misalnya di Desktop atau Documents)
2. Ekstrak semua file dari `AluGlassEstimator_v3.zip` ke folder tersebut
3. Pastikan semua file berikut ada di folder hasil ekstraksi:
   - AluGlassApp.exe (Aplikasi utama)
   - Semua file DLL yang diperlukan
   - Folder Assets dengan semua isinya
   - File konfigurasi (.json dan .dll)

### 4. Menjalankan Aplikasi
1. Klik dua kali pada file `AluGlassApp.exe` untuk menjalankan aplikasi
2. Jika muncul peringatan Windows Defender atau SmartScreen:
   - Klik "More info" atau "Informasi lebih lanjut"
   - Pilih "Run anyway" atau "Jalankan saja"

### 5. Memulai Penggunaan
1. Saat pertama kali dijalankan, aplikasi akan membuat folder data di:
   - `%LOCALAPPDATA%\AluGlassEstimator`
2. Semua data, pengaturan, dan ekspor akan disimpan di folder tersebut
3. Baca dokumen "RINGKASAN_APLIKASI.md" untuk memulai menggunakan aplikasi

## Pemecahan Masalah

Jika Anda mengalami masalah saat menjalankan aplikasi, silakan cek hal-hal berikut:

1. **Aplikasi tidak dapat dijalankan atau error saat startup**:
   - Pastikan .NET Runtime 7.0 terinstal dengan benar
   - Pastikan Visual C++ Redistributable 2019 terinstal
   - Pastikan semua file DLL berada di folder yang sama dengan executable

2. **Error "File DLL yang diperlukan tidak ditemukan"**:
   - Ekstrak ulang semua file dari ZIP ke folder kosong
   - Pastikan antivirus tidak memblokir atau mengkarantina file DLL

3. **Error akses atau permission**:
   - Jalankan aplikasi sebagai Administrator (klik kanan -> Run as administrator)
   - Pindahkan aplikasi ke lokasi yang memiliki akses penuh (misalnya: Documents)

Untuk bantuan lebih lanjut, silakan lihat dokumen "PANDUAN_PEMECAHAN_MASALAH.md" atau hubungi tim dukungan di example@support.com.

## Download Ulang

Jika file ZIP korup atau tidak lengkap, Anda dapat mengunduh ulang dari:
1. [GitHub Release](https://github.com/yourusername/AluGlassEstimator/releases)
2. [Google Drive](https://drive.google.com/file/example)
3. [DropBox](https://dropbox.com/example)

## Pembaruan Aplikasi

Untuk mendapatkan versi terbaru:
1. Unduh versi terbaru dari halaman Release
2. Ekstrak ke folder baru
3. Salin file data dari instalasi lama jika diperlukan (opsional)

## Catatan Penting

- Aplikasi ini dirancang untuk Windows dan tidak berjalan di platform lain
- Jangan pindahkan atau ubah nama file di dalam folder aplikasi
- Data aplikasi disimpan terpisah dari folder instalasi untuk kemudahan backup