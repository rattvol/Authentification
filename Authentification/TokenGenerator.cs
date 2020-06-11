using Authentification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification
{
    public class TokenGenerator
    {
        const int maxTokenLength = 20;
        public static string TokenGenerate(AuthContext _context)
        {
            char[] chars = "!0123456789@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            StringBuilder token = new StringBuilder(maxTokenLength);
            Random random = new Random();
            bool haveSame;
            do
            {
                token.Clear();
                do
                {
                    token.Append(chars[random.Next(0, chars.Length - 1)]);//never use bitwise opertions on server!
                }
                while (token.Length <= maxTokenLength);
                haveSame = CheckSameToken(token.ToString(), _context);
            } while (haveSame);
            return token.ToString();
        }
        public static bool CheckSameToken(string token, AuthContext _context)
        {
            return _context.User.Any(b => b.Token == token);
        }
    }
}
