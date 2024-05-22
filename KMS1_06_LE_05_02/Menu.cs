using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace KMS1_06_LE_05_02
{
    internal class Menu
    {
        UserInput userInput = new UserInput();
        Function function= new Function();
        /// <summary>
        /// Druckt das Menü und verarbeitet die Benutzereingaben.
        /// </summary>
        public void PrintMenu()
        {
            bool exit= false;
            while(!exit)
            {
                Console.WriteLine("\n*** Analyse von Textdateien ***\n[0] Programm beenden\n[1] Pfad zu einer Textdatei anzugeben\n[2] Lesen Textdokument\n[3] Statistick");
                switch (userInput.InputNumber("Geben Sie den Index Ihrer Wahl ein: "))
                {
                    case 0:exit=true; break;
                    case 1:AddPath();break;
                    case 2:ReadDocument();break;
                    case 3:Statistic(); break;
                    default: Console.WriteLine("\n--- Geben Sie nur den Index von 0-4 ein ---\n"); break;
                }
            }
        }

        /// <summary>
        /// Fordert den Benutzer auf, einen Pfad einzugeben, und fügt ihn zur Liste der Pfade hinzu.
        /// </summary>
        private void AddPath()
        {
            string pathFile = userInput.InputPath("Geben Sie Pfad ein: ");
            if (pathFile != null)
            {
                function.AddListPath(pathFile);
                Console.WriteLine("\nPfad wurde erfolgreich in das System eingegeben:\n" + pathFile);
            }else
            {
                Console.WriteLine("Zu viele haben Pfad falsch eingegeben");
            }
            
            
        }

        /// <summary>
        /// Liest das Dokument und zeigt die häufigsten Wörter an.
        /// </summary>
        private void ReadDocument()
        {
            int minLength = 4;//Minimale Anzahl von Buchstaben, die ein Wort haben muss
            if (function.GetListPath().Count == 1)
            {
                function.StreamRead(function.GetListPath().First().Trim(), minLength);// Wenn nur ein Pfad in der Liste ist, diesen Pfad verwenden
            }
            else if (function.GetListPath().Count > 1)
            {
                function.PrintList();// Wenn mehr als ein Pfad in der Liste ist, alle anzeigen und den Benutzer auffordern, einen auszuwählen
                string pathFile = userInput.InputPath("\nGeben Sie die vollständige Pfad der Datei ein, die Sie anzeigen möchten: ");
                if (pathFile != null)
                {
                    function.StreamRead(pathFile, minLength);
                }
                else
                {
                    Console.WriteLine("Zu viele haben Pfad falsch eingegeben");
                }
            }
            else
            {
                Console.WriteLine("\n--- Kein Textdokument existiert. Erste Geben Sie Pfad ---\n");
            }
        }

        /// <summary>
        /// Zeigt die Statistik für alle analysierten Textdokumente an.
        /// </summary>
        private void Statistic()
        {
            if (function.GetStatistic().Count != 0)
            {
                Console.WriteLine(function.Cage(100));
                foreach (var oneData in function.GetStatistic())
                {
                    // Ausgabe des Pfades des Textdokuments und der Gesamtanzahl der Wörter im Text
                    Console.WriteLine($"Pfad: {oneData.Item1}\nGesamtanzahl der Wörter: {oneData.Item2}");

                    // Ausgabe der 5 am häufigsten verwendeten Wörter und ihrer Häufigkeiten
                    Console.WriteLine("Top 5 häufigste Wörter:");
                    foreach (var word in oneData.Item3)
                    {
                        Console.WriteLine($"{word.Key}: {word.Value}");
                    }
                    Console.WriteLine(function.Cage(100));
                }
            }
            else { Console.WriteLine("\n--- Statistic is leer ---\n"); }
            
        }
    }
}
