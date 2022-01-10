using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login_and_Register_System
{
    public partial class EmailVerification : Form
    {
        Random rnd = new Random();
        string code = "";
        OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = db_users.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        string email = "";
        string password = "";
        public EmailVerification(string email, string password)
        {
            InitializeComponent();
            string uemail = "mymailtosend9@gmail.com";
            string upassword = "jficbxzijnomxjgk";
            this.email = email;
            this.password = password;

            code = $"{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(uemail);
                mail.To.Add(email);
                mail.Subject = "Ur personal code is:";
                mail.Body = $"<h1>{code}</h1>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(uemail, upassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(txtCode.Text == code)
                {
                    con.Open();
                    string register = "INSERT INTO tbl_users VALUES ('" + email + "','" + password + "')";
                    cmd = new OleDbCommand(register, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Your Account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new dashboard().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect code");
                }
            }
        }
    }
}
