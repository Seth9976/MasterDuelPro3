using System;
using System.Collections.Generic;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000015 RID: 21
	internal class ServiceManager
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000329E File Offset: 0x0000149E
		public ServiceManager()
		{
			this.m_Services = new Dictionary<Type, IService>();
			AccessibilityManager.screenReaderStatusChanged += this.ScreenReaderStatusChanged;
			this.UpdateServices(AssistiveSupport.isScreenReaderEnabled);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032D4 File Offset: 0x000014D4
		public T GetService<T>() where T : IService
		{
			Type serviceType = typeof(T);
			IService service;
			this.m_Services.TryGetValue(serviceType, out service);
			return (T)((object)service);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003308 File Offset: 0x00001508
		private void StopService<T>() where T : IService
		{
			T service = this.GetService<T>();
			bool flag = service != null;
			if (flag)
			{
				service.Stop();
				this.m_Services.Remove(typeof(T));
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003350 File Offset: 0x00001550
		private void UpdateServices(bool isScreenReaderEnabled)
		{
			if (isScreenReaderEnabled)
			{
				bool flag = !this.m_Services.ContainsKey(typeof(AccessibilityHierarchyService));
				if (flag)
				{
					AccessibilityHierarchyService service = new AccessibilityHierarchyService();
					service.Start();
					this.m_Services.Add(typeof(AccessibilityHierarchyService), service);
				}
			}
			else
			{
				this.StopService<AccessibilityHierarchyService>();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000033B2 File Offset: 0x000015B2
		protected void ScreenReaderStatusChanged(bool isScreenReaderEnabled)
		{
			this.UpdateServices(isScreenReaderEnabled);
		}

		// Token: 0x04000063 RID: 99
		private readonly IDictionary<Type, IService> m_Services;
	}
}
