using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000AD RID: 173
	[InputControlLayout(isGenericTypeOfDevice = true)]
	public class Sensor : InputDevice
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0003381C File Offset: 0x00031A1C
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00033854 File Offset: 0x00031A54
		public float samplingFrequency
		{
			get
			{
				QuerySamplingFrequencyCommand command = QuerySamplingFrequencyCommand.Create();
				if (base.ExecuteCommand<QuerySamplingFrequencyCommand>(ref command) >= 0L)
				{
					return command.frequency;
				}
				throw new NotSupportedException(string.Format("Device '{0}' does not support querying sampling frequency", this));
			}
			set
			{
				SetSamplingFrequencyCommand command = SetSamplingFrequencyCommand.Create(value);
				base.ExecuteCommand<SetSamplingFrequencyCommand>(ref command);
			}
		}
	}
}
