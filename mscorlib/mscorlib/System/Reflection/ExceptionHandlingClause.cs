using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents a clause in a structured exception-handling block.</summary>
	// Token: 0x02000638 RID: 1592
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class ExceptionHandlingClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ExceptionHandlingClause" /> class.</summary>
		// Token: 0x06002F4D RID: 12109 RVA: 0x00003CE1 File Offset: 0x00001EE1
		protected ExceptionHandlingClause()
		{
		}

		/// <summary>A string representation of the exception-handling clause.</summary>
		/// <returns>A string that lists appropriate property values for the filter clause type.</returns>
		// Token: 0x06002F4E RID: 12110 RVA: 0x000B54D4 File Offset: 0x000B36D4
		public override string ToString()
		{
			string text = string.Format("Flags={0}, TryOffset={1}, TryLength={2}, HandlerOffset={3}, HandlerLength={4}", new object[] { this.flags, this.try_offset, this.try_length, this.handler_offset, this.handler_length });
			if (this.catch_type != null)
			{
				text = string.Format("{0}, CatchType={1}", text, this.catch_type);
			}
			if (this.flags == ExceptionHandlingClauseOptions.Filter)
			{
				text = string.Format("{0}, FilterOffset={1}", text, this.filter_offset);
			}
			return text;
		}

		// Token: 0x04001846 RID: 6214
		internal Type catch_type;

		// Token: 0x04001847 RID: 6215
		internal int filter_offset;

		// Token: 0x04001848 RID: 6216
		internal ExceptionHandlingClauseOptions flags;

		// Token: 0x04001849 RID: 6217
		internal int try_offset;

		// Token: 0x0400184A RID: 6218
		internal int try_length;

		// Token: 0x0400184B RID: 6219
		internal int handler_offset;

		// Token: 0x0400184C RID: 6220
		internal int handler_length;
	}
}
