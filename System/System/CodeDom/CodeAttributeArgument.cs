using System;

namespace System.CodeDom
{
	/// <summary>Represents an argument used in a metadata attribute declaration.</summary>
	// Token: 0x020001DE RID: 478
	[Serializable]
	public class CodeAttributeArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class.</summary>
		// Token: 0x06000BA8 RID: 2984 RVA: 0x000026E5 File Offset: 0x000008E5
		public CodeAttributeArgument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified value.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument. </param>
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0003A679 File Offset: 0x00038879
		public CodeAttributeArgument(CodeExpression value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified name and value.</summary>
		/// <param name="name">The name of the attribute property the argument applies to. </param>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument. </param>
		// Token: 0x06000BAA RID: 2986 RVA: 0x0003A688 File Offset: 0x00038888
		public CodeAttributeArgument(string name, CodeExpression value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>Gets or sets the name of the attribute.</summary>
		/// <returns>The name of the attribute property the argument is for.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0003A69E File Offset: 0x0003889E
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x0003A6AF File Offset: 0x000388AF
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets or sets the value for the attribute argument.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value for the attribute argument.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0003A6B8 File Offset: 0x000388B8
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x0003A6C0 File Offset: 0x000388C0
		public CodeExpression Value { get; set; }

		// Token: 0x0400087C RID: 2172
		private string _name;
	}
}
