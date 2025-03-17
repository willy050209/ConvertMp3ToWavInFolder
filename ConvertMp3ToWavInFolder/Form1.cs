using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;

namespace ConvertMp3ToWavInFolder
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static void ConvertMp3ToWavInFolderMethod()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog.SelectedPath;
                string[] mp3Files = Directory.GetFiles(folderPath, "*.mp3");

                foreach (string mp3FilePath in mp3Files)
                {
                    string wavFilePath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(mp3FilePath) + ".wav");
                    ConvertMp3ToWav(mp3FilePath, wavFilePath);
                }

                MessageBox.Show("轉換完成！");
            }
        }

        public static void ConvertMp3ToWav(string mp3FilePath, string wavFilePath)
        {
            try
            {
                using (Mp3FileReader mp3 = new Mp3FileReader(mp3FilePath))
                {
                    using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                    {
                        WaveFileWriter.CreateWaveFile(wavFilePath, pcm);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"轉換 {Path.GetFileName(mp3FilePath)} 時發生錯誤：{ex.Message}");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ConvertMp3ToWavInFolderMethod();
            Application.Exit();
        }
    }
}
