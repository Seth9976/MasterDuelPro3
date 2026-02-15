using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x0200018D RID: 397
	internal class LocalFileEventLog : EventLogImpl
	{
		// Token: 0x0600096E RID: 2414 RVA: 0x000318F1 File Offset: 0x0002FAF1
		public LocalFileEventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000318FA File Offset: 0x0002FAFA
		public override void Close()
		{
			if (this.file_watcher != null)
			{
				this.file_watcher.EnableRaisingEvents = false;
				this.file_watcher = null;
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00031918 File Offset: 0x0002FB18
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			string text = this.FindLogStore(sourceData.LogName);
			if (!Directory.Exists(text))
			{
				base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
				Directory.CreateDirectory(text);
				Directory.CreateDirectory(Path.Combine(text, sourceData.LogName));
				if (this.RunningOnUnix)
				{
					LocalFileEventLog.ModifyAccessPermissions(text, "777");
					LocalFileEventLog.ModifyAccessPermissions(text, "+t");
				}
			}
			Directory.CreateDirectory(Path.Combine(text, sourceData.Source));
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00031995 File Offset: 0x0002FB95
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0003199D File Offset: 0x0002FB9D
		public override void DisableNotification()
		{
			if (this.file_watcher == null)
			{
				return;
			}
			this.file_watcher.EnableRaisingEvents = false;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000319B4 File Offset: 0x0002FBB4
		public override void EnableNotification()
		{
			if (this.file_watcher == null)
			{
				string text = this.FindLogStore(base.CoreEventLog.Log);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				this.file_watcher = new FileSystemWatcher();
				this.file_watcher.Path = text;
				this.file_watcher.Created += delegate(object o, FileSystemEventArgs e)
				{
					LocalFileEventLog localFileEventLog = this;
					lock (localFileEventLog)
					{
						if (this._notifying)
						{
							return;
						}
						this._notifying = true;
					}
					Thread.Sleep(100);
					try
					{
						while (this.GetLatestIndex() > this.last_notification_index)
						{
							try
							{
								EventLog coreEventLog = base.CoreEventLog;
								int num = this.last_notification_index;
								this.last_notification_index = num + 1;
								coreEventLog.OnEntryWritten(this.GetEntry(num));
							}
							catch (Exception)
							{
							}
						}
					}
					finally
					{
						localFileEventLog = this;
						lock (localFileEventLog)
						{
							this._notifying = false;
						}
					}
				};
			}
			this.last_notification_index = this.GetLatestIndex();
			this.file_watcher.EnableRaisingEvents = true;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00031A30 File Offset: 0x0002FC30
		public override bool Exists(string logName, string machineName)
		{
			return Directory.Exists(this.FindLogStore(logName));
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00031A3E File Offset: 0x0002FC3E
		[MonoTODO("Use MessageTable from PE for lookup")]
		protected override string FormatMessage(string source, uint eventID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00031A4C File Offset: 0x0002FC4C
		protected override int GetEntryCount()
		{
			string text = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(text))
			{
				return 0;
			}
			return Directory.GetFiles(text, "*.log").Length;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00031A84 File Offset: 0x0002FC84
		protected override EventLogEntry GetEntry(int index)
		{
			string text = Path.Combine(this.FindLogStore(base.CoreEventLog.Log), (index + 1).ToString(CultureInfo.InvariantCulture) + ".log");
			EventLogEntry eventLogEntry;
			using (TextReader textReader = File.OpenText(text))
			{
				int num = int.Parse(Path.GetFileNameWithoutExtension(text), CultureInfo.InvariantCulture);
				uint num2 = uint.Parse(textReader.ReadLine().Substring(12), CultureInfo.InvariantCulture);
				EventLogEntryType eventLogEntryType = (EventLogEntryType)Enum.Parse(typeof(EventLogEntryType), textReader.ReadLine().Substring(11));
				string text2 = textReader.ReadLine().Substring(8);
				string text3 = textReader.ReadLine().Substring(10);
				short num3 = short.Parse(text3, CultureInfo.InvariantCulture);
				string text4 = "(" + text3 + ")";
				DateTime dateTime = DateTime.ParseExact(textReader.ReadLine().Substring(15), "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
				DateTime lastWriteTime = File.GetLastWriteTime(text);
				int num4 = int.Parse(textReader.ReadLine().Substring(20));
				List<string> list = new List<string>();
				StringBuilder stringBuilder = new StringBuilder();
				while (list.Count < num4)
				{
					char c = (char)textReader.Read();
					if (c == '\0')
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				string[] array = list.ToArray();
				string text5 = this.FormatMessage(text2, num2, array);
				int eventID = EventLog.GetEventID((long)((ulong)num2));
				byte[] array2 = Convert.FromBase64String(textReader.ReadToEnd());
				eventLogEntry = new EventLogEntry(text4, num3, num, eventID, text2, text5, null, Environment.MachineName, eventLogEntryType, dateTime, lastWriteTime, array2, array, (long)((ulong)num2));
			}
			return eventLogEntry;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00031C58 File Offset: 0x0002FE58
		protected override string[] GetLogNames(string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return new string[0];
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			string[] array = new string[directories.Length];
			for (int i = 0; i < directories.Length; i++)
			{
				array[i] = Path.GetFileName(directories[i]);
			}
			return array;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00031CB0 File Offset: 0x0002FEB0
		public override string LogNameFromSourceName(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return string.Empty;
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				return string.Empty;
			}
			return new DirectoryInfo(text).Parent.Name;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00031CF1 File Offset: 0x0002FEF1
		public override bool SourceExists(string source, string machineName)
		{
			return Directory.Exists(this.EventLogStore) && this.FindSourceDirectory(source) != null;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00031D0C File Offset: 0x0002FF0C
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			object obj = LocalFileEventLog.lockObject;
			lock (obj)
			{
				string text = Path.Combine(this.FindLogStore(base.CoreEventLog.Log), (this.GetLatestIndex() + 1).ToString(CultureInfo.InvariantCulture) + ".log");
				try
				{
					using (TextWriter textWriter = File.CreateText(text))
					{
						textWriter.WriteLine("InstanceID: {0}", instanceID.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("EntryType: {0}", (int)type);
						textWriter.WriteLine("Source: {0}", base.CoreEventLog.Source);
						textWriter.WriteLine("Category: {0}", category.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("TimeGenerated: {0}", DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
						textWriter.WriteLine("ReplacementStrings: {0}", replacementStrings.Length.ToString(CultureInfo.InvariantCulture));
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string text2 in replacementStrings)
						{
							stringBuilder.Append(text2);
							stringBuilder.Append('\0');
						}
						textWriter.Write(stringBuilder.ToString());
						textWriter.Write(Convert.ToBase64String(rawData));
					}
				}
				catch (IOException)
				{
					File.Delete(text);
				}
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00031EC0 File Offset: 0x000300C0
		private string FindSourceDirectory(string source)
		{
			string text = null;
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string[] directories2 = Directory.GetDirectories(directories[i], "*");
				for (int j = 0; j < directories2.Length; j++)
				{
					if (string.Compare(Path.GetFileName(directories2[j]), source, true, CultureInfo.InvariantCulture) == 0)
					{
						text = directories2[j];
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00031F30 File Offset: 0x00030130
		private bool RunningOnUnix
		{
			get
			{
				int platform = (int)Environment.OSVersion.Platform;
				return platform == 4 || platform == 128 || platform == 6;
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00031F5C File Offset: 0x0003015C
		private string FindLogStore(string logName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return Path.Combine(this.EventLogStore, logName);
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				if (string.Compare(Path.GetFileName(directories[i]), logName, true, CultureInfo.InvariantCulture) == 0)
				{
					return directories[i];
				}
			}
			return Path.Combine(this.EventLogStore, logName);
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00031FC8 File Offset: 0x000301C8
		private string EventLogStore
		{
			get
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (environmentVariable != null && environmentVariable.Length > "local".Length + 1)
				{
					return environmentVariable.Substring("local".Length + 1);
				}
				if (this.RunningOnUnix)
				{
					return "/var/lib/mono/eventlog";
				}
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mono\\eventlog");
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0003202C File Offset: 0x0003022C
		private int GetLatestIndex()
		{
			int num = 0;
			string[] files = Directory.GetFiles(this.FindLogStore(base.CoreEventLog.Log), "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					int num2 = int.Parse(Path.GetFileNameWithoutExtension(files[i]), CultureInfo.InvariantCulture);
					if (num2 > num)
					{
						num = num2;
					}
				}
				catch
				{
				}
			}
			return num;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00032098 File Offset: 0x00030298
		private static void ModifyAccessPermissions(string path, string permissions)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "chmod";
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.Arguments = string.Format("{0} \"{1}\"", permissions, path);
			Process process = null;
			try
			{
				process = Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				throw new SecurityException("Access permissions could not be modified.", ex);
			}
			process.WaitForExit();
			if (process.ExitCode != 0)
			{
				process.Close();
				throw new SecurityException("Access permissions could not be modified.");
			}
			process.Close();
		}

		// Token: 0x04000723 RID: 1827
		private static readonly object lockObject = new object();

		// Token: 0x04000724 RID: 1828
		private FileSystemWatcher file_watcher;

		// Token: 0x04000725 RID: 1829
		private int last_notification_index;

		// Token: 0x04000726 RID: 1830
		private bool _notifying;
	}
}
