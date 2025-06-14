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
    public partial class FormAdd : Form
    {
        DataBase dataBase = new DataBase();
        public FormAdd()
        {
            InitializeComponent();
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

        private void FormAdd_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
            idINCR.Text = (Convert.ToInt32(dataGridView1[0,dataGridView1.Rows.Count-2].Value)+1).ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            label2.Visible = false;
            label11.Visible = true;
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox1.Focused != true) 
            {
                label2.Visible = true;
                label11.Visible = false;
            }
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                label2.Visible = true;
                label11.Visible = false;
            }
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
            label3.Visible = false;
            label12.Visible = true;
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox2.Focused != true)
            {
                label3.Visible = true;
                label12.Visible = false;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                label3.Visible = true;
                label12.Visible = false;
            }
        }

        private void textBox5_MouseEnter(object sender, EventArgs e)
        {
            label10.Visible = false;
            label13.Visible = true;
        }

        private void textBox5_MouseLeave(object sender, EventArgs e)
        {
            if (textBox5.Text == "" && textBox5.Focused != true)
            {
                label10.Visible = true;
                label13.Visible = false;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                label10.Visible = true;
                label13.Visible = false;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                return; 
            }

            if (int.TryParse(textBox4.Text, out int value) && (value < 0 || value > 23))
            {
                textBox4.Text = "00"; 
                textBox4.SelectionStart = textBox4.Text.Length; 
            }


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                return;
            }

            if (int.TryParse(textBox3.Text, out int value) && (value < 0 || value > 59))
            {
                textBox3.Text = "00";
                textBox3.SelectionStart = textBox3.Text.Length;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                return;
            }

            if (int.TryParse(textBox8.Text, out int value) && (value < 0 || value > 23))
            {
                textBox8.Text = "00";
                textBox8.SelectionStart = textBox8.Text.Length;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                return;
            }

            if (int.TryParse(textBox9.Text, out int value) && (value < 0 || value > 59))
            {
                textBox9.Text = "00";
                textBox9.SelectionStart = textBox9.Text.Length;
            }
        }

        private void textBoxSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox8.Text) ||
                    string.IsNullOrWhiteSpace(textBox9.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!TimeSpan.TryParse(textBox4.Text + ":" + textBox3.Text + ":00", out TimeSpan departureTime) ||
                    !TimeSpan.TryParse(textBox8.Text + ":" + textBox9.Text + ":00", out TimeSpan arrivalTime))
                {
                    MessageBox.Show("Некорректный формат времени. Используйте ЧЧ:ММ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime toDestination = dateTimePicker1.Value.Date + departureTime;
                DateTime toEnd = dateTimePicker2.Value.Date + arrivalTime;

                DialogResult result = MessageBox.Show(
                    $"Номер записи: {idINCR.Text}\n" +
                    $"Авиакомпания: {textBox1.Text}\n" +
                    $"Номер рейса: {textBox2.Text}\n" +
                    $"Дата отправления: {toDestination:dd.MM.yyyy HH:mm:ss}\n" +
                    $"Дата прибытия: {toEnd:dd.MM.yyyy HH:mm:ss}\n" +
                    $"Место назначения: {textBox5.Text}",
                    "Вы действительно хотите добавить следующую запись?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                string addQuery = @"
        INSERT INTO airlines 
            (id, airline_name, flight_number, departure_time, arrival_time, destination) 
        VALUES 
            (@id, @airlineName, @flightNumber, @departureTime, @arrivalTime, @destination)";

                using (var command = new MySqlCommand(addQuery, dataBase.GetConnection()))
                {
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(idINCR.Text));
                    command.Parameters.AddWithValue("@airlineName", textBox1.Text);
                    command.Parameters.AddWithValue("@flightNumber", textBox2.Text);
                    command.Parameters.AddWithValue("@departureTime", toDestination.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    command.Parameters.AddWithValue("@arrivalTime", toEnd.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    command.Parameters.AddWithValue("@destination", textBox5.Text);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Запись успешно создана.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                return;
            }
            if (Convert.ToInt32(textBox4.Text) < 10 && textBox4.TextLength <2)
            {
                string sas = textBox4.Text;
                textBox4.Text = "0" + sas;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
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

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                return;
            }
            if (Convert.ToInt32(textBox8.Text) < 10 && textBox8.TextLength < 2)
            {
                string sas = textBox8.Text;
                textBox8.Text = "0" + sas;
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                return;
            }
            if (Convert.ToInt32(textBox9.Text) < 10 && textBox9.TextLength < 2)
            {
                string sas = textBox9.Text;
                textBox9.Text = "0" + sas;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            label2.Visible = false;
            label11.Visible = true;
        }
    }
}
