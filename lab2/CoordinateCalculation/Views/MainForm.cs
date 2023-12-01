using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Model;

namespace Views
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Список объектов
        /// </summary>
        private List<Movement> _moveList = new List<Movement>();

        public MainForm ( )
        {
            InitializeComponent ( );
            comboBoxType.SelectedIndex = 3;
        }

        /// <summary>
        /// Кнопка Добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click ( object sender, EventArgs e )
        {
            AddObj1 formadd = new AddObj1();
            formadd.ShowDialog ( );
            if ( formadd.CurrentMove != null )
            {
                Movement temp = formadd.CurrentMove;
                _moveList.Add ( temp );
                FillDataGridCards ( _moveList );
            }
        }

        /// <summary>
        /// Кнопка Удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeB_Click ( object sender, EventArgs e )
        {
            if ( _moveList.Count == 0 )
            {
                MessageBox.Show ( @"Список пуст!", @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }
            if ( dataGridView1.SelectedRows.Count == 0 )
            {
                MessageBox.Show ( @"Выберите запись для удаления!", @"Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;
            var result =
                MessageBox.Show(@"Вы уверены, что хотите удалить эту запись?",
                    @"Удаление записи",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
            if ( result == DialogResult.No )
            {
                return;
            }
            _moveList.RemoveAt ( index );
            FillDataGridCards ( _moveList );
        }

        /// <summary>
        /// Метод заполнения таблицы данными из списка карточек
        /// </summary>
        private void FillDataGridCards ( List<Movement> moveList )
        {
            dataGridView1.Rows.Clear ( );
            if ( moveList == null || moveList.Count == 0 ) return;
            foreach ( var item in moveList )
            {
                int i = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[i];
                String type = item.GetType().Name;
                switch ( type )
                {
                    case "UniformMove":
                        row.Cells[0].Value = "Равномерное";
                        break;
                    case "UniformAccek":
                        row.Cells[0].Value = "Равноускоренное";
                        row.Cells[4].Value = ( ( UniformAccek ) item ).Acceleration;
                        break;
                    case "Hesitation":
                        row.Cells[0].Value = "Колебательное";
                        row.Cells[4].Value = ( ( Hesitation ) item ).Amplitude;
                        break;
                }
                row.Cells[1].Value = item.StartPoint;
                row.Cells[2].Value = item.Speed;
                row.Cells[3].Value = item.Period;
                row.Cells[4].Value = item.FinalPoint ( );
            }
            dataGridView1.Update ( );
        }

        private void выходToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            Close ( );
        }

        /// <summary>
        /// Меню Загрузить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void загрузитьToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            if ( _moveList == null ) _moveList = new List<Movement> ( );
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                RestoreDirectory = true,
                DefaultExt = "mv",
                Filter = @"Mov files (*.mv)|*.mv"
            };

            if ( openFileDialog1.ShowDialog ( ) == DialogResult.OK )
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    FileStream readerFileStream =
                        new FileStream(openFileDialog1.FileName, FileMode.Open);
                    if ( _moveList.Count != 0 ) _moveList.Clear ( );
                    _moveList =
                        ( List<Movement> ) formatter.Deserialize ( readerFileStream );
                    readerFileStream.Close ( );
                }
                catch ( Exception )
                {
                    MessageBox.Show ( @"Ошибка чтения файла!" );
                    return;
                }
            }
            else
            {
                return;
            }
            MessageBox.Show ( @"Данные загружены!" );
            FillDataGridCards ( _moveList );
        }

        /// <summary>
        /// Меню Сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            if ( _moveList == null ) _moveList = new List<Movement> ( );
            if ( _moveList.Count < 1 )
            {
                MessageBox.Show ( "Список пуст!" );
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                DefaultExt = "mv",
                Filter = @"Mov files (*.mv)|*.mv"
            };

            if ( saveFileDialog.ShowDialog ( ) == DialogResult.OK )
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    FileStream writerFileStream =
                        new FileStream(saveFileDialog.FileName, FileMode.Create);
                    formatter.Serialize ( writerFileStream, _moveList );
                    writerFileStream.Close ( );
                }
                catch ( Exception )
                {
                    MessageBox.Show ( @"Ошибка записи в файл!" );
                    return;
                }
            }
            else
            {
                return;
            }
            MessageBox.Show ( @"Данные сохранены!" );
        }

        /// <summary>
        /// Нажата клавиша при вводе в текстовое поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFirstPoint_KeyPress ( object sender, KeyPressEventArgs e)
        {
            // Можно вводить цифры, запятую и Backspace

            if ( Char.IsNumber ( e.KeyChar ) |
                 ( e.KeyChar == Convert.ToChar ( "," ) ) |
                 e.KeyChar == '\b' | e.KeyChar == '-' | e.KeyChar == '+') return;
            e.Handled = true;
        }

        /// <summary>
        /// Кнопка Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click ( object sender, EventArgs e )
        {
            if ( _moveList == null || _moveList.Count == 0 )
            {
                MessageBox.Show ( @"Ничего не найдено!" );
                return;
            }

            List<Movement> _searchList = new List<Movement>();
            // Поиск по Типу
            switch ( comboBoxType.SelectedIndex )
            {
                case 0:// Равномерное
                    _searchList = _moveList.FindAll ( r => r.GetType ( ).Name == "UniformMove" );
                    break;
                case 1://Равноускоренное
                    _searchList = _moveList.FindAll ( r => r.GetType ( ).Name == "UniformAccek" );
                    break;
                case 2:// Колебательное
                    _searchList = _moveList.FindAll ( r => r.GetType ( ).Name == "Hesitation" );
                    break;
                case 3:// Любой объект
                    _searchList = new List<Movement> ( _moveList );
                    break;
            }

            try
            {
                // Проверка поля Начальная точка
                if (textBoxFirstPoint.Text != String.Empty)
                    // Поиск по начальной точке
                {
                    _searchList = _searchList != null
                        ? _searchList.FindAll(
                            r => r.StartPoint == Double.Parse(textBoxFirstPoint.Text))
                        : null;
                }

                // Проверка поля Скорость
                if (textBoxSpeed.Text != String.Empty)
                    // Поиск по скорости
                {
                    _searchList = _searchList != null
                        ? _searchList.FindAll(
                            r => r.Speed == Double.Parse(textBoxSpeed.Text))
                        : null;
                }

                // Проверка поля Время
                if (textBoxPeriod.Text != String.Empty)
                    // Поиск по времени движения
                {
                    _searchList = _searchList != null
                        ? _searchList.FindAll(
                            r => r.Period == Double.Parse(textBoxPeriod.Text))
                        : null;
                }
            }
            catch (FormatException e1)
            {
                MessageBox.Show(@"Неверный формат числа!");
            }

            FillDataGridCards ( _searchList );
        }

        private void TextBoxFirstPoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
