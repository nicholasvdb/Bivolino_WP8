using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BivoApp
{
   public static class TextValidator
    {


       public static bool IsValidField(TextBox textb)
       {

           if (textb.Text == String.Empty)
           {
               return false;

           }

           return true;

       }

       public static bool IsValidEmail(string email)
       {

           return Regex.IsMatch(email,
                 @"^(?("")(""[^""]+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"); 

       

       }


       public static bool IsValdidPassword(TextBox textb)
       {

           if ((textb.Text.Length < 5) || (textb.Text == String.Empty))
           {
               return false;

           }
           return true;
       }





    }
}
