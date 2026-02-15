using System;
using Unity;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.Console.CancelKeyPress" /> event. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000172 RID: 370
	[Serializable]
	public sealed class ConsoleCancelEventArgs : EventArgs
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x000396FE File Offset: 0x000378FE
		internal ConsoleCancelEventArgs(ConsoleSpecialKey type)
		{
			this._type = type;
		}

		/// <summary>Gets or sets a value that indicates whether simultaneously pressing the <see cref="F:System.ConsoleModifiers.Control" /> modifier key and the <see cref="F:System.ConsoleKey.C" /> console key (Ctrl+C) or the Ctrl+Break keys terminates the current process. The default is false, which terminates the current process. </summary>
		/// <returns>true if the current process should resume when the event handler concludes; false if the current process should terminate. The default value is false; the current process terminates when the event handler returns. If true, the current process continues. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0003970D File Offset: 0x0003790D
		public bool Cancel { get; }

		// Token: 0x06000D67 RID: 3431 RVA: 0x000176B9 File Offset: 0x000158B9
		internal ConsoleCancelEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040004EB RID: 1259
		private readonly ConsoleSpecialKey _type;
	}
}
