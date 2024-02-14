using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Лаб_13_library;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// тест подсчета слов
        /// </summary>
        [TestMethod]
        public void TestWordCount()
        {
            TxtFile TheFile = new TxtFile("testfile.txt");
            TheFile.TextInFile = "это просто тестовый файл. Если он не прочитается, будет очень плохо;	Однако, сдать экзамен будет нелегко... Но если я постараюсь, все получится! Наверное...М.";
            int actual = TheFile.CountWords();
            int expected = 24;
            Assert.AreEqual(expected, actual, "Подсчет количества слов в файле прошел неверно.");
        }

        /// <summary>
        /// тест операции сравнения 1
        /// </summary>
        [TestMethod]
        public void TestCompare1()
        {
            TxtFile TheFile1 = new TxtFile("testfile1.txt");
            TxtFile TheFile2 = new TxtFile("testfile2.txt");
            TheFile1.TextInFile = "123456 78910";
            TheFile2.TextInFile = "&389271 !!!!";

            bool actual = (TheFile1 == TheFile2);
            bool expected = true;
            Assert.AreEqual(expected, actual, "Проверка на равенство прошла неверно!");
        }

        /// <summary>
        /// тест операции сравнения 2
        /// </summary>
        [TestMethod]
        public void TestCompare2()
        {
            TxtFile TheFile1 = new TxtFile("testfile1.txt");
            TxtFile TheFile2 = new TxtFile("testfile2.txt");
            TheFile1.TextInFile = "ыфогшыфщшгршфщыаршщфым0";
            TheFile2.TextInFile = "1 !!!!";

            bool actual = (TheFile1 != TheFile2);
            bool expected = true;
            Assert.AreEqual(expected, actual, "Проверка на равенство прошла неверно!");
        }

        
        /// <summary>
        /// тест метода по варианту
        /// </summary>
        [TestMethod]
        public void TestMyVariant()
        {
            TxtFile TheFile = new TxtFile("testfile.txt");
            TheFile.TextInFile = "это просто тестовый файол. Если он не прочитается, будет очень плохол.	Однако, сдать экзамен будет нелегко... Но если я постараюсьол, все получится! Наверное...Мол.";
            string[] result = TheFile.FindEndWords("ол");
            string[] waiting = { "файол", "плохол", "Мол" };
            
            bool expected = true;
            for (int i = 0; i < result.Length; i++)
            {
                if (waiting[i] != result[i]) expected = false;
            }
            bool actual = true;

            Assert.AreEqual(expected, actual, "Метод по варианту работает неверно!");
        }
    }
}
