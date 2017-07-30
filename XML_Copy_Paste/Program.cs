using System;
using System.Xml.XPath;
using System.IO;


namespace TaskDr.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 45);

            var doc = new XPathDocument("First.xml");//Загружаю в doc xml файл
            XPathNavigator navi = doc.CreateNavigator();

            XPathExpression name = navi.Compile("copy1/name");//Скомпилированный запрос XPath
            XPathNodeIterator iteratorName = navi.Select(name);//итератор для поска имени

            XPathExpression exCopyFrom = navi.Compile("copy1/copyFrom");
            XPathNodeIterator iteratorFrom = navi.Select(exCopyFrom);

            XPathExpression exCopyTo = navi.Compile("copy1/copyTo");
            XPathNodeIterator iteratorTo = navi.Select(exCopyTo);

            string strName = null;//Имя файла из xml файла
            string strFrom = null;//Путь откуда копировать из xml файла
            string strTo = null;//Путь куда копировать из xml файла

            while (iteratorName.MoveNext())
            {
                strName = iteratorName.Current.Value;
            }
            Console.WriteLine("File Name: {0}", strName);

            while (iteratorFrom.MoveNext())
            {
                strFrom = iteratorFrom.Current.Value;
            }
            Console.WriteLine("Storing place: {0}", strFrom);

            while (iteratorTo.MoveNext())
            {
                strTo = iteratorTo.Current.Value;
            }
            Console.WriteLine("Copy to: {0}", strTo);

            Console.WriteLine(new string('-', 50));

            string filePlace = Path.Combine(strFrom, strName);
            string fileDest = Path.Combine(strTo, strName);

            try
            {
                File.Copy(filePlace, fileDest, true);
                Console.WriteLine("Файл успешно скопирован!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
