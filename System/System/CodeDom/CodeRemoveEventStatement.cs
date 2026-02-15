using System;

namespace System.CodeDom
{
	/// <summary>Represents a statement that removes an event handler.</summary>
	// Token: 0x02000211 RID: 529
	[Serializable]
	public class CodeRemoveEventStatement : CodeStatement
	{
		/// <summary>Gets or sets the event to remove a listener from.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to remove a listener from.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0003B5AC File Offset: 0x000397AC
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

		/// <summary>Gets or sets the event handler to remove.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0003B5D1 File Offset: 0x000397D1
		public CodeExpression Listener { get; }

		// Token: 0x040008EE RID: 2286
		private CodeEventReferenceExpression _eventRef;
	}
}
