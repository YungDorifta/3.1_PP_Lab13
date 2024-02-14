using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Лаб_13_library;

namespace Лаб_13
{
    public partial class Form2 : Form
    {
        //буфер
        public string DataBuf = string.Empty;
        //путь к файлу
        private string path;
        //ссылка на глав.форму
        Form1 mainform;
        //новый путь к каталогу(???)
        FolderBrowserDialog newPath;
        //список файлов
        List<TxtFile> files;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// Выбрать новый каталог
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseNewCatalog(object sender, EventArgs e)
        {
            //выбор нового каталога через спец.окно
            newPath = new FolderBrowserDialog();
            //новый список файлов
            files = new List<TxtFile>();
            if (newPath.ShowDialog() == DialogResult.OK)
            {
                //выбор директории
                DirectoryInfo dir = new DirectoryInfo(newPath.SelectedPath);
                //получение информации о файлах
                FileInfo[] Files = dir.GetFiles("*.*");
                //для каждого файла добавить имя в список
                foreach (FileInfo fi in Files)
                {
                    listBox1.Items.Add(fi.ToString());
                    files.Add(new TxtFile(fi.FullName));
                }
            }
            //обновление текста
            label2.Text = newPath.SelectedPath;
        }
        
        /// <summary>
        /// Обновить список файлов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadFileList(object sender, EventArgs e)
        {
            //очистить от предыдущего содержимого
            listBox1.Items.Clear();
            //определить директорию
            DirectoryInfo dir = new DirectoryInfo(label2.Text);
            //получить и записать имена файлов
            FileInfo[] files = dir.GetFiles(textBox1.Text);
            foreach (FileInfo fi in files)
            {
                listBox1.Items.Add(fi.ToString());
            }
        }

        /// <summary>
        /// Открыть выбранный файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSelectedFile(object sender, EventArgs e)
        {
            string fn = label2.Text;
            string fn2 = listBox1.SelectedItem.ToString();
            string fnfull = fn + "\\" + fn2;
            DataBuf = fnfull;
            this.Close();
        }
        
        /// <summary>
        /// Сортировка списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSort(object sender, EventArgs e)
        {
            if (newPath != null) { 
                //очистка от прошлого
                listBox1.Items.Clear();
                TxtFile tmp;
                foreach (string a in TxtFile.File_search(newPath.SelectedPath, textBox1.Text))
                    files.Add(tmp = new TxtFile(newPath.SelectedPath + '\\' + a));

                if (comboBox1.SelectedIndex == 0) files.Sort(new TxtFile.MyNumOfSymbsComparer());
                else if (comboBox1.SelectedIndex == 1) files.Sort(new TxtFile.NumberOfWordsComparer());
                else files.Sort(new TxtFile.NameComparer());

                for (int i = 0; i < files.Count; i++) listBox1.Items.Add(Path.GetFileName(files[i].FilePath));
            }
        }

        /// <summary>
        /// Запрет ввода символов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = '\0';
        }
    }
}
