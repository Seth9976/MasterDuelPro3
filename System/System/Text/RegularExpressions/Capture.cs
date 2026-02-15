using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single successful subexpression capture. </summary>
	// Token: 0x02000123 RID: 291
	public class Capture
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0001C6D0 File Offset: 0x0001A8D0
		internal Capture(string text, int index, int length)
		{
			this.Text = text;
			this.Index = index;
			this.Length = length;
		}

		/// <summary>The position in the original string where the first character of the captured substring is found.</summary>
		/// <returns>The zero-based starting position in the original string where the captured substring is found.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001C6ED File Offset: 0x0001A8ED
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001C6F5 File Offset: 0x0001A8F5
		public int Index { get; private protected set; }

		/// <summary>Gets the length of the captured substring.</summary>
		/// <returns>The length of the captured substring.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001C6FE File Offset: 0x0001A8FE
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001C706 File Offset: 0x0001A906
		public int Length { get; private protected set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0001C70F File Offset: 0x0001A90F
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001C717 File Offset: 0x0001A917
		protected internal string Text { internal get; private protected set; }

		/// <summary>Gets the captured substring from the input string.</summary>
		/// <returns>The substring that is captured by the match.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001C720 File Offset: 0x0001A920
		public string Value
		{
			get
			{
				return this.Text.Substring(this.Index, this.Length);
			}
		}

		/// <summary>Retrieves the captured substring from the input string by calling the <see cref="P:System.Text.RegularExpressions.Capture.Value" /> property. </summary>
		/// <returns>The substring that was captured by the match.</returns>
		// Token: 0x06000594 RID: 1428 RVA: 0x0001C739 File Offset: 0x0001A939
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001C741 File Offset: 0x0001A941
		internal ReadOnlySpan<char> GetLeftSubstring()
		{
			return this.Text.AsSpan(0, this.Index);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001C755 File Offset: 0x0001A955
		internal ReadOnlySpan<char> GetRightSubstring()
		{
			return this.Text.AsSpan(this.Index + this.Length, this.Text.Length - this.Index - this.Length);
		}
	}
}
