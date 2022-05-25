using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppEnglish
{
	internal class Tutor
	{
		private WordStorage _storage = new WordStorage();
		private Dictionary<string, string> _dic;	
		private Random _random = new Random();

		public Tutor()
		{
			_dic = _storage.GetAllWords();
		}

		public void AddWord(string eng, string rus)
		{
			if (!_dic.ContainsKey(eng))
			{
				_dic.Add(eng, rus);
				_storage.AddWord(eng, rus);
			}				
		}

		public bool CheckWord(string eng, string rus)
		{
			var answer = _dic[eng];
			return answer.ToLower() == rus;
		}

		public string Translate(string eng)
		{
			if (_dic.ContainsKey(eng))
			{
				return _dic[eng];
			}
			
			return null;
		}

		public string GetRandomEngWord()
		{
			var r = _random.Next(0, _dic.Count);
			var keys = new List<string>(_dic.Keys);
			return keys[r];
		}
	}
}
