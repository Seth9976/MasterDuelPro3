using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000287 RID: 647
	public struct FixedAttachmentArray<[IsUnmanaged] DataType> where DataType : struct, ValueType
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x0003F744 File Offset: 0x0003D944
		public FixedAttachmentArray(int numAttachments)
		{
			this.a0 = (this.a1 = (this.a2 = (this.a3 = (this.a4 = (this.a5 = (this.a6 = (this.a7 = new DataType())))))));
			this.activeAttachments = numAttachments;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0003F7A4 File Offset: 0x0003D9A4
		public unsafe FixedAttachmentArray(DataType[] attachments)
		{
			this = new FixedAttachmentArray<DataType>(attachments.Length);
			for (int i = 0; i < this.activeAttachments; i++)
			{
				*this[i] = attachments[i];
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003F7E0 File Offset: 0x0003D9E0
		public unsafe FixedAttachmentArray(NativeArray<DataType> attachments)
		{
			this = new FixedAttachmentArray<DataType>(attachments.Length);
			for (int i = 0; i < this.activeAttachments; i++)
			{
				*this[i] = attachments[i];
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0003F81F File Offset: 0x0003DA1F
		public int size
		{
			get
			{
				return this.activeAttachments;
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003F827 File Offset: 0x0003DA27
		public void Clear()
		{
			this.activeAttachments = 0;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003F830 File Offset: 0x0003DA30
		public unsafe int Add(in DataType data)
		{
			int index = this.activeAttachments;
			fixed (FixedAttachmentArray<DataType>* ptr = (FixedAttachmentArray<DataType>*)(&this))
			{
				DataType* array = (DataType*)ptr;
				array[(IntPtr)index * (IntPtr)sizeof(DataType) / (IntPtr)sizeof(DataType)] = data;
			}
			this.activeAttachments++;
			return index;
		}

		// Token: 0x17000223 RID: 547
		public unsafe ref DataType this[int index]
		{
			get
			{
				fixed (FixedAttachmentArray<DataType>* ptr = (FixedAttachmentArray<DataType>*)(&this))
				{
					DataType* array = (DataType*)ptr;
					return ref array[(IntPtr)index * (IntPtr)sizeof(DataType) / (IntPtr)sizeof(DataType)];
				}
			}
		}

		// Token: 0x04000B4E RID: 2894
		public static FixedAttachmentArray<DataType> Empty = new FixedAttachmentArray<DataType>(0);

		// Token: 0x04000B4F RID: 2895
		public const int MaxAttachments = 8;

		// Token: 0x04000B50 RID: 2896
		private DataType a0;

		// Token: 0x04000B51 RID: 2897
		private DataType a1;

		// Token: 0x04000B52 RID: 2898
		private DataType a2;

		// Token: 0x04000B53 RID: 2899
		private DataType a3;

		// Token: 0x04000B54 RID: 2900
		private DataType a4;

		// Token: 0x04000B55 RID: 2901
		private DataType a5;

		// Token: 0x04000B56 RID: 2902
		private DataType a6;

		// Token: 0x04000B57 RID: 2903
		private DataType a7;

		// Token: 0x04000B58 RID: 2904
		private int activeAttachments;
	}
}
