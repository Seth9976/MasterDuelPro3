using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a method.</summary>
	// Token: 0x02000228 RID: 552
	[Serializable]
	public class CodeMethodReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class.</summary>
		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeMethodReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object and method name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003BBDB File Offset: 0x00039DDB
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
		}

		/// <summary>Gets or sets the expression that indicates the method to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the method to reference.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0003BBF1 File Offset: 0x00039DF1
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x0003BBF9 File Offset: 0x00039DF9
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the method to reference.</summary>
		/// <returns>The name of the method to reference.</returns>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0003BC02 File Offset: 0x00039E02
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x0003BC13 File Offset: 0x00039E13
		public string MethodName
		{
			get
			{
				return this._methodName ?? string.Empty;
			}
			set
			{
				this._methodName = value;
			}
		}

		/// <summary>Gets the type arguments for the current generic method reference expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current code <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</returns>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0003BC1C File Offset: 0x00039E1C
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._typeArguments) == null)
				{
					codeTypeReferenceCollection = (this._typeArguments = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		// Token: 0x04000926 RID: 2342
		private string _methodName;

		// Token: 0x04000927 RID: 2343
		private CodeTypeReferenceCollection _typeArguments;
	}
}
