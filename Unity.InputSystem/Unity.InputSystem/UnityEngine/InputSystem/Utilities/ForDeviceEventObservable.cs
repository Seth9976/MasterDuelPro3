using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200024E RID: 590
	internal class ForDeviceEventObservable : IObservable<InputEventPtr>
	{
		// Token: 0x0600158D RID: 5517 RVA: 0x0006255C File Offset: 0x0006075C
		public ForDeviceEventObservable(IObservable<InputEventPtr> source, Type deviceType, InputDevice device)
		{
			this.m_Source = source;
			this.m_DeviceType = deviceType;
			this.m_Device = device;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00062579 File Offset: 0x00060779
		public IDisposable Subscribe(IObserver<InputEventPtr> observer)
		{
			return this.m_Source.Subscribe(new ForDeviceEventObservable.ForDevice(this.m_DeviceType, this.m_Device, observer));
		}

		// Token: 0x04000C7D RID: 3197
		private IObservable<InputEventPtr> m_Source;

		// Token: 0x04000C7E RID: 3198
		private InputDevice m_Device;

		// Token: 0x04000C7F RID: 3199
		private Type m_DeviceType;

		// Token: 0x0200024F RID: 591
		private class ForDevice : IObserver<InputEventPtr>
		{
			// Token: 0x0600158F RID: 5519 RVA: 0x00062598 File Offset: 0x00060798
			public ForDevice(Type deviceType, InputDevice device, IObserver<InputEventPtr> observer)
			{
				this.m_Device = device;
				this.m_DeviceType = deviceType;
				this.m_Observer = observer;
			}

			// Token: 0x06001590 RID: 5520 RVA: 0x000049FE File Offset: 0x00002BFE
			public void OnCompleted()
			{
			}

			// Token: 0x06001591 RID: 5521 RVA: 0x00054AD1 File Offset: 0x00052CD1
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x06001592 RID: 5522 RVA: 0x000625B8 File Offset: 0x000607B8
			public void OnNext(InputEventPtr value)
			{
				if (this.m_DeviceType != null)
				{
					InputDevice device = InputSystem.GetDeviceById(value.deviceId);
					if (device == null)
					{
						return;
					}
					if (!this.m_DeviceType.IsInstanceOfType(device))
					{
						return;
					}
				}
				if (this.m_Device != null && value.deviceId != this.m_Device.deviceId)
				{
					return;
				}
				this.m_Observer.OnNext(value);
			}

			// Token: 0x04000C80 RID: 3200
			private IObserver<InputEventPtr> m_Observer;

			// Token: 0x04000C81 RID: 3201
			private InputDevice m_Device;

			// Token: 0x04000C82 RID: 3202
			private Type m_DeviceType;
		}
	}
}
