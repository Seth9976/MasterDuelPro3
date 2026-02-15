using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x0200063D RID: 1597
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class RuntimeEventInfo : EventInfo, ISerializable
	{
		// Token: 0x06002F96 RID: 12182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_event_info(RuntimeEventInfo ev, out MonoEventInfo info);

		// Token: 0x06002F97 RID: 12183 RVA: 0x000B5DCC File Offset: 0x000B3FCC
		internal static MonoEventInfo GetEventInfo(RuntimeEventInfo ev)
		{
			MonoEventInfo monoEventInfo;
			RuntimeEventInfo.get_event_info(ev, out monoEventInfo);
			return monoEventInfo;
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002F98 RID: 12184 RVA: 0x000B5DE2 File Offset: 0x000B3FE2
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000B5DEA File Offset: 0x000B3FEA
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.GetBindingFlags();
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000B5DF2 File Offset: 0x000B3FF2
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000B5DFF File Offset: 0x000B3FFF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000B5E0C File Offset: 0x000B400C
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000B5E19 File Offset: 0x000B4019
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, null, MemberTypes.Event);
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000B5E40 File Offset: 0x000B4040
		internal BindingFlags GetBindingFlags()
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			MethodInfo methodInfo = eventInfo.add_method;
			if (methodInfo == null)
			{
				methodInfo = eventInfo.remove_method;
			}
			if (methodInfo == null)
			{
				methodInfo = eventInfo.raise_method;
			}
			return RuntimeType.FilterPreCalculate(methodInfo != null && methodInfo.IsPublic, this.GetDeclaringTypeInternal() != this.ReflectedType, methodInfo != null && methodInfo.IsStatic);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000B5EB8 File Offset: 0x000B40B8
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.add_method != null && eventInfo.add_method.IsPublic))
			{
				return eventInfo.add_method;
			}
			return null;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000B5EF4 File Offset: 0x000B40F4
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.raise_method != null && eventInfo.raise_method.IsPublic))
			{
				return eventInfo.raise_method;
			}
			return null;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000B5F30 File Offset: 0x000B4130
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.remove_method != null && eventInfo.remove_method.IsPublic))
			{
				return eventInfo.remove_method;
			}
			return null;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x000B5F6A File Offset: 0x000B416A
		public override Type DeclaringType
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).declaring_type;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002FA3 RID: 12195 RVA: 0x000B5F77 File Offset: 0x000B4177
		public override Type ReflectedType
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).reflected_type;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x000B5F84 File Offset: 0x000B4184
		public override string Name
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).name;
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000B5F91 File Offset: 0x000B4191
		public override string ToString()
		{
			Type eventHandlerType = this.EventHandlerType;
			return ((eventHandlerType != null) ? eventHandlerType.ToString() : null) + " " + this.Name;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x0003DC2A File Offset: 0x0003BE2A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000B3EED File Offset: 0x000B20ED
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000B3EF6 File Offset: 0x000B20F6
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000B5FB5 File Offset: 0x000B41B5
		public override int MetadataToken
		{
			get
			{
				return RuntimeEventInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06002FAA RID: 12202
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeEventInfo monoEvent);

		// Token: 0x04001863 RID: 6243
		private IntPtr klass;

		// Token: 0x04001864 RID: 6244
		private IntPtr handle;
	}
}
