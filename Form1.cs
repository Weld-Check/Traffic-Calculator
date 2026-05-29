namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            clearAllFields(); 
        }
        // Exits the program
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Opens the secondary info Form
        private void button3_Click(object sender, EventArgs e)
        {
            using (Form2 aboutForm = new())
            {
                aboutForm.ShowDialog(this);
            }
        }
        public List<RadioButton> getRadioButtons()
        {
            return groupBox1.Controls.Cast<RadioButton>().ToList();
        }
        // This clears all fields in the program
        private void clearAllFields()
        {
            label6.Text = string.Empty;
            label7.Text = string.Empty;
            label8.Text = string.Empty;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            getRadioButtons().ConvertAll(x => x.Checked = false);

        }
        // Clear button
        private void button2_Click(object sender, EventArgs e)
        {
            clearAllFields();
        }
        // Checks whether the attempted inputed char is a numeric value or if its an important key such as Backspace
        private bool shouldAcceptKeyPress(KeyPressEventArgs e)
        {
            return char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar);
        }
        // Helper for accepting input, only accepts numeric values + allow backspace and control keys
        private bool acceptTextInput(TextBox textBox, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && textBox.Text.Length >= 3) // no speed will exceed 3 digits as thats faster than the fastest car ever driven
            {
                return false;
            }
            return shouldAcceptKeyPress(e);
        }
        // Gets called when typing into the text boxes
        private void textbox1_OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!acceptTextInput(textBox1, e))
            {
                e.Handled = true;
            }
        }

        private void textbox2_OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!acceptTextInput(textBox2, e))
            {
                e.Handled = true;
            }
        }
        // Proof checking all inputed data
        private bool canCalculate()
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                return false;
            }

            int speedLimit = int.Parse(textBox1.Text);
            int actualSpeed = int.Parse(textBox2.Text);
            if (speedLimit > actualSpeed)
            {
                MessageBox.Show("Error actual speed is less than speed limit!");
                return false;
            }
            return getRadioButtons().Count(button => button.Checked) == 1;
        }
        // Handle entire calculations and display correct information
        private void calculate()
        {
            int speedLimit = int.Parse(textBox1.Text);
            int actualSpeed = int.Parse(textBox2.Text);
            int infraction = getRadioButtons().FindIndex(button => button.Checked); // We've already error checked this value and know there is atleast one button thats checked
            Ticket ticket = new Ticket(speedLimit, actualSpeed, infraction);

            ticket.CalculateTotalCost();
            label6.Text = ticket.ticketCost.ToString();
            label7.Text = ticket.courtCost.ToString();
            label8.Text = ticket.totalCost.ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!canCalculate())
            {
                MessageBox.Show("Error cannot calculate please check all data.");
                return;
            }
            calculate();
        }
    }
}
