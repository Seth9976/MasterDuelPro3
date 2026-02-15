using System;
using System.Threading;

namespace System
{
	/// <summary>Represents the status of an asynchronous operation. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FE RID: 254
	public interface IAsyncResult
	{
		/// <summary>Gets a value that indicates whether the asynchronous operation has completed.</summary>
		/// <returns>true if the operation is complete; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600087F RID: 2175
		bool IsCompleted { get; }

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000880 RID: 2176
		WaitHandle AsyncWaitHandle { get; }

		/// <summary>Gets a user-defined object that qualifies or contains information about an asynchronous operation.</summary>
		/// <returns>A user-defined object that qualifies or contains information about an asynchronous operation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000881 RID: 2177
		object AsyncState { get; }

		/// <summary>Gets a value that indicates whether the asynchronous operation completed synchronously.</summary>
		/// <returns>true if the asynchronous operation completed synchronously; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000882 RID: 2178
		bool CompletedSynchronously { get; }
	}
}
