using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200033B RID: 827
	public class RenamedEventArgs : FileSystemEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.RenamedEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values. </param>
		/// <param name="directory">The name of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		/// <param name="oldName">The old name of the affected file or directory. </param>
		// Token: 0x060014C0 RID: 5312 RVA: 0x00059145 File Offset: 0x00057345
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName)
			: base(changeType, directory, name)
		{
			this._oldName = oldName;
			this._oldFullPath = FileSystemEventArgs.Combine(directory, oldName);
		}

		// Token: 0x04000C10 RID: 3088
		private readonly string _oldName;

		// Token: 0x04000C11 RID: 3089
		private readonly string _oldFullPath;
	}
}
