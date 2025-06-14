using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Timers;
using System.Threading;
using MySql.Data.MySqlClient;

namespace KursovayaBelya
{
    public partial class FormEdit : Form
    {
        DataBase dataBase = new DataBase();
        int currentrow;

        public FormEdit(int rowindex)
        {
            InitializeComponent();
            currentrow = rowindex;
        }
        private void CreateColums()
        {
            dataGridView1.Columns.Add("id", "Номер записи");
            dataGridView1.Columns.Add("airline_name", "Авиакомпания");
            dataGridView1.Columns.Add("flight_number", "Номер рейса");
            dataGridView1.Columns.Add("departure_time", "Дата отправления");
            dataGridView1.Columns.Add("departure_time", "Время отправления");
            dataGridView1.Columns.Add("arrival_time", "Дата прибытия");
            dataGridView1.Columns.Add("arrival_time", "Время прибытия");
            dataGridView1.Columns.Add("destination", "Назначение");
        }
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
            record.GetDateTime(3).ToShortDateString(), record.GetDateTime(3).TimeOfDay,
            record.GetDateTime(4).ToShortDateString(), record.GetDateTime(4).TimeOfDay,
            record.GetString(5));

        }
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string querryString = $"select * from airlines";

            MySqlCommand command = new MySqlCommand(querryString, dataBase.GetConnection());
            dataBase.openConnection();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);

            label1.Text = "Номер редактируемой записи: " + Convert.ToInt32(dataGridView1[0, currentrow].Value).ToString();
            textBox1.Text = dataGridView1[1, currentrow].Value.ToString();
            textBox2.Text = dataGridView1[2, currentrow].Value.ToString();

            DateTime dateGO = Convert.ToDateTime(dataGridView1[3, currentrow].Value);
            dateTimePicker1.Value = Convert.ToDateTime(dateGO.ToString("dd/MM/yyyy"));
            string timeGO = dataGridView1[4, currentrow].Value.ToString();
            textBox3.Text = timeGO[0].ToString()+timeGO[1].ToString();
            textBox4.Text = timeGO[3].ToString() + timeGO[4].ToString();

            DateTime dateOUT = Convert.ToDateTime(dataGridView1[5, currentrow].Value);
            dateTimePicker2.Value = Convert.ToDateTime(dateOUT.ToString("dd/MM/yyyy"));
            string timeOUT = dataGridView1[6, currentrow].Value.ToString();
            textBox5.Text = timeOUT[0].ToString() + timeOUT[1].ToString();
            textBox6.Text = timeOUT[3].ToString() + timeOUT[4].ToString();

            textBox7.Text = dataGridView1[7,currentrow].Value.ToString();
        }
        
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != dataGridView1[1, currentrow].Value.ToString())
            {
                label2.Text = "Авиакомпания*";
            }
            else 
            {
                label2.Text = "Авиакомпания";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text != dataGridView1[2, currentrow].Value.ToString())
            {
                label3.Text = "Номер рейса*";
            }
            else
            {
                label3.Text = "Номер рейса";
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            DateTime dateGO = Convert.ToDateTime(dataGridView1[3, currentrow].Value);
            if (dateTimePicker1.Value.ToString("dd/MM/yyyy") != dateGO.ToString("dd/MM/yyyy"))
            {
                label4.Text = "Дата отправления*";
            }
            else
            {
                label4.Text = "Дата отправления";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            string tocompareGO = dataGridView1[4, currentrow].Value.ToString();
            if (textBox3.Text != (tocompareGO[0].ToString()+tocompareGO[1].ToString()))
            {
                label4.Text = "Дата отправления*";
            }
            else
            {
                label4.Text = "Дата отправления";
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                return;
            }
            if (Convert.ToInt32(textBox3.Text) < 10 && textBox3.TextLength < 2)
            {
                string sas = textBox3.Text;
                textBox3.Text = "0" + sas;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            string tocompareGO = dataGridView1[4, currentrow].Value.ToString();
            if (textBox4.Text != (tocompareGO[3].ToString() + tocompareGO[4].ToString()))
            {
                label4.Text = "Дата отправления*";
            }
            else
            {
                label4.Text = "Дата отправления";
            }

            if (string.IsNullOrEmpty(textBox4.Text))
            {
                return;
            }
            if (Convert.ToInt32(textBox4.Text) < 10 && textBox4.TextLength < 2)
            {
                string sas = textBox4.Text;
                textBox4.Text = "0" + sas;
            }
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            DateTime dateGO = Convert.ToDateTime(dataGridView1[5, currentrow].Value);
            if (dateTimePicker2.Value.ToString("dd/MM/yyyy") != dateGO.ToString("dd/MM/yyyy"))
            {
                label5.Text = "Дата прибытия*";
            }
            else
            {
                label5.Text = "Дата прибытия";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            string tocompareGO = dataGridView1[6, currentrow].Value.ToString();
            if (textBox5.Text != (tocompareGO[0].ToString() + tocompareGO[1].ToString()))
            {
                label5.Text = "Дата прибытия*";
            }
            else
            {
                label5.Text = "Дата прибытия";
            }

            if (string.IsNullOrEmpty(textBox5.Text))
            {
                return;
            }
            if (Convert.ToInt32(textBox5.Text) < 10 && textBox5.TextLength < 2)
            {
                string sas = textBox5.Text;
                textBox5.Text = "0" + sas;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            string tocompareGO = dataGridView1[6, currentrow].Value.ToString();
            if (textBox6.Text != (tocompareGO[3].ToString() + tocompareGO[4].ToString()))
            {
                label4.Text = "Дата прибытия*";
            }
            else
            {
                label4.Text = "Дата прибытия";
            }

            if (string.IsNullOrEmpty(textBox6.Text))
            {
                return;
            }
            if (Convert.ToInt32(textBox6.Text) < 10 && textBox6.TextLength < 2)
            {
                string sas = textBox6.Text;
                textBox6.Text = "0" + sas;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text != dataGridView1[7, currentrow].Value.ToString())
            {
                label6.Text = "Место назначения*";
            }
            else
            {
                label6.Text = "Место назначения";
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                return;
            }

            if (int.TryParse(textBox3.Text, out int value) && (value < 0 || value > 23))
            {
                textBox3.Text = "00";
                textBox3.SelectionStart = textBox3.Text.Length;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                return;
            }

            if (int.TryParse(textBox5.Text, out int value) && (value < 0 || value > 23))
            {
                textBox5.Text = "00";
                textBox5.SelectionStart = textBox5.Text.Length;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                return;
            }

            if (int.TryParse(textBox4.Text, out int value) && (value < 0 || value > 59))
            {
                textBox4.Text = "00";
                textBox4.SelectionStart = textBox4.Text.Length;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                return;
            }

            if (int.TryParse(textBox6.Text, out int value) && (value < 0 || value > 59))
            {
                textBox6.Text = "00";
                textBox6.SelectionStart = textBox6.Text.Length;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                DateTime toDestination = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd") + " " + textBox3.Text + ":" + textBox4.Text + ":00");
                DateTime toEnd = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd") + " " + textBox5.Text + ":" + textBox6.Text + ":00");
                var changeQuery = @"UPDATE airlines 
                       SET airline_name = @airlineName,  
                           departure_time = @departureTime, 
                           arrival_time = @arrivalTime, 
                           destination = @destination 
                       WHERE id = @currentId";

                var command = new MySqlCommand(changeQuery, dataBase.GetConnection());

                command.Parameters.AddWithValue("@airlineName", textBox1.Text);
                command.Parameters.AddWithValue("@departureTime", toDestination.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@arrivalTime", toEnd.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@destination", textBox7.Text);
                command.Parameters.AddWithValue("@currentId", Convert.ToInt32(dataGridView1[0, currentrow].Value));

                command.ExecuteNonQuery();

                MessageBox.Show("Запись успешно отредактирована", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Заполните поля.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
