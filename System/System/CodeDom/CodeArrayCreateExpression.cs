using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates an array.</summary>
	// Token: 0x020001DA RID: 474
	[Serializable]
	public class CodeArrayCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class.</summary>
		// Token: 0x06000B97 RID: 2967 RVA: 0x0003A564 File Offset: 0x00038764
		public CodeArrayCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initialization expressions.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06000B98 RID: 2968 RVA: 0x0003A577 File Offset: 0x00038777
		public CodeArrayCreateExpression(CodeTypeReference createType, params CodeExpression[] initializers)
		{
			this._createType = createType;
			this._initializers.AddRange(initializers);
		}

		/// <summary>Gets or sets the type of array to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0003A5A0 File Offset: 0x000387A0
		public CodeTypeReference CreateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._createType) == null)
				{
					codeTypeReference = (this._createType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
		}

		/// <summary>Gets the initializers with which to initialize the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the initialization values.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0003A5CA File Offset: 0x000387CA
		public CodeExpressionCollection Initializers
		{
			get
			{
				return this._initializers;
			}
		}

		/// <summary>Gets or sets the expression that indicates the size of the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the size of the array.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0003A5D2 File Offset: 0x000387D2
		public CodeExpression SizeExpression { get; }

		// Token: 0x04000873 RID: 2163
		private readonly CodeExpressionCollection _initializers = new CodeExpressionCollection();

		// Token: 0x04000874 RID: 2164
		private CodeTypeReference _createType;
	}
}
