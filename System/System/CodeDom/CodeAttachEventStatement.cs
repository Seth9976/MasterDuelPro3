using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that attaches an event-handler delegate to an event.</summary>
	// Token: 0x020001DD RID: 477
	[Serializable]
	public class CodeAttachEventStatement : CodeStatement
	{
		/// <summary>Gets or sets the event to attach an event-handler delegate to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0003A64C File Offset: 0x0003884C
		public CodeEventReferenceExpression Event
		{
			get
			{
				CodeEventReferenceExpression codeEventReferenceExpression;
				if ((codeEventReferenceExpression = this._eventRef) == null)
				{
					codeEventReferenceExpression = (this._eventRef = new CodeEventReferenceExpression());
				}
				return codeEventReferenceExpression;
			}
		}

		/// <summary>Gets or sets the new event-handler delegate to attach to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler to attach.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0003A671 File Offset: 0x00038871
		public CodeExpression Listener { get; }

		// Token: 0x0400087A RID: 2170
		private CodeEventReferenceExpression _eventRef;
	}
}
