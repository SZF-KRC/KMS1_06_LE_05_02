using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_06_LE_05_02
{
    internal class UserInput
    {
        /// <summary>
        /// Liest eine Ganzzahl vom Benutzer ein und stellt sicher, dass die Eingabe nicht leer ist.
        /// </summary>
        /// <param name="prompt">Die Anzeigeaufforderung.</param>
        /// <returns>Die Benutzereingabe als Ganzzahl.</returns>
        public int InputNumber(string prompt)
        {
            int number;
            while (true)
            {
                Console.Write($"\n{prompt}");// Anzeige der Eingabeaufforderung an den Benutzer
                string input = Console.ReadLine();// Benutzereingabe lesen
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\n--- Eingabe darf nicht leer sein ---\n");
                    continue;// Schleife fortsetzen, wenn die Eingabe leer ist
                }
                try
                {
                    number = Convert.ToInt32(input);// Versuch, die Eingabe in eine Ganzzahl zu konvertieren
                    break;// Schleife unterbrechen, wenn die Konvertierung erfolgreich is
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n--- Geben Sie nur eine Ganzzahl ein ---\n" + e.Message);
                }
            }
            return number;// Rückgabe der Ganzzahl
        }

        /// <summary>
        /// Liest eine Zeichenkette vom Benutzer ein und stellt sicher, dass die Eingabe nicht leer ist.
        /// </summary>
        /// <param name="promt">Die Anzeigeaufforderung.</param>
        /// <returns>Die Benutzereingabe als Zeichenkette.</returns>
        public string InputString(string prompt)
        {
            bool exit = false;
            string input = "";
            while (!exit)
            {
                Console.Write(prompt);// Anzeige der Eingabeaufforderung an den Benutzer
                input = Console.ReadLine();
                if (input.Length < 1) // Überprüfung auf leere Eingabe
                {
                    Console.WriteLine("\n--- Die Eingabe darf nicht leer sein ---\n");
                }
                else
                {
                    exit = true;// Beenden der Schleife bei gültiger Eingabe
                }
            }
            return input;// Rückgabe der Zeichenkette
        }

        /// <summary>
        /// Der Benutzer gibt die vollständige Pfadtext-Datei ein, nach der er sucht. Wenn er eine ungültige Eingabe macht, fordert das Programm ihn auf, ihm zu helfen, und die Hilfsmethode GetCurrentPath() wird gestartet
        /// </summary>
        /// <param name="prompt">Die Anzeigeaufforderung.</param>
        /// <returns>Gibt den vollständigen Pfad zurück, falls vorhanden, andernfalls wird null zurückgegeben.</returns>
        public string InputPath(string prompt)
        {
            int error = 0;
            while(error < 2)
            {
                string inputPath = InputString(prompt);
                if(File.Exists(inputPath))
                {
                    return inputPath;
                }
                else
                {
                    Console.WriteLine("\n--- Das Textdokument existiert nicht. ---\n");
                    if(InputString("Brauchen Sie hilfe mit Pfad (ja/nein): ").ToLower() == "ja")
                    {
                        inputPath = GetCurrentPath();
                        if(inputPath != null)
                        {
                            return inputPath;
                        }else
                        {
                            Console.WriteLine("\n--- Das Textdokument existiert nicht. ---\n");                           
                        }                    
                    }                   
                }
                error++;
            }
            return null;
        }

        /// <summary>
        /// Eine Hilfsmethode, die den aktuellen Ordner des Programms anzeigt und der Benutzer nur den Namen des Textdokuments schreibt.
        /// </summary>
        /// <returns>Gibt den vollständigen Pfad zurück, falls vorhanden, andernfalls wird null zurückgegeben.</returns>
        private string GetCurrentPath()
        {
            string currentPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            Console.WriteLine(currentPath + "\\");
            string currentFilePath = InputString("Adresa zlozky s dokumentmi. Doplnte este nazov dokumentu napr. (dokument.txt): ");
            if(File.Exists (currentPath+"\\"+ currentFilePath))
                {
                return currentPath + "\\" + currentFilePath;
                }
            return null;
        }
    }
}
