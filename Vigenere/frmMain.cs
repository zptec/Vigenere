/*
 * Problem of the Day: 3/6/14
 * http://www.problemotd.com/
 * 
 * Vigenère cipher
 * The Vigenère cipher made its rounds in the mid-1550s up until the end of the
 * American Civil War. It was very easy for soldiers to encode messages and
 * pass them around to all the allied camps.
 *
 * The cipher requires a key and a message. It works like this:
 *
 *     Key: REDDIT
 * Message: TODAYISMYBIRTHDAY
 *
 * REDDITREDDITREDDI
 * TODAYISMYBIRTHDAY
 * -----------------
 * KSGDGBJQBEQKKLGDG
 *
 * Using a 0 based alphabet (A=0), R is the 17th letter of the alphabet and T
 * is the 19th letter of the alphabet. (17 + 19) mod 26 = 11 which is where K
 * resides in the alphabet. Repeat for each key/message letter combination
 * until done.
 *
 * Today's problem of the day is two part. The first part is to implement a
 * Vigenère cipher in the programming language of your choice. Feel free to
 * post solutions or links to solutions in the comments.
 *
 * The second part is to try and implement something to crack the message below
 * (the key is 5 or less characters).
 *
 * ZEJFOKHTMSRMELCPODWHCGAW (Solution: DAY)
 *
 * Good luck!
 */
using System;
using System.Text;
using System.Windows.Forms;

namespace Vigenere
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCipherKey.Text = "";
            txtMessage.Text = "";
            txtResult.Text = "";
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCipherKey.Text))
            {
                MessageBox.Show("Please enter a cipher key to use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Please enter a message to decrypt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtCipherKey.Text = SanitizeString(txtCipherKey.Text);
            txtMessage.Text = SanitizeString(txtMessage.Text);
            Vigenere vigenere = new Vigenere(txtCipherKey.Text, txtMessage.Text);
            vigenere.Decrypt();
            txtResult.Text = vigenere.Result;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCipherKey.Text))
            {
                MessageBox.Show("Please enter a cipher key to use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Please enter a message to encrypt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtCipherKey.Text = SanitizeString(txtCipherKey.Text);
            txtMessage.Text = SanitizeString(txtMessage.Text);
            Vigenere vigenere = new Vigenere(txtCipherKey.Text, txtMessage.Text);
            vigenere.Encrypt();
            txtResult.Text = vigenere.Result;
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txtMessage.SelectAll();
        }

        private void txtResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txtMessage.SelectAll();
        }

        private string SanitizeString(string message)
        {
            StringBuilder trimmedMessage = new StringBuilder();

            message = message.ToUpper().Trim();
            for (int i = 0; i < message.Length; i++)
                if (message[i] >= 'A' && message[i] <= 'Z') // remove non-characters
                    trimmedMessage.Append(message[i]);

            return trimmedMessage.ToString();
        }
    }

    public class Vigenere
    {
        string message;
        int keyLength;
        int messageLength;

        public Vigenere(string key, string message)
        {
            this.Key = key;
            keyLength = key.Length;
            this.message = message;
            messageLength = message.Length;
        }

        public string Result { get; private set; }

        public string Key { get; private set; }

        // http://en.wikipedia.org/wiki/Vigen%C3%A8re_cipher#Algebraic_description
        public void Decrypt()
        {
            StringBuilder cryptoString = new StringBuilder();

            for (int i = 0; i < messageLength; i++)
            {
                // Broken down for legibility
                int keyChar = (int)Key[i % keyLength] - 65;
                int messageChar = (int)message[i] - 65;
                int charDifference = messageChar - keyChar;
                int newCharValue = (charDifference < 0) ? (charDifference + 26) : (charDifference % 26);
                char newChar = (char)(newCharValue + 65);
                cryptoString.Append(newChar);
            }

            Result = cryptoString.ToString();
        }

        public void Encrypt()
        {
            StringBuilder cryptoString = new StringBuilder();

            for (int i = 0; i < messageLength; i++)
            {
                // Broken down for legibility
                int keyChar = (int)Key[i % keyLength] - 65;
                int messageChar = (int)message[i] - 65;
                int newCharValue = (keyChar + messageChar) % 26;
                char newChar = (char)(newCharValue + 65);
                cryptoString.Append(newChar);
            }

            Result = cryptoString.ToString();
        }
    }
}
