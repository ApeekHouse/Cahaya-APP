using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AluGlassApp.App.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace AluGlassApp.App
{
    public class App : Application
    {
        public static Window? MainWindow { get; private set; }
        
        // Lokasi folder data aplikasi
        public static string AppDataPath { get; private set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AluGlassEstimator"
        );
        
        public override void Initialize()
        {
            try
            {
                // Daftarkan handler untuk unhandled exceptions
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                
                // Pastikan direktori aplikasi ada
                EnsureApplicationDirectories();
                
                // Bersihkan file temporary yang mungkin tersisa dari crash sebelumnya
                Task.Run(() => CleanupTempFiles());
                
                // Inisialisasi aplikasi
                AvaloniaXamlLoader.Load(this);
            }
            catch (Exception ex)
            {
                // Log dan tampilkan error
                LogError(ex, "inisialisasi");
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            try
            {
                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    // Buat backup data jika diperlukan
                    Task.Run(() => CreateDataBackup());
                    
                    // Inisialisasi jendela utama
                    desktop.MainWindow = new MainWindow();
                    MainWindow = desktop.MainWindow;
                    
                    // Handler saat aplikasi akan ditutup
                    desktop.ShutdownRequested += Desktop_ShutdownRequested;
                }

                base.OnFrameworkInitializationCompleted();
            }
            catch (Exception ex)
            {
                // Log dan tampilkan error
                LogError(ex, "memulai aplikasi");
            }
        }
        
        // Handler ketika aplikasi akan ditutup
        private void Desktop_ShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
        {
            try
            {
                // Bersihkan file temporary
                CleanupTempFiles();
            }
            catch
            {
                // Abaikan error saat cleanup
            }
        }
        
        // Handler untuk unhandled exceptions di aplikasi
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                // Log dan tampilkan error
                LogError(ex, "runtime aplikasi");
            }
        }
        
        // Fungsi untuk memastikan direktori aplikasi ada
        private void EnsureApplicationDirectories()
        {
            try
            {
                // Buat direktori utama
                Directory.CreateDirectory(AppDataPath);
                
                // Buat subdirektori yang diperlukan
                string[] folders = new[] {
                    "Data",     // Untuk file database
                    "Exports",  // Untuk laporan yang diekspor
                    "Logs",     // Untuk log error
                    "Backup",   // Untuk backup data
                    "Temp"      // Untuk file sementara
                };
                
                foreach (string folder in folders)
                {
                    Directory.CreateDirectory(Path.Combine(AppDataPath, folder));
                }
            }
            catch
            {
                // Abaikan error, folder akan dibuat saat diperlukan
            }
        }
        
        // Buat backup data penting
        private void CreateDataBackup()
        {
            try
            {
                string dataDirectory = Path.Combine(AppDataPath, "Data");
                string backupDirectory = Path.Combine(AppDataPath, "Backup");
                
                if (!Directory.Exists(dataDirectory) || Directory.GetFiles(dataDirectory).Length == 0)
                {
                    // Tidak ada data untuk dibackup
                    return;
                }
                
                // Buat backup dengan timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupPath = Path.Combine(backupDirectory, $"backup_{timestamp}");
                Directory.CreateDirectory(backupPath);
                
                // Salin file data
                foreach (string file in Directory.GetFiles(dataDirectory))
                {
                    string fileName = Path.GetFileName(file);
                    File.Copy(file, Path.Combine(backupPath, fileName), true);
                }
                
                // Batasi jumlah backup (simpan 5 terakhir)
                string[] backupFolders = Directory.GetDirectories(backupDirectory);
                Array.Sort(backupFolders);
                
                if (backupFolders.Length > 5)
                {
                    for (int i = 0; i < backupFolders.Length - 5; i++)
                    {
                        try
                        {
                            Directory.Delete(backupFolders[i], true);
                        }
                        catch
                        {
                            // Abaikan error saat menghapus backup lama
                        }
                    }
                }
            }
            catch
            {
                // Abaikan error backup
            }
        }
        
        // Bersihkan file temporary
        private void CleanupTempFiles()
        {
            try
            {
                string tempDirectory = Path.Combine(AppDataPath, "Temp");
                
                if (Directory.Exists(tempDirectory))
                {
                    foreach (string file in Directory.GetFiles(tempDirectory))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch
                        {
                            // Abaikan error saat menghapus file temp
                        }
                    }
                }
            }
            catch
            {
                // Abaikan error cleanup
            }
        }
        
        // Log error ke file
        private void LogError(Exception ex, string stage)
        {
            try
            {
                // Log error ke file
                string logDir = Path.Combine(AppDataPath, "Logs");
                Directory.CreateDirectory(logDir);
                string logFile = Path.Combine(logDir, $"error_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                string logContent = 
                    $"Error Time: {DateTime.Now}\n" +
                    $"Stage: {stage}\n" +
                    $"Error Message: {ex.Message}\n" +
                    $"Stack Trace: {ex.StackTrace}";
                
                File.WriteAllText(logFile, logContent);
                
                // Tampilkan pesan error ke pengguna
                ShowErrorMessage(
                    $"Terjadi kesalahan saat {stage}: {ex.Message}\n\n" +
                    $"Detail kesalahan telah disimpan di:\n{logFile}\n\n" +
                    "Silakan coba jalankan aplikasi kembali atau hubungi dukungan teknis."
                );
            }
            catch
            {
                // Jika gagal log, coba tampilkan pesan error saja
                try
                {
                    ShowErrorMessage(
                        $"Terjadi kesalahan saat {stage}: {ex.Message}\n\n" +
                        "Silakan coba jalankan aplikasi kembali atau hubungi dukungan teknis."
                    );
                }
                catch
                {
                    // Kita sudah berusaha semaksimal mungkin
                }
            }
        }
        
        // Tampilkan pesan error ke pengguna
        private void ShowErrorMessage(string message)
        {
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    // Gunakan JavaScript alert melalui mshta
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/c mshta \"javascript:alert('{message.Replace("'", "\\'")}');window.close();\"";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                    
                    // Tunggu sebentar agar dialog muncul
                    Thread.Sleep(100);
                }
            }
            catch
            {
                // Abaikan error saat menampilkan pesan error
            }
        }
    }
}