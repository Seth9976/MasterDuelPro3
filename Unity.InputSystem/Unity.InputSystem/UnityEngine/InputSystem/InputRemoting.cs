using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000096 RID: 150
	public sealed class InputRemoting : IObservable<InputRemoting.Message>, IObserver<InputRemoting.Message>
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00032682 File Offset: 0x00030882
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x0003268F File Offset: 0x0003088F
		public bool sending
		{
			get
			{
				return (this.m_Flags & InputRemoting.Flags.Sending) == InputRemoting.Flags.Sending;
			}
			private set
			{
				if (value)
				{
					this.m_Flags |= InputRemoting.Flags.Sending;
					return;
				}
				this.m_Flags &= ~InputRemoting.Flags.Sending;
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000326B2 File Offset: 0x000308B2
		internal InputRemoting(InputManager manager, bool startSendingOnConnect = false)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			this.m_LocalManager = manager;
			if (startSendingOnConnect)
			{
				this.m_Flags |= InputRemoting.Flags.StartSendingOnConnect;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000326E0 File Offset: 0x000308E0
		public void StartSending()
		{
			if (this.sending)
			{
				return;
			}
			this.m_LocalManager.onEvent += this.SendEvent;
			this.m_LocalManager.onDeviceChange += this.SendDeviceChange;
			this.m_LocalManager.onLayoutChange += this.SendLayoutChange;
			this.sending = true;
			this.SendInitialMessages();
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00032748 File Offset: 0x00030948
		public void StopSending()
		{
			if (!this.sending)
			{
				return;
			}
			this.m_LocalManager.onEvent -= this.SendEvent;
			this.m_LocalManager.onDeviceChange -= this.SendDeviceChange;
			this.m_LocalManager.onLayoutChange -= this.SendLayoutChange;
			this.sending = false;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000327AC File Offset: 0x000309AC
		void IObserver<InputRemoting.Message>.OnNext(InputRemoting.Message msg)
		{
			switch (msg.type)
			{
			case InputRemoting.MessageType.Connect:
				InputRemoting.ConnectMsg.Process(this);
				return;
			case InputRemoting.MessageType.Disconnect:
				InputRemoting.DisconnectMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewLayout:
				InputRemoting.NewLayoutMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewDevice:
				InputRemoting.NewDeviceMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.NewEvents:
				InputRemoting.NewEventsMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.RemoveDevice:
				InputRemoting.RemoveDeviceMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.RemoveLayout:
				break;
			case InputRemoting.MessageType.ChangeUsages:
				InputRemoting.ChangeUsageMsg.Process(this, msg);
				return;
			case InputRemoting.MessageType.StartSending:
				InputRemoting.StartSendingMsg.Process(this);
				return;
			case InputRemoting.MessageType.StopSending:
				InputRemoting.StopSendingMsg.Process(this);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000049FE File Offset: 0x00002BFE
		void IObserver<InputRemoting.Message>.OnError(Exception error)
		{
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000049FE File Offset: 0x00002BFE
		void IObserver<InputRemoting.Message>.OnCompleted()
		{
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00032834 File Offset: 0x00030A34
		public IDisposable Subscribe(IObserver<InputRemoting.Message> observer)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			InputRemoting.Subscriber subscriber = new InputRemoting.Subscriber
			{
				owner = this,
				observer = observer
			};
			ArrayHelpers.Append<InputRemoting.Subscriber>(ref this.m_Subscribers, subscriber);
			return subscriber;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00032871 File Offset: 0x00030A71
		private void SendInitialMessages()
		{
			this.SendAllGeneratedLayouts();
			this.SendAllDevices();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00032880 File Offset: 0x00030A80
		private void SendAllGeneratedLayouts()
		{
			foreach (KeyValuePair<InternedString, Func<InputControlLayout>> entry in this.m_LocalManager.m_Layouts.layoutBuilders)
			{
				this.SendLayout(entry.Key);
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000328E8 File Offset: 0x00030AE8
		private void SendLayout(string layoutName)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			InputRemoting.Message? message = InputRemoting.NewLayoutMsg.Create(this, layoutName);
			if (message != null)
			{
				this.Send(message.Value);
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003291C File Offset: 0x00030B1C
		private void SendAllDevices()
		{
			foreach (InputDevice device in this.m_LocalManager.devices)
			{
				this.SendDevice(device);
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00032978 File Offset: 0x00030B78
		private void SendDevice(InputDevice device)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device.remote)
			{
				return;
			}
			InputRemoting.Message newDeviceMessage = InputRemoting.NewDeviceMsg.Create(device);
			this.Send(newDeviceMessage);
			InputRemoting.Message stateEventMessage = InputRemoting.NewEventsMsg.CreateStateEvent(device);
			this.Send(stateEventMessage);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000329B4 File Offset: 0x00030BB4
		private void SendEvent(InputEventPtr eventPtr, InputDevice device)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device != null && device.remote)
			{
				return;
			}
			InputRemoting.Message message = InputRemoting.NewEventsMsg.Create(eventPtr.data, 1);
			this.Send(message);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000329EC File Offset: 0x00030BEC
		private void SendDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (device.remote)
			{
				return;
			}
			InputRemoting.Message msg;
			if (change != InputDeviceChange.Added)
			{
				if (change != InputDeviceChange.Removed)
				{
					switch (change)
					{
					case InputDeviceChange.UsageChanged:
						msg = InputRemoting.ChangeUsageMsg.Create(device);
						break;
					case InputDeviceChange.ConfigurationChanged:
						return;
					case InputDeviceChange.SoftReset:
						msg = InputRemoting.NewEventsMsg.CreateResetEvent(device, false);
						break;
					case InputDeviceChange.HardReset:
						msg = InputRemoting.NewEventsMsg.CreateResetEvent(device, true);
						break;
					default:
						return;
					}
				}
				else
				{
					msg = InputRemoting.RemoveDeviceMsg.Create(device);
				}
			}
			else
			{
				msg = InputRemoting.NewDeviceMsg.Create(device);
			}
			this.Send(msg);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00032A64 File Offset: 0x00030C64
		private void SendLayoutChange(string layout, InputControlLayoutChange change)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			if (!this.m_LocalManager.m_Layouts.IsGeneratedLayout(new InternedString(layout)))
			{
				return;
			}
			if (change != InputControlLayoutChange.Added && change != InputControlLayoutChange.Replaced)
			{
				return;
			}
			InputRemoting.Message? message = InputRemoting.NewLayoutMsg.Create(this, layout);
			if (message != null)
			{
				this.Send(message.Value);
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00032ABC File Offset: 0x00030CBC
		private void Send(InputRemoting.Message msg)
		{
			InputRemoting.Subscriber[] subscribers = this.m_Subscribers;
			for (int i = 0; i < subscribers.Length; i++)
			{
				subscribers[i].observer.OnNext(msg);
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00032AEC File Offset: 0x00030CEC
		private int FindOrCreateSenderRecord(int senderId)
		{
			if (this.m_Senders != null)
			{
				int senderCount = this.m_Senders.Length;
				for (int i = 0; i < senderCount; i++)
				{
					if (this.m_Senders[i].senderId == senderId)
					{
						return i;
					}
				}
			}
			InputRemoting.RemoteSender sender = new InputRemoting.RemoteSender
			{
				senderId = senderId
			};
			return ArrayHelpers.Append<InputRemoting.RemoteSender>(ref this.m_Senders, sender);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00032B4A File Offset: 0x00030D4A
		private static InternedString BuildLayoutNamespace(int senderId)
		{
			return new InternedString(string.Format("Remote::{0}", senderId));
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00032B64 File Offset: 0x00030D64
		private int FindLocalDeviceId(int remoteDeviceId, int senderIndex)
		{
			InputRemoting.RemoteInputDevice[] localDevices = this.m_Senders[senderIndex].devices;
			if (localDevices != null)
			{
				int numLocalDevices = localDevices.Length;
				for (int i = 0; i < numLocalDevices; i++)
				{
					if (localDevices[i].remoteId == remoteDeviceId)
					{
						return localDevices[i].localId;
					}
				}
			}
			return 0;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00032BB4 File Offset: 0x00030DB4
		private InputDevice TryGetDeviceByRemoteId(int remoteDeviceId, int senderIndex)
		{
			int localId = this.FindLocalDeviceId(remoteDeviceId, senderIndex);
			return this.m_LocalManager.TryGetDeviceById(localId);
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00032BD6 File Offset: 0x00030DD6
		internal InputManager manager
		{
			get
			{
				return this.m_LocalManager;
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00032BE0 File Offset: 0x00030DE0
		public void RemoveRemoteDevices(int participantId)
		{
			int senderIndex = this.FindOrCreateSenderRecord(participantId);
			InputRemoting.RemoteInputDevice[] devices = this.m_Senders[senderIndex].devices;
			if (devices != null)
			{
				foreach (InputRemoting.RemoteInputDevice remoteDevice in devices)
				{
					InputDevice device = this.m_LocalManager.TryGetDeviceById(remoteDevice.localId);
					if (device != null)
					{
						this.m_LocalManager.RemoveDevice(device, false);
					}
				}
			}
			ArrayHelpers.EraseAt<InputRemoting.RemoteSender>(ref this.m_Senders, senderIndex);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00032C58 File Offset: 0x00030E58
		private static byte[] SerializeData<TData>(TData data)
		{
			string json = JsonUtility.ToJson(data);
			return Encoding.UTF8.GetBytes(json);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00032C7C File Offset: 0x00030E7C
		private static TData DeserializeData<TData>(byte[] data)
		{
			return JsonUtility.FromJson<TData>(Encoding.UTF8.GetString(data));
		}

		// Token: 0x040003E7 RID: 999
		private InputRemoting.Flags m_Flags;

		// Token: 0x040003E8 RID: 1000
		private InputManager m_LocalManager;

		// Token: 0x040003E9 RID: 1001
		private InputRemoting.Subscriber[] m_Subscribers;

		// Token: 0x040003EA RID: 1002
		private InputRemoting.RemoteSender[] m_Senders;

		// Token: 0x02000097 RID: 151
		public enum MessageType
		{
			// Token: 0x040003EC RID: 1004
			Connect,
			// Token: 0x040003ED RID: 1005
			Disconnect,
			// Token: 0x040003EE RID: 1006
			NewLayout,
			// Token: 0x040003EF RID: 1007
			NewDevice,
			// Token: 0x040003F0 RID: 1008
			NewEvents,
			// Token: 0x040003F1 RID: 1009
			RemoveDevice,
			// Token: 0x040003F2 RID: 1010
			RemoveLayout,
			// Token: 0x040003F3 RID: 1011
			ChangeUsages,
			// Token: 0x040003F4 RID: 1012
			StartSending,
			// Token: 0x040003F5 RID: 1013
			StopSending
		}

		// Token: 0x02000098 RID: 152
		public struct Message
		{
			// Token: 0x040003F6 RID: 1014
			public int participantId;

			// Token: 0x040003F7 RID: 1015
			public InputRemoting.MessageType type;

			// Token: 0x040003F8 RID: 1016
			public byte[] data;
		}

		// Token: 0x02000099 RID: 153
		[Flags]
		private enum Flags
		{
			// Token: 0x040003FA RID: 1018
			Sending = 1,
			// Token: 0x040003FB RID: 1019
			StartSendingOnConnect = 2
		}

		// Token: 0x0200009A RID: 154
		[Serializable]
		internal struct RemoteSender
		{
			// Token: 0x040003FC RID: 1020
			public int senderId;

			// Token: 0x040003FD RID: 1021
			public InternedString[] layouts;

			// Token: 0x040003FE RID: 1022
			public InputRemoting.RemoteInputDevice[] devices;
		}

		// Token: 0x0200009B RID: 155
		[Serializable]
		internal struct RemoteInputDevice
		{
			// Token: 0x040003FF RID: 1023
			public int remoteId;

			// Token: 0x04000400 RID: 1024
			public int localId;

			// Token: 0x04000401 RID: 1025
			public InputDeviceDescription description;
		}

		// Token: 0x0200009C RID: 156
		internal class Subscriber : IDisposable
		{
			// Token: 0x06000971 RID: 2417 RVA: 0x00032C8E File Offset: 0x00030E8E
			public void Dispose()
			{
				ArrayHelpers.Erase<InputRemoting.Subscriber>(ref this.owner.m_Subscribers, this);
			}

			// Token: 0x04000402 RID: 1026
			public InputRemoting owner;

			// Token: 0x04000403 RID: 1027
			public IObserver<InputRemoting.Message> observer;
		}

		// Token: 0x0200009D RID: 157
		private static class ConnectMsg
		{
			// Token: 0x06000973 RID: 2419 RVA: 0x00032CA2 File Offset: 0x00030EA2
			public static void Process(InputRemoting receiver)
			{
				if (receiver.sending)
				{
					receiver.SendInitialMessages();
					return;
				}
				if ((receiver.m_Flags & InputRemoting.Flags.StartSendingOnConnect) == InputRemoting.Flags.StartSendingOnConnect)
				{
					receiver.StartSending();
				}
			}
		}

		// Token: 0x0200009E RID: 158
		private static class StartSendingMsg
		{
			// Token: 0x06000974 RID: 2420 RVA: 0x00032CC4 File Offset: 0x00030EC4
			public static void Process(InputRemoting receiver)
			{
				receiver.StartSending();
			}
		}

		// Token: 0x0200009F RID: 159
		private static class StopSendingMsg
		{
			// Token: 0x06000975 RID: 2421 RVA: 0x00032CCC File Offset: 0x00030ECC
			public static void Process(InputRemoting receiver)
			{
				receiver.StopSending();
			}
		}

		// Token: 0x020000A0 RID: 160
		private static class DisconnectMsg
		{
			// Token: 0x06000976 RID: 2422 RVA: 0x00032CD4 File Offset: 0x00030ED4
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				Debug.Log("DisconnectMsg.Process");
				receiver.RemoveRemoteDevices(msg.participantId);
				receiver.StopSending();
			}
		}

		// Token: 0x020000A1 RID: 161
		private static class NewLayoutMsg
		{
			// Token: 0x06000977 RID: 2423 RVA: 0x00032CF4 File Offset: 0x00030EF4
			public static InputRemoting.Message? Create(InputRemoting sender, string layoutName)
			{
				InputControlLayout layout;
				try
				{
					layout = sender.m_LocalManager.TryLoadControlLayout(new InternedString(layoutName));
					if (layout == null)
					{
						Debug.Log(string.Format("Could not find layout '{0}' meant to be sent through remote connection; this should not happen", layoutName));
						return null;
					}
				}
				catch (Exception exception)
				{
					Debug.Log(string.Format("Could not load layout '{0}'; not sending to remote listeners (exception: {1})", layoutName, exception));
					return null;
				}
				InputRemoting.NewLayoutMsg.Data data = new InputRemoting.NewLayoutMsg.Data
				{
					name = layoutName,
					layoutJson = layout.ToJson(),
					isOverride = layout.isOverride
				};
				return new InputRemoting.Message?(new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewLayout,
					data = InputRemoting.SerializeData<InputRemoting.NewLayoutMsg.Data>(data)
				});
			}

			// Token: 0x06000978 RID: 2424 RVA: 0x00032DB8 File Offset: 0x00030FB8
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				InputRemoting.NewLayoutMsg.Data data = InputRemoting.DeserializeData<InputRemoting.NewLayoutMsg.Data>(msg.data);
				int senderIndex = receiver.FindOrCreateSenderRecord(msg.participantId);
				InternedString internedLayoutName = new InternedString(data.name);
				receiver.m_LocalManager.RegisterControlLayout(data.layoutJson, data.name, data.isOverride);
				ArrayHelpers.Append<InternedString>(ref receiver.m_Senders[senderIndex].layouts, internedLayoutName);
			}

			// Token: 0x020000A2 RID: 162
			[Serializable]
			public struct Data
			{
				// Token: 0x04000404 RID: 1028
				public string name;

				// Token: 0x04000405 RID: 1029
				public string layoutJson;

				// Token: 0x04000406 RID: 1030
				public bool isOverride;
			}
		}

		// Token: 0x020000A3 RID: 163
		private static class NewDeviceMsg
		{
			// Token: 0x06000979 RID: 2425 RVA: 0x00032E20 File Offset: 0x00031020
			public static InputRemoting.Message Create(InputDevice device)
			{
				InputRemoting.NewDeviceMsg.Data data2 = default(InputRemoting.NewDeviceMsg.Data);
				data2.name = device.name;
				data2.layout = device.layout;
				data2.deviceId = device.deviceId;
				data2.description = device.description;
				data2.usages = device.usages.Select((InternedString x) => x.ToString()).ToArray<string>();
				InputRemoting.NewDeviceMsg.Data data = data2;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewDevice,
					data = InputRemoting.SerializeData<InputRemoting.NewDeviceMsg.Data>(data)
				};
			}

			// Token: 0x0600097A RID: 2426 RVA: 0x00032EC4 File Offset: 0x000310C4
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int senderIndex = receiver.FindOrCreateSenderRecord(msg.participantId);
				InputRemoting.NewDeviceMsg.Data data = InputRemoting.DeserializeData<InputRemoting.NewDeviceMsg.Data>(msg.data);
				InputRemoting.RemoteInputDevice[] devices = receiver.m_Senders[senderIndex].devices;
				if (devices != null)
				{
					InputRemoting.RemoteInputDevice[] array = devices;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].remoteId == data.deviceId)
						{
							Debug.LogError(string.Format("Already received device with id {0} (layout '{1}', description '{3}) from remote {2}", new object[] { data.deviceId, data.layout, msg.participantId, data.description }));
							return;
						}
					}
				}
				InputDevice device;
				try
				{
					InternedString internedLayoutName = new InternedString(data.layout);
					device = receiver.m_LocalManager.AddDevice(internedLayoutName, data.name, default(InternedString));
					device.m_ParticipantId = msg.participantId;
				}
				catch (Exception exception)
				{
					Debug.LogError(string.Format("Could not create remote device '{0}' with layout '{1}' locally (exception: {2})", data.description, data.layout, exception));
					return;
				}
				device.m_Description = data.description;
				device.m_DeviceFlags |= InputDevice.DeviceFlags.Remote;
				foreach (string usage in data.usages)
				{
					receiver.m_LocalManager.AddDeviceUsage(device, new InternedString(usage));
				}
				InputRemoting.RemoteInputDevice record = new InputRemoting.RemoteInputDevice
				{
					remoteId = data.deviceId,
					localId = device.deviceId,
					description = data.description
				};
				ArrayHelpers.Append<InputRemoting.RemoteInputDevice>(ref receiver.m_Senders[senderIndex].devices, record);
			}

			// Token: 0x020000A4 RID: 164
			[Serializable]
			public struct Data
			{
				// Token: 0x04000407 RID: 1031
				public string name;

				// Token: 0x04000408 RID: 1032
				public string layout;

				// Token: 0x04000409 RID: 1033
				public int deviceId;

				// Token: 0x0400040A RID: 1034
				public string[] usages;

				// Token: 0x0400040B RID: 1035
				public InputDeviceDescription description;
			}
		}

		// Token: 0x020000A6 RID: 166
		private static class NewEventsMsg
		{
			// Token: 0x0600097E RID: 2430 RVA: 0x000330A4 File Offset: 0x000312A4
			public unsafe static InputRemoting.Message CreateResetEvent(InputDevice device, bool isHardReset)
			{
				DeviceResetEvent resetEvent = DeviceResetEvent.Create(device.deviceId, isHardReset, -1.0);
				return InputRemoting.NewEventsMsg.Create((InputEvent*)UnsafeUtility.AddressOf<DeviceResetEvent>(ref resetEvent), 1);
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x000330D4 File Offset: 0x000312D4
			public static InputRemoting.Message CreateStateEvent(InputDevice device)
			{
				InputEventPtr eventPtr;
				InputRemoting.Message message;
				using (StateEvent.From(device, out eventPtr, Allocator.Temp))
				{
					message = InputRemoting.NewEventsMsg.Create(eventPtr.data, 1);
				}
				return message;
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x0003311C File Offset: 0x0003131C
			public unsafe static InputRemoting.Message Create(InputEvent* events, int eventCount)
			{
				uint totalSize = 0U;
				InputEventPtr eventPtr = new InputEventPtr(events);
				int i = 0;
				while (i < eventCount)
				{
					totalSize = totalSize.AlignToMultipleOf(4U) + eventPtr.sizeInBytes;
					i++;
					eventPtr = eventPtr.Next();
				}
				byte[] data = new byte[totalSize];
				byte[] array;
				byte* dataPtr;
				if ((array = data) == null || array.Length == 0)
				{
					dataPtr = null;
				}
				else
				{
					dataPtr = &array[0];
				}
				UnsafeUtility.MemCpy((void*)dataPtr, (void*)events, (long)((ulong)totalSize));
				array = null;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.NewEvents,
					data = data
				};
			}

			// Token: 0x06000981 RID: 2433 RVA: 0x000331A4 File Offset: 0x000313A4
			public unsafe static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				InputManager manager = receiver.m_LocalManager;
				byte[] array;
				byte* dataPtr;
				if ((array = msg.data) == null || array.Length == 0)
				{
					dataPtr = null;
				}
				else
				{
					dataPtr = &array[0];
				}
				IntPtr dataEndPtr = new IntPtr((void*)(dataPtr + msg.data.Length));
				int eventCount = 0;
				InputEventPtr eventPtr = new InputEventPtr((InputEvent*)dataPtr);
				int senderIndex = receiver.FindOrCreateSenderRecord(msg.participantId);
				while (eventPtr.data < (InputEvent*)dataEndPtr.ToPointer())
				{
					int remoteDeviceId = eventPtr.deviceId;
					int localDeviceId = receiver.FindLocalDeviceId(remoteDeviceId, senderIndex);
					eventPtr.deviceId = localDeviceId;
					if (localDeviceId != 0)
					{
						manager.QueueEvent(eventPtr);
					}
					eventCount++;
					eventPtr = eventPtr.Next();
				}
				array = null;
			}
		}

		// Token: 0x020000A7 RID: 167
		private static class ChangeUsageMsg
		{
			// Token: 0x06000982 RID: 2434 RVA: 0x0003324C File Offset: 0x0003144C
			public static InputRemoting.Message Create(InputDevice device)
			{
				InputRemoting.ChangeUsageMsg.Data data2 = default(InputRemoting.ChangeUsageMsg.Data);
				data2.deviceId = device.deviceId;
				data2.usages = device.usages.Select((InternedString x) => x.ToString()).ToArray<string>();
				InputRemoting.ChangeUsageMsg.Data data = data2;
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.ChangeUsages,
					data = InputRemoting.SerializeData<InputRemoting.ChangeUsageMsg.Data>(data)
				};
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x000332CC File Offset: 0x000314CC
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int senderIndex = receiver.FindOrCreateSenderRecord(msg.participantId);
				InputRemoting.ChangeUsageMsg.Data data = InputRemoting.DeserializeData<InputRemoting.ChangeUsageMsg.Data>(msg.data);
				InputDevice device = receiver.TryGetDeviceByRemoteId(data.deviceId, senderIndex);
				if (device != null)
				{
					foreach (InternedString deviceUsage in device.usages)
					{
						if (!data.usages.Contains(deviceUsage))
						{
							receiver.m_LocalManager.RemoveDeviceUsage(device, new InternedString(deviceUsage));
						}
					}
					foreach (string dataUsage in data.usages)
					{
						InternedString internedDataUsage = new InternedString(dataUsage);
						if (!device.usages.Contains(internedDataUsage))
						{
							receiver.m_LocalManager.AddDeviceUsage(device, new InternedString(dataUsage));
						}
					}
				}
			}

			// Token: 0x020000A8 RID: 168
			[Serializable]
			public struct Data
			{
				// Token: 0x0400040E RID: 1038
				public int deviceId;

				// Token: 0x0400040F RID: 1039
				public string[] usages;
			}
		}

		// Token: 0x020000AA RID: 170
		private static class RemoveDeviceMsg
		{
			// Token: 0x06000987 RID: 2439 RVA: 0x000333D0 File Offset: 0x000315D0
			public static InputRemoting.Message Create(InputDevice device)
			{
				return new InputRemoting.Message
				{
					type = InputRemoting.MessageType.RemoveDevice,
					data = BitConverter.GetBytes(device.deviceId)
				};
			}

			// Token: 0x06000988 RID: 2440 RVA: 0x00033400 File Offset: 0x00031600
			public static void Process(InputRemoting receiver, InputRemoting.Message msg)
			{
				int senderIndex = receiver.FindOrCreateSenderRecord(msg.participantId);
				int remoteDeviceId = BitConverter.ToInt32(msg.data, 0);
				InputDevice device = receiver.TryGetDeviceByRemoteId(remoteDeviceId, senderIndex);
				if (device != null)
				{
					receiver.m_LocalManager.RemoveDevice(device, false);
				}
			}
		}
	}
}
