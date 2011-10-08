using System;
using robokins.Utility;
using robokins.IRC;

namespace robokins.Trigger
{
	partial class TriggerAction
	{
		
		/// <summary>
		/// Searches a web page
		/// 
		/// Requires:
		///  * searchUri
		///  * startsWith
		///  * endsWith
		/// </summary>
		/// <param name="msg"> A <see cref="ReceivedMessage"/> </param>
		/// <param name="resp"> A <see cref="TriggerResponse"/> </param>
		/// <returns>
		/// Always true.
		/// </returns>
		static bool HandlePageSearch(TriggerAction act, ReceivedMessage msg, ref TriggerResponse resp)
		{
			string uri = Format(msg, act.searchUri);
			string data = HTTP.DownloadPage(uri);
			string match = Texts.StringBetween(data, act.startsWith, act.endsWith);
			
			if (match == null)
			{
				// Can later be changed to a user specified fail message or
				// setting act.Respond to false.
				resp.Message = String.Format(act.ReturnFormat, "no match");
				
				// we return false so another lower priority function can still
				// try to use the input
				return false;
			}
			else
			{
				resp.Message = String.Format(act.ReturnFormat, match);
			}
			
			return true;
		}
		
		/// <summary>
		/// Processes flags in the pattern by replacing them with
		/// meaningful values.
		/// </summary>
		/// <param name="msg">
		/// A <see cref="ReceivedMessage"/>
		/// </param>
		/// <param name="pattern">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		protected static string Format(ReceivedMessage msg, string pattern)
		{
			Console.WriteLine("In Format():");
			
			char[] SPACE = {' '};
			string[] request = msg.Text.Split(SPACE, 2, StringSplitOptions.RemoveEmptyEntries);
			string[] requestWords = request[1].Split(SPACE, StringSplitOptions.RemoveEmptyEntries);
			
			string customized = pattern.Replace("{all}", request[1])
				.Replace("{host}", msg.User.Host)
				.Replace("{nick}", msg.User.Nick)
				.Replace("{ident}", msg.User.Ident);
			
			Console.WriteLine("customized: {0}", customized);
			
			if (requestWords.Length > 0)
			{
				int lastIndex = requestWords.Length - 1;
				customized = customized.Replace("{last}", requestWords[lastIndex]);
				Console.WriteLine("Length > 0");
			}
			
			try {
				string formatted = string.Format(customized, requestWords);
				Console.WriteLine("End Format() Clean:");
				return formatted;
			}
			
			// The user of the software might have made a mistake in their .conf file
			// We can use the 'customized' string as a fall back and alert them to the error
			// It should be fairly noticable that their results are incorrect
			catch (FormatException e) {
				Console.WriteLine("WARNING: In trigger {0}, {1}", request[0], e.Message);
				return customized;
			}
		}
	}
}

