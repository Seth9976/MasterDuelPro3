using System;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200002D RID: 45
	internal struct EventSanitizer
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00005210 File Offset: 0x00003410
		public void Reset()
		{
			this._sanitizers = new EventSanitizer.IEventSanitizer[0];
			foreach (EventSanitizer.IEventSanitizer sanitizer in this._sanitizers)
			{
				sanitizer.Reset();
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000524C File Offset: 0x0000344C
		public void BeforeProviderUpdate()
		{
			bool flag = this._sanitizers == null;
			if (flag)
			{
				this.Reset();
			}
			foreach (EventSanitizer.IEventSanitizer sanitizer in this._sanitizers)
			{
				sanitizer.BeforeProviderUpdate();
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005290 File Offset: 0x00003490
		public void AfterProviderUpdate()
		{
			bool flag = this._sanitizers == null;
			if (flag)
			{
				this.Reset();
			}
			foreach (EventSanitizer.IEventSanitizer sanitizer in this._sanitizers)
			{
				sanitizer.AfterProviderUpdate();
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000052D4 File Offset: 0x000034D4
		public void Inspect(in Event ev)
		{
			bool flag = this._sanitizers == null;
			if (flag)
			{
				this.Reset();
			}
			foreach (EventSanitizer.IEventSanitizer sanitizer in this._sanitizers)
			{
				sanitizer.Inspect(in ev);
			}
		}

		// Token: 0x040000DC RID: 220
		private EventSanitizer.IEventSanitizer[] _sanitizers;

		// Token: 0x0200002E RID: 46
		private interface IEventSanitizer
		{
			// Token: 0x060000ED RID: 237
			void Reset();

			// Token: 0x060000EE RID: 238
			void BeforeProviderUpdate();

			// Token: 0x060000EF RID: 239
			void AfterProviderUpdate();

			// Token: 0x060000F0 RID: 240
			void Inspect(in Event ev);
		}
	}
}
