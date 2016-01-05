using System.Net.Mail;

namespace MaxExperiment.Util
{
    /// <summary>
    /// Util for validating various data format.
    /// </summary>
    public static class FormatValidator
    {
        /// <summary>
        /// Return true of string is valid email format.
        /// </summary>
        /// <param name="email">email address.</param>
        /// <returns>true of string is valid email format.</returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
