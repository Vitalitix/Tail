using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Tail {
	class Program {
		static readonly string reloadChar = "~";
		static long lastMaxOffset = 0;
		static string filter = "";
		static Regex filterRegEx = new Regex(filter);

		/*static void Main2(string[] args)
		{
			Console.Title = args[0];
            var exitEvent = new ManualResetEvent(false);
			Console.CancelKeyPress += (sender, eventArgs) => {
				eventArgs.Cancel = true;
				exitEvent.Set();
			};
			try
			{
				if (args.Length == 1)
				{
					string fileName = args[0];
					FileInfo fi = new FileInfo(fileName);
					if (fi.Length < 200)
						lastMaxOffset = 0;
					else
						lastMaxOffset = fi.Length-200;
					ShowChanges(fileName);
                    FileSystemWatcher watcher = new FileSystemWatcher();
					watcher.Path = Path.GetDirectoryName(fileName);
					watcher.Filter = Path.GetFileName(fileName);
					watcher.Changed += new FileSystemEventHandler(watcher_Changed);
					watcher.EnableRaisingEvents = true;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			exitEvent.WaitOne();
		}
		private static void watcher_Changed(object sender, FileSystemEventArgs e)
		{
			try
			{
				using (StreamReader reader = new StreamReader(new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
			{
				reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);
				//read out of the file until the EOF
				Console.Write(reader.ReadToEnd());
				//update the last max offset
				lastMaxOffset = reader.BaseStream.Position;
			}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		*/
		static void Main(string[] args) {
			DateTime lastUpdate = DateTime.Now;
			if (args.Length == 1) {
				Console.Title = args[0];
				string fileName = args[0];
				using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))) {
					//start at the end of the file
					if (reader.BaseStream.Length < 999)
						lastMaxOffset = 0;
					else
						lastMaxOffset = reader.BaseStream.Length - 999;
					while (true) {
						Thread.Sleep(100);
						if (Console.KeyAvailable) {
							var key = Console.ReadKey(true);
							switch (key.Key) {
								case ConsoleKey.Backspace:
									if (filter.Length > 0) {
										filter = filter.Substring(0, filter.Length - 1);
									}
									break;
								default:
									filter += key.KeyChar;
									break;
							}
							try {
								if (filter.Contains(reloadChar)) {
									lastMaxOffset = 0;
									filter = filter.Replace(reloadChar, "");
									Console.Clear();
								}
								filterRegEx = new Regex(filter);
							} catch { }
						}
						try {
							reader.BaseStream.Seek(lastMaxOffset, SeekOrigin.Begin);
							if (lastMaxOffset != reader.BaseStream.Length) {
								var newText = reader.ReadToEnd().Replace("\0", ""); //read out of the file until the EOF
								if (filter == "") {
									Console.Write(newText);
								} else {
									var lines = newText.Split(new char[] { '\n', '\r' });
									try {
										foreach (var line in lines) {
											if (filterRegEx.Match(line) != Match.Empty) {
												Console.WriteLine(line);
											}
										}
									} catch { }
								}
								lastMaxOffset = reader.BaseStream.Position; //update the last max offset
								lastUpdate = DateTime.Now;
							} else {
								int seconds = (int)(DateTime.Now - lastUpdate).TotalSeconds;
								Console.Title = $"{args[0]}, Last update: {seconds / 60:00}:{seconds % 60:00}, Filter: {filter}";
							}
						} catch (Exception e) {
							Console.WriteLine(e.Message);
						}
					}
				}
			}
		}
	}
}
