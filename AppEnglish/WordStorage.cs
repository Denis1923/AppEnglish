using System;
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

		public void AddWord(string eng, string rus)
		{
			using (var writer = new StreamWriter(_PATH, true))
			{
				writer.WriteLine($"{eng} | {rus}");
			}
		}
	}
}
