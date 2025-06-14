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
    public partial class FormSUBD : Form
    {
        DataBase dataBase = new DataBase();
        public FormSUBD()
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

            dataGridView2.Columns.Add("id", "Номер");
            dataGridView2.Columns.Add("first_name", "Имя");
            dataGridView2.Columns.Add("last_name", "Фамилия");
            dataGridView2.Columns.Add("passport_number", "Номер паспорта");
            dataGridView2.Columns.Add("seat_number", "Место");
            dataGridView2.Columns.Add("flight_number", "Номер рейса");
            dataGridView2.Columns.Add("ticket_class", "Класс полёта");
            dataGridView2.Columns.Add("has_checked_in", "CI");
            dataGridView2.Columns.Add("special_requests", "Пожелания");
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
            record.GetDateTime(3).ToShortDateString(), record.GetDateTime(3).TimeOfDay,
            record.GetDateTime(4).ToShortDateString(), record.GetDateTime(4).TimeOfDay,
            record.GetString(5));

        }
        private void ReadSingleRow2(DataGridView dgw, IDataRecord record)
        {
            try
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
                record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6),
                record.GetBoolean(7), record.GetString(8));
            }
            catch 
            {
                dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
                record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6),
                record.GetBoolean(7), "-");
            }


        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string querryString = "SELECT * FROM airlines"; 

            MySqlCommand command = new MySqlCommand(querryString, dataBase.GetConnection());
            dataBase.openConnection();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
            this.Width = 1000;   
            this.Height = 500;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.Violet;
            dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.Violet;
            dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.Violet;
            dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.Violet;
            dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.Violet;
            dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.Violet;
            dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.Violet;
            dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.Violet;
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 255);
            dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 255);
            dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 255);
            dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 255);
            dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 255);
            dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 255);
            dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 255);
            dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Style.BackColor = Color.FromArgb(255, 192, 255);

        }
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchString = "SELECT * FROM airlines WHERE concat (id, airline_name, flight_number, departure_time, arrival_time, destination) like '%" + textBoxSearch.Text + "%'";
            MySqlCommand com = new MySqlCommand(searchString, dataBase.GetConnection());
            dataBase.openConnection();
            MySqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }

        private void Search2(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string flightNumber = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();

            string searchString = "SELECT * FROM passengers WHERE flight_number LIKE @flightNumber";

            string connectionString = "server=localhost;user=root;password=qqwrd;database=airline_db;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(searchString, connection))
                    {
                        command.Parameters.AddWithValue("@flightNumber", $"%{flightNumber}%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReadSingleRow2(dgw, reader);
                            }
                        }
                    }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (checking > 0)
            {
                this.Width = 1603;
                this.Height = 500;
                Search2(dataGridView2);
            }
        }

        private void textBoxSearch_Click(object sender, EventArgs e)
        {
            textBoxSearch.Text = "";
            textBoxSearch.ForeColor = Color.Black;
        }

        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            textBoxSearch.Text = "Введите текст...";
            RefreshDataGrid(dataGridView1);
            textBoxSearch.ForeColor = SystemColors.InactiveCaption;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form add = new FormAdd();
            add.Show();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Form edit = new FormEdit(dataGridView1.CurrentCell.RowIndex);
            edit.Show();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            var DeleteQuery = $"delete from airlines where id = {dataGridView1[0,dataGridView1.CurrentCell.RowIndex].Value}";
            var command = new MySqlCommand(DeleteQuery, dataBase.GetConnection());
            command.ExecuteNonQuery();
            RefreshDataGrid(dataGridView1);
            MessageBox.Show("Запись успешно удалена", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        int checking = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (checking > 0) 
            {
                checking--;
                this.Width = 1000;
                this.Height = 500;
                button1.FlatStyle = FlatStyle.Popup;
            }
            else
            {
                checking++;
                button1.FlatStyle = FlatStyle.Standard;
            }
        }
    }
}
