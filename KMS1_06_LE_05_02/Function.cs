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
    /// <summary>
    /// Die Klasse, die Daten speichert und mit einem Textdokument arbeitet
    /// </summary>
    internal class Function
    {
        private HashSet<string> _listPath = new HashSet<string>();
        private Dictionary<string, int> _dictionaryOfWord = new Dictionary<string, int>();
        private List<(string, int, List<KeyValuePair<string, int>>)> _statistik = new List<(string, int, List<KeyValuePair<string, int>>)>();

        /// <summary>
        /// Vom Benutzer angegebener Anzeigelistenpfad
        /// </summary>
        public void PrintList()
        {
            foreach (var item in _listPath)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Wenn Sie ein bestimmtes Textdokument öffnen, werden auch Statistiken angezeigt
        /// </summary>
        /// <param name="path">Konkreter Pfad des Textdokuments</param>
        /// <param name="minLength">Minimale Anzahl von Buchstaben, die ein Wort haben muss</param>
        public void StreamRead(string path, int minLength)
        {
            string cage = "_";
            List<string> list = new List<string>();// Erstellen einer temporär Liste von Zeichenketten
            using (StreamReader reader = new StreamReader(path))// Öffnen der Datei mit einem StreamReader
            {
                Console.WriteLine(Cage(100));
                while (!reader.EndOfStream)// Lesen der Datei bis zum Ende
                {
                    string line = reader.ReadLine();
                    Console.WriteLine(line);
                    list.Add(line);// Hinzufügen der gelesenen Zeile zur Liste
                }
                Console.WriteLine(Cage(100));
                StreamCountTop5(path,list, minLength);// Aufrufen der Methode zum Zählen der Wörter und Anzeigen der 5 häufigsten Wörter
                list.Clear();// Leeren der Liste
            }
        }

        /// <summary>
        /// Den Text in einzelne Wörter aufteilen, sodass alle Satzzeichen vom Text getrennt werden,
        /// dann alle Wörter gezählt werden und schließlich die 5 am häufigsten verwendeten Wörter im Text angezeigt werden.
        /// </summary>
        /// <param name="path">Konkreter Pfad des Textdokuments</param>
        /// <param name="list">Liste von Zeichenketten, wobei jede Zeichenkette eine Zeile aus der Textdatei ist</param>
        /// <param name="minLength">Minimale Anzahl von Buchstaben, die ein Wort haben muss</param>
        private void StreamCountTop5(string path, List<string> list, int minLength)
        {
            string[] lines = list.ToArray();// Konvertieren der Liste in ein Array von Zeichenketten
            string allText = string.Join(" ", lines);// Zusammenfügen aller Zeilen in einen einzigen Text
            string cleanedText = Regex.Replace(allText, @"[^\w\s]", string.Empty);// Entfernen der Satzzeichen aus dem gesamten Text
            string[] words = cleanedText.Split(new[] { ' ', '\n', '\t', '\r' }, StringSplitOptions.RemoveEmptyEntries);// Aufteilen des Textes in Wörter
            Console.WriteLine($"\nGesamtanzahl der Wörter: {words.Length}");//Zeigt die Anzahl der Wörter im Text an
            
            foreach (string word in words)
            {
                string lowerWord = word.ToLower();
                if (lowerWord.Length >= minLength)
                {
                    if (_dictionaryOfWord.ContainsKey(lowerWord))// Wenn im Wörterbuch bereits ein Schlüsselwort vorhanden ist, weisen wir Wert + 1 zu
                    {
                        _dictionaryOfWord[lowerWord]++;
                    }
                    else
                    {
                        _dictionaryOfWord[lowerWord] = 1;//Wenn das Schlüsselwort zum ersten Mal in der Liste vorkommt, schreiben wir seinen Wert auf 1
                    }
                }
            }
            Console.WriteLine("TTop 5 häufigste Wörter:");
            // Erstellung einer temporären Liste, in der die Werte im Wörterbuch zunächst nach dem höchsten Wert sortiert werden und wir dann die ersten 5 Wörterbücher auswählen, die wir in eine Liste konvertieren
            var top5words = _dictionaryOfWord.OrderByDescending(word => word.Value).Take(5).ToList();
            foreach (var word in top5words)
            {
                Console.WriteLine($"\t{word.Key}:{word.Value}");//Auflistung max der 5 häufigsten Wörter im Text
            }
            _statistik.Add((path, words.Length, top5words));
        }

        /// <summary>
        /// Getter für Liste der gespeicherten Pfade
        /// </summary>
        /// <returns>Gibt das gesamte HashSet zurück</returns>
        public HashSet<string> GetListPath() => _listPath;

        /// <summary>
        /// Gibt die Statistikliste zurück.
        /// </summary>
        /// <returns>Liste von Tupeln, wobei jedes Tupel den Pfad, die Gesamtanzahl der Wörter und die Liste der Top 5 Wörter mit deren Häufigkeiten enthält.</returns>
        public List<(string, int, List<KeyValuePair<string, int>>)> GetStatistic() => _statistik;

        /// <summary>
        /// Speichern eines weiteren Pfad-Textdokuments
        /// </summary>
        /// <param name="pathFile">Konkreter Pfad des Textdokuments</param>
        public void AddListPath(string pathFile)
        {
            _listPath.Add(pathFile);
        }

        /// <summary>
        /// Erstellt eine fortlaufende Linie aus Unterstrichen, die zur visuellen Trennung verwendet werden
        /// </summary>
        /// <param name="input">Anzahl der Wiederholungen</param>
        /// <returns>Gibt einen String in einer Größe zurück, die Sie im Voraus auswählen können</returns>
        public string Cage(int input)
        {
            char cageChar = '_';
            string result;
            return result = new string(cageChar, input);
        }
    }
}
