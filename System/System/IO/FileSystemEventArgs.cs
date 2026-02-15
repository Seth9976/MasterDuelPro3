using System;

namespace System.IO
{
	/// <summary>Provides data for the directory events: <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, <see cref="E:System.IO.FileSystemWatcher.Deleted" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000338 RID: 824
	public class FileSystemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values, which represents the kind of change detected in the file system. </param>
		/// <param name="directory">The root directory of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		// Token: 0x060014AF RID: 5295 RVA: 0x00058E25 File Offset: 0x00057025
		public FileSystemEventArgs(WatcherChangeTypes changeType, string directory, string name)
		{
			this._changeType = changeType;
			this._name = name;
			this._fullPath = Path.GetFullPath(FileSystemEventArgs.Combine(directory, name));
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00058E50 File Offset: 0x00057050
		internal static string Combine(string directoryPath, string name)
		{
			bool flag = false;
			if (directoryPath.Length > 0)
			{
				char c = directoryPath[directoryPath.Length - 1];
				flag = c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
			}
			if (!flag)
			{
				return directoryPath + Path.DirectorySeparatorChar.ToString() + name;
			}
			return directoryPath + name;
		}

		/// <summary>Gets the name of the affected file or directory.</summary>
		/// <returns>The name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00058EA7 File Offset: 0x000570A7
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000C0C RID: 3084
		private readonly WatcherChangeTypes _changeType;

		// Token: 0x04000C0D RID: 3085
		private readonly string _name;

		// Token: 0x04000C0E RID: 3086
		private readonly string _fullPath;
	}
}
