using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000060 RID: 96
	public class RespControlVector : ArrayList
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00002E3E File Offset: 0x0000103E
		public RespControlVector(int cap, int incr)
			: base(cap)
		{
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		public void registerResponseControl(string oid, Type controlClass)
		{
			lock (this)
			{
				this.Add(new RespControlVector.RegisteredControl(this, oid, controlClass));
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public Type findResponseControl(string searchOID)
		{
			Type type;
			lock (this)
			{
				for (int i = 0; i < this.Count; i++)
				{
					RespControlVector.RegisteredControl registeredControl;
					if ((registeredControl = (RespControlVector.RegisteredControl)this[i]) == null)
					{
						throw new FieldAccessException();
					}
					if (registeredControl.myOID.CompareTo(searchOID) == 0)
					{
						return registeredControl.myClass;
					}
				}
				type = null;
			}
			return type;
		}

		// Token: 0x02000061 RID: 97
		private class RegisteredControl
		{
			// Token: 0x0600037F RID: 895 RVA: 0x0000FA8C File Offset: 0x0000DC8C
			private void InitBlock(RespControlVector enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000380 RID: 896 RVA: 0x0000FA95 File Offset: 0x0000DC95
			public RespControlVector Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0000FA9D File Offset: 0x0000DC9D
			public RegisteredControl(RespControlVector enclosingInstance, string oid, Type controlClass)
			{
				this.InitBlock(enclosingInstance);
				this.myOID = oid;
				this.myClass = controlClass;
			}

			// Token: 0x0400021F RID: 543
			private RespControlVector enclosingInstance;

			// Token: 0x04000220 RID: 544
			public string myOID;

			// Token: 0x04000221 RID: 545
			public Type myClass;
		}
	}
}
