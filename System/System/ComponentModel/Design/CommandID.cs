using System;
using System.Globalization;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a unique command identifier that consists of a numeric command ID and a GUID menu group identifier.</summary>
	// Token: 0x020002DE RID: 734
	public class CommandID
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.CommandID" /> class using the specified menu group GUID and command ID number.</summary>
		/// <param name="menuGroup">The GUID of the group that this menu command belongs to. </param>
		/// <param name="commandID">The numeric identifier of this menu command. </param>
		// Token: 0x060011E0 RID: 4576 RVA: 0x000517B6 File Offset: 0x0004F9B6
		public CommandID(Guid menuGroup, int commandID)
		{
			this.Guid = menuGroup;
			this.ID = commandID;
		}

		/// <summary>Gets the numeric command ID.</summary>
		/// <returns>The command ID number.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x000517CC File Offset: 0x0004F9CC
		public virtual int ID { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.CommandID" /> instances are equal.</summary>
		/// <returns>true if the specified object is equivalent to this one; otherwise, false.</returns>
		/// <param name="obj">The object to compare. </param>
		// Token: 0x060011E2 RID: 4578 RVA: 0x000517D4 File Offset: 0x0004F9D4
		public override bool Equals(object obj)
		{
			if (!(obj is CommandID))
			{
				return false;
			}
			CommandID commandID = (CommandID)obj;
			return commandID.Guid.Equals(this.Guid) && commandID.ID == this.ID;
		}

		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060011E3 RID: 4579 RVA: 0x00051818 File Offset: 0x0004FA18
		public override int GetHashCode()
		{
			return (this.Guid.GetHashCode() << 2) | this.ID;
		}

		/// <summary>Gets the GUID of the menu group that the menu command identified by this <see cref="T:System.ComponentModel.Design.CommandID" /> belongs to.</summary>
		/// <returns>The GUID of the command group for this command.</returns>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00051842 File Offset: 0x0004FA42
		public virtual Guid Guid { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current object.</summary>
		/// <returns>A string that contains the command ID information, both the GUID and integer identifier. </returns>
		// Token: 0x060011E5 RID: 4581 RVA: 0x0005184C File Offset: 0x0004FA4C
		public override string ToString()
		{
			return this.Guid.ToString() + " : " + this.ID.ToString(CultureInfo.CurrentCulture);
		}
	}
}
