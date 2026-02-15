using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000028 RID: 40
	public class LdapConstraints : ICloneable
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000078DA File Offset: 0x00005ADA
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000078E2 File Offset: 0x00005AE2
		public virtual int HopLimit
		{
			get
			{
				return this.hopLimit;
			}
			set
			{
				this.hopLimit = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000078EB File Offset: 0x00005AEB
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000078F3 File Offset: 0x00005AF3
		internal virtual Hashtable Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = (Hashtable)value.Clone();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007906 File Offset: 0x00005B06
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000790E File Offset: 0x00005B0E
		public virtual bool ReferralFollowing
		{
			get
			{
				return this.doReferrals;
			}
			set
			{
				this.doReferrals = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007917 File Offset: 0x00005B17
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000791F File Offset: 0x00005B1F
		public virtual int TimeLimit
		{
			get
			{
				return this.msLimit;
			}
			set
			{
				this.msLimit = value;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007928 File Offset: 0x00005B28
		public LdapConstraints()
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007938 File Offset: 0x00005B38
		public LdapConstraints(int msLimit, bool doReferrals, LdapReferralHandler handler, int hop_limit)
		{
			this.msLimit = msLimit;
			this.doReferrals = doReferrals;
			this.refHandler = handler;
			this.hopLimit = hop_limit;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007965 File Offset: 0x00005B65
		public virtual LdapControl[] getControls()
		{
			return this.controls;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000796D File Offset: 0x00005B6D
		public virtual object getProperty(string name)
		{
			if (this.properties == null)
			{
				return null;
			}
			return this.properties[name];
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007985 File Offset: 0x00005B85
		internal virtual LdapReferralHandler getReferralHandler()
		{
			return this.refHandler;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000798D File Offset: 0x00005B8D
		public virtual void setControls(LdapControl control)
		{
			if (control == null)
			{
				this.controls = null;
				return;
			}
			this.controls = new LdapControl[1];
			this.controls[0] = (LdapControl)control.Clone();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000079BC File Offset: 0x00005BBC
		public virtual void setControls(LdapControl[] controls)
		{
			if (controls == null || controls.Length == 0)
			{
				this.controls = null;
				return;
			}
			this.controls = new LdapControl[controls.Length];
			for (int i = 0; i < controls.Length; i++)
			{
				this.controls[i] = (LdapControl)controls[i].Clone();
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007A09 File Offset: 0x00005C09
		public virtual void setProperty(string name, object value_Renamed)
		{
			if (this.properties == null)
			{
				this.properties = new Hashtable();
			}
			SupportClass.PutElement(this.properties, name, value_Renamed);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007A2C File Offset: 0x00005C2C
		public virtual void setReferralHandler(LdapReferralHandler handler)
		{
			this.refHandler = handler;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007A38 File Offset: 0x00005C38
		public object Clone()
		{
			object obj2;
			try
			{
				object obj = base.MemberwiseClone();
				if (this.controls != null)
				{
					((LdapConstraints)obj).controls = new LdapControl[this.controls.Length];
					this.controls.CopyTo(((LdapConstraints)obj).controls, 0);
				}
				if (this.properties != null)
				{
					((LdapConstraints)obj).properties = (Hashtable)this.properties.Clone();
				}
				obj2 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj2;
		}

		// Token: 0x040000AD RID: 173
		private int msLimit;

		// Token: 0x040000AE RID: 174
		private int hopLimit = 10;

		// Token: 0x040000AF RID: 175
		private bool doReferrals;

		// Token: 0x040000B0 RID: 176
		private LdapReferralHandler refHandler;

		// Token: 0x040000B1 RID: 177
		private LdapControl[] controls;

		// Token: 0x040000B2 RID: 178
		private static object nameLock = new object();

		// Token: 0x040000B3 RID: 179
		private static int lConsNum;

		// Token: 0x040000B4 RID: 180
		private string name;

		// Token: 0x040000B5 RID: 181
		private Hashtable properties;
	}
}
