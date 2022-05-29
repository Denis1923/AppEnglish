using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace AppEnglish
{
	internal class Program
	{

		static TelegramBotClient Bot;
		static Tutor Tutor = new Tutor();
		static Dictionary<int, string> LastWork = new Dictionary<int, string>();

		const string COMMAND_LIST = @"Список команд:
		/add <eng> <rus> - добавление анг слова и его перевод в словарь
		/get - получаем случайное англ слово из словаря
		/check <rus> <eng> - проверяем правильность перевода англ слова";

		static void Main(string[] args)
		{
			Bot = new TelegramBotClient("5343457232:AAH8clBt_uQM1A_83zLjAhIs2WNQRMbhuE0");

			Bot.OnMessage += Bot_OnMessage;
			Bot.StartReceiving();
			Console.ReadLine();
			Bot.StopReceiving();
		}

		private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
		{
			if (e == null || e.Message == null || e.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
				return;

			Console.WriteLine(e.Message.Text);

			int userId = e.Message.From.Id;
			var msgArgs = e.Message.Text.Split(' ');
			string text;

			switch (msgArgs[0])
			{
				case "/start":
					text = COMMAND_LIST;
					break;

				case "/add":
					text = AddWords(msgArgs);
					break;

				case "/get":
					text = GetRandomEngWord(userId);
					break;

				case "/check":
					text = CheckWord(msgArgs);
					var newWord = GetRandomEngWord(userId);
					text = $"{text} \r\nСледующее слово: {newWord}";
					break;

				default:
					if (LastWork.ContainsKey(userId))
					{
						text = CheckWord(LastWork[userId], msgArgs[0]);
						 newWord = GetRandomEngWord(userId);
						text = $"{text} \r\nСледующее слово: {newWord}";
					}	
					else
						text = COMMAND_LIST;
					break;
			}

			await Bot.SendTextMessageAsync(e.Message.From.Id, text);
		}

		private static string GetRandomEngWord(int userId)
		{
			var text = Tutor.GetRandomEngWord();
			if (LastWork.ContainsKey(userId))			
				LastWork[userId] = text;			
			else			
				LastWork.Add(userId, text);			

			return text;
		}

		private static string CheckWord(string[] msgArgs)
		{
			if (msgArgs.Length != 3)
			{
				return "Неправильное количество аргументов. Их должно быть 2";
			}
			else
			{
				return CheckWord(msgArgs[1], msgArgs[2]);
			}
		}

		private static string CheckWord(string eng, string rus)
		{
			if (Tutor.CheckWord(eng, rus))
			{
				return "Правильно!";
			}
			else
			{
				var correctAnswer = Tutor.Translate(eng);
				return $"Не правильно. Правильный ответ: {correctAnswer}";
			}
		}

		private static string AddWords(string[] msgArgs)
		{
			if (msgArgs.Length != 3)
			{
				return "Неправильное количество аргументов. Их должно быть 2";
			}
			else
			{
				Tutor.AddWord(msgArgs[1], msgArgs[2]);
				return "Новое слово добавлено в словарь";
			}
		}
	}
}
