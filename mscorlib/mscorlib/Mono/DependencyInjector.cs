using System;
using System.Reflection;

namespace Mono
{
	// Token: 0x0200002F RID: 47
	internal static class DependencyInjector
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000026AC File Offset: 0x000008AC
		internal static ISystemDependencyProvider SystemProvider
		{
			get
			{
				if (DependencyInjector.systemDependency != null)
				{
					return DependencyInjector.systemDependency;
				}
				object obj = DependencyInjector.locker;
				ISystemDependencyProvider systemDependencyProvider;
				lock (obj)
				{
					if (DependencyInjector.systemDependency != null)
					{
						systemDependencyProvider = DependencyInjector.systemDependency;
					}
					else
					{
						DependencyInjector.systemDependency = DependencyInjector.ReflectionLoad();
						if (DependencyInjector.systemDependency == null)
						{
							throw new PlatformNotSupportedException("Cannot find 'Mono.SystemDependencyProvider, System' dependency");
						}
						systemDependencyProvider = DependencyInjector.systemDependency;
					}
				}
				return systemDependencyProvider;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002724 File Offset: 0x00000924
		internal static void Register(ISystemDependencyProvider provider)
		{
			object obj = DependencyInjector.locker;
			lock (obj)
			{
				if (DependencyInjector.systemDependency != null && DependencyInjector.systemDependency != provider)
				{
					throw new InvalidOperationException();
				}
				DependencyInjector.systemDependency = provider;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002778 File Offset: 0x00000978
		private static ISystemDependencyProvider ReflectionLoad()
		{
			Type type = Type.GetType("Mono.SystemDependencyProvider, System");
			if (type == null)
			{
				return null;
			}
			PropertyInfo property = type.GetProperty("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
			if (property == null)
			{
				return null;
			}
			return (ISystemDependencyProvider)property.GetValue(null);
		}

		// Token: 0x04000109 RID: 265
		private static object locker = new object();

		// Token: 0x0400010A RID: 266
		private static ISystemDependencyProvider systemDependency;
	}
}
