using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChanceryStore.models
{
    public class UserBL
    {
        // вошедший пользователь
        static User loggedUser = new User();

        static public User LoggedUser
        { get { return loggedUser; }
            set { loggedUser = value; }
        }

        /// <summary>
        /// Проверить верно ли введен логин и пароль
        /// </summary>
        static public string generateLogin(string login)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder("");
            string result;

            string SetNumChar = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            int randomDigit; // рандомная цифра
            do
            {
                string selectedLetterNum = ""; // рандомный знак
                randomDigit = random.Next(0, SetNumChar.Length - 1);
                selectedLetterNum += SetNumChar[randomDigit];
                sb.Append(login + selectedLetterNum);
            }

            while (UserSql.checkLoginHas(sb.ToString()));

            result = sb.ToString();

            return result;
        }

        static public void SaveFileUser(string firstName, string lastName, string login, string password)
        {
            string namePath = "D:";

            string text = $"Логин: {login} \nПароль: {password} \n";
            try
            {
                string path = $@"{namePath}\ChanceryStore\Логины и пароли\Логин,Пароль - {firstName} -{lastName}.txt";


            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine(text);
            }
        }

            catch 
            {
                try { 
                namePath = "C:";
                string path = $@"{namePath}\ChanceryStore\Логины и пароли\Логин,Пароль - {firstName} -{lastName}.txt";
                }
                catch
                {
                    try
                    {
                        namePath = "C:";
                        string path = $@"{namePath}\ChanceryStore\Логины и пароли\Логин,Пароль - {firstName} -{lastName}.txt";
                    }
                    catch { namePath = Environment.CurrentDirectory;
                        string path = $@"{namePath}\ChanceryStore\Логины и пароли\Логин,Пароль - {firstName} -{lastName}.txt";
                    }

                }
            }

           
        }

       
    }
}
