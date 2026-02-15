using System;
using System.Text.RegularExpressions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a verb that can be invoked from a designer.</summary>
	// Token: 0x020002DF RID: 735
	public class DesignerVerb : MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user. </param>
		/// <param name="handler">The event handler that performs the actions of the verb. </param>
		// Token: 0x060011E6 RID: 4582 RVA: 0x0005188A File Offset: 0x0004FA8A
		public DesignerVerb(string text, EventHandler handler)
			: base(handler, StandardCommands.VerbFirst)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		/// <summary>Gets the text description for the verb command on the menu.</summary>
		/// <returns>A description for the verb command.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x000518C0 File Offset: 0x0004FAC0
		public string Text
		{
			get
			{
				object obj = this.Properties["Text"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
		}

		/// <summary>Overrides <see cref="M:System.Object.ToString" />.</summary>
		/// <returns>The verb's text, or an empty string ("") if the text field is empty.</returns>
		// Token: 0x060011E8 RID: 4584 RVA: 0x000518ED File Offset: 0x0004FAED
		public override string ToString()
		{
			return this.Text + " : " + base.ToString();
		}
	}
}
