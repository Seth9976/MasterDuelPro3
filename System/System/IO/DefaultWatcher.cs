using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.IO
{
	// Token: 0x0200034C RID: 844
	internal class DefaultWatcher : IFileWatcher
	{
		// Token: 0x060014F3 RID: 5363 RVA: 0x000026E5 File Offset: 0x000008E5
		private DefaultWatcher()
		{
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005996F File Offset: 0x00057B6F
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (DefaultWatcher.instance != null)
			{
				watcher = DefaultWatcher.instance;
				return true;
			}
			DefaultWatcher.instance = new DefaultWatcher();
			watcher = DefaultWatcher.instance;
			return true;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00059994 File Offset: 0x00057B94
		public void StartDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			lock (this)
			{
				if (DefaultWatcher.watches == null)
				{
					DefaultWatcher.watches = new Hashtable();
				}
				if (DefaultWatcher.thread == null)
				{
					DefaultWatcher.thread = new Thread(new ThreadStart(this.Monitor));
					DefaultWatcher.thread.IsBackground = true;
					DefaultWatcher.thread.Start();
				}
			}
			Hashtable hashtable = DefaultWatcher.watches;
			lock (hashtable)
			{
				DefaultWatcherData defaultWatcherData = (DefaultWatcherData)DefaultWatcher.watches[fileSystemWatcher];
				if (defaultWatcherData == null)
				{
					defaultWatcherData = new DefaultWatcherData();
					defaultWatcherData.Files = new Dictionary<string, FileData>();
					DefaultWatcher.watches[fileSystemWatcher] = defaultWatcherData;
				}
				defaultWatcherData.FSW = fileSystemWatcher;
				defaultWatcherData.Directory = fileSystemWatcher.FullPath;
				defaultWatcherData.NoWildcards = !fileSystemWatcher.Pattern.HasWildcard;
				if (defaultWatcherData.NoWildcards)
				{
					defaultWatcherData.FileMask = Path.Combine(defaultWatcherData.Directory, fileSystemWatcher.MangledFilter);
				}
				else
				{
					defaultWatcherData.FileMask = fileSystemWatcher.MangledFilter;
				}
				defaultWatcherData.IncludeSubdirs = fileSystemWatcher.IncludeSubdirectories;
				defaultWatcherData.Enabled = true;
				defaultWatcherData.DisabledTime = DateTime.MaxValue;
				this.UpdateDataAndDispatch(defaultWatcherData, false);
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00059AEC File Offset: 0x00057CEC
		public void StopDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			lock (this)
			{
				if (DefaultWatcher.watches == null)
				{
					return;
				}
			}
			Hashtable hashtable = DefaultWatcher.watches;
			lock (hashtable)
			{
				DefaultWatcherData defaultWatcherData = (DefaultWatcherData)DefaultWatcher.watches[fileSystemWatcher];
				if (defaultWatcherData != null)
				{
					object filesLock = defaultWatcherData.FilesLock;
					lock (filesLock)
					{
						defaultWatcherData.Enabled = false;
						defaultWatcherData.DisabledTime = DateTime.UtcNow;
					}
				}
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void Dispose(object handle)
		{
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00059BB0 File Offset: 0x00057DB0
		private void Monitor()
		{
			int num = 0;
			for (;;)
			{
				Thread.Sleep(750);
				Hashtable hashtable = DefaultWatcher.watches;
				Hashtable hashtable2;
				lock (hashtable)
				{
					if (DefaultWatcher.watches.Count == 0)
					{
						if (++num == 20)
						{
							break;
						}
						continue;
					}
					else
					{
						hashtable2 = (Hashtable)DefaultWatcher.watches.Clone();
					}
				}
				if (hashtable2.Count != 0)
				{
					num = 0;
					using (IEnumerator enumerator = hashtable2.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DefaultWatcherData defaultWatcherData = (DefaultWatcherData)obj;
							if (this.UpdateDataAndDispatch(defaultWatcherData, true))
							{
								hashtable = DefaultWatcher.watches;
								lock (hashtable)
								{
									DefaultWatcher.watches.Remove(defaultWatcherData.FSW);
								}
							}
						}
						continue;
					}
					break;
				}
			}
			lock (this)
			{
				DefaultWatcher.thread = null;
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00059CE8 File Offset: 0x00057EE8
		private bool UpdateDataAndDispatch(DefaultWatcherData data, bool dispatch)
		{
			if (!data.Enabled)
			{
				return data.DisabledTime != DateTime.MaxValue && (DateTime.UtcNow - data.DisabledTime).TotalSeconds > 5.0;
			}
			this.DoFiles(data, data.Directory, dispatch);
			return false;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00059D44 File Offset: 0x00057F44
		private static void DispatchEvents(FileSystemWatcher fsw, FileAction action, string filename)
		{
			RenamedEventArgs renamedEventArgs = null;
			lock (fsw)
			{
				fsw.DispatchEvents(action, filename, ref renamedEventArgs);
				if (fsw.Waiting)
				{
					fsw.Waiting = false;
					global::System.Threading.Monitor.PulseAll(fsw);
				}
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00059D9C File Offset: 0x00057F9C
		private void DoFiles(DefaultWatcherData data, string directory, bool dispatch)
		{
			bool flag = Directory.Exists(directory);
			if (flag && data.IncludeSubdirs)
			{
				foreach (string text in Directory.GetDirectories(directory))
				{
					this.DoFiles(data, text, dispatch);
				}
			}
			string[] array;
			if (!flag)
			{
				array = DefaultWatcher.NoStringsArray;
			}
			else if (!data.NoWildcards)
			{
				array = Directory.GetFileSystemEntries(directory, data.FileMask);
			}
			else if (File.Exists(data.FileMask) || Directory.Exists(data.FileMask))
			{
				array = new string[] { data.FileMask };
			}
			else
			{
				array = DefaultWatcher.NoStringsArray;
			}
			object filesLock = data.FilesLock;
			lock (filesLock)
			{
				if (data.Enabled)
				{
					this.IterateAndModifyFilesData(data, directory, dispatch, array);
				}
			}
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00059E7C File Offset: 0x0005807C
		private void IterateAndModifyFilesData(DefaultWatcherData data, string directory, bool dispatch, string[] files)
		{
			foreach (KeyValuePair<string, FileData> keyValuePair in data.Files)
			{
				FileData value = keyValuePair.Value;
				if (value.Directory == directory)
				{
					value.NotExists = true;
				}
			}
			foreach (string text in files)
			{
				FileData fileData;
				if (!data.Files.TryGetValue(text, out fileData))
				{
					try
					{
						data.Files.Add(text, DefaultWatcher.CreateFileData(directory, text));
					}
					catch
					{
						data.Files.Remove(text);
						goto IL_00CA;
					}
					if (dispatch)
					{
						DefaultWatcher.DispatchEvents(data.FSW, FileAction.Added, Path.GetRelativePath(data.Directory, text));
					}
				}
				else if (fileData.Directory == directory)
				{
					fileData.NotExists = false;
				}
				IL_00CA:;
			}
			if (!dispatch)
			{
				return;
			}
			List<string> list = null;
			foreach (KeyValuePair<string, FileData> keyValuePair2 in data.Files)
			{
				string key = keyValuePair2.Key;
				if (keyValuePair2.Value.NotExists)
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(key);
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Removed, Path.GetRelativePath(data.Directory, key));
				}
			}
			if (list != null)
			{
				foreach (string text2 in list)
				{
					data.Files.Remove(text2);
				}
				list = null;
			}
			foreach (KeyValuePair<string, FileData> keyValuePair3 in data.Files)
			{
				string key2 = keyValuePair3.Key;
				FileData value2 = keyValuePair3.Value;
				DateTime creationTime;
				DateTime lastWriteTime;
				try
				{
					creationTime = File.GetCreationTime(key2);
					lastWriteTime = File.GetLastWriteTime(key2);
				}
				catch
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(key2);
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Removed, Path.GetRelativePath(data.Directory, key2));
					continue;
				}
				if (creationTime != value2.CreationTime || lastWriteTime != value2.LastWriteTime)
				{
					value2.CreationTime = creationTime;
					value2.LastWriteTime = lastWriteTime;
					DefaultWatcher.DispatchEvents(data.FSW, FileAction.Modified, Path.GetRelativePath(data.Directory, key2));
				}
			}
			if (list != null)
			{
				foreach (string text3 in list)
				{
					data.Files.Remove(text3);
				}
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0005A184 File Offset: 0x00058384
		private static FileData CreateFileData(string directory, string filename)
		{
			FileData fileData = new FileData();
			string text = Path.Combine(directory, filename);
			fileData.Directory = directory;
			fileData.Attributes = File.GetAttributes(text);
			fileData.CreationTime = File.GetCreationTime(text);
			fileData.LastWriteTime = File.GetLastWriteTime(text);
			return fileData;
		}

		// Token: 0x04000C4A RID: 3146
		private static DefaultWatcher instance;

		// Token: 0x04000C4B RID: 3147
		private static Thread thread;

		// Token: 0x04000C4C RID: 3148
		private static Hashtable watches;

		// Token: 0x04000C4D RID: 3149
		private static string[] NoStringsArray = new string[0];
	}
}
