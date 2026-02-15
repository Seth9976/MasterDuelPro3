using System;
using System.Runtime.CompilerServices;
using System.Runtime.Interop;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000036 RID: 54
	internal sealed class EtwProvider : DiagnosticsEventProvider
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00005DF2 File Offset: 0x00003FF2
		internal EtwProvider(Guid id)
			: base(id)
		{
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005DFB File Offset: 0x00003FFB
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00005E03 File Offset: 0x00004003
		internal Action ControllerCallBack
		{
			get
			{
				return this.invokeControllerCallback;
			}
			set
			{
				this.invokeControllerCallback = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005E0C File Offset: 0x0000400C
		internal bool IsEnd2EndActivityTracingEnabled
		{
			get
			{
				return this.end2EndActivityTracingEnabled;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005E14 File Offset: 0x00004014
		protected override void OnControllerCommand()
		{
			this.end2EndActivityTracingEnabled = false;
			if (this.invokeControllerCallback != null)
			{
				this.invokeControllerCallback();
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005E30 File Offset: 0x00004030
		internal void SetEnd2EndActivityTracingEnabled(bool isEnd2EndActivityTracingEnabled)
		{
			this.end2EndActivityTracingEnabled = isEnd2EndActivityTracingEnabled;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005E3C File Offset: 0x0000403C
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, EventTraceActivity eventTraceActivity, string value1, string value2)
		{
			value1 = value1 ?? string.Empty;
			value2 = value2 ?? string.Empty;
			fixed (string text = value1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				bool flag;
				fixed (string text2 = value2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					byte* ptr3 = stackalloc byte[(UIntPtr)(sizeof(UnsafeNativeMethods.EventData) * 2)];
					UnsafeNativeMethods.EventData* ptr4 = (UnsafeNativeMethods.EventData*)ptr3;
					ptr4->DataPointer = ptr;
					ptr4->Size = (uint)((value1.Length + 1) * 2);
					ptr4[1].DataPointer = ptr2;
					ptr4[1].Size = (uint)((value2.Length + 1) * 2);
					flag = base.WriteEvent(ref eventDescriptor, eventTraceActivity, 2, (IntPtr)((void*)ptr3));
					text = null;
				}
				return flag;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005EEC File Offset: 0x000040EC
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, EventTraceActivity eventTraceActivity, string value1, string value2, string value3)
		{
			value1 = value1 ?? string.Empty;
			value2 = value2 ?? string.Empty;
			value3 = value3 ?? string.Empty;
			fixed (string text = value1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = value2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					bool flag;
					fixed (string text3 = value3)
					{
						char* ptr3 = text3;
						if (ptr3 != null)
						{
							ptr3 += RuntimeHelpers.OffsetToStringData / 2;
						}
						byte* ptr4 = stackalloc byte[(UIntPtr)(sizeof(UnsafeNativeMethods.EventData) * 3)];
						UnsafeNativeMethods.EventData* ptr5 = (UnsafeNativeMethods.EventData*)ptr4;
						ptr5->DataPointer = ptr;
						ptr5->Size = (uint)((value1.Length + 1) * 2);
						ptr5[1].DataPointer = ptr2;
						ptr5[1].Size = (uint)((value2.Length + 1) * 2);
						ptr5[2].DataPointer = ptr3;
						ptr5[2].Size = (uint)((value3.Length + 1) * 2);
						flag = base.WriteEvent(ref eventDescriptor, eventTraceActivity, 3, (IntPtr)((void*)ptr4));
						text = null;
						text2 = null;
					}
					return flag;
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005FF0 File Offset: 0x000041F0
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, EventTraceActivity eventTraceActivity, string value1, string value2, string value3, string value4)
		{
			value1 = value1 ?? string.Empty;
			value2 = value2 ?? string.Empty;
			value3 = value3 ?? string.Empty;
			value4 = value4 ?? string.Empty;
			fixed (string text = value1)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = value2)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text3 = value3)
					{
						char* ptr3 = text3;
						if (ptr3 != null)
						{
							ptr3 += RuntimeHelpers.OffsetToStringData / 2;
						}
						bool flag;
						fixed (string text4 = value4)
						{
							char* ptr4 = text4;
							if (ptr4 != null)
							{
								ptr4 += RuntimeHelpers.OffsetToStringData / 2;
							}
							byte* ptr5 = stackalloc byte[(UIntPtr)(sizeof(UnsafeNativeMethods.EventData) * 4)];
							UnsafeNativeMethods.EventData* ptr6 = (UnsafeNativeMethods.EventData*)ptr5;
							ptr6->DataPointer = ptr;
							ptr6->Size = (uint)((value1.Length + 1) * 2);
							ptr6[1].DataPointer = ptr2;
							ptr6[1].Size = (uint)((value2.Length + 1) * 2);
							ptr6[2].DataPointer = ptr3;
							ptr6[2].Size = (uint)((value3.Length + 1) * 2);
							ptr6[3].DataPointer = ptr4;
							ptr6[3].Size = (uint)((value4.Length + 1) * 2);
							flag = base.WriteEvent(ref eventDescriptor, eventTraceActivity, 4, (IntPtr)((void*)ptr5));
							text = null;
							text2 = null;
							text3 = null;
						}
						return flag;
					}
				}
			}
		}

		// Token: 0x04000084 RID: 132
		private Action invokeControllerCallback;

		// Token: 0x04000085 RID: 133
		private bool end2EndActivityTracingEnabled;
	}
}
