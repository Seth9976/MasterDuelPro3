using System;
using System.ComponentModel;

namespace System.IO
{
	/// <summary>Sets the description visual designers can display when referencing an event, extender, or property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200035A RID: 858
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IODescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use. </param>
		// Token: 0x06001547 RID: 5447 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public IODescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the description.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0003E42C File Offset: 0x0003C62C
		public override string Description
		{
			get
			{
				return base.DescriptionValue;
			}
		}
	}
}
