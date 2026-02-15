using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a Windows menu or toolbar command item.</summary>
	// Token: 0x020002E9 RID: 745
	public class MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommand" /> class.</summary>
		/// <param name="handler">The event to raise when the user selects the menu item or toolbar button. </param>
		/// <param name="command">The unique command ID that links this menu command to the environment's menu. </param>
		// Token: 0x060011FD RID: 4605 RVA: 0x00051905 File Offset: 0x0004FB05
		public MenuCommand(EventHandler handler, CommandID command)
		{
			this._execHandler = handler;
			this.CommandID = command;
			this._status = 3;
		}

		/// <summary>Gets the public properties associated with the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the public properties of the <see cref="T:System.ComponentModel.Design.MenuCommand" />. </returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x00051924 File Offset: 0x0004FB24
		public virtual IDictionary Properties
		{
			get
			{
				IDictionary dictionary;
				if ((dictionary = this._properties) == null)
				{
					dictionary = (this._properties = new HybridDictionary());
				}
				return dictionary;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> associated with this menu command.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.CommandID" /> associated with the menu command.</returns>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00051949 File Offset: 0x0004FB49
		public virtual CommandID CommandID { get; }

		/// <summary>Returns a string representation of this menu command.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.ComponentModel.Design.MenuCommand.CommandID" /> property appended with the names of any flags that are set, separated by pipe bars (|). These flag properties include <see cref="P:System.ComponentModel.Design.MenuCommand.Checked" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Enabled" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Supported" />, and <see cref="P:System.ComponentModel.Design.MenuCommand.Visible" />.</returns>
		// Token: 0x06001200 RID: 4608 RVA: 0x00051954 File Offset: 0x0004FB54
		public override string ToString()
		{
			string text = this.CommandID.ToString() + " : ";
			if ((this._status & 1) != 0)
			{
				text += "Supported";
			}
			if ((this._status & 2) != 0)
			{
				text += "|Enabled";
			}
			if ((this._status & 16) == 0)
			{
				text += "|Visible";
			}
			if ((this._status & 4) != 0)
			{
				text += "|Checked";
			}
			return text;
		}

		// Token: 0x04000AE8 RID: 2792
		private EventHandler _execHandler;

		// Token: 0x04000AE9 RID: 2793
		private int _status;

		// Token: 0x04000AEA RID: 2794
		private IDictionary _properties;
	}
}
