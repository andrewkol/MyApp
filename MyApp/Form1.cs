using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyApp
{
    public partial class Form1 : Form
    {
        bool checker = false;
        int numStr = 0;
        string loginRole = "";
        
        
        public Form1()
        {
            InitializeComponent();
            textBox3.Hide();
            checkBox2.Hide();
            textBox2.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
            checkBox1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(FindUser(textBox1.Text))
            {
                if (!checker)
                {
                    MessageBox.Show(
            "Повторно введите пароль.",
            "Регистрация",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
                    textBox3.Show();
                    checkBox2.Show();
                    checker = true;
                    checkBox2.Checked = true;
                }
                else
                {
                    if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                    {
                        MessageBox.Show(
                                "Пустые поля запрещены.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        if (CheckPassword(textBox2.Text, textBox3.Text))
                        {
                            Register(textBox1.Text, textBox2.Text);
                            MessageBox.Show(
                            "Вы успешно зарегестрированы. Войдите для продолжения работы.",
                            "Регистрация",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                            textBox3.Hide();
                            checkBox2.Hide();
                            checker = false;
                            textBox2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show(
                            "Проверьте пароль.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Пользователь уже зарегестрирован.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar)
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox3.UseSystemPasswordChar)
                textBox3.UseSystemPasswordChar = false;
            else
                textBox3.UseSystemPasswordChar = true;
        }
        private bool CheckPassword(string a, string b)
        {
            if (a == b)
                return true;
            return false;
        }
        private bool FindUser(string a)
        {
            StreamReader readLogin = new StreamReader("login.txt");
            string line;
            int counter = 0;
            while ((line = readLogin.ReadLine()) != null)
            {
                if (a == line)
                {
                    readLogin.Close();
                    numStr = counter;
                    return false;
                }
                counter++;
            }
            readLogin.Close();
            return true;
        }
        private void Register(string a, string b)
        {
            StreamWriter writeLogin = new StreamWriter("login.txt", true);
            StreamWriter writePassword = new StreamWriter("password.txt", true);
            StreamWriter writeRole = new StreamWriter("role.txt", true);
            writeLogin.WriteLine(a);
            writePassword.WriteLine(b);
            writeRole.WriteLine("User");
            writeLogin.Close();
            writePassword.Close();
            writeRole.Close();
        }
        private void Login(string a, string b)
        {
            if(!checker)
            {
                if (a != "" && b != "")
                {
                    if (!FindUser(a))
                    {
                        if (LoginCheckPassword(b))
                        {
                            GetRole();
                            switch (loginRole)
                            {
                                case "User":
                                    {
                                        Form newForm = new UserForm();
                                        newForm.Show();
                                        break;
                                    }
                                case "SuperUser":
                                    {
                                        Form newForm = new SuperUserForm();
                                        newForm.Show();
                                        break;
                                    }
                                case "Administrator":
                                    {
                                        Form newForm = new AdminForm();
                                        newForm.Show();
                                        break;
                                    }
                                default:
                                    {
                                        MessageBox.Show(
                                            "Ошибка.",
                                            "Ошибка",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information,
                                            MessageBoxDefaultButton.Button1);
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            MessageBox.Show(
                                "Пароль неправильный.",
                                "Ошибка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Пользователь не зарегестрирован.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show(
                            "Пустые поля запрещены.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(
                            "Завершите регистрацию.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login(textBox1.Text, textBox2.Text);
        }
        private bool LoginCheckPassword(string a)
        {
            StreamReader readPassword = new StreamReader("password.txt");
            string line;
            int counter = 0;
            while ((line = readPassword.ReadLine()) != null)
            {
                if (a == line && counter == numStr)
                {
                    readPassword.Close();
                    return true;
                }
                counter++;
            }
            readPassword.Close();
            return false;
        }
        private string GetRole()
        {
            StreamReader readRole = new StreamReader("role.txt");
            string line;
            int counter = 0;
            while ((line = readRole.ReadLine()) != null)
            {
                if (counter == numStr)
                {
                    
                    readRole.Close();
                    loginRole = line;
                    return line;
                }
                counter++;
            }
            return "";
        }
    }
}
