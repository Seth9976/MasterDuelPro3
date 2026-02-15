using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemDescriptorStore
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002380 File Offset: 0x00000580
		[RequiredByNativeCode]
		internal static void InitializeManagedDescriptor(IntPtr ptr, IntegratedSubsystemDescriptor desc)
		{
			desc.m_Ptr = ptr;
			SubsystemDescriptorStore.s_IntegratedDescriptors.Add(desc);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002398 File Offset: 0x00000598
		[RequiredByNativeCode]
		internal static void ClearManagedDescriptors()
		{
			foreach (IntegratedSubsystemDescriptor descriptor in SubsystemDescriptorStore.s_IntegratedDescriptors)
			{
				descriptor.m_Ptr = IntPtr.Zero;
			}
			SubsystemDescriptorStore.s_IntegratedDescriptors.Clear();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023FC File Offset: 0x000005FC
		private unsafe static void ReportSingleSubsystemAnalytics(string id)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(id, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = id.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				SubsystemDescriptorStore.ReportSingleSubsystemAnalytics_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002450 File Offset: 0x00000650
		internal static void RegisterDescriptor<TDescriptor, TBaseTypeInList>(TDescriptor descriptor, List<TBaseTypeInList> storeInList) where TDescriptor : TBaseTypeInList where TBaseTypeInList : ISubsystemDescriptor
		{
			for (int finderIndex = 0; finderIndex < storeInList.Count; finderIndex++)
			{
				TBaseTypeInList tbaseTypeInList = storeInList[finderIndex];
				bool flag = tbaseTypeInList.id != descriptor.id;
				if (!flag)
				{
					Debug.LogWarning("Registering subsystem descriptor with duplicate ID '" + descriptor.id + "' - overwriting previous entry.");
					storeInList[finderIndex] = (TBaseTypeInList)((object)descriptor);
					return;
				}
			}
			SubsystemDescriptorStore.ReportSingleSubsystemAnalytics(descriptor.id);
			storeInList.Add((TBaseTypeInList)((object)descriptor));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024FD File Offset: 0x000006FD
		internal static void RegisterDeprecatedDescriptor(SubsystemDescriptor descriptor)
		{
			SubsystemDescriptorStore.RegisterDescriptor<SubsystemDescriptor, SubsystemDescriptor>(descriptor, SubsystemDescriptorStore.s_DeprecatedDescriptors);
		}

		// Token: 0x06000023 RID: 35
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReportSingleSubsystemAnalytics_Injected(ref ManagedSpanWrapper id);

		// Token: 0x0400000C RID: 12
		private static List<IntegratedSubsystemDescriptor> s_IntegratedDescriptors = new List<IntegratedSubsystemDescriptor>();

		// Token: 0x0400000D RID: 13
		private static List<SubsystemDescriptorWithProvider> s_StandaloneDescriptors = new List<SubsystemDescriptorWithProvider>();

		// Token: 0x0400000E RID: 14
		private static List<SubsystemDescriptor> s_DeprecatedDescriptors = new List<SubsystemDescriptor>();
	}
}
