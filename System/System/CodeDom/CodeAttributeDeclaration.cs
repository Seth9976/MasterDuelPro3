using System;

namespace System.CodeDom
{
	/// <summary>Represents an attribute declaration.</summary>
	// Token: 0x020001E0 RID: 480
	[Serializable]
	public class CodeAttributeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class.</summary>
		// Token: 0x06000BB2 RID: 2994 RVA: 0x0003A6FF File Offset: 0x000388FF
		public CodeAttributeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name.</summary>
		/// <param name="name">The name of the attribute. </param>
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0003A712 File Offset: 0x00038912
		public CodeAttributeDeclaration(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name and arguments.</summary>
		/// <param name="name">The name of the attribute. </param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" />  that contains the arguments for the attribute. </param>
		// Token: 0x06000BB4 RID: 2996 RVA: 0x0003A72C File Offset: 0x0003892C
		public CodeAttributeDeclaration(string name, params CodeAttributeArgument[] arguments)
		{
			this.Name = name;
			this.Arguments.AddRange(arguments);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0003A752 File Offset: 0x00038952
		public CodeAttributeDeclaration(CodeTypeReference attributeType)
			: this(attributeType, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference and arguments.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the arguments for the attribute.</param>
		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003A75C File Offset: 0x0003895C
		public CodeAttributeDeclaration(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
		{
			this._attributeType = attributeType;
			if (attributeType != null)
			{
				this._name = attributeType.BaseType;
			}
			if (arguments != null)
			{
				this.Arguments.AddRange(arguments);
			}
		}

		/// <summary>Gets or sets the name of the attribute being declared.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0003A794 File Offset: 0x00038994
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0003A7A5 File Offset: 0x000389A5
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
				this._attributeType = new CodeTypeReference(this._name);
			}
		}

		/// <summary>Gets the arguments for the attribute.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> that contains the arguments for the attribute.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0003A7BF File Offset: 0x000389BF
		public CodeAttributeArgumentCollection Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		/// <summary>Gets the code type reference for the code attribute declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the <see cref="T:System.CodeDom.CodeAttributeDeclaration" />.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0003A7C7 File Offset: 0x000389C7
		public CodeTypeReference AttributeType
		{
			get
			{
				return this._attributeType;
			}
		}

		// Token: 0x0400087E RID: 2174
		private string _name;

		// Token: 0x0400087F RID: 2175
		private readonly CodeAttributeArgumentCollection _arguments = new CodeAttributeArgumentCollection();

		// Token: 0x04000880 RID: 2176
		private CodeTypeReference _attributeType;
	}
}
