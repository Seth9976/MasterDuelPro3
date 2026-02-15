using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
	// Token: 0x020002C1 RID: 705
	public class ProgressChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> class.</summary>
		/// <param name="progressPercentage">The percentage of an asynchronous task that has been completed.</param>
		/// <param name="userState">A unique user state.</param>
		// Token: 0x060010BC RID: 4284 RVA: 0x00045FF3 File Offset: 0x000441F3
		public ProgressChangedEventArgs(int progressPercentage, object userState)
		{
			this.progressPercentage = progressPercentage;
			this.userState = userState;
		}

		/// <summary>Gets the asynchronous task progress percentage.</summary>
		/// <returns>A percentage value indicating the asynchronous task progress.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00046009 File Offset: 0x00044209
		[SRDescription("Percentage progress made in operation.")]
		public int ProgressPercentage
		{
			get
			{
				return this.progressPercentage;
			}
		}

		/// <summary>Gets a unique user state.</summary>
		/// <returns>A unique <see cref="T:System.Object" /> indicating the user state.</returns>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x00046011 File Offset: 0x00044211
		[SRDescription("User-supplied state to identify operation.")]
		public object UserState
		{
			get
			{
				return this.userState;
			}
		}

		// Token: 0x04000A7F RID: 2687
		private readonly int progressPercentage;

		// Token: 0x04000A80 RID: 2688
		private readonly object userState;
	}
}
