using System;
using System.Collections.Generic;
using System.Text;

namespace robokins.Trigger
{
	public struct TriggerResponse
	{
		/// <summary>
		/// String to respond with.  It's sent verbatum unless
		/// Action is true.
		/// </summary>
		public string Message;
		
		/// <summary>
		/// Send privately to the asker.  It will not be visible
		/// to the public channel.
		/// </summary>
		public bool Notify;
		
		/// <summary>
		/// Appears as if the bot is talking in the 1st person.
		/// On many chat clients this is done by typing "/me {Message}"
		/// </summary>
		public bool Action;
		
		/// <summary>
		/// This indicates that the default search should be used.
		/// Typically the function that sets this will not do any other
		/// actions.  One exceptions is Message may be modified, in which
		/// case the altered Message will be used to search.
		/// </summary>
		public bool Search;
		
		/// <summary>
		/// For most cases this will be true.
		/// If false, no response will be given at all. 
		/// </summary>
		public bool Respond;
	}
}
