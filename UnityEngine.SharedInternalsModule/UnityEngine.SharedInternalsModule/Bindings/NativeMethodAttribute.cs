using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200000D RID: 13
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	internal class NativeMethodAttribute : Attribute
	{
		// Token: 0x17000008 RID: 8
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000021E7 File Offset: 0x000003E7
		public string Name
		{
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021F0 File Offset: 0x000003F0
		public bool IsThreadSafe
		{
			[CompilerGenerated]
			set
			{
				this.<IsThreadSafe>k__BackingField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000021F9 File Offset: 0x000003F9
		public bool IsFreeFunction
		{
			[CompilerGenerated]
			set
			{
				this.<IsFreeFunction>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002202 File Offset: 0x00000402
		public bool ThrowsException
		{
			[CompilerGenerated]
			set
			{
				this.<ThrowsException>k__BackingField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000220B File Offset: 0x0000040B
		public bool HasExplicitThis
		{
			[CompilerGenerated]
			set
			{
				this.<HasExplicitThis>k__BackingField = value;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002068 File Offset: 0x00000268
		public NativeMethodAttribute()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002214 File Offset: 0x00000414
		public NativeMethodAttribute(string name)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			bool flag2 = name == "";
			if (flag2)
			{
				throw new ArgumentException("name cannot be empty", "name");
			}
			this.Name = name;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002263 File Offset: 0x00000463
		public NativeMethodAttribute(string name, bool isFreeFunction)
			: this(name)
		{
			this.IsFreeFunction = isFreeFunction;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002276 File Offset: 0x00000476
		public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe)
			: this(name, isFreeFunction)
		{
			this.IsThreadSafe = isThreadSafe;
		}
	}
}
