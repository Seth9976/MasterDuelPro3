using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemManager
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002130 File Offset: 0x00000330
		[RequiredByNativeCode]
		private static void ReloadSubsystemsStarted()
		{
			bool flag = SubsystemManager.reloadSubsytemsStarted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsStarted();
			}
			bool flag2 = SubsystemManager.beforeReloadSubsystems != null;
			if (flag2)
			{
				SubsystemManager.beforeReloadSubsystems();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000216C File Offset: 0x0000036C
		[RequiredByNativeCode]
		private static void ReloadSubsystemsCompleted()
		{
			bool flag = SubsystemManager.reloadSubsytemsCompleted != null;
			if (flag)
			{
				SubsystemManager.reloadSubsytemsCompleted();
			}
			bool flag2 = SubsystemManager.afterReloadSubsystems != null;
			if (flag2)
			{
				SubsystemManager.afterReloadSubsystems();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021A8 File Offset: 0x000003A8
		[RequiredByNativeCode]
		private static void InitializeIntegratedSubsystem(IntPtr ptr, IntegratedSubsystem subsystem)
		{
			subsystem.m_Ptr = ptr;
			subsystem.SetHandle(subsystem);
			SubsystemManager.s_IntegratedSubsystems.Add(subsystem);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021C8 File Offset: 0x000003C8
		[RequiredByNativeCode]
		private static void ClearSubsystems()
		{
			foreach (IntegratedSubsystem subsystem in SubsystemManager.s_IntegratedSubsystems)
			{
				subsystem.m_Ptr = IntPtr.Zero;
			}
			SubsystemManager.s_IntegratedSubsystems.Clear();
			SubsystemManager.s_StandaloneSubsystems.Clear();
			SubsystemManager.s_DeprecatedSubsystems.Clear();
		}

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StaticConstructScriptingClassMap();

		// Token: 0x06000019 RID: 25 RVA: 0x00002244 File Offset: 0x00000444
		static SubsystemManager()
		{
			SubsystemManager.StaticConstructScriptingClassMap();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000226A File Offset: 0x0000046A
		public static void GetSubsystems<T>(List<T> subsystems) where T : ISubsystem
		{
			subsystems.Clear();
			SubsystemManager.AddSubsystemSubset<IntegratedSubsystem, T>(SubsystemManager.s_IntegratedSubsystems, subsystems);
			SubsystemManager.AddSubsystemSubset<SubsystemWithProvider, T>(SubsystemManager.s_StandaloneSubsystems, subsystems);
			SubsystemManager.AddSubsystemSubset<Subsystem, T>(SubsystemManager.s_DeprecatedSubsystems, subsystems);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002298 File Offset: 0x00000498
		private static void AddSubsystemSubset<TBaseTypeInList, TQueryType>(List<TBaseTypeInList> copyFrom, List<TQueryType> copyTo) where TBaseTypeInList : ISubsystem where TQueryType : ISubsystem
		{
			foreach (TBaseTypeInList subsystem in copyFrom)
			{
				TQueryType concreteSubsystem;
				bool flag;
				if (subsystem is TQueryType)
				{
					concreteSubsystem = subsystem as TQueryType;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					copyTo.Add(concreteSubsystem);
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002314 File Offset: 0x00000514
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal static IntegratedSubsystem GetIntegratedSubsystemByPtr(IntPtr ptr)
		{
			foreach (IntegratedSubsystem subsystem in SubsystemManager.s_IntegratedSubsystems)
			{
				bool flag = subsystem.m_Ptr == ptr;
				if (flag)
				{
					return subsystem;
				}
			}
			return null;
		}

		// Token: 0x04000005 RID: 5
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action beforeReloadSubsystems;

		// Token: 0x04000006 RID: 6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action afterReloadSubsystems;

		// Token: 0x04000007 RID: 7
		private static List<IntegratedSubsystem> s_IntegratedSubsystems = new List<IntegratedSubsystem>();

		// Token: 0x04000008 RID: 8
		private static List<SubsystemWithProvider> s_StandaloneSubsystems = new List<SubsystemWithProvider>();

		// Token: 0x04000009 RID: 9
		private static List<Subsystem> s_DeprecatedSubsystems = new List<Subsystem>();

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action reloadSubsytemsStarted;

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action reloadSubsytemsCompleted;
	}
}
