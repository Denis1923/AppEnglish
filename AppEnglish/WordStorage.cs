﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEnglish
{
	internal class WordStorage
	{
		private const string _PATH = "wordstorage.txt";

		public Dictionary<string, string> GetAllWords()
		{
			try
			{
				var dic = new Dictionary<string, string>();

				if (File.Exists(_PATH))
				{
					foreach (var line in File.ReadAllLines(_PATH))
					{
						var words = line.Split('|');
						if (words.Length == 2)
							dic.Add(words[0], words[1]);
					}
				}

				return dic;
			}
			catch (Exception)
			{
				Console.WriteLine("Не удалось считать файл со словарем!");
				return new Dictionary<string, string>();
			}			
		}

		public void AddWord(string eng, string rus)
		{
			try
			{
				using (var writer = new StreamWriter(_PATH, true))
				{
					writer.WriteLine($"{eng} | {rus}");
				}
			}
			catch (Exception)
			{
				Console.WriteLine($"Не удалось добавить слово {eng} в словарь!");
			}			
		}
	}
}
