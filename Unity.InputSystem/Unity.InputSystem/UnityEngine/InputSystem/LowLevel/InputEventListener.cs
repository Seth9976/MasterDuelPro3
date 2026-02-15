using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001B6 RID: 438
	public struct InputEventListener : IObservable<InputEventPtr>
	{
		// Token: 0x0600104D RID: 4173 RVA: 0x0004EFEC File Offset: 0x0004D1EC
		public static InputEventListener operator +(InputEventListener _, Action<InputEventPtr, InputDevice> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			InputManager s_Manager = InputSystem.s_Manager;
			lock (s_Manager)
			{
				InputSystem.s_Manager.onEvent += callback;
			}
			return default(InputEventListener);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004F048 File Offset: 0x0004D248
		public static InputEventListener operator -(InputEventListener _, Action<InputEventPtr, InputDevice> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			InputManager s_Manager = InputSystem.s_Manager;
			lock (s_Manager)
			{
				InputSystem.s_Manager.onEvent -= callback;
			}
			return default(InputEventListener);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0004F0A4 File Offset: 0x0004D2A4
		public IDisposable Subscribe(IObserver<InputEventPtr> observer)
		{
			if (InputEventListener.s_ObserverState == null)
			{
				InputEventListener.s_ObserverState = new InputEventListener.ObserverState();
			}
			if (InputEventListener.s_ObserverState.observers.length == 0)
			{
				InputSystem.s_Manager.onEvent += InputEventListener.s_ObserverState.onEventDelegate;
			}
			InputEventListener.s_ObserverState.observers.AppendWithCapacity(observer, 10);
			return new InputEventListener.DisposableObserver
			{
				observer = observer
			};
		}

		// Token: 0x040009FD RID: 2557
		internal static InputEventListener.ObserverState s_ObserverState;

		// Token: 0x020001B7 RID: 439
		internal class ObserverState
		{
			// Token: 0x06001050 RID: 4176 RVA: 0x0004F106 File Offset: 0x0004D306
			public ObserverState()
			{
				this.onEventDelegate = delegate(InputEventPtr eventPtr, InputDevice device)
				{
					for (int i = this.observers.length - 1; i >= 0; i--)
					{
						this.observers[i].OnNext(eventPtr);
					}
				};
			}

			// Token: 0x040009FE RID: 2558
			public InlinedArray<IObserver<InputEventPtr>> observers;

			// Token: 0x040009FF RID: 2559
			public Action<InputEventPtr, InputDevice> onEventDelegate;
		}

		// Token: 0x020001B8 RID: 440
		private class DisposableObserver : IDisposable
		{
			// Token: 0x06001052 RID: 4178 RVA: 0x0004F158 File Offset: 0x0004D358
			public void Dispose()
			{
				int index = InputEventListener.s_ObserverState.observers.IndexOfReference(this.observer);
				if (index >= 0)
				{
					InputEventListener.s_ObserverState.observers.RemoveAtWithCapacity(index);
				}
				if (InputEventListener.s_ObserverState.observers.length == 0)
				{
					InputSystem.s_Manager.onEvent -= InputEventListener.s_ObserverState.onEventDelegate;
				}
			}

			// Token: 0x04000A00 RID: 2560
			public IObserver<InputEventPtr> observer;
		}
	}
}
