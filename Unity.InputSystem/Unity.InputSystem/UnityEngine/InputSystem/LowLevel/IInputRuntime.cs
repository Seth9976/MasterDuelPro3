using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001C6 RID: 454
	internal interface IInputRuntime
	{
		// Token: 0x060010EA RID: 4330
		int AllocateDeviceId();

		// Token: 0x060010EB RID: 4331
		void Update(InputUpdateType type);

		// Token: 0x060010EC RID: 4332
		unsafe void QueueEvent(InputEvent* ptr);

		// Token: 0x060010ED RID: 4333
		unsafe long DeviceCommand(int deviceId, InputDeviceCommand* commandPtr);

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060010EE RID: 4334
		// (set) Token: 0x060010EF RID: 4335
		InputUpdateDelegate onUpdate { get; set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060010F0 RID: 4336
		// (set) Token: 0x060010F1 RID: 4337
		Action<InputUpdateType> onBeforeUpdate { get; set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060010F2 RID: 4338
		// (set) Token: 0x060010F3 RID: 4339
		Func<InputUpdateType, bool> onShouldRunUpdate { get; set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060010F4 RID: 4340
		// (set) Token: 0x060010F5 RID: 4341
		Action<int, string> onDeviceDiscovered { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060010F6 RID: 4342
		// (set) Token: 0x060010F7 RID: 4343
		Action<bool> onPlayerFocusChanged { get; set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060010F8 RID: 4344
		bool isPlayerFocused { get; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060010F9 RID: 4345
		// (set) Token: 0x060010FA RID: 4346
		Action onShutdown { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060010FB RID: 4347
		// (set) Token: 0x060010FC RID: 4348
		float pollingFrequency { get; set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060010FD RID: 4349
		double currentTime { get; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060010FE RID: 4350
		double currentTimeForFixedUpdate { get; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060010FF RID: 4351
		float unscaledGameTime { get; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001100 RID: 4352
		double currentTimeOffsetToRealtimeSinceStartup { get; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001101 RID: 4353
		// (set) Token: 0x06001102 RID: 4354
		bool runInBackground { get; set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001103 RID: 4355
		Vector2 screenSize { get; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001104 RID: 4356
		ScreenOrientation screenOrientation { get; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001105 RID: 4357
		// (set) Token: 0x06001106 RID: 4358
		bool normalizeScrollWheelDelta { get; set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001107 RID: 4359
		float scrollWheelDeltaPerTick { get; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001108 RID: 4360
		bool isInBatchMode { get; }
	}
}
