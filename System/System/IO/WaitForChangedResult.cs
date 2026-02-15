using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	/// <summary>Contains information on the change that occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200033D RID: 829
	public struct WaitForChangedResult
	{
		// Token: 0x060014C3 RID: 5315 RVA: 0x00059166 File Offset: 0x00057366
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, string oldName, bool timedOut)
		{
			this.ChangeType = changeType;
			this.Name = name;
			this.OldName = oldName;
			this.TimedOut = timedOut;
		}

		/// <summary>Gets or sets the type of change that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000483 RID: 1155
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x00059185 File Offset: 0x00057385
		public WatcherChangeTypes ChangeType
		{
			[CompilerGenerated]
			set
			{
				this.<ChangeType>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the name of the file or directory that changed.</summary>
		/// <returns>The name of the file or directory that changed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000484 RID: 1156
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x0005918E File Offset: 0x0005738E
		public string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the original name of the file or directory that was renamed.</summary>
		/// <returns>The original name of the file or directory that was renamed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000485 RID: 1157
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x00059197 File Offset: 0x00057397
		public string OldName
		{
			[CompilerGenerated]
			set
			{
				this.<OldName>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the wait operation timed out.</summary>
		/// <returns>true if the <see cref="M:System.IO.FileSystemWatcher.WaitForChanged(System.IO.WatcherChangeTypes)" /> method timed out; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000486 RID: 1158
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x000591A0 File Offset: 0x000573A0
		public bool TimedOut
		{
			[CompilerGenerated]
			set
			{
				this.<TimedOut>k__BackingField = value;
			}
		}

		// Token: 0x04000C12 RID: 3090
		internal static readonly WaitForChangedResult TimedOutResult = new WaitForChangedResult((WatcherChangeTypes)0, null, null, true);
	}
}
