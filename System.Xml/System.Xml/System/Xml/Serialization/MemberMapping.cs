using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200016E RID: 366
	internal class MemberMapping : AccessorMapping
	{
		// Token: 0x06001196 RID: 4502 RVA: 0x00054BFC File Offset: 0x00052DFC
		internal MemberMapping()
		{
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00054C0C File Offset: 0x00052E0C
		private MemberMapping(MemberMapping mapping)
			: base(mapping)
		{
			this.name = mapping.name;
			this.checkShouldPersist = mapping.checkShouldPersist;
			this.checkSpecified = mapping.checkSpecified;
			this.isReturnValue = mapping.isReturnValue;
			this.readOnly = mapping.readOnly;
			this.sequenceId = mapping.sequenceId;
			this.memberInfo = mapping.memberInfo;
			this.checkSpecifiedMemberInfo = mapping.checkSpecifiedMemberInfo;
			this.checkShouldPersistMethodInfo = mapping.checkShouldPersistMethodInfo;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00054C93 File Offset: 0x00052E93
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00054C9B File Offset: 0x00052E9B
		internal bool CheckShouldPersist
		{
			get
			{
				return this.checkShouldPersist;
			}
			set
			{
				this.checkShouldPersist = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00054CA4 File Offset: 0x00052EA4
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00054CAC File Offset: 0x00052EAC
		internal SpecifiedAccessor CheckSpecified
		{
			get
			{
				return this.checkSpecified;
			}
			set
			{
				this.checkSpecified = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00054CB5 File Offset: 0x00052EB5
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x00054CCB File Offset: 0x00052ECB
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00054CD4 File Offset: 0x00052ED4
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x00054CDC File Offset: 0x00052EDC
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00054CE5 File Offset: 0x00052EE5
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00054CED File Offset: 0x00052EED
		internal MemberInfo CheckSpecifiedMemberInfo
		{
			get
			{
				return this.checkSpecifiedMemberInfo;
			}
			set
			{
				this.checkSpecifiedMemberInfo = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00054CF6 File Offset: 0x00052EF6
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00054CFE File Offset: 0x00052EFE
		internal MethodInfo CheckShouldPersistMethodInfo
		{
			get
			{
				return this.checkShouldPersistMethodInfo;
			}
			set
			{
				this.checkShouldPersistMethodInfo = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00054D07 File Offset: 0x00052F07
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x00054D0F File Offset: 0x00052F0F
		internal bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00054D18 File Offset: 0x00052F18
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x00054D20 File Offset: 0x00052F20
		internal bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x00054D29 File Offset: 0x00052F29
		internal bool IsSequence
		{
			get
			{
				return this.sequenceId >= 0;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00054D37 File Offset: 0x00052F37
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00054D3F File Offset: 0x00052F3F
		internal int SequenceId
		{
			get
			{
				return this.sequenceId;
			}
			set
			{
				this.sequenceId = value;
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00054D48 File Offset: 0x00052F48
		private string GetNullableType(TypeDesc td)
		{
			if (td.IsMappedType || (!td.IsValueType && (base.Elements[0].IsSoap || td.ArrayElementTypeDesc == null)))
			{
				return td.FullName;
			}
			if (td.ArrayElementTypeDesc != null)
			{
				return this.GetNullableType(td.ArrayElementTypeDesc) + "[]";
			}
			return "System.Nullable`1[" + td.FullName + "]";
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00054DB7 File Offset: 0x00052FB7
		internal MemberMapping Clone()
		{
			return new MemberMapping(this);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00054DBF File Offset: 0x00052FBF
		internal string GetTypeName(CodeDomProvider codeProvider)
		{
			if (base.IsNeedNullable && codeProvider.Supports(GeneratorSupport.GenericTypeReference))
			{
				return this.GetNullableType(base.TypeDesc);
			}
			return base.TypeDesc.FullName;
		}

		// Token: 0x04000865 RID: 2149
		private string name;

		// Token: 0x04000866 RID: 2150
		private bool checkShouldPersist;

		// Token: 0x04000867 RID: 2151
		private SpecifiedAccessor checkSpecified;

		// Token: 0x04000868 RID: 2152
		private bool isReturnValue;

		// Token: 0x04000869 RID: 2153
		private bool readOnly;

		// Token: 0x0400086A RID: 2154
		private int sequenceId = -1;

		// Token: 0x0400086B RID: 2155
		private MemberInfo memberInfo;

		// Token: 0x0400086C RID: 2156
		private MemberInfo checkSpecifiedMemberInfo;

		// Token: 0x0400086D RID: 2157
		private MethodInfo checkShouldPersistMethodInfo;
	}
}
