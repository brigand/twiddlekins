using System;
using System.Collections.Generic;
using System.Text;
using robokins.IRC;

namespace robokins.Trigger
{
	/// <summary>
	/// Used to process a message and give a response.
	/// </summary>
	/// <returns>
	/// True to signal the message is ready to be sent.
	/// </returns>
	delegate bool TriggerHandleDelegate(TriggerAction act, ReceivedMessage msg, ref TriggerResponse resp);
	
	partial class TriggerAction
	{
		#region Fields
		protected string searchUri = string.Empty;
		protected string matchString = string.Empty;
		protected string startsWith = string.Empty;
		protected string endsWith = string.Empty;
		protected bool encode;
		protected string returnFormat = string.Empty;
		protected int requiredArguments;
		protected TriggerHandleDelegate action;
		#endregion
		
		#region Properties
		/// <summary>
		/// String used when executing a search.
		/// </summary>
		public string SearchUri
		{
			get { return searchUri; }
		}

		/// <summary>
		/// Variable used for simple string matching.
		/// </summary>
		public string MatchString
		{
			get { return matchString; }
		}

		/// <summary>
		/// Used to find the start when extracting a section out of returned 
		/// data from a search.
		/// </summary>
		public string StartsWith
		{
			get { return startsWith; }
		}

		/// <summary>
		/// Used to find the end when extracting a section out of returned
		/// data from a search.
		/// </summary>
		public string EndsWith
		{
			get { return endsWith; }
		}

		/// <summary>
		/// Tells the handler whether or not to encode user input when passing it to
		/// the SearchUri.
		/// </summary>
		public bool Encode
		{
			get { return encode; }
		}

		public string ReturnFormat
		{
			get { return returnFormat; }
		}
		

		public int RequiredArguments
		{
			get { return requiredArguments; }
		}
		#endregion

		
		
		#region Overloads
		
		/// <summary>
		/// Construct a new TriggerAction that searches a web-page using the
		/// the normal replacement format for TriggerActions.  
		/// </summary>
		/// <param name="uri">
		/// The uri to be downloaded and parsed. 
		/// 
		/// All C# rules for formatted string apply.  {0} matches the first word 
		/// after the trigger, {1} the second, and so on.  {all} and {last} match
		/// the entire search string.  All input is url safe encoded.
		/// </param>
		/// <param name="starts_with">
		/// The starting text for a match.  This text isn't included in output.
		/// </param>
		/// <param name="ends_with">
		/// The ending text for a match.
		/// </param>
		/// <param name="format">
		/// Specifies the response format.  If blank or null it defaults to the 
		/// matched text.  It uses the <see cref="System.String.Format"> function
		/// where {0} will contain the entire match.
		/// </param>
		/// <returns>
		/// A <see cref="TriggerAction"/>
		/// </returns>
		public static TriggerAction ActionFromWrapper(string uri, string starts_with, string ends_with, string format)
		{
			TriggerAction ta = new TriggerAction(HandlePageSearch);
			ta.searchUri = uri;
			ta.startsWith = starts_with;
			ta.endsWith = ends_with;
			ta.returnFormat = format;
			return ta;
		}
		
		public TriggerAction(TriggerHandleDelegate action)
		{
			this.action = action;
		}


		#endregion
		
		public bool Handle(ReceivedMessage msg, ref TriggerResponse resp)
		{
			// A round-about way of doing this, but it works
			// There should be a simpler way to give the delegate access
			// to 'this'
			return this.action(this, msg, ref resp);
		}
		
	}
}
