using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Лаб_13_library;
using System.IO;

namespace Лаб_13
{
    public partial class Form1 : Form
    {
        //имя файла
        public string FileName;
        //файл
        TxtFile TextFile;
        //статус сохранения файла
        public bool save_file = false;


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Число слов: 0";
            toolStripStatusLabel2.Text = "Число строк: 0";
        }

        /// <summary>
        /// Создать файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateFile(object sender, EventArgs e)
        {
            //открытие диалоговогово окна
            openFileDialog1.Filter = "Текстовый файл(*.txt)|*.txt|Файл rtf(*.rtf)|*.rtf|Все файлы(*.*)|*.*";
            openFileDialog1.Title = "Создайте текстовый файл в нужном каталоге";

            //если нажали "ОК"
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //изменить имя файла на введенное
                FileName = openFileDialog1.FileName;

                //новое открытие текстового файла
                TxtFile TextFile = new TxtFile(FileName);
                
                //очистить текстовое поле вывода от старого текста
                richTextBox1.Clear();
                
                //вывод количества строк и слов текста в поле ввода-вывода
                toolStripStatusLabel1.Text = "Число слов: 0" ;
                toolStripStatusLabel2.Text = "Число строк: 0";
            }

            //вывести последнее действие: открытие файла
            toolStripStatusLabel3.Text = "Последнее действие: создать файл";
        }

        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile(object sender, EventArgs e)
        {
            //открытие диалоговогово окна
            openFileDialog1.Filter = "Текстовый файл(*.txt)|*.txt|Файл rtf(*.rtf)|*.rtf|Все файлы(*.*)|*.*";
            openFileDialog1.Title = "Открыть текстовый файл";

            //если нажали "ОК"
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //изменить имя файла на введенное
                FileName = openFileDialog1.FileName;
                
                //новое открытие текстового файла
                TxtFile TextFile = new TxtFile(FileName);

                //прочитать текстовый файл
                TextFile.GetFileText(FileName);

                //очистить текстовое поле вывода от старого текста
                richTextBox1.Clear();

                //вывести текст из полученного файла в редактор
                richTextBox1.AppendText(TextFile.TextInFile);

                //посчитать количество строк
                int linesCount = richTextBox1.Lines.Count();

                //текст, отображающийся только в поле ввода-вывода
                var mas = (richTextBox1.Text.Trim(' ', '\n')).Split(' ', '\n');

                //подсчет слов
                int words = 0;
                for (int i = 0; i < mas.Length; i++)
                    if (mas[i] != "") words++;

                //вывод количества строк и слов текста не всего файла, а только в поле ввода-вывода
                toolStripStatusLabel1.Text = "Число слов: " + words.ToString();
                toolStripStatusLabel2.Text = "Число строк: " + linesCount;
            }

            //вывести последнее действие: открытие файла
            toolStripStatusLabel3.Text = "Последнее действие: открыть файл";
        }

        /// <summary>
        /// Изменение текста в поле ввода-вывода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputFieldTextChanged(object sender, EventArgs e)
        {
            //обновление статуса
            toolStripStatusLabel3.Text = "Последнее действие: редактирование текста";
            
            //обновление данных о тексте
            int linesCount = richTextBox1.Lines.Count();
            var mas = (richTextBox1.Text.Trim(' ', '\n')).Split(' ', '\n');
            int words = 0;
            for (int i = 0; i < mas.Length; i++)
                if (mas[i] != "") words++;
            toolStripStatusLabel1.Text = "Число слов: " + words.ToString();
            toolStripStatusLabel2.Text = "Число строк: " + linesCount;
        }

        /// <summary>
        /// Очистка файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearFile(object sender, EventArgs e)
        {
            //очистить поле
            richTextBox1.Clear();
            //обновить последнее действие
            toolStripStatusLabel3.Text = "Последнее действие: очистка поля ввода-вывода";
        }
        
        /// <summary>
        /// Поиск файлов в указанном каталоге
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void найтиФайлыВУказанномКаталогеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //обновление последнего действия
            toolStripStatusLabel3.Text = "Последнее действие: открыть файл";

            //вызов другого окна
            Form2 f2 = new Form2();
            f2.Owner = this;
            f2.ShowDialog();

            //если файл не пуст
            if (f2.DataBuf != string.Empty)
            {
                
                //передача данных о выбранном в другом окне файле
                FileName = f2.DataBuf;
                TxtFile TextFile = new TxtFile(FileName);
                TextFile.GetFileText(FileName);

                //ввод текста из файла в поле ввода-вывода
                richTextBox1.Clear();
                richTextBox1.AppendText(TextFile.TextInFile);

                //обновление счетчиков строк и слов 
                int linesCount = richTextBox1.Lines.Count();
                var mas = (richTextBox1.Text.Trim(' ', '\n')).Split(' ', '\n');
                int words = 0;
                for (int i = 0; i < mas.Length; i++)
                    if (mas[i] != "") words++;
                toolStripStatusLabel1.Text = "Число слов: " + words.ToString();
                toolStripStatusLabel2.Text = "Число строк: " + linesCount;
            }
            else
            {
                richTextBox1.Text = "Ошибка!";
            }
        }

        /// <summary>
        /// Показать/скрыть панель инструментов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void показатьСкрытьПанельИнструментовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //изменить значение видимости панэли элементов на противоположное
            if (toolStrip1.Visible)
            {
                toolStrip1.Hide();
            }
            else
            {
                toolStrip1.Show();
            }
        }
        
        /// <summary>
        /// Счет кол-ва слов в тексте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountWordsInText(object sender, EventArgs e)
        {
            //подсчет кол-ва слов
            TxtFile TextFile = new TxtFile(FileName);
            int count = TextFile.CountWords();
            
            //вывод в всплывающее окошко результата
            MessageBox.Show("Количество слов в файле на данный момент: " + count.ToString(), "Подсчет количества слов", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        /// <summary>
        /// Метод по варианту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void методПоВариантуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(this);
            f3.Owner = this;
            f3.ShowDialog();
        }

        /// <summary>
        /// Показать информацию об авторе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAboutAuthor(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Белашев Арсений Дмитриевич\n Группа: 32928/1, №4", "Об авторе", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        
        //!!!
        /// <summary>
        /// Показать информацию о программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAboutProgram(object sender, EventArgs e)
        {
            MessageBox.Show("О программе... Да ну, такая себе :) ", "О программе");
        }

        /// <summary>
        /// Печать файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print(object sender, EventArgs e)
        {
            //открыть поток печати в файл печати, записать в файл содержимое поля ввода-вывода, закрыть поток
            StreamWriter PrinterWriter = new StreamWriter("rbt.txt");
            PrinterWriter.WriteLine(richTextBox1.Text);
            PrinterWriter.Close();

            //обновление имени файла
            FileName = saveFileDialog1.FileName;

            //открытие файла
            TxtFile TextFile = new TxtFile((FileName == "") ? "rbt.txt" : FileName);

            //печать файла
            //если ввод-вывод пуст
            if (richTextBox1.Text == "")
                return;
            else
            {
                //работа с диалоговым окном
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    //обновление статуса
                    toolStripStatusLabel3.Text = "Последнее действие: текст печатается...";
                    //печать всех страниц со шрифтом, соответствующем полю ввода-вывода
                    TextFile.PrintAllPages(richTextBox1.Font);
                }

                //обновление последнего действия
                toolStripStatusLabel3.Text = "Последнее действие: конец печати";
            }
        }

        /// <summary>
        /// Установть шрифт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFont(object sender, EventArgs e)
        {
            //если поле ввода-вывода
            if (richTextBox1.Text != "")
            {
                //работа с окнами
                if (fontDialog1.ShowDialog() == DialogResult.Cancel) return;
                if (colorDialog1.ShowDialog() == DialogResult.OK) richTextBox1.ForeColor = colorDialog1.Color;
                //сохранить шрифт
                richTextBox1.Font = fontDialog1.Font;
                //обновление последнего действия
                toolStripStatusLabel3.Text = "Последнее действие: изменение шрифта текста";
                return;
            }
        }

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile(object sender, EventArgs e)
        {
            //открытие нового текстового файла
            TxtFile TextFile = new TxtFile(FileName);

            //текст в открытом файле - текст в поле ввода-вывода
            TextFile.TextInFile= richTextBox1.Text;

            //если файл не существует
            if (!File.Exists(TextFile.FilePath))
            {
                //работа с диалоговым окном
                saveFileDialog1.Filter = "Текстовый файл(*.txt)|*.txt|Файл rtf(*.rtf)|*.rtf|Все файлы(*.*)|*.*";
                saveFileDialog1.Title = "Сохранените файл";

                //сохранение полученного имени файла
                FileName = saveFileDialog1.FileName;

                //вывод нового сохраненного файла
                TextFile = new TxtFile(FileName)
                {
                    TextInFile = richTextBox1.Text
                };

                //сохранить файл
                TextFile.SaveFile();
                save_file = true;
            }
            else
            {
                //просто сохранить файл
                TextFile.SaveFile();
                save_file = true;
            }

            //вывести последнее действие: сохранить файл
            toolStripStatusLabel3.Text = "Последнее действие: сохранить файл";
        }

        /// <summary>
        /// Сохранить файл как
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileAs(object sender, EventArgs e)
        {
            //открытие нового файла
            TxtFile TextFile = new TxtFile(FileName)
            {
                TextInFile = richTextBox1.Text
            };

            //работа с диалоговым окном
            saveFileDialog1.Filter = "Текстовый файл(*.txt)|*.txt|Файл rtf(*.rtf)|*.rtf|Все файлы(*.*)|*.*";
            saveFileDialog1.Title = "Сохранить файл как..";

            //когда работа с диалоговым окном завершена
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //обновить имя файла
                FileName = saveFileDialog1.FileName;
                //открыть его заново
                TextFile = new TxtFile(FileName)
                {
                    TextInFile = richTextBox1.Text
                };
                //сохранение файла
                TextFile.SaveFile();
                save_file = true;
            }

            //установить последнее действие - сохранение файла
            toolStripStatusLabel3.Text = "Последнее действие: сохранить файл";
        }

        /// <summary>
        /// Вызов процесса выхода из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Процесс выхода из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingProcess(object sender, FormClosingEventArgs e)
        {
            //если в поле ввода не пусто и файл не сохранен
            if (richTextBox1.Text != "" && !save_file)
            {
                //вызвать окно подтверждения
                DialogResult test = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNo);
                //если пользователь согласен
                if (test == DialogResult.Yes)
                    //вызвать сохранение
                    SaveFile(sender, e);
            }
        }
    }
}
