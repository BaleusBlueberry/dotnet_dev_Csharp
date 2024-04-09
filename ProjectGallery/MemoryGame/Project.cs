﻿using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MemoryGame
{
    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "Memory Game";

        public BitmapImage Icon
        {
            get
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/Memory.png");
                return new BitmapImage(uri);
            }
        }

        public void Run()
        {
            MainWindow winodw = new MainWindow();
            winodw.ShowDialog();
        }
    }
}