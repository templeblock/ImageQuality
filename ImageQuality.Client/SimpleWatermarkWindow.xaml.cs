﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ImageQuality;
using Microsoft.Win32;

namespace ImageQualityClient
{
    /// <summary>
    /// Interaction logic for SimpleWatermarkWindow.xaml
    /// </summary>
    public partial class SimpleWatermarkWindow : Window
    {
        private SimpleWatermarkDetect _watermarkDetect;

        public SimpleWatermarkWindow()
        {
            InitializeComponent();
            _watermarkDetect = new SimpleWatermarkDetect();
        }

        private void DetectText_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.jpg;*.png";
            if (ofd.ShowDialog() == true)
            {
                var fileBytes = File.ReadAllBytes(ofd.FileName);

                var sw = Stopwatch.StartNew();

                ResultText.Text = _watermarkDetect.Detect(fileBytes).Trim();

                sw.Stop();
                TimeText.Text = String.Format("{0}ms", sw.ElapsedMilliseconds);

                MemoryStream ms = new MemoryStream(fileBytes);
                var imageSource = new BitmapImage();
                imageSource.BeginInit();
                imageSource.StreamSource = ms;
                imageSource.EndInit();

                Image.Source = imageSource;
            }
        }
    }
}