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
    public partial class AdminForm : Form
    {
        List<string> users = new List<string>();
        List<string> roles = new List<string>();
        List<string> passwords = new List<string>();
        public AdminForm()
        {
            InitializeComponent();
            MakePrep();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void GetUsers()
        {
            StreamReader readLogin = new StreamReader("login.txt");
            string line;
            while ((line = readLogin.ReadLine()) != null)
            {
                users.Add(line);
            }
            readLogin.Close();
        }
        private void FillUsers()
        {
            foreach(var b in users)
            {
                listBox1.Items.Add(b);
            }
        }
        private void GetRoles()
        {
            StreamReader readRole = new StreamReader("role.txt");
            string line;
            while ((line = readRole.ReadLine()) != null)
            {
                roles.Add(line);
            }
            readRole.Close();
        }
        private void FillRoles()
        {
            foreach (var b in roles)
            {
                listBox2.Items.Add(b);
            }
        }
        private void MakePrep()
        {
            users.Clear();
            roles.Clear();
            passwords.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            GetUsers();
            FillUsers();
            GetRoles();
            FillRoles();
        }
        private void ChangeRole(string newRole, int num)
        {
            roles[num] = newRole;
            StreamWriter writeRole = new StreamWriter("role.txt", false);
            foreach (var b in roles)
            {
                writeRole.WriteLine(b);
            }
            writeRole.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItems.Count != 0)
            {
                ChangeRole(comboBox1.SelectedItem.ToString(), listBox1.SelectedIndex);
                MakePrep();
            }
            else
            {
                MessageBox.Show(
                            "Выберите пользователя.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 0)
            {
                DeleteUser(listBox1.SelectedIndex);
            }
            else
            {
                MessageBox.Show(
                            "Выберите пользователя.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
            }
        }
        private void DeleteUser(int num)
        {
            GetPasswords();
            users.RemoveAt(num);
            roles.RemoveAt(num);
            passwords.RemoveAt(num);
            StreamWriter writeLogin = new StreamWriter("login.txt", false);
            StreamWriter writePassword = new StreamWriter("password.txt", false);
            StreamWriter writeRole = new StreamWriter("role.txt", false);
            foreach(var b in users)
            {
                writeLogin.WriteLine(b);
            }
            foreach (var b in passwords)
            {
                writePassword.WriteLine(b);
            }
            foreach (var b in roles)
            {
                writeRole.WriteLine(b);
            }
            writeLogin.Close();
            writePassword.Close();
            writeRole.Close();
            MakePrep();
        }
        private void GetPasswords()
        {
            StreamReader readPassword = new StreamReader("password.txt");
            string line;
            while ((line = readPassword.ReadLine()) != null)
            {
                passwords.Add(line);
            }
            readPassword.Close();
        }
    }
}
