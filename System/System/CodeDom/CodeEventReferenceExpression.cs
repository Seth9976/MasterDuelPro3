using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an event.</summary>
	// Token: 0x020001F6 RID: 502
	[Serializable]
	public class CodeEventReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class.</summary>
		// Token: 0x06000BFC RID: 3068 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodeEventReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class using the specified target object and event name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event to reference. </param>
		// Token: 0x06000BFD RID: 3069 RVA: 0x0003AB98 File Offset: 0x00038D98
		public CodeEventReferenceExpression(CodeExpression targetObject, string eventName)
		{
			this.TargetObject = targetObject;
			this._eventName = eventName;
		}

		/// <summary>Gets or sets the object that contains the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0003ABAE File Offset: 0x00038DAE
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x0003ABB6 File Offset: 0x00038DB6
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the event.</summary>
		/// <returns>The name of the event.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0003ABBF File Offset: 0x00038DBF
		public string EventName
		{
			get
			{
				return this._eventName ?? string.Empty;
			}
		}

		// Token: 0x040008B0 RID: 2224
		private string _eventName;
	}
}
