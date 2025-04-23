# Petunjuk Penggunaan AluGlass Estimator di Replit

## Informasi Penting

AluGlass Estimator adalah aplikasi desktop Windows yang dikembangkan menggunakan C# dan .NET 7 dengan Avalonia UI. Karena sifat aplikasi ini sebagai aplikasi desktop dengan antarmuka grafis, aplikasi tidak dapat dijalankan langsung di lingkungan Replit.

## Menjalankan Aplikasi di Replit

Saat Anda menjalankan aplikasi di Replit, Anda akan melihat pesan informasi yang menjelaskan bahwa aplikasi harus diunduh dan dijalankan di komputer Windows Anda.

```
======================================================
AluGlass Estimator - Aplikasi Desktop Windows
======================================================
Aplikasi ini adalah aplikasi desktop yang dirancang untuk Windows.
Anda tidak dapat menjalankannya langsung di Replit.

Untuk menggunakan aplikasi ini:
1. Unduh file AluGlassApp.App.exe dari folder 'publish'
2. Pastikan untuk mengunduh semua file DLL pendukung juga
3. Jalankan aplikasi di komputer Windows Anda

Silakan lihat CARA_UNDUH.md untuk instruksi lengkap.
======================================================
```

## Mengapa Tidak Bisa Dijalankan di Replit?

Replit adalah lingkungan pengembangan berbasis cloud yang berjalan di server Linux. Aplikasi desktop yang menggunakan library GUI native seperti Avalonia membutuhkan dependensi sistem yang spesifik untuk platform (seperti libfontconfig.so.1 di Linux, atau komponen Windows UI di Windows). Beberapa dependensi ini tidak tersedia di lingkungan Replit.

## Bagaimana Cara Menggunakan AluGlass Estimator?

Untuk menggunakan aplikasi, ikuti langkah-langkah berikut:

1. **Unduh Aplikasi:**
   - Navigasikan ke folder `AluGlassEstimator/publish`
   - Unduh file `AluGlassApp.App.exe` dan semua file DLL yang diperlukan

2. **Jalankan di Windows:**
   - Pindahkan semua file ke folder yang sama di komputer Windows Anda
   - Jalankan file `AluGlassApp.App.exe`

Untuk instruksi lebih rinci, silakan lihat [CARA_UNDUH.md](../CARA_UNDUH.md).

## Memodifikasi Aplikasi

Meskipun Anda tidak dapat menjalankan aplikasi secara langsung di Replit, Anda masih dapat:

1. **Melihat dan Mengedit Kode:**
   - Semua kode sumber tersedia di folder `src`
   - Anda dapat mengedit file-file kode untuk membuat perubahan

2. **Melakukan Build:**
   - Menggunakan perintah `dotnet build` untuk memverifikasi bahwa kode dapat dikompilasi
   - Menggunakan perintah `dotnet publish -r win-x64 --self-contained true -p:PublishSingleFile=true -c Release src/AluGlassApp.App/AluGlassApp.App.csproj` untuk membuat build baru untuk Windows

## Fitur Aplikasi

Untuk informasi tentang fitur yang tersedia dalam aplikasi, silakan lihat [RINGKASAN_APLIKASI.md](../RINGKASAN_APLIKASI.md).