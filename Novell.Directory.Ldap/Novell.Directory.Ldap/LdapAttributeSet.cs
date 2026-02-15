using System;
using System.Collections;
using System.Text;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000020 RID: 32
	public class LdapAttributeSet : SupportClass.AbstractSetSupport, ICloneable
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005AA1 File Offset: 0x00003CA1
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005AAE File Offset: 0x00003CAE
		public LdapAttributeSet()
		{
			this.map = new Hashtable();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public override object Clone()
		{
			object obj3;
			try
			{
				object obj = base.MemberwiseClone();
				foreach (object obj2 in this)
				{
					((LdapAttributeSet)obj).Add(((LdapAttribute)obj2).Clone());
				}
				obj3 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj3;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005B28 File Offset: 0x00003D28
		public virtual LdapAttribute getAttribute(string attrName)
		{
			return (LdapAttribute)this.map[attrName.ToUpper()];
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005B40 File Offset: 0x00003D40
		public virtual LdapAttribute getAttribute(string attrName, string lang)
		{
			string text = attrName + ";" + lang;
			return (LdapAttribute)this.map[text.ToUpper()];
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005B70 File Offset: 0x00003D70
		public virtual LdapAttributeSet getSubset(string subtype)
		{
			LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
			foreach (object obj in this)
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				if (ldapAttribute.hasSubtype(subtype))
				{
					ldapAttributeSet.Add(ldapAttribute.Clone());
				}
			}
			return ldapAttributeSet;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005BB7 File Offset: 0x00003DB7
		public override IEnumerator GetEnumerator()
		{
			return this.map.Values.GetEnumerator();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005BC9 File Offset: 0x00003DC9
		public override bool IsEmpty()
		{
			return this.map.Count == 0;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005BDC File Offset: 0x00003DDC
		public override bool Contains(object attr)
		{
			LdapAttribute ldapAttribute = (LdapAttribute)attr;
			return this.map.ContainsKey(ldapAttribute.Name.ToUpper());
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005C08 File Offset: 0x00003E08
		public override bool Add(object attr)
		{
			LdapAttribute ldapAttribute = (LdapAttribute)attr;
			string text = ldapAttribute.Name.ToUpper();
			if (this.map.ContainsKey(text))
			{
				return false;
			}
			SupportClass.PutElement(this.map, text, ldapAttribute);
			return true;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005C48 File Offset: 0x00003E48
		public override bool Remove(object object_Renamed)
		{
			string text;
			if (object_Renamed is string)
			{
				text = (string)object_Renamed;
			}
			else
			{
				text = ((LdapAttribute)object_Renamed).Name;
			}
			return text != null && SupportClass.HashtableRemove(this.map, text.ToUpper()) != null;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005C8B File Offset: 0x00003E8B
		public override void Clear()
		{
			this.map.Clear();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005C98 File Offset: 0x00003E98
		public override bool AddAll(ICollection c)
		{
			bool flag = false;
			IEnumerator enumerator = c.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this.Add(enumerator.Current))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005CCC File Offset: 0x00003ECC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapAttributeSet: ");
			IEnumerator enumerator = this.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				if (!flag)
				{
					stringBuilder.Append(" ");
				}
				flag = false;
				LdapAttribute ldapAttribute = (LdapAttribute)enumerator.Current;
				stringBuilder.Append(ldapAttribute.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000092 RID: 146
		private Hashtable map;
	}
}
