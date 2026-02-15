using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that invokes a method.</summary>
	// Token: 0x02000204 RID: 516
	[Serializable]
	public class CodeMethodInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class.</summary>
		// Token: 0x06000C3F RID: 3135 RVA: 0x0003B034 File Offset: 0x00039234
		public CodeMethodInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified target object, method name, and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object with the method to invoke. </param>
		/// <param name="methodName">The name of the method to invoke. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters to call the method with. </param>
		// Token: 0x06000C40 RID: 3136 RVA: 0x0003B047 File Offset: 0x00039247
		public CodeMethodInvokeExpression(CodeExpression targetObject, string methodName, params CodeExpression[] parameters)
		{
			this._method = new CodeMethodReferenceExpression(targetObject, methodName);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the method to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke.</returns>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0003B074 File Offset: 0x00039274
		public CodeMethodReferenceExpression Method
		{
			get
			{
				CodeMethodReferenceExpression codeMethodReferenceExpression;
				if ((codeMethodReferenceExpression = this._method) == null)
				{
					codeMethodReferenceExpression = (this._method = new CodeMethodReferenceExpression());
				}
				return codeMethodReferenceExpression;
			}
		}

		/// <summary>Gets the parameters to invoke the method with.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to invoke the method with.</returns>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0003B099 File Offset: 0x00039299
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();

		// Token: 0x040008D6 RID: 2262
		private CodeMethodReferenceExpression _method;
	}
}
