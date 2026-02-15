using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F4 RID: 1268
	internal sealed class ObjectProgress
	{
		// Token: 0x0600279E RID: 10142 RVA: 0x0009FADC File Offset: 0x0009DCDC
		internal ObjectProgress()
		{
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0009FAF8 File Offset: 0x0009DCF8
		internal void Init()
		{
			this.isInitial = false;
			this.count = 0;
			this.expectedType = BinaryTypeEnum.ObjectUrt;
			this.expectedTypeInformation = null;
			this.name = null;
			this.objectTypeEnum = InternalObjectTypeE.Empty;
			this.memberTypeEnum = InternalMemberTypeE.Empty;
			this.memberValueEnum = InternalMemberValueE.Empty;
			this.dtType = null;
			this.numItems = 0;
			this.nullCount = 0;
			this.typeInformation = null;
			this.memberLength = 0;
			this.binaryTypeEnumA = null;
			this.typeInformationA = null;
			this.memberNames = null;
			this.memberTypes = null;
			this.pr.Init();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0009FB87 File Offset: 0x0009DD87
		internal void ArrayCountIncrement(int value)
		{
			this.count += value;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x0009FB98 File Offset: 0x0009DD98
		internal bool GetNext(out BinaryTypeEnum outBinaryTypeEnum, out object outTypeInformation)
		{
			outBinaryTypeEnum = BinaryTypeEnum.Primitive;
			outTypeInformation = null;
			if (this.objectTypeEnum == InternalObjectTypeE.Array)
			{
				if (this.count == this.numItems)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnum;
				outTypeInformation = this.typeInformation;
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.count++;
				return true;
			}
			else
			{
				if (this.count == this.memberLength && !this.isInitial)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnumA[this.count];
				outTypeInformation = this.typeInformationA[this.count];
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.name = this.memberNames[this.count];
				Type[] array = this.memberTypes;
				this.dtType = this.memberTypes[this.count];
				this.count++;
				return true;
			}
		}

		// Token: 0x0400137C RID: 4988
		internal static int opRecordIdCount = 1;

		// Token: 0x0400137D RID: 4989
		internal bool isInitial;

		// Token: 0x0400137E RID: 4990
		internal int count;

		// Token: 0x0400137F RID: 4991
		internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;

		// Token: 0x04001380 RID: 4992
		internal object expectedTypeInformation;

		// Token: 0x04001381 RID: 4993
		internal string name;

		// Token: 0x04001382 RID: 4994
		internal InternalObjectTypeE objectTypeEnum;

		// Token: 0x04001383 RID: 4995
		internal InternalMemberTypeE memberTypeEnum;

		// Token: 0x04001384 RID: 4996
		internal InternalMemberValueE memberValueEnum;

		// Token: 0x04001385 RID: 4997
		internal Type dtType;

		// Token: 0x04001386 RID: 4998
		internal int numItems;

		// Token: 0x04001387 RID: 4999
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x04001388 RID: 5000
		internal object typeInformation;

		// Token: 0x04001389 RID: 5001
		internal int nullCount;

		// Token: 0x0400138A RID: 5002
		internal int memberLength;

		// Token: 0x0400138B RID: 5003
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x0400138C RID: 5004
		internal object[] typeInformationA;

		// Token: 0x0400138D RID: 5005
		internal string[] memberNames;

		// Token: 0x0400138E RID: 5006
		internal Type[] memberTypes;

		// Token: 0x0400138F RID: 5007
		internal ParseRecord pr = new ParseRecord();
	}
}
