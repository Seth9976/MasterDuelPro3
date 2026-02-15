using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a delegate.</summary>
	// Token: 0x020001F0 RID: 496
	[Serializable]
	public class CodeDelegateCreateExpression : CodeExpression
	{
		/// <summary>Gets or sets the data type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</returns>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0003AAE0 File Offset: 0x00038CE0
		public CodeTypeReference DelegateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._delegateType) == null)
				{
					codeTypeReference = (this._delegateType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
		}

		/// <summary>Gets or sets the object that contains the event-handler method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0003AB0A File Offset: 0x00038D0A
		public CodeExpression TargetObject { get; }

		/// <summary>Gets or sets the name of the event handler method.</summary>
		/// <returns>The name of the event handler method.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0003AB12 File Offset: 0x00038D12
		public string MethodName
		{
			get
			{
				return this._methodName ?? string.Empty;
			}
		}

		// Token: 0x040008AA RID: 2218
		private CodeTypeReference _delegateType;

		// Token: 0x040008AB RID: 2219
		private string _methodName;
	}
}
