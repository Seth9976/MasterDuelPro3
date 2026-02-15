using System;

namespace System.Net
{
	/// <summary>Specifies the status codes returned for a File Transfer Protocol (FTP) operation.</summary>
	// Token: 0x020003A5 RID: 933
	public enum FtpStatusCode
	{
		/// <summary>Included for completeness, this value is never returned by servers.</summary>
		// Token: 0x04000E66 RID: 3686
		Undefined,
		/// <summary>Specifies that the response contains a restart marker reply. The text of the description that accompanies this status contains the user data stream marker and the server marker.</summary>
		// Token: 0x04000E67 RID: 3687
		RestartMarker = 110,
		/// <summary>Specifies that the service is not available now; try your request later.</summary>
		// Token: 0x04000E68 RID: 3688
		ServiceTemporarilyNotAvailable = 120,
		/// <summary>Specifies that the data connection is already open and the requested transfer is starting.</summary>
		// Token: 0x04000E69 RID: 3689
		DataAlreadyOpen = 125,
		/// <summary>Specifies that the server is opening the data connection.</summary>
		// Token: 0x04000E6A RID: 3690
		OpeningData = 150,
		/// <summary>Specifies that the command completed successfully.</summary>
		// Token: 0x04000E6B RID: 3691
		CommandOK = 200,
		/// <summary>Specifies that the command is not implemented by the server because it is not needed.</summary>
		// Token: 0x04000E6C RID: 3692
		CommandExtraneous = 202,
		/// <summary>Specifies the status of a directory.</summary>
		// Token: 0x04000E6D RID: 3693
		DirectoryStatus = 212,
		/// <summary>Specifies the status of a file.</summary>
		// Token: 0x04000E6E RID: 3694
		FileStatus,
		/// <summary>Specifies the system type name using the system names published in the Assigned Numbers document published by the Internet Assigned Numbers Authority.</summary>
		// Token: 0x04000E6F RID: 3695
		SystemType = 215,
		/// <summary>Specifies that the server is ready for a user login operation.</summary>
		// Token: 0x04000E70 RID: 3696
		SendUserCommand = 220,
		/// <summary>Specifies that the server is closing the control connection.</summary>
		// Token: 0x04000E71 RID: 3697
		ClosingControl,
		/// <summary>Specifies that the server is closing the data connection and that the requested file action was successful.</summary>
		// Token: 0x04000E72 RID: 3698
		ClosingData = 226,
		/// <summary>Specifies that the server is entering passive mode.</summary>
		// Token: 0x04000E73 RID: 3699
		EnteringPassive,
		/// <summary>Specifies that the user is logged in and can send commands.</summary>
		// Token: 0x04000E74 RID: 3700
		LoggedInProceed = 230,
		/// <summary>Specifies that the server accepts the authentication mechanism specified by the client, and the exchange of security data is complete.</summary>
		// Token: 0x04000E75 RID: 3701
		ServerWantsSecureSession = 234,
		/// <summary>Specifies that the requested file action completed successfully.</summary>
		// Token: 0x04000E76 RID: 3702
		FileActionOK = 250,
		/// <summary>Specifies that the requested path name was created.</summary>
		// Token: 0x04000E77 RID: 3703
		PathnameCreated = 257,
		/// <summary>Specifies that the server expects a password to be supplied.</summary>
		// Token: 0x04000E78 RID: 3704
		SendPasswordCommand = 331,
		/// <summary>Specifies that the server requires a login account to be supplied.</summary>
		// Token: 0x04000E79 RID: 3705
		NeedLoginAccount,
		/// <summary>Specifies that the requested file action requires additional information.</summary>
		// Token: 0x04000E7A RID: 3706
		FileCommandPending = 350,
		/// <summary>Specifies that the service is not available.</summary>
		// Token: 0x04000E7B RID: 3707
		ServiceNotAvailable = 421,
		/// <summary>Specifies that the data connection cannot be opened.</summary>
		// Token: 0x04000E7C RID: 3708
		CantOpenData = 425,
		/// <summary>Specifies that the connection has been closed.</summary>
		// Token: 0x04000E7D RID: 3709
		ConnectionClosed,
		/// <summary>Specifies that the requested action cannot be performed on the specified file because the file is not available or is being used.</summary>
		// Token: 0x04000E7E RID: 3710
		ActionNotTakenFileUnavailableOrBusy = 450,
		/// <summary>Specifies that an error occurred that prevented the request action from completing.</summary>
		// Token: 0x04000E7F RID: 3711
		ActionAbortedLocalProcessingError,
		/// <summary>Specifies that the requested action cannot be performed because there is not enough space on the server.</summary>
		// Token: 0x04000E80 RID: 3712
		ActionNotTakenInsufficientSpace,
		/// <summary>Specifies that the command has a syntax error or is not a command recognized by the server.</summary>
		// Token: 0x04000E81 RID: 3713
		CommandSyntaxError = 500,
		/// <summary>Specifies that one or more command arguments has a syntax error.</summary>
		// Token: 0x04000E82 RID: 3714
		ArgumentSyntaxError,
		/// <summary>Specifies that the command is not implemented by the FTP server.</summary>
		// Token: 0x04000E83 RID: 3715
		CommandNotImplemented,
		/// <summary>Specifies that the sequence of commands is not in the correct order.</summary>
		// Token: 0x04000E84 RID: 3716
		BadCommandSequence,
		/// <summary>Specifies that login information must be sent to the server.</summary>
		// Token: 0x04000E85 RID: 3717
		NotLoggedIn = 530,
		/// <summary>Specifies that a user account on the server is required.</summary>
		// Token: 0x04000E86 RID: 3718
		AccountNeeded = 532,
		/// <summary>Specifies that the requested action cannot be performed on the specified file because the file is not available.</summary>
		// Token: 0x04000E87 RID: 3719
		ActionNotTakenFileUnavailable = 550,
		/// <summary>Specifies that the requested action cannot be taken because the specified page type is unknown. Page types are described in RFC 959 Section 3.1.2.3</summary>
		// Token: 0x04000E88 RID: 3720
		ActionAbortedUnknownPageType,
		/// <summary>Specifies that the requested action cannot be performed.</summary>
		// Token: 0x04000E89 RID: 3721
		FileActionAborted,
		/// <summary>Specifies that the requested action cannot be performed on the specified file.</summary>
		// Token: 0x04000E8A RID: 3722
		ActionNotTakenFilenameNotAllowed
	}
}
