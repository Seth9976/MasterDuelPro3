using System;
using System.Globalization;
using System.Text;

namespace System.Net.WebSockets
{
	// Token: 0x020004D7 RID: 1239
	internal static class WebSocketValidate
	{
		// Token: 0x06001E6A RID: 7786 RVA: 0x000856F8 File Offset: 0x000838F8
		internal static void ThrowIfInvalidState(WebSocketState currentState, bool isDisposed, WebSocketState[] validStates)
		{
			string text = string.Empty;
			if (validStates != null && validStates.Length != 0)
			{
				int i = 0;
				while (i < validStates.Length)
				{
					WebSocketState webSocketState = validStates[i];
					if (currentState == webSocketState)
					{
						if (isDisposed)
						{
							throw new ObjectDisposedException("WebSocket");
						}
						return;
					}
					else
					{
						i++;
					}
				}
				text = string.Join<WebSocketState>(", ", validStates);
			}
			throw new WebSocketException(WebSocketError.InvalidState, SR.Format("The WebSocket is in an invalid state ('{0}') for this operation. Valid states are: '{1}'", currentState, text));
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00085760 File Offset: 0x00083960
		internal static void ValidateSubprotocol(string subProtocol)
		{
			if (string.IsNullOrWhiteSpace(subProtocol))
			{
				throw new ArgumentException("Empty string is not a valid subprotocol value. Please use \\\"null\\\" to specify no value.", "subProtocol");
			}
			string text = null;
			foreach (char c in subProtocol)
			{
				if (c < '!' || c > '~')
				{
					text = string.Format(CultureInfo.InvariantCulture, "[{0}]", (int)c);
					break;
				}
				if (!char.IsLetterOrDigit(c) && "()<>@,;:\\\"/[]?={} ".IndexOf(c) >= 0)
				{
					text = c.ToString();
					break;
				}
			}
			if (text != null)
			{
				throw new ArgumentException(SR.Format("The WebSocket protocol '{0}' is invalid because it contains the invalid character '{1}'.", subProtocol, text), "subProtocol");
			}
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x000857FC File Offset: 0x000839FC
		internal static void ValidateCloseStatus(WebSocketCloseStatus closeStatus, string statusDescription)
		{
			if (closeStatus == WebSocketCloseStatus.Empty && !string.IsNullOrEmpty(statusDescription))
			{
				throw new ArgumentException(SR.Format("The close status description '{0}' is invalid. When using close status code '{1}' the description must be null.", statusDescription, WebSocketCloseStatus.Empty), "statusDescription");
			}
			if ((closeStatus >= (WebSocketCloseStatus)0 && closeStatus <= (WebSocketCloseStatus)999) || closeStatus == (WebSocketCloseStatus)1006 || closeStatus == (WebSocketCloseStatus)1015)
			{
				throw new ArgumentException(SR.Format("The close status code '{0}' is reserved for system use only and cannot be specified when calling this method.", (int)closeStatus), "closeStatus");
			}
			int num = 0;
			if (!string.IsNullOrEmpty(statusDescription))
			{
				num = Encoding.UTF8.GetByteCount(statusDescription);
			}
			if (num > 123)
			{
				throw new ArgumentException(SR.Format("The close status description '{0}' is too long. The UTF8-representation of the status description must not be longer than {1} bytes.", statusDescription, 123), "statusDescription");
			}
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000858AC File Offset: 0x00083AAC
		internal static void ValidateArraySegment(ArraySegment<byte> arraySegment, string parameterName)
		{
			if (arraySegment.Array == null)
			{
				throw new ArgumentNullException(parameterName + ".Array");
			}
			if (arraySegment.Offset < 0 || arraySegment.Offset > arraySegment.Array.Length)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Offset");
			}
			if (arraySegment.Count < 0 || arraySegment.Count > arraySegment.Array.Length - arraySegment.Offset)
			{
				throw new ArgumentOutOfRangeException(parameterName + ".Count");
			}
		}
	}
}
