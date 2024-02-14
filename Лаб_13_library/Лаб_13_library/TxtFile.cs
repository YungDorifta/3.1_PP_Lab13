using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;

namespace Лаб_13_library
{
    //работа с текстовым файлом
    public class TxtFile
    {
        //поля:
        //путь к файлу
        public string FilePath;
        //имя файла
        string _FileName;
        //содержимое файла
        string _FileText;
        //поток чтения для принтера
        StreamReader PrinterStream;
        //шрифт для вывода в принтер
        Font PrinterFont;

        //методы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="FileName">Имя файла</param>
        public TxtFile(string FileName)
        {
            this._FileName = FileName;
            this.FilePath = FileName;
            this._FileText = GetFileText(FileName);
        }

        /// <summary>
        /// геттер/сеттер имени файла
        /// </summary>
        public string NameOfFile
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;
            }
        }

        /// <summary>
        /// геттер/сеттер текста внутри файла
        /// </summary>
        public string TextInFile
        {
            get
            {
                return this._FileText;
            }
            set
            {
                this._FileText = value;
            }
        }

        /// <summary>
        /// Чтение текста из файла
        /// </summary>
        /// <param name="Filename"></param>
        /// <returns></returns>
        public string GetFileText(string Filename)
        {
            string FileText = "";

            try
            {
                //чтение содержимого файла через поток 
                System.IO.StreamReader FileReader = new System.IO.StreamReader(Filename, System.Text.Encoding.GetEncoding(1251));
                FileText = FileReader.ReadToEnd();
                FileReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return FileText;
        }

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <returns>Успешность сохранения файла</returns>
        public bool SaveFile()
        {
            try
            {
                //перезапись файла при его существовании
                Encoding encoding = Encoding.Unicode;
                StreamWriter InFileWriter = new StreamWriter(this._FileName, false, encoding);

                //получение содержимого файла построчно
                string[] str = this._FileText.Split('\n');

                //запись полученных строк в файл
                for (int i = 0; i < str.Length; i++) InFileWriter.WriteLine(str[i]);

                //закрыть поток записи
                InFileWriter.Close();

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка сохранения файла!");
                return false;
            }
        }

        /// <summary>
        /// Печать одной страницы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            //размеры окна печати
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            //распечатываемая строка
            string line = null;
            //стартовая позиция
            int count = 0;

            //вычисление количества строк на странице
            linesPerPage = ev.MarginBounds.Height / PrinterFont.GetHeight(ev.Graphics); 

            //печать каждой строки страницы
            while (count < linesPerPage && ((line = PrinterStream.ReadLine()) != null))
            {
                //позицыя распечатывания
                yPos = topMargin + (count * PrinterFont.GetHeight(ev.Graphics));
                //распечатывание строки
                ev.Graphics.DrawString(line, PrinterFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                //увеличение значения счетчика строк
                count++;
            }

            //если остались строки - оставить сигнал, что нужно распечатать еще страницы
            if (line != null) 
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        /// <summary>
        /// Печать всех страниц
        /// </summary>
        /// <param name="PrintingFont">Шрифт вывода файла</param>
        /// <returns>Успех вывода файла</returns>
        public bool PrintAllPages(Font PrintingFont)
        {
            try
            {
                //поток вывода
                this.PrinterStream = new StreamReader(this._FileName, Encoding.UTF8);

                try
                {
                    //установка шрифта файла
                    this.PrinterFont = PrintingFont;
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(PrintPage);
                    //вывод файла
                    pd.Print();
                    return true;
                }
                finally
                {
                    //закрыть поток вывода файла после вывода
                    this.PrinterStream.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка печати файла!");
                return false;
            }
        }

        /// <summary>
        /// Нахождение кол-ва слов в тексте
        /// </summary>
        /// <returns>Кол-во слов в тексте</returns>
        public int CountWords()
        {
            //разделители строк
            string[] separators = { " ", ".", ",", "\t", "\n", ";", ":", "!", "?", "-" };

            //разделение текста на слова
            string[] FileWordsArray = this._FileText.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //вывод количества слов
            return FileWordsArray.Length;
        }

        /// <summary>
        /// Перегрузка оператора проверки файлов на равенство
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator ==(TxtFile obj1, TxtFile obj2)
        {
            return obj1.TextInFile.Length == obj2.TextInFile.Length;
        }

        /// <summary>
        /// Перегрузка оператора проверки файлов на неравенство
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator !=(TxtFile obj1, TxtFile obj2)
        {
            return obj1.TextInFile.Length != obj2.TextInFile.Length;
        }

        /// <summary>
        /// Поиск файла в указанном каталоге
        /// </summary>
        /// <param name="directory">Каталог поиска</param>
        /// <param name="search_pattern"></param>
        /// <returns></returns>
        public static string[] File_search(string directory, string search_pattern = "") => search_pattern == "" ? Directory.GetFiles(directory) : Directory.GetFiles(directory, search_pattern);
        
        /// <summary>
        /// Сравнение файлов по имени
        /// </summary>
        public class NameComparer : IComparer<TxtFile>
        {
            public int Compare(TxtFile obj1, TxtFile obj2)
            {
                //вернуть результат сравнения имен файлов
                return String.Compare(System.IO.Path.GetFileName(obj1.FilePath), System.IO.Path.GetFileName(obj2.FilePath));
            }
        }

        /// <summary>
        /// сравнение файлов по количеству слов содержимого текста
        /// </summary>
        public class NumberOfWordsComparer : IComparer<TxtFile>
        {
            public int Compare(TxtFile obj1, TxtFile obj2)
            {
                //вернуть результат сравнения количества слов
                return String.Compare(obj1.CountWords().ToString(), obj2.CountWords().ToString());
            }
        }

        /// <summary>
        /// Сравнение файлов по количеству символов содержимого текста
        /// </summary>
        public class MyNumOfSymbsComparer : IComparer<TxtFile>
        {
            public int Compare(TxtFile obj1, TxtFile obj2)
            {
                return String.Compare(obj1._FileText.Length.ToString(), obj2._FileText.Length.ToString());
            }
        }
        
        /// <summary>
        /// метод обработки файла по своему варианту (найти слова в конце предложений, заканчивающихся на заданную букву/слог)
        /// </summary>
        /// <param name="Ending">конечный слог/буква слова</param>
        /// <returns>массив соответствующих слов</returns>
        public string[] FindEndWords(string Ending)
        {
            //длина окончания слова
            int EndingLength = Ending.Length;

            //нахождение массива предложений
            string[] SeparatorsForSentence = { ".", "?", "!" };
            string[] Sentences = this._FileText.Split(SeparatorsForSentence, StringSplitOptions.RemoveEmptyEntries);

            //нахождение массива слов в конце предложений
            string[] LastWords = new string[Sentences.Length];
            string[] SeparatorsForWords = { " ", ",", "\t", "\n", ";", ":", "-" };
            int count = 0;
            foreach (string Sentence in Sentences)
            {
                string[] WordsInSentence = Sentence.Split(SeparatorsForWords, StringSplitOptions.RemoveEmptyEntries);
                LastWords[count] = WordsInSentence[WordsInSentence.Length - 1];
                count++;
            }
            
            //нахождение среди последних слов в предложениях тех, которые содержат введенную букву/слог
            string FoundWordsString = "";
            foreach (string Word in LastWords)
            {
                //если концовка слова соответствует введенной
                if (Word.Substring(Word.Length - EndingLength, EndingLength) == Ending)
                {
                    //добавить ее в строку финальных слов с пробелом
                    FoundWordsString += Word + " ";
                }
            }


            //найти конечный массив слов
            string[] FinalSeparators = { " " };
            string[] FoundWords = FoundWordsString.Split(FinalSeparators, StringSplitOptions.RemoveEmptyEntries);

            //возвратить массив из найденных слов
            return FoundWords;
        }

        /// <summary>
        /// Перегрузка присваивания хэш-кода
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Перегрузка сравнения-равенства
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
