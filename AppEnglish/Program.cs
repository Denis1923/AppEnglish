using System;

namespace AppEnglish
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var tutor = new Tutor();
			tutor.AddWord("dog", "пес");
			tutor.AddWord("cat", "кот");
			tutor.AddWord("black", "черный");

			while (true)
			{
				var word = tutor.GetRandomEngWord();
				Console.WriteLine($"Как переводится слово \"{word}\"?");
				var userAnswer = Console.ReadLine();

				if (tutor.CheckWord(word, userAnswer))
					Console.WriteLine("Правильно!");
				else
				{
					var correctAnswer = tutor.Translate(word);
					Console.WriteLine($"Не правильно. Правильный ответ: {correctAnswer}");
				}
			}
		}
	}
}
