using System;

namespace System.Linq.Expressions
{
	/// <summary>Used to represent the target of a <see cref="T:System.Linq.Expressions.GotoExpression" />.</summary>
	// Token: 0x020000A8 RID: 168
	public sealed class LabelTarget
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x00015BE8 File Offset: 0x00013DE8
		internal LabelTarget(Type type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Gets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00015BFE File Offset: 0x00013DFE
		public string Name { get; }

		/// <summary>The type of value that is passed when jumping to the label (or <see cref="T:System.Void" /> if no value should be passed).</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of the value that is passed when jumping to the label or <see cref="T:System.Void" /> if no value should be passed</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00015C06 File Offset: 0x00013E06
		public Type Type { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060005AA RID: 1450 RVA: 0x00015C0E File Offset: 0x00013E0E
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return "UnamedLabel";
		}
	}
}
