using System;

namespace System.Xml.Linq
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Linq.XObject.Changing" /> and <see cref="E:System.Xml.Linq.XObject.Changed" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000021 RID: 33
	public class XObjectChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XObjectChangeEventArgs" /> class. </summary>
		/// <param name="objectChange">An <see cref="T:System.Xml.Linq.XObjectChange" /> that contains the event arguments for LINQ to XML events.</param>
		// Token: 0x060000D6 RID: 214 RVA: 0x00005243 File Offset: 0x00003443
		public XObjectChangeEventArgs(XObjectChange objectChange)
		{
			this._objectChange = objectChange;
		}

		// Token: 0x04000054 RID: 84
		private XObjectChange _objectChange;

		/// <summary>Event argument for an <see cref="F:System.Xml.Linq.XObjectChange.Add" /> change event.</summary>
		// Token: 0x04000055 RID: 85
		public static readonly XObjectChangeEventArgs Add = new XObjectChangeEventArgs(XObjectChange.Add);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Remove" /> change event.</summary>
		// Token: 0x04000056 RID: 86
		public static readonly XObjectChangeEventArgs Remove = new XObjectChangeEventArgs(XObjectChange.Remove);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Name" /> change event.</summary>
		// Token: 0x04000057 RID: 87
		public static readonly XObjectChangeEventArgs Name = new XObjectChangeEventArgs(XObjectChange.Name);

		/// <summary>Event argument for a <see cref="F:System.Xml.Linq.XObjectChange.Value" /> change event.</summary>
		// Token: 0x04000058 RID: 88
		public static readonly XObjectChangeEventArgs Value = new XObjectChangeEventArgs(XObjectChange.Value);
	}
}
