using System;
using System.Collections.Generic;
using System.Text;

namespace robokins
{
	class TriggerAction
	{
		#region Fields
		protected string searchUri = string.Empty;
		protected string matchString = string.Empty;
		protected string startsWith = string.Empty;
		protected string endsWith = string.Empty;
		protected bool encode;
		protected string returnformat = string.Empty;
		protected int requiredArguments;
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
			get { return returnformat; }
		}

		public int RequiredArguments
		{
			get { return requiredArguments; }
		}
		#endregion

		/// <summary>
		/// Creates a new trigger action using the provided information.
		/// </summary>
		/// <param name="search">Search URI to use when performing the search</param>
		/// <param name="match">String used for "simple" matching against returned data in a search</param>
		/// <param name="starts">Start string used when extracting sections from returned data</param>
		/// <param name="ends">End string used when extracting sections from returned data</param>
		/// <param name="enc">Tells the handler if we want the user input encoded or not</param>
		public TriggerAction(string search, string match, string starts, string ends, bool enc, string _returnformat, int requiredarguments)
		{
			searchUri = search;
			matchString = match;
			startsWith = starts;
			endsWith = ends;
			encode = enc;
			returnformat = _returnformat;
			requiredArguments = requiredarguments;
		}
		#region Overloads
		public TriggerAction(string search, string match, string _returnformat, int requiredargs) :
			this(search, match, null, null, true, _returnformat, requiredargs) { }


		#endregion

	}
}
