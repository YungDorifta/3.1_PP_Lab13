using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Лаб_13_library;

namespace Лаб_13
{
    public partial class Form3 : Form
    {
        //форма-родитель
        Form1 f1;
        //исходный файл
        TxtFile TextFile ;

        /// <summary>
        /// Создание формы
        /// </summary>
        /// <param name="f1"></param>
        public Form3(Form1 f1)
        {
            InitializeComponent();
            //унаследовать файл от первой формы
            this.f1 = f1;
            this.TextFile = new TxtFile(f1.FileName);
        }
        
        /// <summary>
        /// При изменении текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") button1.Enabled = false;
            else button1.Enabled = true;
        }

        /// <summary>
        /// Вычислить результаты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            string text = "";
            string subtext = textBox1.Text;
            foreach (string str in TextFile.FindEndWords(subtext))
            {
                
                text += str + "\n";
            }
            richTextBox1.Text = text;
        }
    }
}
