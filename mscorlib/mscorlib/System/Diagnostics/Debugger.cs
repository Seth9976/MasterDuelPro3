using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Enables communication with a debugger. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006DC RID: 1756
	[ComVisible(true)]
	public sealed class Debugger
	{
		/// <summary>Gets a value that indicates whether a debugger is attached to the process.</summary>
		/// <returns>true if a debugger is attached; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x000DAC34 File Offset: 0x000D8E34
		public static bool IsAttached
		{
			get
			{
				return Debugger.IsAttached_internal();
			}
		}

		// Token: 0x06003784 RID: 14212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAttached_internal();

		/// <summary>Checks to see if logging is enabled by an attached debugger.</summary>
		/// <returns>true if a debugger is attached and logging is enabled; otherwise, false. The attached debugger is the registered managed debugger in the DbgManagedDebugger registry key. For more information on this key, see Enabling JIT-Attach Debugging.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003785 RID: 14213
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLogging();

		// Token: 0x06003786 RID: 14214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Log_icall(int level, ref string category, ref string message);

		/// <summary>Posts a message for the attached debugger.</summary>
		/// <param name="level">A description of the importance of the message. </param>
		/// <param name="category">The category of the message. </param>
		/// <param name="message">The message to show. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003787 RID: 14215 RVA: 0x000DAC3B File Offset: 0x000D8E3B
		public static void Log(int level, string category, string message)
		{
			Debugger.Log_icall(level, ref category, ref message);
		}

		/// <summary>Notifies a debugger that execution is about to enter a path that involves a cross-thread dependency.</summary>
		// Token: 0x06003788 RID: 14216 RVA: 0x00002C89 File Offset: 0x00000E89
		public static void NotifyOfCrossThreadDependency()
		{
		}

		/// <summary>Represents the default category of message with a constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001DF0 RID: 7664
		public static readonly string DefaultCategory = "";
	}
}
