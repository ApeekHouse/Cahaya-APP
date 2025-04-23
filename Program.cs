using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using SkiaSharp;
using System.Globalization;
using System.Collections.Generic;

namespace AluGlassApp.App
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            // Set the working directory to the application location
            try {
                string? location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (location != null) {
                    Directory.SetCurrentDirectory(location);
                }
            } catch {
                // Ignore errors, we'll continue with current directory
            }
            
            try
            {
                // Check if we're running on Replit
                if (IsReplitEnvironment())
                {
                    // We're on Replit, show comprehensive info
                    ShowReplitInformation();
                    return;
                }
                
                // Check if we're on Linux (not Replit)
                if (IsLinuxEnvironment() && !IsReplitEnvironment())
                {
                    System.Console.WriteLine("======================================================");
                    System.Console.WriteLine("AluGlass Estimator - Aplikasi Desktop Windows");
                    System.Console.WriteLine("======================================================");
                    System.Console.WriteLine("Aplikasi ini dioptimalkan untuk Windows dan mungkin tidak");
                    System.Console.WriteLine("berjalan dengan baik di Linux karena ketergantungan pada");
                    System.Console.WriteLine("library khusus Windows.");
                    System.Console.WriteLine("Gunakan versi Windows untuk pengalaman optimal.");
                    System.Console.WriteLine("======================================================");
                    
                    // We still try to run on Linux, but preemptively handle SkiaSharp issues
                    try
                    {
                        // Test SkiaSharp initialization
                        var testInfo = new SkiaSharp.SKImageInfo(1, 1);
                    }
                    catch (TypeInitializationException ex)
                    {
                        System.Console.Error.WriteLine($"Error initializing SkiaSharp: {ex.Message}");
                        System.Console.Error.WriteLine("Library native untuk rendering grafis tidak tersedia di sistem ini.");
                        System.Console.Error.WriteLine("Aplikasi ini dirancang untuk Windows dan memerlukan library khusus.");
                        System.Console.Error.WriteLine("Silakan gunakan di sistem Windows atau lihat CARA_UNDUH.md");
                        return;
                    }
                }
                
                // Check for required DLLs on Windows
                if (IsWindowsEnvironment() && !AreRequiredDllsPresent())
                {
                    ShowErrorMessage(
                        "File DLL yang diperlukan tidak ditemukan.\n\n" +
                        "Pastikan semua file berikut berada di folder yang sama dengan executable:\n" +
                        "- av_libglesv2.dll\n" +
                        "- e_sqlite3.dll\n" +
                        "- libHarfBuzzSharp.dll\n" +
                        "- libSkiaSharp.dll\n\n" +
                        "Coba ekstrak ulang file AluGlassEstimator_v3.zip ke folder kosong."
                    );
                    
                    System.Console.Error.WriteLine("ERROR: File DLL yang diperlukan tidak ditemukan.");
                    System.Console.Error.WriteLine("Pastikan semua file berikut berada di folder yang sama dengan executable:");
                    System.Console.Error.WriteLine("- av_libglesv2.dll");
                    System.Console.Error.WriteLine("- e_sqlite3.dll");
                    System.Console.Error.WriteLine("- libHarfBuzzSharp.dll");
                    System.Console.Error.WriteLine("- libSkiaSharp.dll");
                    return;
                }
                
                // Check for necessary access rights
                if (!HasWriteAccessToAppFolder())
                {
                    ShowErrorMessage(
                        "Aplikasi memerlukan akses tulis ke folder aplikasi.\n\n" +
                        "Coba salah satu dari tindakan berikut:\n" +
                        "1. Jalankan aplikasi sebagai Administrator (klik kanan -> Run as administrator)\n" +
                        "2. Pindahkan aplikasi ke folder yang memiliki akses penuh (misal: Documents)\n" +
                        "3. Pastikan folder aplikasi tidak dalam mode read-only"
                    );
                    
                    System.Console.Error.WriteLine("ERROR: Tidak memiliki akses tulis ke folder aplikasi.");
                    return;
                }
                
                // Ensure data directories exist
                EnsureDataDirectoriesExist();
                
                // Test SkiaSharp initialization before starting the app
                try
                {
                    var testInfo = new SkiaSharp.SKImageInfo(1, 1);
                }
                catch (TypeInitializationException ex)
                {
                    System.Console.Error.WriteLine($"Error initializing SkiaSharp: {ex.Message}");
                    ShowErrorMessage(
                        "Gagal menginisialisasi komponen grafis (SkiaSharp).\n\n" +
                        "Kemungkinan penyebab:\n" +
                        "- Library DLL yang diperlukan tidak terpasang atau rusak\n" +
                        "- Library grafis sistem tidak tersedia\n\n" +
                        "Silakan coba: \n" +
                        "1. Pastikan Visual C++ Redistributable 2019+ terinstal\n" +
                        "2. Ekstrak ulang semua file dari paket ZIP\n" +
                        "3. Gunakan di sistem Windows 10 atau yang lebih baru"
                    );
                    return;
                }
                
                // We're on Windows or other supported platform, proceed normally
                BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex)
            {
                // Log exception details to a file for troubleshooting
                try
                {
                    string logDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AluGlassEstimator", "Logs");
                    Directory.CreateDirectory(logDir);
                    string logFile = Path.Combine(logDir, $"error_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                    File.WriteAllText(logFile, $"Error Time: {DateTime.Now}\nError Message: {ex.Message}\nStack Trace: {ex.StackTrace}");
                    
                    ShowErrorMessage(
                        $"Aplikasi mengalami kesalahan: {ex.Message}\n\n" +
                        $"Detail kesalahan telah disimpan di:\n{logFile}\n\n" +
                        "Silakan coba jalankan aplikasi kembali atau hubungi dukungan teknis."
                    );
                }
                catch
                {
                    // If we can't even log the error, show a simple message
                    ShowErrorMessage(
                        $"Aplikasi mengalami kesalahan yang tidak terduga: {ex.Message}\n\n" +
                        "Silakan coba jalankan aplikasi kembali atau hubungi dukungan teknis."
                    );
                }
                
                System.Console.Error.WriteLine($"Aplikasi mengalami kesalahan: {ex.Message}");
                System.Console.Error.WriteLine("Silakan coba jalankan aplikasi kembali atau hubungi dukungan teknis.");
            }
        }
        
        // Check if running in Replit environment
        private static bool IsReplitEnvironment()
        {
            try
            {
                // Deteksi lingkungan Replit berdasarkan variabel lingkungan
                string? replId = System.Environment.GetEnvironmentVariable("REPL_ID");
                string? replOwner = System.Environment.GetEnvironmentVariable("REPL_OWNER");
                string? replSlug = System.Environment.GetEnvironmentVariable("REPL_SLUG");
                
                // Jika salah satu variabel lingkungan Replit ditemukan, kita ada di Replit
                bool isReplit = !string.IsNullOrEmpty(replId) || 
                               !string.IsNullOrEmpty(replOwner) || 
                               !string.IsNullOrEmpty(replSlug);
                
                // Untuk pengembangan, uncomment baris di bawah untuk mengabaikan deteksi Replit
                // isReplit = false;
                
                return isReplit;
            }
            catch
            {
                return false;
            }
        }
        
        // Check if running on Windows
        private static bool IsWindowsEnvironment()
        {
            try
            {
                return System.Environment.OSVersion.Platform == PlatformID.Win32NT;
            }
            catch
            {
                return false;
            }
        }
        
        // Check if running on Linux
        private static bool IsLinuxEnvironment()
        {
            try
            {
                return System.Environment.OSVersion.Platform == PlatformID.Unix;
            }
            catch
            {
                return false;
            }
        }
        
        // Show comprehensive information when running on Replit
        private static void ShowReplitInformation()
        {
            System.Console.WriteLine("======================================================");
            System.Console.WriteLine("AluGlass Estimator - Aplikasi Desktop Windows");
            System.Console.WriteLine("======================================================");
            System.Console.WriteLine("Aplikasi ini adalah aplikasi desktop yang dirancang untuk Windows.");
            System.Console.WriteLine("Replit digunakan untuk repositori pengembangan dan distribusi, tetapi");
            System.Console.WriteLine("aplikasi ini tidak dapat dijalankan langsung di Replit atau di web browser.");
            System.Console.WriteLine();
            System.Console.WriteLine("Untuk menjalankan aplikasi ini, ikuti langkah-langkah berikut:");
            System.Console.WriteLine("1. Unduh file AluGlassEstimator_v3.zip dari repositori ini");
            System.Console.WriteLine("2. Ekstrak file ZIP tersebut ke komputer Windows Anda");
            System.Console.WriteLine("3. Jalankan file AluGlassApp.exe dari folder hasil ekstraksi");
            System.Console.WriteLine();
            System.Console.WriteLine("Untuk informasi lebih detail, silakan baca dokumen:");
            System.Console.WriteLine("- CARA_UNDUH.md: Instruksi pengunduhan dan instalasi");
            System.Console.WriteLine("- RINGKASAN_APLIKASI.md: Gambaran umum fitur aplikasi");
            System.Console.WriteLine("- PANDUAN_PEMECAHAN_MASALAH.md: Solusi untuk masalah umum");
            System.Console.WriteLine("======================================================");
        }
        
        // Check if required DLLs are present in the application directory
        private static bool AreRequiredDllsPresent()
        {
            try
            {
                string[] requiredDlls = new[]
                {
                    "av_libglesv2.dll",
                    "e_sqlite3.dll",
                    "libHarfBuzzSharp.dll",
                    "libSkiaSharp.dll"
                };
                
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                
                return requiredDlls.All(dll => File.Exists(Path.Combine(appDirectory, dll)));
            }
            catch
            {
                // If we can't check, assume they're present
                return true;
            }
        }
        
        // Check if we have write access to the application folder
        private static bool HasWriteAccessToAppFolder()
        {
            try
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string testFile = Path.Combine(appDirectory, $"write_test_{Guid.NewGuid()}.tmp");
                
                // Try to create a test file
                File.WriteAllText(testFile, "test");
                
                // If we got here, we have write access
                try
                {
                    File.Delete(testFile);
                }
                catch
                {
                    // Ignore cleanup errors
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        // Ensure all necessary data directories exist
        private static void EnsureDataDirectoriesExist()
        {
            try
            {
                // Create application data folder
                string appDataPath = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                    "AluGlassEstimator"
                );
                
                // Create necessary subdirectories
                string[] folders = new[] {
                    "Data",     // For database files
                    "Exports",  // For exported reports
                    "Logs",     // For error logs
                    "Backup",   // For data backups
                    "Temp"      // For temporary files
                };
                
                Directory.CreateDirectory(appDataPath);
                
                foreach (string folder in folders)
                {
                    Directory.CreateDirectory(Path.Combine(appDataPath, folder));
                }
            }
            catch
            {
                // Ignore errors, we'll create folders as needed
            }
        }
        
        // Show an error message dialog (for Windows)
        private static void ShowErrorMessage(string message)
        {
            try
            {
                if (IsWindowsEnvironment())
                {
                    // Try to show a Windows message box
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/c mshta \"javascript:alert('{message.Replace("'", "\\'")}');window.close();\"";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                    
                    // Wait a bit for the dialog to show
                    Thread.Sleep(100);
                }
            }
            catch
            {
                // Ignore errors, we'll fallback to console messages
            }
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}
