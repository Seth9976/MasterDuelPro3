using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its zone of origin. This class cannot be inherited.</summary>
	// Token: 0x02000350 RID: 848
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06001DDF RID: 7647 RVA: 0x00075BBD File Offset: 0x00073DBD
		internal ZoneMembershipCondition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ZoneMembershipCondition" /> class with the zone that determines membership.</summary>
		/// <param name="zone">The <see cref="T:System.Security.SecurityZone" /> for which to test. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="zone" /> parameter is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DE0 RID: 7648 RVA: 0x00075BCC File Offset: 0x00073DCC
		public ZoneMembershipCondition(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		/// <summary>Gets or sets the zone for which the membership condition tests.</summary>
		/// <returns>The zone for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null. </exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> to an invalid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x00075BE2 File Offset: 0x00073DE2
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x00075BEC File Offset: 0x00073DEC
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					throw new ArgumentException(Locale.GetText("invalid zone"));
				}
				if (value == SecurityZone.NoZone)
				{
					throw new ArgumentException(Locale.GetText("NoZone isn't valid for membership condition"));
				}
				this.zone = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The evidence set against which to make the test. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DE3 RID: 7651 RVA: 0x00075C3C File Offset: 0x00073E3C
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				Zone zone = obj as Zone;
				if (zone != null && zone.SecurityZone == this.zone)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x00075C7F File Offset: 0x00073E7F
		public IMembershipCondition Copy()
		{
			return new ZoneMembershipCondition(this.zone);
		}

		/// <summary>Determines whether the zone from the specified object is equivalent to the zone contained in the current <see cref="T:System.Security.Policy.ZoneMembershipCondition" />.</summary>
		/// <returns>true if the zone from the specified object is equivalent to the zone contained in the current <see cref="T:System.Security.Policy.ZoneMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.ZoneMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property for the current object or the specified object is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property for the current object or the specified object is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x00075C8C File Offset: 0x00073E8C
		public override bool Equals(object o)
		{
			ZoneMembershipCondition zoneMembershipCondition = o as ZoneMembershipCondition;
			return zoneMembershipCondition != null && zoneMembershipCondition.SecurityZone == this.zone;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x00075CB3 File Offset: 0x00073EB3
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level context used to resolve named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06001DE7 RID: 7655 RVA: 0x00075CC0 File Offset: 0x00073EC0
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			string text = e.Attribute("Zone");
			if (text != null)
			{
				this.zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
			}
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DE8 RID: 7656 RVA: 0x00075D0F File Offset: 0x00073F0F
		public override int GetHashCode()
		{
			return this.zone.GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DE9 RID: 7657 RVA: 0x00075D22 File Offset: 0x00073F22
		public override string ToString()
		{
			return "Zone - " + this.zone.ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DEA RID: 7658 RVA: 0x00075D3F File Offset: 0x00073F3F
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level context for resolving named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06001DEB RID: 7659 RVA: 0x00075D48 File Offset: 0x00073F48
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(ZoneMembershipCondition), this.version);
			securityElement.AddAttribute("Zone", this.zone.ToString());
			return securityElement;
		}

		// Token: 0x04000DBD RID: 3517
		private readonly int version = 1;

		// Token: 0x04000DBE RID: 3518
		private SecurityZone zone;
	}
}
