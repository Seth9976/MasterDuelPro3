using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000008 RID: 8
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoAccessibilityNodeData")]
	[NativeHeader("Modules/Accessibility/Bindings/AccessibilityNodeData.bindings.h")]
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNodeData.h")]
	internal struct AccessibilityNodeData
	{
		// Token: 0x1700000C RID: 12
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026E3 File Offset: 0x000008E3
		public int id
		{
			[CompilerGenerated]
			set
			{
				this.<id>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000026EC File Offset: 0x000008EC
		public bool isActive
		{
			[CompilerGenerated]
			set
			{
				this.<isActive>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000026F5 File Offset: 0x000008F5
		public string label
		{
			[CompilerGenerated]
			set
			{
				this.<label>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000026FE File Offset: 0x000008FE
		public string value
		{
			[CompilerGenerated]
			set
			{
				this.<value>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002707 File Offset: 0x00000907
		public string hint
		{
			[CompilerGenerated]
			set
			{
				this.<hint>k__BackingField = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002710 File Offset: 0x00000910
		public AccessibilityRole role
		{
			[CompilerGenerated]
			set
			{
				this.<role>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002719 File Offset: 0x00000919
		public bool allowsDirectInteraction
		{
			[CompilerGenerated]
			set
			{
				this.<allowsDirectInteraction>k__BackingField = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002722 File Offset: 0x00000922
		public AccessibilityState state
		{
			[CompilerGenerated]
			set
			{
				this.<state>k__BackingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000272B File Offset: 0x0000092B
		public Rect frame
		{
			[CompilerGenerated]
			set
			{
				this.<frame>k__BackingField = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002734 File Offset: 0x00000934
		public int parentId
		{
			[CompilerGenerated]
			set
			{
				this.<parentId>k__BackingField = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000273D File Offset: 0x0000093D
		public int[] childIds
		{
			[CompilerGenerated]
			set
			{
				this.<childIds>k__BackingField = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002746 File Offset: 0x00000946
		internal SystemLanguage language
		{
			[CompilerGenerated]
			set
			{
				this.<language>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000274F File Offset: 0x0000094F
		public bool implementsSelected
		{
			[CompilerGenerated]
			set
			{
				this.<implementsSelected>k__BackingField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002758 File Offset: 0x00000958
		public bool implementsDismissed
		{
			[CompilerGenerated]
			set
			{
				this.<implementsDismissed>k__BackingField = value;
			}
		}
	}
}
