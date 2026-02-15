using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents an exception handler in a byte array of IL to be passed to a method such as <see cref="M:System.Reflection.Emit.MethodBuilder.SetMethodBody(System.Byte[],System.Int32,System.Byte[],System.Collections.Generic.IEnumerable{System.Reflection.Emit.ExceptionHandler},System.Collections.Generic.IEnumerable{System.Int32})" />.</summary>
	// Token: 0x02000650 RID: 1616
	[ComVisible(false)]
	public readonly struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		/// <summary>Gets the token of the exception type handled by this handler.</summary>
		/// <returns>The token of the exception type handled by this handler, or 0 if none exists.</returns>
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000B7EBB File Offset: 0x000B60BB
		public int ExceptionTypeToken
		{
			get
			{
				return this.m_exceptionClass;
			}
		}

		/// <summary>Gets the byte offset at which the code that is protected by this exception handler begins.</summary>
		/// <returns>The byte offset at which the code that is protected by this exception handler begins.</returns>
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000B7EC3 File Offset: 0x000B60C3
		public int TryOffset
		{
			get
			{
				return this.m_tryStartOffset;
			}
		}

		/// <summary>Gets the length, in bytes, of the code protected by this exception handler.</summary>
		/// <returns>The length, in bytes, of the code protected by this exception handler.</returns>
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000B7ECB File Offset: 0x000B60CB
		public int TryLength
		{
			get
			{
				return this.m_tryEndOffset - this.m_tryStartOffset;
			}
		}

		/// <summary>Gets the byte offset at which the filter code for the exception handler begins.</summary>
		/// <returns>The byte offset at which the filter code begins, or 0 if no filter  is present.</returns>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000B7EDA File Offset: 0x000B60DA
		public int FilterOffset
		{
			get
			{
				return this.m_filterOffset;
			}
		}

		/// <summary>Gets the byte offset of the first instruction of the exception handler.</summary>
		/// <returns>The byte offset of the first instruction of the exception handler.</returns>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000B7EE2 File Offset: 0x000B60E2
		public int HandlerOffset
		{
			get
			{
				return this.m_handlerStartOffset;
			}
		}

		/// <summary>Gets the length, in bytes, of the exception handler.</summary>
		/// <returns>The length, in bytes, of the exception handler.</returns>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x000B7EEA File Offset: 0x000B60EA
		public int HandlerLength
		{
			get
			{
				return this.m_handlerEndOffset - this.m_handlerStartOffset;
			}
		}

		/// <summary>Gets a value that represents the kind of exception handler this object represents.</summary>
		/// <returns>One of the enumeration values that specifies the kind of exception handler.</returns>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000B7EF9 File Offset: 0x000B60F9
		public ExceptionHandlingClauseOptions Kind
		{
			get
			{
				return this.m_kind;
			}
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000B7F01 File Offset: 0x000B6101
		public override int GetHashCode()
		{
			return this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset ^ (int)this.m_kind;
		}

		/// <summary>Indicates whether this instance of the <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> and this instance are equal; otherwise, false.</returns>
		/// <param name="obj">The object to compare this instance to.</param>
		// Token: 0x060030A6 RID: 12454 RVA: 0x000B7F33 File Offset: 0x000B6133
		public override bool Equals(object obj)
		{
			return obj is ExceptionHandler && this.Equals((ExceptionHandler)obj);
		}

		/// <summary>Indicates whether this instance of the <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object is equal to another <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object.</summary>
		/// <returns>true if <paramref name="other" /> and this instance are equal; otherwise, false.</returns>
		/// <param name="other">The exception handler object to compare this instance to.</param>
		// Token: 0x060030A7 RID: 12455 RVA: 0x000B7F4C File Offset: 0x000B614C
		public bool Equals(ExceptionHandler other)
		{
			return other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset && other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset && other.m_kind == this.m_kind;
		}

		// Token: 0x040018D3 RID: 6355
		internal readonly int m_exceptionClass;

		// Token: 0x040018D4 RID: 6356
		internal readonly int m_tryStartOffset;

		// Token: 0x040018D5 RID: 6357
		internal readonly int m_tryEndOffset;

		// Token: 0x040018D6 RID: 6358
		internal readonly int m_filterOffset;

		// Token: 0x040018D7 RID: 6359
		internal readonly int m_handlerStartOffset;

		// Token: 0x040018D8 RID: 6360
		internal readonly int m_handlerEndOffset;

		// Token: 0x040018D9 RID: 6361
		internal readonly ExceptionHandlingClauseOptions m_kind;
	}
}
