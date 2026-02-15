using System;
using System.Collections;
using System.Globalization;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000025 RID: 37
	public class LdapCompareAttrNames : IComparer
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00005DA4 File Offset: 0x00003FA4
		private void InitBlock()
		{
			this.location = CultureInfo.CurrentCulture;
			this.collator = CultureInfo.CurrentCulture.CompareInfo;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005DC1 File Offset: 0x00003FC1
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00005DC9 File Offset: 0x00003FC9
		public virtual CultureInfo Locale
		{
			get
			{
				return this.location;
			}
			set
			{
				this.collator = value.CompareInfo;
				this.location = value;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005DDE File Offset: 0x00003FDE
		public LdapCompareAttrNames(string attrName)
		{
			this.InitBlock();
			this.sortByNames = new string[1];
			this.sortByNames[0] = attrName;
			this.sortAscending = new bool[1];
			this.sortAscending[0] = true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005E16 File Offset: 0x00004016
		public LdapCompareAttrNames(string attrName, bool ascendingFlag)
		{
			this.InitBlock();
			this.sortByNames = new string[1];
			this.sortByNames[0] = attrName;
			this.sortAscending = new bool[1];
			this.sortAscending[0] = ascendingFlag;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005E50 File Offset: 0x00004050
		public LdapCompareAttrNames(string[] attrNames)
		{
			this.InitBlock();
			this.sortByNames = new string[attrNames.Length];
			this.sortAscending = new bool[attrNames.Length];
			for (int i = 0; i < attrNames.Length; i++)
			{
				this.sortByNames[i] = attrNames[i];
				this.sortAscending[i] = true;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005EA8 File Offset: 0x000040A8
		public LdapCompareAttrNames(string[] attrNames, bool[] ascendingFlags)
		{
			this.InitBlock();
			if (attrNames.Length != ascendingFlags.Length)
			{
				throw new LdapException("UNEQUAL_LENGTHS", 18, null);
			}
			this.sortByNames = new string[attrNames.Length];
			this.sortAscending = new bool[ascendingFlags.Length];
			for (int i = 0; i < attrNames.Length; i++)
			{
				this.sortByNames[i] = attrNames[i];
				this.sortAscending[i] = ascendingFlags[i];
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005F18 File Offset: 0x00004118
		public virtual int Compare(object object1, object object2)
		{
			LdapEntry ldapEntry = (LdapEntry)object1;
			LdapEntry ldapEntry2 = (LdapEntry)object2;
			int num = 0;
			if (this.collator == null)
			{
				this.collator = CultureInfo.CurrentCulture.CompareInfo;
			}
			int num2;
			do
			{
				LdapAttribute attribute = ldapEntry.getAttribute(this.sortByNames[num]);
				LdapAttribute attribute2 = ldapEntry2.getAttribute(this.sortByNames[num]);
				if (attribute != null && attribute2 != null)
				{
					string[] stringValueArray = attribute.StringValueArray;
					string[] stringValueArray2 = attribute2.StringValueArray;
					num2 = this.collator.Compare(stringValueArray[0], stringValueArray2[0]);
				}
				else if (attribute != null)
				{
					num2 = -1;
				}
				else if (attribute2 != null)
				{
					num2 = 1;
				}
				else
				{
					num2 = 0;
				}
				num++;
			}
			while (num2 == 0 && num < this.sortByNames.Length);
			if (this.sortAscending[num - 1])
			{
				return num2;
			}
			return -num2;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005FD8 File Offset: 0x000041D8
		public override bool Equals(object comparator)
		{
			if (!(comparator is LdapCompareAttrNames))
			{
				return false;
			}
			LdapCompareAttrNames ldapCompareAttrNames = (LdapCompareAttrNames)comparator;
			if (ldapCompareAttrNames.sortByNames.Length != this.sortByNames.Length || ldapCompareAttrNames.sortAscending.Length != this.sortAscending.Length)
			{
				return false;
			}
			for (int i = 0; i < this.sortByNames.Length; i++)
			{
				if (ldapCompareAttrNames.sortAscending[i] != this.sortAscending[i])
				{
					return false;
				}
				if (!ldapCompareAttrNames.sortByNames[i].ToUpper().Equals(this.sortByNames[i].ToUpper()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000095 RID: 149
		private string[] sortByNames;

		// Token: 0x04000096 RID: 150
		private bool[] sortAscending;

		// Token: 0x04000097 RID: 151
		private CultureInfo location;

		// Token: 0x04000098 RID: 152
		private CompareInfo collator;
	}
}
